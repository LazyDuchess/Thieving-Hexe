using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public GameObject crosshair;
    private Camera camera;
    public float height = 5f;
    public float distance = 5f;
    public float aimDistance = 10f;
    private Vector3 aimLocation = Vector3.zero;
    public float velocityOffset = 0.5f;
    public float velocityOffsetLerp = 5f;
    public GameObject xRay;

    private Vector3 currentVelocityOffset = Vector3.zero;

    bool cursorBefore = false;

    public static GameCamera instance;



    public Vector3 joyLookVelocity = Vector3.zero;

    public float joyLookAccel = 5f;
    public float joyLookMaxSpeed = 10f;
    public float joyLookDeAccel = 10f;

    public bool joystickLook = false;
    public bool joystickDeadStop = false;
    public bool joystickAimAssist = true;
    public float joystickAimAssistDistance = 3f;

    public bool mouseEnabled = true;
    public bool joystickEnabled = true;

    public bool playerTwo = false;

    public bool splitScreen = false;

    public float joyLerp = 5f;

    private void Awake()
    {
        instance = this;
    }

    PlayerController getTargetPlayer()
    {
        if (playerTwo)
            return GameController.instance.coopPlayer;
        return GameController.instance.playerController;
    }

    void Start()
    {
        Cursor.visible = false;
        camera = GetComponent<Camera>();
    }

    void updateVelocityOffset()
    {
        var targetVelocityOffset = getTargetPlayer().GetRigidBody().velocity * velocityOffset;
        currentVelocityOffset = Vector3.Lerp(currentVelocityOffset, targetVelocityOffset, velocityOffsetLerp * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.debugSpawnMonsters)
            ProcessDebug();
        updateVelocityOffset();
        var target = getTargetPlayer();
        ProcessAimInput();
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y + height, target.transform.position.z + distance);
        transform.LookAt(target.transform, Vector3.up);
        var aimDistanceY = aimDistance;
        var yoff = 0f;
        if (splitScreen)
        {
            if (joystickLook)
                yoff = 0.25f;
            aimDistanceY *= 2f;
        }
        transform.position = transform.position - new Vector3(aimLocation.x * aimDistance, 0f, (aimLocation.y + yoff) * aimDistanceY) - currentVelocityOffset;
    }

    void ProcessDebug()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var monster = Instantiate(GameController.instance.monsterPrefab, getTargetPlayer().GetAim(), Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            var monster = Instantiate(GameController.instance.monsterPrefab, getTargetPlayer().GetAim(), Quaternion.identity);
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
        if (PauseController.paused)
            return;
        //Invalid target - no player controller. todo - lerp back to zero? - No don't do.
        if (!getTargetPlayer() || !GameController.instance.controlEnabled || GameController.GetBusy())
        {
            if (!cursorBefore)
            {
                Cursor.visible = true;
                cursorBefore = true;
            }
            crosshair.SetActive(false);
            return;
        }
        if (cursorBefore)
        {
            Cursor.visible = false;
            cursorBefore = false;
        }

        if (joystickEnabled)
        {

            var lookHorizontal = Input.GetAxisRaw("Look Horizontal");
            var lookVertical = -Input.GetAxisRaw("Look Vertical");
            if (lookHorizontal != 0f || lookVertical != 0f)
                joystickLook = true;
            /*
            //Debug.Log("Horizontal: " + lookHorizontal.ToString());
            //Debug.Log("Vertical: " + lookVertical.ToString());

            if (joyLookVelocity.x > 0f)
            {
                if (joystickDeadStop)
                {
                    if (lookHorizontal < 0f)
                        joyLookVelocity.x = 0f;
                }
                if (lookHorizontal == 0f)
                {
                    joyLookVelocity.x -= joyLookDeAccel * Time.deltaTime;
                    if (joyLookVelocity.x < 0f)
                        joyLookVelocity.x = 0f;
                }
            }

            if (joyLookVelocity.x < 0f)
            {
                if (joystickDeadStop)
                {
                    if (lookHorizontal > 0f)
                        joyLookVelocity.x = 0f;
                }
                if (lookHorizontal == 0f)
                {
                    joyLookVelocity.x += joyLookDeAccel * Time.deltaTime;
                    if (joyLookVelocity.x > 0f)
                        joyLookVelocity.x = 0f;
                }
            }

            if (joyLookVelocity.y > 0f)
            {
                if (joystickDeadStop)
                {
                    if (lookVertical < 0f)
                        joyLookVelocity.y = 0f;
                }
                if (lookVertical == 0f)
                {
                    joyLookVelocity.y -= joyLookDeAccel * Time.deltaTime;
                    if (joyLookVelocity.y < 0f)
                        joyLookVelocity.y = 0f;
                }
            }

            if (joyLookVelocity.y < 0f)
            {
                if (joystickDeadStop)
                {
                    if (lookVertical > 0f)
                        joyLookVelocity.y = 0f;
                }
                if (lookVertical == 0f)
                {
                    joyLookVelocity.y += joyLookDeAccel * Time.deltaTime;
                    if (joyLookVelocity.y > 0f)
                        joyLookVelocity.y = 0f;
                }
            }

            if (lookHorizontal != 0f || lookVertical != 0f)
                joystickLook = true;

            if (lookHorizontal > 0f)
            {
                if (joyLookVelocity.x < joyLookMaxSpeed*lookHorizontal)
                    joyLookVelocity += lookHorizontal * Vector3.right * joyLookAccel * Time.deltaTime;
            }
            else
            {
                if (joyLookVelocity.x > joyLookMaxSpeed * lookHorizontal)
                    joyLookVelocity += lookHorizontal * Vector3.right * joyLookAccel * Time.deltaTime;
            }

            if (lookVertical > 0f)
            {
                if (joyLookVelocity.y < joyLookMaxSpeed * lookVertical)
                    joyLookVelocity += lookVertical * Vector3.up * joyLookAccel * Time.deltaTime;
            }
            else
            {
                if (joyLookVelocity.y > joyLookMaxSpeed * lookVertical)
                    joyLookVelocity += lookVertical * Vector3.up * joyLookAccel * Time.deltaTime;
            }

            if (joyLookVelocity.magnitude >= joyLookMaxSpeed)
                joyLookVelocity = joyLookVelocity.normalized * joyLookMaxSpeed;


            */

            var targetVel = new Vector3(lookHorizontal * joyLookMaxSpeed, lookVertical * joyLookMaxSpeed, 0f);

            joyLookVelocity = Vector3.Lerp(joyLookVelocity, targetVel, Time.deltaTime * joyLerp);

            aimLocation += joyLookVelocity * Time.deltaTime;
            var maxAim = 0.5f;
            var maxAimVert = 0.5f;
            var minAimVert = -0.5f;
            if (splitScreen)
                maxAimVert = 0.0f;
            if (aimLocation.x >= maxAim)
                aimLocation.x = maxAim;
            if (aimLocation.x <= -maxAim)
                aimLocation.x = -maxAim;
            if (aimLocation.y >= maxAimVert)
                aimLocation.y = maxAimVert;
            if (aimLocation.y <= minAimVert)
                aimLocation.y = minAimVert;

            
        }
        var finalLocation = aimLocation;

        var aimScreen = Vector3.zero;

        var screenHeight = Screen.height;
        var screenHeightOffset = 0.5f;


        if (!joystickLook)
        {
            if (mouseEnabled)
            {
                finalLocation = InputUtils.GetMouse();
                aimLocation = finalLocation;
                
                aimScreen = InputUtils.GetMouseScreen();
                if (splitScreen)
                {
                    if (aimLocation.y <= 0.0f)
                        aimLocation.y = 0.0f;
                    aimLocation.y -= 0.25f;
                    if (aimScreen.y <= Screen.height / 2)
                        aimScreen.y = Screen.height / 2;
                }
            }
        }
        else
        {
            aimScreen = new Vector3((finalLocation.x + 0.5f) * Screen.width, (finalLocation.y + screenHeightOffset) * screenHeight, 0f);
        }
            Ray ray = camera.ScreenPointToRay(aimScreen);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, GameController.GetGroundMask()))
            {
            var finalVec = hit.point;
                if (joystickLook && joystickAimAssist)
            {
                var lastDistance = 0f;
                CharacterComponent lastChar = null;
                    var chars = GameController.GetCharacters();
                    foreach (var element in chars)
                    {
                        if (element.GetTeam() != getTargetPlayer().team && element.IsAlive())
                    {
                        var dist = Vector3.Distance(hit.point, element.transform.position);
                        if (lastChar == null && dist <= joystickAimAssistDistance)
                        {
                            lastChar = element;
                            lastDistance = dist;
                        }
                        else
                        {
                            if (dist < lastDistance && dist <= joystickAimAssistDistance)
                            {
                                lastChar = element;
                                lastDistance = dist;
                            }
                        }
                    }
                }
                    if (lastChar != null)
                {
                    var currentWep = getTargetPlayer().inventory.GetCurrentItem();
                    var travelSpeed = 0f;
                    if (currentWep != null)
                        travelSpeed = currentWep.aimAssistTravelSpeed;
                    finalVec = new Vector3(lastChar.transform.position.x, hit.point.y, lastChar.transform.position.z);
                    if (travelSpeed != 0f)
                    {
                        var fromPlayerDist = Vector3.Distance(getTargetPlayer().transform.position, lastChar.transform.position);
                        finalVec = new Vector3(lastChar.transform.position.x, hit.point.y, lastChar.transform.position.z) + (lastChar.FlatVelocity() * fromPlayerDist * travelSpeed);
                    }
                }
            }
            getTargetPlayer().SetAim(finalVec);
                crosshair.SetActive(true);
            crosshair.transform.position = hit.point;
            }
            else
            {
                crosshair.SetActive(false);
                var floorDistance = ray.origin.y - getTargetPlayer().transform.position.y;
                getTargetPlayer().SetAim(ray.origin + (ray.direction * floorDistance));
            }
    }
}
