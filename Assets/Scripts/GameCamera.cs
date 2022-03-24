using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    private Camera camera;
    public float height = 5f;
    public float distance = 5f;
    public float aimDistance = 10f;
    private Vector3 aimLocation = Vector3.zero;

    void Start()
    {
        camera = GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        var target = GameController.instance.player;
        ProcessAimInput();
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y + height, target.transform.position.z + distance);
        transform.LookAt(target.transform, Vector3.up);
        transform.position = transform.position - (new Vector3(aimLocation.x, 0f, aimLocation.y) * aimDistance);
    }

    //Translate mouse coords to player aim
    void ProcessAimInput()
    {
        //Invalid target - no player controller. todo - lerp back to zero?
        if (!GameController.instance.playerController)
            return;
        aimLocation = InputUtils.GetMouse();
        Ray ray = camera.ScreenPointToRay(InputUtils.GetMouseScreen());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, GameController.GetGroundMask()))
        {
            GameController.instance.playerController.SetAim(hit.point);
        }
    }
}
