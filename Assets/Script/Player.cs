using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    float fSpeed = 0.03f;

    [SerializeField]
    uint u_playerNum = 0;

    Vector3 velocity;
    Ray ray;

    bool isGetItem = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocity = new Vector3(0, 0, 1);

        isGetItem = false;

        UpdateRay();
    }

    // Update is called once per frame
    void Update()
    {
        //�L�[����
        UpdateInput();

        //���C�X�V
        UpdateRay();
        UpdateBrightPlane();

        //�F�X�V
        UpdateColor();
    }

    private void UpdateInput()
    {
        string strPlayer = (u_playerNum + 1).ToString() + "P_";

        if (Input.GetAxis(strPlayer + "Horizontal") < 0)
        {
            transform.position += new Vector3(-1, 0, 0) * fSpeed;
            velocity += new Vector3(-1, 0, 0);
            velocity.Normalize();
        }
        else if (Input.GetAxis(strPlayer + "Horizontal") > 0)
        {
            transform.position += new Vector3(1, 0, 0) * fSpeed;
            velocity += new Vector3(1, 0, 0);
            velocity.Normalize();
        }
        if (Input.GetAxis(strPlayer + "Vertical") < 0)
        {
            transform.position += new Vector3(0, 0, 1) * fSpeed;
            velocity += new Vector3(0, 0, 1);
            velocity.Normalize();
        }
        else if (Input.GetAxis(strPlayer + "Vertical") > 0)
        {
            transform.position += new Vector3(0, 0, -1) * fSpeed;
            velocity += new Vector3(0, 0, -1);
            velocity.Normalize();
        }
    }

    //���C�̏��X�V
    private void UpdateRay()
    {
        ray.origin = transform.position;
        ray.direction = (velocity + new Vector3(0, -0.5f, 0)).normalized;
    }

    //������1�}�X�O�̏������点��
    private void UpdateBrightPlane()
    {
        //�����̑O�ɂ���I�u�W�F�N�g�擾
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2.0f))
        {
            //��
            if (hit.collider.CompareTag("Plane"))
            {
                hit.collider.gameObject.GetComponent<Plane>().SetBright(true);
            }
            //�A�C�e��
            else if(hit.collider.CompareTag("Item"))
            {
                hit.collider.gameObject.GetComponent<Item>().SetBright(true);

                //�X�y�[�X�o�[������Ă�����A�C�e���Q�b�g
                string strPlayer = (u_playerNum + 1).ToString() + "P_";
                if (Input.GetButtonDown(strPlayer + "A"))
                {
                    isGetItem = true;
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    private void UpdateColor()
    {
        if (isGetItem)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.magenta;
        }
    }
}
