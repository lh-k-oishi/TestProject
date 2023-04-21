using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour, ICellObject
{
    public const int CELL_NUM = 15;  //マス数

    bool bright = false;            //光ってるか

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
    /// 光らせるか否か
    /// </summary>
    /// <param name="enable"></param>
    public void SetBright(bool enable)
    {
        bright = enable;
    }

    /// <summary>
    /// セルの位置からpositionセット
    /// </summary>
    /// <param name="x">横位置</param>
    /// <param name="y">縦位置</param>
    public void SetPosFromCell(uint x, uint y)
    {
        Vector3 scale = new Vector3(1, 1, 1);
        Vector3 pos = new Vector3(-scale.x * CELL_NUM / 2 + scale.z * x, 0, -scale.z * CELL_NUM / 2 + scale.z * y);
        transform.position = pos;
    }
}
