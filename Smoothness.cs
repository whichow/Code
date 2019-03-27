using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 用来平滑各个骨骼点的运动
/// </summary>
public class Smoothness
{
    private Vector3 lastSt1;
    private Vector3 lastSt2;

    // private Vector3 ExponentialSmoothing(float factor, Vector3 realVal, Vector3 lastVal)
    // {
    //     var ret = factor * realVal + (1 - factor) * lastVal;
    //     return ret;
    // }
    /// <summary>
    /// 指数平滑方法
    /// </summary>
    /// <param name="factor"></param>
    /// <param name="realVal"></param>
    /// <returns></returns>
    public Vector3 ExponentialSmoothing(float factor, Vector3 realVal)
    {
        Vector3 at;
        Vector3 bt;

        lastSt1 = St1(factor, realVal, lastSt1);
        lastSt2 = St2(factor, lastSt1, lastSt2);
        at = 2 * lastSt1 - lastSt2;
        bt = factor / (1 - factor) * (lastSt1 - lastSt2);

        var ret = at + bt * 1;
        return ret;
    }

    private Vector3 St1(float factor, Vector3 realVal, Vector3 lastSt1)
    {
        var ret = factor * realVal + (1 - factor) * lastSt1;
        return ret;
    }

    private Vector3 St2(float factor, Vector3 st1, Vector3 lastSt2)
    {
        var ret = factor * st1 + (1 - factor) * lastSt2;
        return ret;
    }
}
