using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPowerUp : MonoBehaviour
{
    public GameObject pickupEffect;
    public float multiplyer = 1.4f;

     void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player")) {
            Pickup(other);
        }
    }

    void Pickup(Collider player)
    {
        //spawn cool ffect
        Instantiate(pickupEffect, transform.position, transform.rotation);


        //apply effect

        
        //remove power up object
        Destroy(gameObject);
    }


}
