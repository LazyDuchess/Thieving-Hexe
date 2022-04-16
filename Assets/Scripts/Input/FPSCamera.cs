using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    public float rotation = 0f;
    public GameObject head;
    public static FPSCamera instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public Quaternion GetRotation()
    {
        return Quaternion.Euler(new Vector3(0f, rotation, 0f));
    }

    void Update()
    {
        transform.position = head.transform.position;
        var quatRotation = Quaternion.Euler(new Vector3(0f, rotation, 0f));
        transform.rotation = quatRotation;
        var heading = quatRotation * Vector3.forward;
        GameController.instance.playerController.SetAim(GameController.instance.player.transform.position + (heading * 2f));
        rotation += Input.GetAxisRaw("Mouse X") * 6f;
    }
}
