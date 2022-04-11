using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Projectile : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.PostEvent("Play_Firegem_Flight", gameObject);
        var projectileComponent = GetComponent<Projectile>();
        projectileComponent.onHitEvent += StopSound;
    }

    void StopSound()
    {
        AkSoundEngine.PostEvent("Stop_Firegem_Flight", gameObject);
    }

}
