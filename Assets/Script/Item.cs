using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, ICellObject
{

    bool bright = false;            //�����Ă邩

    // Start is called before the first frame update
    void Start()
    {
        
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
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.cyan;
        }
    }

    /// <summary>
    /// �Z���̈ʒu����position�Z�b�g
    /// </summary>
    /// <param name="x">���ʒu</param>
    /// <param name="y">�c�ʒu</param>
    public void SetPosFromCell(uint x, uint y)
    {
        Vector3 scale = new Vector3(1, 1, 1);
        Vector3 pos = new Vector3(-scale.x * Plane.CELL_NUM / 2 + scale.z * x, 0, -scale.z * Plane.CELL_NUM / 2 + scale.z * y);
        transform.position = pos;
    }

    /// <summary>
    /// ���点�邩�ۂ�
    /// </summary>
    /// <param name="enable"></param>
    public void SetBright(bool enable)
    {
        bright = enable;
    }
}
