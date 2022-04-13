using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Projectile : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        if (GameController.GetAudioHacks())
            AkSoundEngine.PostEvent("Play_Firegem_Flight", GameCamera.instance.gameObject);
        else
            AkSoundEngine.PostEvent("Play_Firegem_Flight", gameObject);
        var projectileComponent = GetComponent<Projectile>();
        projectileComponent.onHitEvent += StopSound;
    }

    void StopSound()
    {
        if (GameController.GetAudioHacks())
            AkSoundEngine.PostEvent("Stop_Firegem_Flight", GameCamera.instance.gameObject);
        else
            AkSoundEngine.PostEvent("Stop_Firegem_Flight", gameObject);
    }

}
