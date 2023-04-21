using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Timer
{
    public float fTime { get; private set; }        //現在時間
    public float fStartTime { get; private set; }   //開始時間
    public float fEndTime { get; private set; }     //終了時間

    public bool isStart { get; private set; }       //スタートしたか・更新中か
    public bool isLoop { get; private set; }        //タイマーをループさせるか

    public Timer()
    {
        fTime = 0;
        fStartTime = 0;
        fEndTime = 1000;
        isStart = false;
        isLoop = false;
    }

    /// <summary>
    /// タイマーの値をセットする
    /// </summary>
    /// <param name="start">開始値(ms)</param>
    /// <param name="end">終了値(ms)</param>
    /// <param name="isLoop">ループさせるか</param>
    public Timer (float start, float end, bool isLoop = false)
    {
        this.fStartTime = start;
        this.fEndTime = end;
        this.isLoop = isLoop;

        fTime = 0;
        isStart = false;
    }

    /// <summary>
    /// タイマー更新
    /// </summary>
    public void Update()
    {
        //スタートしていないならreturn
        if (isStart == false) { return; }

        //正の方向に増えるとき
        if (fStartTime < fEndTime)
        {
            fTime += Time.deltaTime * 1000;
            if (fTime > fEndTime)
            {
                if (isLoop)
                {
                    fTime -= fEndTime;
                }
                else
                {
                    fTime = fEndTime;
                    isStart = false;
                }
            }
        }
        //負の方向に増えるとき
        else if (fStartTime > fEndTime)
        {
            fTime -= Time.deltaTime * 1000;
            if (fTime < fEndTime)
            {
                if (isLoop)
                {
                    fTime += fEndTime;
                }
                else
                {
                    fTime = fEndTime;
                    isStart = false;
                }
            }
        }
    }

    /// <summary>
    /// タイマーの情報セット
    /// </summary>
    /// <param name="start">開始値(ms)</param>
    /// <param name="end">終了値(ms)</param>
    /// <param name="isLoop">ループさせるか</param>
    public void SetTimer(float start, float end, bool isLoop)
    {
        fStartTime = start;
        fEndTime = end;
        this.isLoop = isLoop;

        isStart = false;
    }

    /// <summary>
    /// タイマーの更新を開始する
    /// </summary>
    public void StartTimer()
    {
        isStart = true;
    }

    /// <summary>
    /// タイマーの更新を止める
    /// </summary>
    public void StopTimer()
    {
        isStart = false;
    }

    /// <summary>
    /// タイマーの値を開始値に戻す
    /// </summary>
    public void ResetTimer()
    {
        fTime = fStartTime;
        isStart = false;
    }

    /// <summary>
    /// タイマーの値を開始値に戻し、スタート
    /// </summary>
    public void ReStartTimer()
    {
        ResetTimer();
        StartTimer();
    }

    /// <summary>
    /// タイマーの値が開始値でないかを返す
    /// </summary>
    /// <returns>タイマーの値が開始値でないか</returns>
    public bool IsStarted()
    {
        return fTime != fStartTime;
    }

    public bool IsEnd()
    {
        return fTime == fEndTime;
    }
}
