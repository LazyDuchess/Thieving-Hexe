using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    public float lifetime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Delete", lifetime);
    }

    void Delete()
    {
        Destroy(gameObject);
    }
}
