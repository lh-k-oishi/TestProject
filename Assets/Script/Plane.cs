using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour, ICellObject
{
    public const int CELL_NUM = 15;  //�}�X��

    bool bright = false;            //�����Ă邩

    // Start is called before the first frame update
    void Start()
    {
        //bright = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateColor();
    }

    void UpdateColor()
    {
        if (bright)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    /// <summary>
    /// ���点�邩�ۂ�
    /// </summary>
    /// <param name="enable"></param>
    public void SetBright(bool enable)
    {
        bright = enable;
    }

    /// <summary>
    /// �Z���̈ʒu����position�Z�b�g
    /// </summary>
    /// <param name="x">���ʒu</param>
    /// <param name="y">�c�ʒu</param>
    public void SetPosFromCell(uint x, uint y)
    {
        Vector3 scale = new Vector3(1, 1, 1);
        Vector3 pos = new Vector3(-scale.x * CELL_NUM / 2 + scale.z * x, 0, -scale.z * CELL_NUM / 2 + scale.z * y);
        transform.position = pos;
    }
}
