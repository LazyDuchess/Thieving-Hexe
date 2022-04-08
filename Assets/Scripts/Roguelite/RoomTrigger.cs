using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoomTrigger : MonoBehaviour
{
    public abstract void OnPlayerEnter();
    public abstract void OnPlayerComeBack();
}
