using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class QuarterPlayer : MonoBehaviour
{
    static Vector3[] s_sharedPos = new Vector3[4];          //���L�����4�l�̈ʒu���W
    static bool s_isDestroyed = false;                      //�N��1�l�����̃R�}���h������

    [SerializeField] private float fSpeed = 0.01f;          //�ړ��X�s�[�h
    [SerializeField] private GameObject pfSinglePlayer;     //�P�̃v���C���[�v���n�u

    private int playerNum = -1;                             //�v���C���[�ԍ�
    private Vector3 initMoveStartPos = Vector3.zero;        //�o�����ړ��J�n�ʒu
    private Vector3 initMoveEndPos = Vector3.zero;          //�o�����ړ��I���ʒu

    private Timer tmrAppear = new Timer(0, 500);

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        tmrAppear.Update();

        //���łɒN�������̃R�}���h�������Ă����炻��ɒǏ]����
        if (s_isDestroyed)
        {
            Destroy(this.gameObject);
            return;
        }

        //�o�����ړ�
        if (tmrAppear.isStart)
        {
            Appear();
        }
        //�C���v�b�g�X�V
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

    //�o��
    private void Appear()
    {
        //�������Z�Ƃ߂�
        GetComponent<Rigidbody>().Sleep();

        //�C�[�W���O�ňړ�������
        transform.position = Easing.GetEaseValue(Easing.EasingType.OutCubic, initMoveStartPos, initMoveEndPos, tmrAppear);
    }

    //�ړ�
    private void Move()
    {
        //�e�v���C���[�ԍ����Ƃ̑��슄�蓖��
        string strPlayer = (playerNum + 1).ToString() + "P_";

        //�ړ�
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

    //����
    private void Unite()
    {
        //�e�v���C���[�ԍ����Ƃ̑��슄�蓖��
        string strPlayer = (playerNum + 1).ToString() + "P_";

        //����
        if (Input.GetButtonDown(strPlayer + "A"))
        {
            var gen = Instantiate(pfSinglePlayer);
            gen.transform.position = (s_sharedPos[0] + s_sharedPos[1] + s_sharedPos[2] + s_sharedPos[3]) / 4;
            s_isDestroyed = true;
            Destroy(this.gameObject);
        }
    }
}
