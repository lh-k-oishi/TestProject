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
        //�ǐ���
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

    //�ǐ���
    void GenerateWall()
    {
        //��
        if (roomNorth == null)
        {
            var wallN = Instantiate(pfWall);
            wallN.transform.position = transform.position +
                new Vector3(0, transform.localScale.y / 2 + pfWall.transform.localScale.y / 2, transform.localScale.z / 2) + 
                new Vector3(0, 0, -wallTickness / 2);
            wallN.transform.localScale = new Vector3(transform.localScale.x - wallTickness * 2, wallN.transform.localScale.y, wallTickness);

            wallN.transform.parent = transform;     //�q�I�u�W�F�N�g�Ƃ��Đ���
        }
        else
        {
            //���̕����̎�O�ǂƎ����̉��ǂ̒������ׁA�Z�������ق����h�A��������
            //�����������Ȃ��O�ɂ��镔�����h�A��������
            bool isGenerate = transform.localScale.x <= roomNorth.transform.localScale.x;
            if (isGenerate)
            {
                //�h�A����
                GenerateDoor(
                    transform.position + new Vector3(0, transform.localScale.y / 2 + pfDoor.transform.localScale.y / 2, transform.localScale.z / 2) +
                    new Vector3(0, 0, -wallTickness / 2),
                    0
                    );
            }
        }

        //�E
        if (roomEast == null)
        {
            var wallE = Instantiate(pfWall);
            wallE.transform.position = transform.position +
                new Vector3(transform.localScale.x / 2, transform.localScale.y / 2 + pfWall.transform.localScale.y / 2, 0) +
                new Vector3(-wallTickness / 2, 0, 0);
            wallE.transform.localScale = new Vector3(wallTickness, wallE.transform.localScale.y, transform.localScale.z - wallTickness * 2);

            wallE.transform.parent = transform;     //�q�I�u�W�F�N�g�Ƃ��Đ���
        }
        else
        {
            //�������������V�����̂Œ��ӁI�I
            //�E�̕����̍��ǂƎ����̉E�ǂ̒������ׁA�Z�������ق����h�A��������
            //�����������Ȃ獶�ɂ��镔�����h�A��������
            bool isGenerate = transform.localScale.z <= roomEast.transform.localScale.z;
            if (isGenerate)
            {
                //�ڂ��Ă���ʂ̗��[�̍��W�����߂�
                float x = transform.position.x + transform.localScale.x / 2;
                float y = transform.localScale.y / 2 + pfDoor.transform.localScale.y / 2;
                float z1 = 0, z2 = 0;

                //��[
                if (transform.position.z + transform.localScale.z <= roomEast.transform.position.z + roomEast.transform.localScale.z)
                {
                    z1 = transform.position.z + transform.localScale.z;
                }
                else if (transform.position.z + transform.localScale.z > roomEast.transform.position.z + roomEast.transform.localScale.z)
                {
                    z1 = roomEast.transform.position.z + roomEast.transform.localScale.z;
                }
                //���[
                if (transform.position.z - transform.localScale.z >= roomEast.transform.position.z - roomEast.transform.localScale.z)
                {
                    z2 = transform.position.z - transform.localScale.z;
                }
                else if (transform.position.z + transform.localScale.z < roomEast.transform.position.z + roomEast.transform.localScale.z)
                {
                    z2 = roomEast.transform.position.z - roomEast.transform.localScale.z;
                }

                //�h�A����
                GenerateDoor(
                    new Vector3(x, y, (z1 + z2) / 2) +
                    new Vector3(-wallTickness / 2, 0, 0),
                    90
                    );
            }
        }

        //��O
        if (roomSouth == null)
        {
            var wallS = Instantiate(pfWall);
            wallS.transform.position = transform.position +
                new Vector3(0, transform.localScale.y / 2 + pfWall.transform.localScale.y / 2, -transform.localScale.z / 2) + 
                new Vector3(0, 0, wallTickness / 2);
            wallS.transform.localScale = new Vector3(transform.localScale.x - wallTickness * 2, wallS.transform.localScale.y, wallTickness);

            wallS.transform.parent = transform;     //�q�I�u�W�F�N�g�Ƃ��Đ���
        }
        else
        {
            //��O�̕����̉��ǂƎ����̎�O�ǂ̒������ׁA�Z�������ق����h�A��������
            //�����������Ȃ��O�ɂ��镔�����h�A��������
            bool isGenerate = transform.localScale.x < roomSouth.transform.localScale.x;
            if (isGenerate)
            {
                //�h�A����
                GenerateDoor(
                    transform.position + new Vector3(0, transform.localScale.y / 2 + pfDoor.transform.localScale.y / 2, -transform.localScale.z / 2) + 
                    new Vector3(0, 0, +wallTickness / 2),
                    180
                    );
            }
        }

        //��
        if (roomWest == null)
        {
            var wallW = Instantiate(pfWall);
            wallW.transform.position = transform.position +
                new Vector3(-transform.localScale.x / 2, transform.localScale.y / 2 + pfWall.transform.localScale.y / 2, 0) + 
                new Vector3(wallTickness / 2, 0, 0);
            wallW.transform.localScale = new Vector3(wallTickness, wallW.transform.localScale.y, transform.localScale.z - wallTickness * 2);

            wallW.transform.parent = transform;     //�q�I�u�W�F�N�g�Ƃ��Đ���
        }
        else
        {
            //���̕����̉E�ǂƎ����̍��ǂ̒������ׁA�Z�������ق����h�A��������
            //�����������Ȃ獶�ɂ��镔�����h�A��������
            bool isGenerate = transform.localScale.z < roomWest.transform.localScale.z;
            if (isGenerate)
            {
                //�h�A����
                GenerateDoor(
                    transform.position + new Vector3(-transform.localScale.x / 2, transform.localScale.y / 2 + pfDoor.transform.localScale.y / 2, 0) +
                    new Vector3(wallTickness / 2, 0, 0),
                    270
                    );
            }
        }

        //�l���ɒ��𗧂Ă�
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
    /// �h�A����
    /// </summary>
    /// <param name="pos">�����ʒu</param>
    /// <param name="rot">��](�x)</param>
    void GenerateDoor(Vector3 pos, float rot)
    {
        var v = Instantiate(pfDoor, pos, Quaternion.Euler(0, rot, 0));
    }

    //������
    void GeneratePillar(Vector3 pos)
    {
        var v = Instantiate(pfWall, pos, Quaternion.identity);
    }
}
