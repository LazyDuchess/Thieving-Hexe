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
    public float velocityOffset = 0.5f;
    public float velocityOffsetLerp = 5f;

    private Vector3 currentVelocityOffset = Vector3.zero;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void updateVelocityOffset()
    {
        var targetVelocityOffset = GameController.instance.playerController.GetRigidBody().velocity * velocityOffset;
        currentVelocityOffset = Vector3.Lerp(currentVelocityOffset, targetVelocityOffset, velocityOffsetLerp * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.debugSpawnMonsters)
            ProcessDebug();
        updateVelocityOffset();
        var target = GameController.instance.player;
        ProcessAimInput();
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y + height, target.transform.position.z + distance);
        transform.LookAt(target.transform, Vector3.up);
        transform.position = transform.position - (new Vector3(aimLocation.x, 0f, aimLocation.y) * aimDistance) - currentVelocityOffset;
    }

    void ProcessDebug()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var monster = Instantiate(GameController.instance.monsterPrefab, GameController.instance.playerController.GetAim(), Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            var monster = Instantiate(GameController.instance.monsterPrefab, GameController.instance.playerController.GetAim(), Quaternion.identity);
            monster.GetComponent<HealthController>().team = 0;
        }
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            GameController.instance.aiEnabled = !GameController.instance.aiEnabled;
        }
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
        else
        {
            var floorDistance = ray.origin.y - GameController.instance.player.transform.position.y;
            GameController.instance.playerController.SetAim(ray.origin + (ray.direction * floorDistance));
        }
    }
}