using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveChooser : MonoBehaviour
{
    public List<GameObject> options;
    // Start is called before the first frame update
    void Start()
    {
        var opt = Random.Range(0, options.Count);
        for(var i=0;i<options.Count;i++)
        {
            if (i != opt)
                Destroy(options[i].gameObject);
        }
        options[opt].SetActive(true);
    }
}
