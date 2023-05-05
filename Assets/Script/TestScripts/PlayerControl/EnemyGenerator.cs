using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField, Tooltip("敵のプレハブ")]
    private GameObject enemyPrefab;
    [Header(" <<<<  敵の生成位置   >>>> ")]
    [SerializeField, Tooltip("X座標：")]
    private float enemyPosX = 0;
    [SerializeField, Tooltip("Y座標：")]
    private float enemyPosY = 0;
    [SerializeField, Tooltip("Z座標：")]
    private float enemyPosZ = 0;
    // 敵生成時間間隔
    private float interval;
    // 経過時間
    private float time = 0f;

    private void Start()
    {
        interval = 5f;
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time > interval)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.position = new Vector3(enemyPosX, enemyPosY, enemyPosZ);
            time = 0f;
            enemy.GetComponent<MoveTo>().FindTarget();
        }
    }
}
