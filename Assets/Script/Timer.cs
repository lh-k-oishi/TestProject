using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public float fTime { get; private set; }        //���ݎ���
    public float fStartTime { get; private set; }   //�J�n����
    public float fEndTime { get; private set; }     //�I������

    public bool isStart { get; private set; }       //�X�^�[�g�������E�X�V����
    public bool isLoop { get; private set; }        //�^�C�}�[�����[�v�����邩

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
        //�X�^�[�g���Ă��Ȃ��Ȃ�return
        if (isStart == false) { return; }

        //���̕����ɑ�����Ƃ�
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
        //���̕����ɑ�����Ƃ�
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
