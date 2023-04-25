using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject pfRoom;

    [SerializeField]
    int minScale = 15;

    [SerializeField]
    int maxScale = 40;

    [SerializeField]
    int minRoomCount = 10;

    [SerializeField]
    int maxRoomCount = 15;

    List<GameObject> rooms = new List<GameObject>();

    System.Random random;

    int seed = 0;
    int roomCount = 0;

    // Update is called once per frame
    void Update()
    {

    }

    // 押された瞬間のコールバック
    public void OnPress(InputAction.CallbackContext context)
    {
        // 押された瞬間でPerformedとなる
        if (!context.performed) return;

        Generate(true);
    }

    void Generate(bool isRandom)
    {
        //クリア
        ResetRooms();

        //ランダム生成ならシード値更新
        if (isRandom)
        {
            System.Random rSeed = new System.Random();
            seed = rSeed.Next();
        }

        random = new System.Random(seed);

        //ステージ情報を決定
        roomCount = random.Next(minRoomCount, maxRoomCount);

        //まず入口生成
        var entrance = GeneratePlane(0, (int)(maxScale * 0.75), (int)(maxScale * 0.75));

        //部屋数が上限に達するまで生成
        int rowCount = 1;
        List<int> generatedRoomCount = new List<int>
        {
            1      //入口
        };

        //X座標に加算する値
        float addMoveX = entrance.transform.position.x + entrance.transform.localScale.x;
        while (rooms.Count < roomCount)
        {
            //1列2個部屋か3個部屋か
            int genCount = random.Next(2, 4);

            //最大部屋数を超えないかチェック
            if (genCount + rooms.Count > roomCount) { genCount = roomCount - rooms.Count; }
            generatedRoomCount.Add(genCount);

            for (int i = 0; i < genCount; i++)
            {
                var v = GeneratePlane();
                //部屋の最大サイズ x 列カウント分座標移動
                int rn = random.Next(0, 5);
                float moveX = addMoveX + rn;
                rn = random.Next((int)(maxScale * 0.75), (int)(maxScale * 0.9));
                float moveZ = i * rn - (genCount - 1) * rn / 2;

                v.gameObject.transform.position += new Vector3(moveX, 0, moveZ);
            }

            //2ずつ拡大していく
            //Z軸方向に拡大
            for (int i = 1; i < rooms.Count; i++)
            {
                while (rooms[i].transform.localScale.z < maxScale)
                {
                    //拡大後のスケールで試算
                    Vector3 afterExtendScale = rooms[i].transform.localScale + new Vector3(0, 0, 2);
                    bool isCollision = false;

                    for (int j = 0; j < rooms.Count; j++)
                    {
                        if (i == j) { continue; }

                        if (IsCollision(rooms[i].transform.position, rooms[j].transform.position,
                            afterExtendScale, rooms[j].transform.localScale))
                        {
                            isCollision = true;
                            break;
                        }
                    }

                    //衝突しなかったら拡大
                    if (isCollision == false)
                    {
                        rooms[i].transform.localScale = afterExtendScale;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            //X軸方向に拡大 (後ろ方向)
            for (int i = 1; i < rooms.Count; i++)
            {
                while (rooms[i].transform.localScale.x < maxScale)
                {
                    //拡大後のスケールで試算
                    Vector3 afterExtendScale = rooms[i].transform.localScale + new Vector3(2, 0, 0);
                    Vector3 afterExtendPos = rooms[i].transform.position - new Vector3(1, 0, 0);
                    bool isCollision = false;

                    for (int j = 0; j < rooms.Count; j++)
                    {
                        if (i == j) { continue; }

                        if (IsCollision(afterExtendPos, rooms[j].transform.position,
                            afterExtendScale, rooms[j].transform.localScale))
                        {
                            isCollision = true;
                            break;
                        }
                    }

                    //衝突しなかったら拡大
                    if (isCollision == false)
                    {
                        rooms[i].transform.localScale = afterExtendScale;
                        rooms[i].transform.position = afterExtendPos;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            //いま追加した中で一番奥にあるものの値を加算
            float maxPosX = 0;
            for (int i = 0; i < rooms.Count; i++)
            {
                if (maxPosX < rooms[i].transform.position.x + rooms[i].transform.localScale.x)
                {
                    maxPosX = rooms[i].transform.position.x + rooms[i].transform.localScale.x;
                }
            }
            addMoveX = maxPosX;
            rowCount++;
        }

        //縦に隙間がある場合があるので矯正
        for (int i = 1; i < rooms.Count; i++)
        {
            //自分の後ろにオブジェクトなければスルー
            bool isExistBack = false;
            for (int j = 0; j < rooms.Count; j++)
            {
                if (i == j) { continue; }
                if (rooms[j].transform.position.x < rooms[i].transform.position.x &&
                    rooms[j].transform.position.z + rooms[j].transform.localScale.z / 2 > rooms[i].transform.position.z - rooms[i].transform.localScale.z / 2 &&
                    rooms[j].transform.position.z - rooms[j].transform.localScale.z / 2 < rooms[i].transform.position.z + rooms[i].transform.localScale.z / 2)
                {
                    isExistBack = true;
                    break;
                }
            }
            if (isExistBack == false) { continue; }

            while (true)
            {
                //拡大後のスケールで試算
                Vector3 afterExtendPos = rooms[i].transform.position - new Vector3(1, 0, 0);
                bool isCollision = false;

                for (int j = 0; j < rooms.Count; j++)
                {
                    if (i == j) { continue; }

                    if (IsCollision(afterExtendPos, rooms[j].transform.position,
                        rooms[i].transform.localScale, rooms[j].transform.localScale))
                    {
                        isCollision = true;
                        break;
                    }
                }

                //衝突しなかったら移動
                if (isCollision == false)
                {
                    rooms[i].transform.position = afterExtendPos;
                }
                else
                {
                    break;
                }
            }
        }
    }

    void ResetRooms()
    {
        foreach(var v in rooms)
        {
            Destroy(v.gameObject);
        }
        rooms.Clear();
    }

    GameObject GeneratePlane(int roomType = -1, float scaleX = -1, float scaleZ = -1)
    {
        //部屋生成
        var v = Instantiate(pfRoom);

        //大きさランダムで決定
　        if (scaleX == -1)
        {
            scaleX = random.Next(minScale / 2, (int)(maxScale * 0.75) / 2) * 2;
        }
        if (scaleZ == -1)
        {
            scaleZ = random.Next(minScale / 2, (int)(maxScale * 0.75) / 2) * 2;
        }

        //部屋の情報セット
        v.GetComponent<Room>().SetRoomInfo(roomType == -1 ? random.Next(0, 10) : roomType, scaleX, scaleZ);

        //部屋管理コンテナに追加
        rooms.Add(v);

        return v;
    }

    bool IsCollision(Vector3 center1, Vector3 center2, Vector3 scale1, Vector3 scale2)
    {
        float w1 = scale1.x;
        float w2 = scale2.x;
        float h1 = scale1.z;
        float h2 = scale2.z;
        Vector2 c1 = new Vector2(center1.x, center1.z);
        Vector2 c2 = new Vector2(center2.x, center2.z);

        float wdiv2 = (w1 + w2) / 2;
        float hdiv2 = (h1 + h2) / 2;

        Vector2 distance = c1 - c2;
        distance = new Vector2(Mathf.Abs(distance.x), Mathf.Abs(distance.y));

        return distance.x < wdiv2 && distance.y < hdiv2;
    }
}
