using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    /// <summary>
    /// �^�C�}�[�̒l���Z�b�g����
    /// </summary>
    /// <param name="start">�J�n�l(ms)</param>
    /// <param name="end">�I���l(ms)</param>
    /// <param name="isLoop">���[�v�����邩</param>
    public Timer (float start, float end, bool isLoop = false)
    {
        this.fStartTime = start;
        this.fEndTime = end;
        this.isLoop = isLoop;

        fTime = 0;
        isStart = false;
    }

    /// <summary>
    /// �^�C�}�[�X�V
    /// </summary>
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

    /// <summary>
    /// �^�C�}�[�̏��Z�b�g
    /// </summary>
    /// <param name="start">�J�n�l(ms)</param>
    /// <param name="end">�I���l(ms)</param>
    /// <param name="isLoop">���[�v�����邩</param>
    public void SetTimer(float start, float end, bool isLoop)
    {
        fStartTime = start;
        fEndTime = end;
        this.isLoop = isLoop;

        isStart = false;
    }

    /// <summary>
    /// �^�C�}�[�̍X�V���J�n����
    /// </summary>
    public void StartTimer()
    {
        isStart = true;
    }

    /// <summary>
    /// �^�C�}�[�̍X�V���~�߂�
    /// </summary>
    public void StopTimer()
    {
        isStart = false;
    }

    /// <summary>
    /// �^�C�}�[�̒l���J�n�l�ɖ߂�
    /// </summary>
    public void ResetTimer()
    {
        fTime = fStartTime;
        isStart = false;
    }

    /// <summary>
    /// �^�C�}�[�̒l���J�n�l�ɖ߂��A�X�^�[�g
    /// </summary>
    public void ReStartTimer()
    {
        ResetTimer();
        StartTimer();
    }

    /// <summary>
    /// �^�C�}�[�̒l���J�n�l�łȂ�����Ԃ�
    /// </summary>
    /// <returns>�^�C�}�[�̒l���J�n�l�łȂ���</returns>
    public bool IsStarted()
    {
        return fTime != fStartTime;
    }

    public bool IsEnd()
    {
        return fTime == fEndTime;
    }
}
