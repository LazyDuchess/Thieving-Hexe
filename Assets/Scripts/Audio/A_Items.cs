using UnityEngine;

public class A_Items : MonoBehaviour
{
    //use to play sound when item is picked up
    //get collission info perhaps

    [SerializeField] private AK.Wwise.Event itemPickup;
    [SerializeField] private AK.Wwise.Switch itemPickupState;
    public CrystalPowerUp crystalPower;


    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            //set the current state of the crystal you just collided with and then play the crystal sound that is relevant to the state
            itemPickupState.SetValue(this.gameObject);
            itemPickup.Post(gameObject);
        }
    }
 
}
