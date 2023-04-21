using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class SinglePlayer : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float fSpeed = 0.01f;
    [SerializeField] GameObject pfQuarterPlayer;

    private Vector3 initMoveStartPos = Vector3.zero;        //出現時移動開始位置
    private Vector3 initMoveEndPos = Vector3.zero;          //出現時移動終了位置

    private Timer tmrAppear = new Timer(0, 500);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        tmrAppear.Update();

        //出現時移動
        if (tmrAppear.isStart)
        {
            Appear();
        }
        //インプット更新
        else
        {
            Move();
            Quart();
        }
    }

    public void Init(Vector3 initMoveStartPos, Vector3 initMoveEndPos)
    {
        this.initMoveStartPos = initMoveStartPos;
        this.initMoveEndPos = initMoveEndPos;

        tmrAppear.StartTimer();
    }


    //出現
    private void Appear()
    {
        //物理演算とめる
        GetComponent<Rigidbody>().Sleep();

        //イージングで移動させる
        transform.position = Easing.GetEaseValue(Easing.EasingType.InBack, initMoveStartPos, initMoveEndPos, tmrAppear);
    }

    //移動
    private void Move()
    {
        for (int nPlayer = 0; nPlayer < 4; nPlayer++)
        {
            string strPlayer = (nPlayer + 1).ToString() + "P_";

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
    }

    private void Quart()
    {
        for (int nPlayer = 0; nPlayer < 4; nPlayer++)
        {
            string strPlayer = (nPlayer + 1).ToString() + "P_";

            if (Input.GetButtonDown(strPlayer + "A"))
            {
                for (int i = 0; i < 4; i++)
                {
                    var gen = Instantiate(pfQuarterPlayer);

                    Vector3 genPos = transform.position + new Vector3(-gen.transform.localScale.x, 0, gen.transform.localScale.z) +
                        new Vector3(i % 2 * gen.transform.localScale.x * 2, 0, i / 2 * -gen.transform.localScale.z * 2);
                    gen.GetComponent<QuarterPlayer>().Init(i, transform.position, genPos);
                }
                Destroy(this.gameObject);
                break;
            }
        }

    }
}
