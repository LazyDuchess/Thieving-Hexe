using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBody;
    public GameObject mesh;
    private Vector3 aimLocation = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        DummyAim();
    }
    
    void DummyAim()
    {
        aimLocation = transform.position + transform.forward * 5f;
    }

    public void SetAim(Vector3 location)
    {
        aimLocation = location;
    }
    void Update()
    {
        mesh.transform.LookAt(new Vector3(aimLocation.x, mesh.transform.position.y, aimLocation.z), Vector3.up);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //test input
        transform.position -= Input.GetAxis("Horizontal") * Vector3.right * 5f * Time.deltaTime;
        transform.position -= Input.GetAxis("Vertical") * Vector3.forward * 5f * Time.deltaTime;
    }
}
