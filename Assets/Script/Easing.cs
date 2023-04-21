using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Easing
{
    public enum EasingType
    {
        Line,
        InSine,
        OutSine,
        InOutSine,
        InQuad,
        OutQuad,
        InOutQuad,
        InCubic,
        OutCubic,
        InOutCubic,
        InQuart,
        OutQuart,
        InOutQuart,
        InQuint,
        OutQuint,
        InOutQuint,
        InExpo,
        OutExpo,
        InOutExpo,
        InCirc,
        OutCirc,
        InOutCirc,
        InBack,
        OutBack,
        InOutBack,
        InElastic,
        OutElastic,
        InOutElastic,
        InBounce,
        OutBounce,
        InOutBounce,
    }

    public static float GetEaseValue(EasingType type, float start, float end, float t)
    {
        float fResult;
        if (t < 0)
        {
            fResult = start;
        }
        else if (t > 1)
        {
            fResult = end;
        }
        else
        {
            float mul = 0;
            if (type == EasingType.Line)
            {
                mul = t;
            }
            else if (type == EasingType.InSine)
            {
                mul = 1.0f - MathF.Cos((t * MathF.PI) / 2);
            }
            else if (type == EasingType.OutSine)
            {
                mul = MathF.Sin((t * MathF.PI) / 2);
            }
            else if (type == EasingType.InOutSine)
            {
                mul = -(MathF.Cos(MathF.PI * t) - 1) / 2;
            }
            else if (type == EasingType.InQuad)
            {
                mul = t * t;
            }
            else if (type == EasingType.OutQuad)
            {
                mul = 1.0f - (1.0f - t) * (1.0f - t);
            }
            else if (type == EasingType.InOutQuad)
            {
                mul = t < 0.5f ? 2.0f * t * t : 1.0f - MathF.Pow(-2 * t + 2, 2) / 2;
            }
            else if (type == EasingType.InCubic)
            {
                mul = t * t * t;
            }
            else if (type == EasingType.OutCubic)
            {
                mul = 1.0f - MathF.Pow(1.0f - t, 3);
            }
            else if (type == EasingType.InOutCubic)
            {
                mul = t < 0.5f ? 4.0f * t * t * t : 1 - MathF.Pow(-2.0f * t + 2, 3) / 2;
            }
            else if (type == EasingType.InQuart)
            {
                mul = t * t * t * t;
            }
            else if (type == EasingType.OutQuart)
            {
                mul = 1.0f - MathF.Pow(1.0f - t, 4);
            }
            else if (type == EasingType.InOutQuart)
            {
                mul = t < 0.5f ? 8.0f * t * t * t * t : 1 - MathF.Pow(-2.0f * t + 2, 4) / 2;
            }
            else if (type == EasingType.InQuint)
            {
                mul = t * t * t * t * t;
            }
            else if (type == EasingType.OutQuint)
            {
                mul = 1.0f - MathF.Pow(1.0f - t, 5);
            }
            else if (type == EasingType.InOutQuint)
            {
                mul = t < 0.5f ? 16.0f * t * t * t * t * t : 1 - MathF.Pow(-2.0f * t + 2, 5) / 2;
            }
            else if (type == EasingType.InExpo)
            {
                mul = t == 0 ? 0 : MathF.Pow(2, 10.0f * t - 10);
            }
            else if (type == EasingType.OutExpo)
            {
                mul = t == 1 ? 1 : 1.0f - MathF.Pow(2, -10.0f * t);
            }
            else if (type == EasingType.InOutExpo)
            {
                mul = t == 0 ? 0 : t == 1 ? 1 : 
                    t < 0.5 ? MathF.Pow(2, 20.0f * t - 10) / 2 : (2.0f - MathF.Pow(2, -20.0f * t + 10)) / 2;
            }
            else if (type == EasingType.InCirc)
            {
                mul = 1.0f - MathF.Sqrt(1.0f - MathF.Pow(t, 2));
            }
            else if (type == EasingType.OutCirc)
            {
                mul = MathF.Sqrt(1.0f - MathF.Pow(t - 1, 2));
            }
            else if (type == EasingType.InOutCirc)
            {
                mul = t < 0.5f ? (1 - MathF.Sqrt(1 - MathF.Pow(2.0f * t, 2))) / 2 : 
                    (MathF.Sqrt(1.0f - MathF.Pow(-2.0f * t + 2, 2)) + 1) / 2;
            }
            else if (type == EasingType.InBack)
            {
                const float c1 = 1.70158f;
                const float c3 = c1 + 1;

                mul = c3 * t * t * t - c1 * t * t;
            }
            else if (type == EasingType.OutBack)
            {
                const float c1 = 1.70158f;
                const float c3 = c1 + 1;

                mul = 1.0f + c3 * MathF.Pow(t - 1, 3) + c1 * MathF.Pow(t - 1, 2);
            }
            else if (type == EasingType.InOutBack)
            {
                const float c1 = 1.70158f;
                const float c2 = c1 * 1.525f;

                mul = t < 0.5f
                  ? (MathF.Pow(2.0f * t, 2) * ((c2 + 1) * 2 * t - c2)) / 2
                  : (MathF.Pow(2.0f * t - 2, 2) * ((c2 + 1) * (t * 2 - 2) + c2) + 2) / 2;
            }
            else if (type == EasingType.InElastic)
            {
                const float c4 = (2.0f * MathF.PI) / 3;

                mul = t == 0 ? 0 : t == 1 ? 1 :
                    -MathF.Pow(2, 10.0f * t - 10) * MathF.Sin((t * 10 - 10.75f) * c4);
            }
            else if (type == EasingType.OutElastic)
            {
                const float c4 = (2.0f * MathF.PI) / 3;

                mul = t == 0 ? 0: t == 1 ? 1 : MathF.Pow(2, -10.0f * t) * MathF.Sin((t * 10 - 0.75f) * c4) + 1;
            }
            else if (type == EasingType.InOutElastic)
            {
                const float c5 = (2.0f * MathF.PI) / 4.5f;

                mul = t == 0 ? 0 : t == 1 ? 1 : t < 0.5f
                    ? -(MathF.Pow(2, 20.0f * t - 10) * MathF.Sin((20.0f * t - 11.125f) * c5)) / 2
                    : (MathF.Pow(2, -20.0f * t + 10) * MathF.Sin((20.0f * t - 11.125f) * c5)) / 2 + 1;
            }
            else if (type == EasingType.InBounce)
            {
                const float n1 = 7.5625f;
                const float d1 = 2.75f;

                t = 1.0f - t;

                if (t < 1.0f / d1)
                {
                    mul = 1.0f - n1 * t * t;
                }
                else if (t < 2.0f / d1)
                {
                    mul = 1.0f - n1 * (t -= 1.5f / d1) * t + 0.75f;
                }
                else if (t < 2.5 / d1)
                {
                    mul = 1.0f - n1 * (t -= 2.25f / d1) * t + 0.9375f;
                }
                else
                {
                    mul = 1.0f - n1 * (t -= 2.625f / d1) * t + 0.984375f;
                }
            }
            else if (type == EasingType.OutBounce)
            {
                const float n1 = 7.5625f;
                const float d1 = 2.75f;

                if (t < 1.0f / d1)
                {
                    mul = n1 * t * t;
                }
                else if (t < 2.0f / d1)
                {
                    mul = n1 * (t -= 1.5f / d1) * t + 0.75f;
                }
                else if (t < 2.5 / d1)
                {
                    mul = n1 * (t -= 2.25f / d1) * t + 0.9375f;
                }
                else
                {
                    mul = n1 * (t -= 2.625f / d1) * t + 0.984375f;
                }
            }
            else if (type == EasingType.InOutBounce)
            {
                const float n1 = 7.5625f;
                const float d1 = 2.75f;


                if (t < 0.5f)
                {
                    t = 1.0f - 2.0f * t;

                    if (t < 1.0f / d1)
                    {
                        mul = (1.0f - (n1 * t * t)) / 2;
                    }
                    else if (t < 2.0f / d1)
                    {
                        mul = (1.0f - (n1 * (t -= 1.5f / d1) * t + 0.75f)) / 2;
                    }
                    else if (t < 2.5 / d1)
                    {
                        mul = (1.0f - (n1 * (t -= 2.25f / d1) * t + 0.9375f)) / 2;
                    }
                    else
                    {
                        mul = (1.0f - (n1 * (t -= 2.625f / d1) * t + 0.984375f)) / 2;
                    }
                }
                else
                {
                    t = 2.0f * t;

                    if (t < 1.0f / d1)
                    {
                        mul = (1.0f + (n1 * t * t)) / 2;
                    }
                    else if (t < 2.0f / d1)
                    {
                        mul = (1.0f + (n1 * (t -= 1.5f / d1) * t + 0.75f)) / 2;
                    }
                    else if (t < 2.5 / d1)
                    {
                        mul = (1.0f + (n1 * (t -= 2.25f / d1) * t + 0.9375f)) / 2;
                    }
                    else
                    {
                        mul = (1.0f + (n1 * (t -= 2.625f / d1) * t + 0.984375f)) / 2;
                    }
                }
            }

            fResult = start + (end - start) * mul;
        }

        return fResult;
    }

    public static Vector3 GetEaseValue(EasingType type, Vector3 start, Vector3 end, float t)
    {
        Vector3 result = new Vector3(
            GetEaseValue(type, start.x, end.x, t),
            GetEaseValue(type, start.y, end.y, t),
            GetEaseValue(type, start.z, end.z, t)
            );

        return result;
    }

    public static float GetEaseValue(EasingType type, float start, float end, Timer timer)
    {
        float n = timer.fTime;
        float s = timer.fStartTime;
        float e = timer.fEndTime;

        float subN_S = n - s;
        float subE_S = e - s;

        //0œŽZ
        if (subE_S == 0) { return 0; }

        float t = subN_S / subE_S;

        return GetEaseValue(type, start, end, t);
    }

    public static float GetEaseValue(EasingType type, float start, float end, Timer timer, float startTime, float endTime)
    {
        float n = timer.fTime;
        float s = startTime;
        float e = endTime;

        float subN_S = n - s;
        float subE_S = e - s;

        //0œŽZ
        if (subE_S == 0) { return 0; }

        float t = subN_S / subE_S;

        return GetEaseValue(type, start, end, t);
    }

    public static Vector3 GetEaseValue(EasingType type, Vector3 start, Vector3 end, Timer timer)
    {
        float n = timer.fTime;
        float s = timer.fStartTime;
        float e = timer.fEndTime;

        float subN_S = n - s;
        float subE_S = e - s;

        //0œŽZ
        if (subE_S == 0) { return Vector3.zero; }

        float t = subN_S / subE_S;

        return GetEaseValue(type, start, end, t);
    }

    public static Vector3 GetEaseValue(EasingType type, Vector3 start, Vector3 end, Timer timer, float startTime, float endTime)
    {
        float n = timer.fTime;
        float s = startTime;
        float e = endTime;

        float subN_S = n - s;
        float subE_S = e - s;

        //0œŽZ
        if (subE_S == 0) { return Vector3.zero; }

        float t = subN_S / subE_S;

        return GetEaseValue(type, start, end, t);
    }
}
