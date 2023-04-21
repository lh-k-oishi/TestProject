using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    [SerializeField]
    GameObject pfPlane;

    List<GameObject> planes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //地形生成
        for (uint i = 0; i < Plane.CELL_NUM; i++)
        {
            for (uint j = 0; j < Plane.CELL_NUM; j++)
            {
                //Plane生成
                var generatedObject = Instantiate(pfPlane);

                //位置セット
                generatedObject.GetComponent<Plane>().SetPosFromCell(j, i);

                planes.Add(generatedObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var v in planes)
        {
            v.GetComponent<Plane>().SetBright(false);
        }
    }
}
