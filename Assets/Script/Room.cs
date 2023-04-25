using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    int roomType = -1;

    public void SetRoomInfo(int roomType, float scaleX, float scaleZ)
    {
        this.roomType = roomType;
        transform.localScale = new Vector3(scaleX, 1, scaleZ);
    }
}
