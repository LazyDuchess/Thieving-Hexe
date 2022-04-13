using UnityEngine;

public class A_Items : MonoBehaviour
{
    //use to play sound when item is picked up
    //get collission info perhaps

    [SerializeField] private AK.Wwise.Event itemPickup;


    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {

            itemPickup.Post(gameObject);
        }
    }
 
}
