using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class QuarterPlayer : MonoBehaviour
{
    static Vector3[] s_sharedPos = new Vector3[4];          //共有される4人の位置座標
    static bool s_isDestroyed = false;                      //誰か1人が合体コマンド押した

    [SerializeField] private float fSpeed = 0.01f;          //移動スピード
    [SerializeField] private GameObject pfSinglePlayer;     //単体プレイヤープレハブ

    private int playerNum = -1;                             //プレイヤー番号
    private Vector3 initMoveStartPos = Vector3.zero;        //出現時移動開始位置
    private Vector3 initMoveEndPos = Vector3.zero;          //出現時移動終了位置

    private Timer tmrAppear = new Timer(0, 500);

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        tmrAppear.Update();

        //すでに誰かが合体コマンドを押していたらそれに追従する
        if (s_isDestroyed)
        {
            Destroy(this.gameObject);
            return;
        }

        //出現時移動
        if (tmrAppear.isStart)
        {
            Appear();
        }
        //インプット更新
        else
        {
            Move();
            Unite();
        }

        s_sharedPos[playerNum] = transform.position;
    }

    public void Init(int playerNum, Vector3 initMoveStartPos, Vector3 initMoveEndPos)
    {
        this.playerNum = playerNum;
        this.initMoveStartPos = initMoveStartPos;
        this.initMoveEndPos = initMoveEndPos;
        s_isDestroyed = false;

        tmrAppear.StartTimer();
    }

    //出現
    private void Appear()
    {
        //物理演算とめる
        GetComponent<Rigidbody>().Sleep();

        //イージングで移動させる
        transform.position = Easing.GetEaseValue(Easing.EasingType.OutCubic, initMoveStartPos, initMoveEndPos, tmrAppear);
    }

    //移動
    private void Move()
    {
        //各プレイヤー番号ごとの操作割り当て
        string strPlayer = (playerNum + 1).ToString() + "P_";

        //移動
        if (Input.GetAxis(strPlayer + "Horizontal") < 0)
        {
            transform.position += new Vector3(-1, 0, 0) * fSpeed;
        }
        else if (Input.GetAxis(strPlayer + "Horizontal") > 0)
        {
            transform.position += new Vector3(1, 0, 0) * fSpeed;
        }
        if (Input.GetAxis(strPlayer + "Vertical") < 0)
        {
            transform.position += new Vector3(0, 0, 1) * fSpeed;
        }
        else if (Input.GetAxis(strPlayer + "Vertical") > 0)
        {
            transform.position += new Vector3(0, 0, -1) * fSpeed;
        }
    }

    //合体
    private void Unite()
    {
        //各プレイヤー番号ごとの操作割り当て
        string strPlayer = (playerNum + 1).ToString() + "P_";

        //合体
        if (Input.GetButtonDown(strPlayer + "A"))
        {
            var gen = Instantiate(pfSinglePlayer);
            gen.transform.position = (s_sharedPos[0] + s_sharedPos[1] + s_sharedPos[2] + s_sharedPos[3]) / 4;
            s_isDestroyed = true;
            Destroy(this.gameObject);
        }
    }
}
