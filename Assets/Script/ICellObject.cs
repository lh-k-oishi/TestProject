using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICellObject
{
    /// <summary>
    /// �Z���̈ʒu����position�Z�b�g
    /// </summary>
    /// <param name="x">���ʒu</param>
    /// <param name="y">�c�ʒu</param>
    public void SetPosFromCell(uint x, uint y);
}
