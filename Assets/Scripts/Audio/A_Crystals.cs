using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Crystals : MonoBehaviour
{
    //use to play sound when item is picked up
    //get collission info perhaps

    [SerializeField] private AK.Wwise.Event crystalPickupSound;
    [SerializeField] private AK.Wwise.Switch crystalPickupState;
    public CrystalPowerUp crystalPower;


    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            //set the current state of the crystal you just collided with and then play the crystal sound that is relevant to the state
            crystalPickupState.SetValue(this.gameObject);
            crystalPickupSound.Post(gameObject);
        }
    }

}
