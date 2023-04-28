using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class NewRoom : MonoBehaviour
{
    [SerializeField]
    GameObject pfWall;

    [SerializeField]
    GameObject pfDoor;

    [SerializeField]
    GameObject roomNorth;

    [SerializeField]
    GameObject roomEast;

    [SerializeField]
    GameObject roomSouth;

    [SerializeField]
    GameObject roomWest;


    [SerializeField]
    float wallTickness = 1;

    private void Awake()
    {
        //壁生成
        GenerateWall();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //壁生成
    void GenerateWall()
    {
        //奥
        if (roomNorth == null)
        {
            var wallN = Instantiate(pfWall);
            wallN.transform.position = transform.position +
                new Vector3(0, transform.localScale.y / 2 + pfWall.transform.localScale.y / 2, transform.localScale.z / 2) + 
                new Vector3(0, 0, -wallTickness / 2);
            wallN.transform.localScale = new Vector3(transform.localScale.x - wallTickness * 2, wallN.transform.localScale.y, wallTickness);

            wallN.transform.parent = transform;     //子オブジェクトとして生成
        }
        else
        {
            //奥の部屋の手前壁と自分の奥壁の長さを比べ、短かったほうがドア生成する
            //長さが同じなら手前にある部屋がドア生成する
            bool isGenerate = transform.localScale.x <= roomNorth.transform.localScale.x;
            if (isGenerate)
            {
                //ドア生成
                GenerateDoor(
                    transform.position + new Vector3(0, transform.localScale.y / 2 + pfDoor.transform.localScale.y / 2, transform.localScale.z / 2) +
                    new Vector3(0, 0, -wallTickness / 2),
                    0
                    );
            }
        }

        //右
        if (roomEast == null)
        {
            var wallE = Instantiate(pfWall);
            wallE.transform.position = transform.position +
                new Vector3(transform.localScale.x / 2, transform.localScale.y / 2 + pfWall.transform.localScale.y / 2, 0) +
                new Vector3(-wallTickness / 2, 0, 0);
            wallE.transform.localScale = new Vector3(wallTickness, wallE.transform.localScale.y, transform.localScale.z - wallTickness * 2);

            wallE.transform.parent = transform;     //子オブジェクトとして生成
        }
        else
        {
            //ここだけ処理新しいので注意！！
            //右の部屋の左壁と自分の右壁の長さを比べ、短かったほうがドア生成する
            //長さが同じなら左にある部屋がドア生成する
            bool isGenerate = transform.localScale.z <= roomEast.transform.localScale.z;
            if (isGenerate)
            {
                //接している面の両端の座標を求める
                float x = transform.position.x + transform.localScale.x / 2;
                float y = transform.localScale.y / 2 + pfDoor.transform.localScale.y / 2;
                float z1 = 0, z2 = 0;

                //上端
                if (transform.position.z + transform.localScale.z <= roomEast.transform.position.z + roomEast.transform.localScale.z)
                {
                    z1 = transform.position.z + transform.localScale.z;
                }
                else if (transform.position.z + transform.localScale.z > roomEast.transform.position.z + roomEast.transform.localScale.z)
                {
                    z1 = roomEast.transform.position.z + roomEast.transform.localScale.z;
                }
                //下端
                if (transform.position.z - transform.localScale.z >= roomEast.transform.position.z - roomEast.transform.localScale.z)
                {
                    z2 = transform.position.z - transform.localScale.z;
                }
                else if (transform.position.z + transform.localScale.z < roomEast.transform.position.z + roomEast.transform.localScale.z)
                {
                    z2 = roomEast.transform.position.z - roomEast.transform.localScale.z;
                }

                //ドア生成
                GenerateDoor(
                    new Vector3(x, y, (z1 + z2) / 2) +
                    new Vector3(-wallTickness / 2, 0, 0),
                    90
                    );
            }
        }

        //手前
        if (roomSouth == null)
        {
            var wallS = Instantiate(pfWall);
            wallS.transform.position = transform.position +
                new Vector3(0, transform.localScale.y / 2 + pfWall.transform.localScale.y / 2, -transform.localScale.z / 2) + 
                new Vector3(0, 0, wallTickness / 2);
            wallS.transform.localScale = new Vector3(transform.localScale.x - wallTickness * 2, wallS.transform.localScale.y, wallTickness);

            wallS.transform.parent = transform;     //子オブジェクトとして生成
        }
        else
        {
            //手前の部屋の奥壁と自分の手前壁の長さを比べ、短かったほうがドア生成する
            //長さが同じなら手前にある部屋がドア生成する
            bool isGenerate = transform.localScale.x < roomSouth.transform.localScale.x;
            if (isGenerate)
            {
                //ドア生成
                GenerateDoor(
                    transform.position + new Vector3(0, transform.localScale.y / 2 + pfDoor.transform.localScale.y / 2, -transform.localScale.z / 2) + 
                    new Vector3(0, 0, +wallTickness / 2),
                    180
                    );
            }
        }

        //左
        if (roomWest == null)
        {
            var wallW = Instantiate(pfWall);
            wallW.transform.position = transform.position +
                new Vector3(-transform.localScale.x / 2, transform.localScale.y / 2 + pfWall.transform.localScale.y / 2, 0) + 
                new Vector3(wallTickness / 2, 0, 0);
            wallW.transform.localScale = new Vector3(wallTickness, wallW.transform.localScale.y, transform.localScale.z - wallTickness * 2);

            wallW.transform.parent = transform;     //子オブジェクトとして生成
        }
        else
        {
            //左の部屋の右壁と自分の左壁の長さを比べ、短かったほうがドア生成する
            //長さが同じなら左にある部屋がドア生成する
            bool isGenerate = transform.localScale.z < roomWest.transform.localScale.z;
            if (isGenerate)
            {
                //ドア生成
                GenerateDoor(
                    transform.position + new Vector3(-transform.localScale.x / 2, transform.localScale.y / 2 + pfDoor.transform.localScale.y / 2, 0) +
                    new Vector3(wallTickness / 2, 0, 0),
                    270
                    );
            }
        }

        //四隅に柱を立てる
        GeneratePillar(transform.position + 
            new Vector3(-transform.localScale.x / 2, transform.localScale.y / 2 + pfWall.transform.localScale.y / 2, transform.localScale.z / 2) +
            new Vector3(wallTickness / 2, 0, -wallTickness / 2));
        GeneratePillar(transform.position + 
            new Vector3(transform.localScale.x / 2, transform.localScale.y / 2 + pfWall.transform.localScale.y / 2, transform.localScale.z / 2) +
            new Vector3(-wallTickness / 2, 0, -wallTickness / 2));
        GeneratePillar(transform.position + 
            new Vector3(transform.localScale.x / 2, transform.localScale.y / 2 + pfWall.transform.localScale.y / 2, -transform.localScale.z / 2) +
            new Vector3(-wallTickness / 2, 0, wallTickness / 2));
        GeneratePillar(transform.position + 
            new Vector3(-transform.localScale.x / 2, transform.localScale.y / 2 + pfWall.transform.localScale.y / 2, -transform.localScale.z / 2) +
            new Vector3(wallTickness / 2, 0, wallTickness / 2));
    }

    /// <summary>
    /// ドア生成
    /// </summary>
    /// <param name="pos">生成位置</param>
    /// <param name="rot">回転(度)</param>
    void GenerateDoor(Vector3 pos, float rot)
    {
        var v = Instantiate(pfDoor, pos, Quaternion.Euler(0, rot, 0));
    }

    //柱生成
    void GeneratePillar(Vector3 pos)
    {
        var v = Instantiate(pfWall, pos, Quaternion.identity);
    }
}
