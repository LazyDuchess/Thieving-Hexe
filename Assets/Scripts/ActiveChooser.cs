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
        foreach(var element in options)
        {
            element.SetActive(false);
        }
        options[opt].SetActive(true);
    }
}
