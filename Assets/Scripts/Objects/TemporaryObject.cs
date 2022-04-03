using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryObject : MonoBehaviour
{
    public float duration = 1f;

    private void Start()
    {
        Invoke("Delete", duration);
    }

    public void Delete()
    {
        Destroy(this.gameObject);
    }
}
