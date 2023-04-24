using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;

public class SinglePlayer : MonoBehaviour
{
    [SerializeField] private float fSpeed = 0.01f;

    public static Vector3 s_sharedPos { get; private set; } = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //インプット更新
        Move();
    }

    //移動
    private void Move()
    {
        for (int i = 0; i < 4; i++)
        {
            if (QuarterPlayer.s_isUnite[i])
            {
                transform.position += QuarterPlayer.s_sharedVel[i] * fSpeed;
            }
        }
        s_sharedPos = transform.position;
    }
}
