using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Objects : MonoBehaviour
{

    [SerializeField] private AK.Wwise.Event playSoundOne;
    [SerializeField] private AK.Wwise.Event playSoundTwo;
    [SerializeField] private AK.Wwise.Event playSoundThree;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void PlaySoundOne()
    {
        playSoundOne.Post(gameObject);
    }

    public void PlaySoundTwo()
    {
        playSoundOne.Post(gameObject);
    }

    public void PlaySoundThree()
    {
        playSoundOne.Post(gameObject);
    }
}
