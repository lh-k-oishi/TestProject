using System.Collections;
using System.Collections.Generic;
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

    public Timer (float fStartTime, float fEndTime, bool isLoop = false)
    {
        this.fStartTime = fStartTime;
        this.fEndTime = fEndTime;
        this.isLoop = isLoop;

        fTime = 0;
        isStart = false;
    }

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

    public void SetTimer(float start, float end, bool isLoop)
    {
        fStartTime = start;
        fEndTime = end;
        this.isLoop = isLoop;

        isStart = false;
    }

    public void StartTimer()
    {
        isStart = true;
    }

    public void StopTimer()
    {
        isStart = false;
    }

    public void ResetTimer()
    {
        fTime = fStartTime;
        isStart = false;
    }

    public void ReStartTimer()
    {
        ResetTimer();
        StartTimer();
    }
}
