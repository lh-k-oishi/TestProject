using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    GameObject pfItem;

    GameObject item;

    // Start is called before the first frame update
    void Start()
    {
        uint posX = (uint)Random.Range(0, Plane.CELL_NUM);
        uint posY = (uint)Random.Range(0, Plane.CELL_NUM);

        item = Instantiate(pfItem);

        //位置セット
        item.GetComponent<Item>().SetPosFromCell(posX, posY);
    }

    // Update is called once per frame
    void Update()
    {
        if (item != null)
        {
            item.GetComponent<Item>().SetBright(false);
        }
    }
}
