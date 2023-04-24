using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.InputSystem;

public class QuarterPlayer : MonoBehaviour
{
    [SerializeField] private float fSpeed = 0.01f;                  //移動スピード

    public static int s_generatedCount { get; private set; } = 0;   //生成されたプレイヤーの数
    public static Vector3[] s_sharedVel { get; private set; } = new Vector3[4];   //共有する移動方向
    public static bool[] s_isUnite { get; private set; } = new bool[4];   //共有する合体状況

    private int playerNum = -1;                                     //プレイヤー番号
    private Vector3 moveStartPos = Vector3.zero;                //移動開始位置
    private Vector3 moveEndPos = Vector3.zero;                  //移動終了位置

    private Timer tmrAppear = new Timer(0, 500);                    //出現時タイマー
    private Timer tmrUnite = new Timer(0, 500);                     //合体時タイマー

    private InputAction move;                                       //移動インプット
    private InputAction uniteAppear;                                      //合体インプット

    void Awake()
    {
        playerNum = s_generatedCount;
        s_generatedCount++;

        var playerInput = GetComponent<PlayerInput>();
        move = playerInput.actions["Move"];
        uniteAppear = playerInput.actions["Unite"];

        s_isUnite[playerNum] = true;
        gameObject.GetComponent<Renderer>().enabled = false;
    }

    void Update()
    {
        tmrAppear.Update();
        tmrUnite.Update();

        if (s_isUnite[playerNum] == false)
        {
            //出現時更新
            if (tmrAppear.isStart)
            {
                Appearing();
            }
            //合体時更新
            else if (tmrUnite.IsStarted())
            {
                Uniting();
            }
            //インプット更新
            else
            {
                Move();
                Unite();
            }
        }
        else
        {
            Move();
            Appear();
        }
    }

    //分身しているプレイヤーが3人以下であるか
    static bool EnableAppear()
    {
        int count = 0;
        for (int i = 0; i < 4; i++)
        {
            count += s_isUnite[i] == false ? 1 : 0;
        }

        return count < 3;
    }

    //出現
    void Appear()
    {
        if (uniteAppear.ReadValue<float>() > 0f && EnableAppear())
        {
            moveStartPos = SinglePlayer.s_sharedPos;
            if (playerNum == 0)
            {
                moveEndPos = moveStartPos + new Vector3(-1.5f, 0, 1.5f);
            }
            else if (playerNum == 1)
            {
                moveEndPos = moveStartPos + new Vector3(1.5f, 0, 1.5f);
            }
            else if (playerNum == 2)
            {
                moveEndPos = moveStartPos + new Vector3(-1.5f, 0, -1.5f);
            }
            else
            {
                moveEndPos = moveStartPos + new Vector3(1.5f, 0, -1.5f);
            }

            tmrAppear.ReStartTimer();
            s_isUnite[playerNum] = false;
            gameObject.GetComponent<Renderer>().enabled = true;
            //物理演算とめる
            GetComponent<Rigidbody>().Sleep();
        }
    }

    //出現更新処理
    private void Appearing()
    {
        //物理演算とめる
        GetComponent<Rigidbody>().Sleep();

        //イージングで移動させる
        transform.position = Easing.GetEaseValue(Easing.EasingType.OutCubic, moveStartPos, moveEndPos, tmrAppear);
    }

    //移動
    private void Move()
    {
        Vector2 dir = move.ReadValue<Vector2>();
        s_sharedVel[playerNum] = new Vector3(dir.x, 0, dir.y);

        if (s_isUnite[playerNum])
        {
            transform.position = SinglePlayer.s_sharedPos;
        }
        else
        {
            transform.position += s_sharedVel[playerNum] * fSpeed;
        }
    }

    //合体
    private void Unite()
    {
        if (uniteAppear.ReadValue<float>() > 0f)
        {
            moveStartPos = transform.position;
            moveEndPos = SinglePlayer.s_sharedPos;
            tmrUnite.StartTimer();
        }
    }

    //合体更新処理
    private void Uniting()
    {
        transform.position = Easing.GetEaseValue(Easing.EasingType.InBack, moveStartPos, moveEndPos, tmrUnite);

        if (tmrUnite.IsEnd())
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            s_isUnite[playerNum] = true;
            tmrUnite.ResetTimer();
        }
    }
}
