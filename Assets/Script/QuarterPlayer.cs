using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.InputSystem;

public class QuarterPlayer : MonoBehaviour
{
    [SerializeField] private float fSpeed = 0.01f;                                  //�ړ��X�s�[�h

    public static int s_generatedCount { get; private set; } = 0;                   //�������ꂽ�v���C���[�̐�
    public static Vector3[] s_sharedVel { get; private set; } = new Vector3[4];     //���L����ړ�����
    public static bool[] s_isUnite { get; private set; } = new bool[4];             //���L���鍇�̏�

    private int playerNum = -1;                     //�v���C���[�ԍ�
    private Vector3 moveStartPos = Vector3.zero;    //�ړ��J�n�ʒu
    private Vector3 moveEndPos = Vector3.zero;      //�ړ��I���ʒu

    private Timer tmrAppear = new Timer(0, 500);    //�o�����^�C�}�[
    private Timer tmrUnite = new Timer(0, 500);     //���̎��^�C�}�[

    private InputAction move;                       //�ړ��C���v�b�g
    private InputAction uniteAppear;                //���̃C���v�b�g

    void Awake()
    {
        //�v���C���[�ԍ������蓖��
        playerNum = s_generatedCount;
        s_generatedCount++;

        //�C���v�b�g�A�N�V�����擾
        var playerInput = GetComponent<PlayerInput>();
        move = playerInput.actions["Move"];
        uniteAppear = playerInput.actions["Unite"];

        //������Ԃ͍���
        s_isUnite[playerNum] = true;
        //���f����\��
        gameObject.GetComponent<Renderer>().enabled = false;
    }

    void Update()
    {
        //�^�C�}�[�X�V
        tmrAppear.Update();
        tmrUnite.Update();

        //���g������
        if (s_isUnite[playerNum] == false)
        {
            //�o�����X�V
            if (tmrAppear.isStart)
            {
                Appearing();
            }
            //���̎��X�V
            else if (tmrUnite.IsStarted())
            {
                Uniting();
            }
            //�C���v�b�g�X�V
            else
            {
                Move();
                Unite();
            }
        }
        //���̎�����
        else
        {
            //�C���v�b�g�X�V
            Move();
            Appear();
        }
    }

    //���g���Ă���v���C���[��generatedCount - 1�l�ȉ��ł��邩
    static bool EnableAppear()
    {
        int count = 0;
        for (int i = 0; i < s_generatedCount; i++)
        {
            count += s_isUnite[i] == false ? 1 : 0;
        }

        return count < s_generatedCount - 1;
    }

    //���g
    void Appear()
    {
        //�o���{�^���������l�������������Ă����番�g������
        if (uniteAppear.ReadValue<float>() > 0f && EnableAppear())
        {
            //�{�̈ʒu����΂߈ʒu��
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
            //���̉���
            s_isUnite[playerNum] = false;
            //���f���\��
            gameObject.GetComponent<Renderer>().enabled = true;
            //�������Z�Ƃ߂�
            GetComponent<Rigidbody>().Sleep();
        }
    }

    //�o���X�V����
    private void Appearing()
    {
        //�������Z�Ƃ߂�
        GetComponent<Rigidbody>().Sleep();

        //�C�[�W���O�ňړ�������
        transform.position = Easing.GetEaseValue(Easing.EasingType.OutCubic, moveStartPos, moveEndPos, tmrAppear);
    }

    //�ړ�
    private void Move()
    {
        //�W���C�X�e�B�b�N�̕����擾
        Vector2 dir = move.ReadValue<Vector2>();
        s_sharedVel[playerNum] = new Vector3(dir.x, 0, dir.y);

        //���̎��͖{�̂̈ʒu�Ɠ���
        if (s_isUnite[playerNum])
        {
            transform.position = SinglePlayer.s_sharedPos;
        }
        else
        {
            transform.position += s_sharedVel[playerNum] * fSpeed;
        }
    }

    //����
    private void Unite()
    {
        //���̃{�^������
        if (uniteAppear.ReadValue<float>() > 0f)
        {
            //���Ȉʒu���{�̈ʒu
            moveStartPos = transform.position;
            moveEndPos = SinglePlayer.s_sharedPos;
            tmrUnite.StartTimer();
        }
    }

    //���̍X�V����
    private void Uniting()
    {
        //�ʒu�̓C�[�W���O�ŕ⊮
        transform.position = Easing.GetEaseValue(Easing.EasingType.InBack, moveStartPos, moveEndPos, tmrUnite);

        //�ړ��I����
        if (tmrUnite.IsEnd())
        {
            //���f��
            gameObject.GetComponent<Renderer>().enabled = false;

            //����
            s_isUnite[playerNum] = true;

            //�^�C�}�[���Z�b�g
            tmrUnite.ResetTimer();
        }
    }
}
