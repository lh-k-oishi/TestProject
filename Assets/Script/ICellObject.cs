using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICellObject
{
    /// <summary>
    /// セルの位置からpositionセット
    /// </summary>
    /// <param name="x">横位置</param>
    /// <param name="y">縦位置</param>
    public void SetPosFromCell(uint x, uint y);
}
