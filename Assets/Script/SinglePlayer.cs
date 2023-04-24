using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;

public class SinglePlayer : MonoBehaviour
{
    [SerializeField] private float fSpeed = 0.01f;       //�ړ��X�s�[�h

    public static Vector3 s_sharedPos { get; private set; } = Vector3.zero;     //���L����鎩�Ȉʒu

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //�C���v�b�g�X�V
        Move();
    }

    //�ړ�
    private void Move()
    {
        //4�l�̈ړ����������Z
        for (int i = 0; i < 4; i++)
        {
            //���̂��Ă�����̂̂݉��Z
            if (QuarterPlayer.s_isUnite[i])
            {
                transform.position += QuarterPlayer.s_sharedVel[i] * fSpeed;
            }
        }
        s_sharedPos = transform.position;
    }
}
