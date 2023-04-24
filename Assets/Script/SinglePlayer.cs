using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;

public class SinglePlayer : MonoBehaviour
{
    [SerializeField] private float fSpeed = 0.01f;
    [SerializeField] private GameObject pfQuarterPlayer;

    private Rigidbody rb;
    private Vector3 initMoveStartPos = Vector3.zero;        //出現時移動開始位置
    private Vector3 initMoveEndPos = Vector3.zero;          //出現時移動終了位置

    private InputAction move;
    private InputAction quart;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        var playerInput = GetComponent<PlayerInput>();
        move = playerInput.actions["Move"];
        quart = playerInput.actions["Quart"];
    }

    // Update is called once per frame
    void Update()
    {
        //インプット更新
        Move();
        Quart();
    }

    //初期化
    public void Init(Vector3 initMoveStartPos, Vector3 initMoveEndPos)
    {
        this.initMoveStartPos = initMoveStartPos;
        this.initMoveEndPos = initMoveEndPos;
    }

    //移動
    private void Move()
    {
        //for (int nPlayer = 0; nPlayer < 4; nPlayer++)
        //{
        //    string strPlayer = (nPlayer + 1).ToString() + "P_";

        //    if (move.ReadValue<Vector2>())
        //    {
        //        transform.position += new Vector3(-1, 0, 0) * fSpeed;
        //    }
        //    else if (Input.GetAxis(strPlayer + "Horizontal") > 0)
        //    {
        //        transform.position += new Vector3(1, 0, 0) * fSpeed;
        //    }
        //    if (Input.GetAxis(strPlayer + "Vertical") < 0)
        //    {
        //        transform.position += new Vector3(0, 0, 1) * fSpeed;
        //    }
        //    else if (Input.GetAxis(strPlayer + "Vertical") > 0)
        //    {
        //        transform.position += new Vector3(0, 0, -1) * fSpeed;
        //    }
        //}

        Vector2 moveDir = move.ReadValue<Vector2>();
        transform.position += new Vector3(moveDir.x, 0, moveDir.y) * fSpeed;

    }

    private void Quart()
    {
        for (int nPlayer = 0; nPlayer < 4; nPlayer++)
        {
            string strPlayer = (nPlayer + 1).ToString() + "P_";

            if (quart.ReadValue<float>() > 0f)
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
