using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComponent : HealthController
{
    public float dashTime = 3f;
    public float dashSpeed = 1.3f;
    public float dashReplenish = 0.3f;

    public bool canDash = false;
    bool dashing = false;
    float currentDashTime;
    bool dashRecovering = false;


    public ItemComponent holding;

    //Looking
    public bool lookAt = true;
    public float lookAtDistance = 50f;
    public float lookAtLerp = 5f;
    public float lookAtTrackLerp = 5f;

    bool lookingAtFriendly = false;
    float lookAtDuration = 0f;
    float lookAtCD = 0f;

    [HideInInspector]
    public Vector3 lookAtTarget = Vector3.zero;
    [HideInInspector]
    public float lookAtIntensity = 0f;

    public float height = 1.9f;
    public Action currentAction = null;
    public List<Action> queuedActions = new List<Action>();

    public float rotationLerp = 10f;
    public GameObject mesh;

    //Stats
    public float acceleration = 25f;
    public float maxSpeed = 7f;
    public float deacceleration = 15f;

    public float flinchDuration = 0.25f;
    public bool moveDuringFlinch = true;

    public float flinchTagging = 0.4f;

    //Input
    public Vector3 movementVector = Vector3.zero;

    //Event Listeners
    List<MonoBehaviour> subscribedEntities = new List<MonoBehaviour>();
    List<CharacterEffect> effects = new List<CharacterEffect>();

    private RigidbodyConstraints oldConsrtaints;

    public bool culled = false;

    public bool hasEffectWithID(string id)
    {
        foreach(var element in effects)
        {
            if (element.id == id)
                return true;
        }
        return false;
    }

    public void AddEffect(CharacterEffect effect)
    {
        effects.Add(effect);
    }

    public void StripAllEffects()
    {
        effects.Clear();
    }

    protected virtual void OnDestroy()
    {
        GameController.dirtyCharacters();
    }

    public void Optimize()
    {
        if (!culled)
            oldConsrtaints = rigidBody.constraints;
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        culled = true;
    }

    public void Unoptimize()
    {
        rigidBody.constraints = oldConsrtaints;
        culled = false;
    }

    protected virtual void Awake()
    {
        GameController.dirtyCharacters();
        currentDashTime = dashTime;
        var rot = transform.rotation;
        transform.rotation = Quaternion.identity;
        mesh.transform.rotation = rot;
    }

    public void SetDashing(bool dashing)
    {
        var oldDash = this.dashing;
        if (!CanDash() && dashing == true)
            dashing = false;
        this.dashing = dashing;
        if (this.dashing != oldDash)
        {
            if (this.dashing == true)
                SendEvent("DashStart");
            else
                SendEvent("DashEnd");
        }
    }

    public float GetDashStamina()
    {
        return currentDashTime;
    }

    void DashLoop()
    {
        var degradeMultiply = 1f;
        foreach (var element in effects)
            degradeMultiply *= element.sprintDegradeMultiplier;
        if (currentDashTime <= 0f && !dashRecovering)
        {
            currentDashTime = 0f;
            dashRecovering = true;
            if (dashing)
                SendEvent("DashEnd");
            dashing = false;
        }
        if (currentDashTime >= dashTime && dashRecovering)
        {
            currentDashTime = dashTime;
            dashRecovering = false;
        }
        if (dashRecovering)
        {
            if (dashing)
                SendEvent("DashEnd");
            dashing = false;
        }
        if (currentDashTime < dashTime && !dashing)
        {
            currentDashTime += Time.deltaTime * dashReplenish;
        }
        if (!dashRecovering && dashing)
        {
            currentDashTime -= Time.deltaTime * degradeMultiply;
        }
    }

    public bool CanDash()
    {
        if (!canDash)
            return false;
        if (!IsAlive())
            return false;
        if (ActionBusy())
        {
            if (currentAction != null)
            {
                if (!currentAction.canDash)
                    return false;
            }
        }
        if (dashRecovering)
            return false;
        if (movementVector == Vector3.zero)
            return false;
        return true;
    }

    void EffectLoop()
    {
        var effectsToRemove = new List<CharacterEffect>();
        foreach(var element in effects)
        {
            element.duration -= Time.deltaTime;
            if (element.duration <= 0f)
                effectsToRemove.Add(element);
        }
        foreach(var element in effectsToRemove)
        {
            effects.Remove(element);
        }
    }

    void lookAtLoop()
    {
        if (!IsAlive())
            return;
        var actionAllows = true;
        if (currentAction != null)
        {
            if (!currentAction.lookAt)
                actionAllows = false;
        }
        var ents = GameController.GetCharacters();
        CharacterComponent lastEnt = null;
        var lastDistance = 0f;
        if (actionAllows)
        {
            foreach (var element in ents)
            {
                var heading = (element.transform.position - transform.position).normalized;
                var headDot = Vector3.Dot(mesh.transform.forward, heading);
                if (headDot >= 0.2f && element.IsAlive())
                {
                    var distance = Vector3.Distance(transform.position, element.transform.position);
                    if (!lastEnt)
                    {
                        lastEnt = element;
                        lastDistance = distance;
                    }
                    else
                    {
                        if (lastEnt.GetTeam() == GetTeam())
                        {
                            if (element.GetTeam() != GetTeam())
                            {
                                lastEnt = element;
                                lastDistance = distance;
                            }
                            else
                            {
                                if (distance < lastDistance)
                                {
                                    lastEnt = element;
                                    lastDistance = distance;
                                }
                            }
                        }
                        else
                        {
                            if (element.GetTeam() != GetTeam())
                            {
                                if (distance < lastDistance)
                                {
                                    lastEnt = element;
                                    lastDistance = distance;
                                }
                            }
                        }
                    }
                }
            }
            lookAtCD -= Time.deltaTime;
            if (lastEnt)
            {
                if (lastEnt.GetTeam() == GetTeam())
                {
                    if (lookAtCD <= 0f)
                    {
                        if (!lookingAtFriendly)
                        {
                            lookAtDuration = Random.Range(2f, 4f);
                            lookingAtFriendly = true;
                        }
                        else
                        {
                            lookAtDuration -= Time.deltaTime;
                            if (lookAtDuration <= 0f)
                            {
                                lookAtCD = Random.Range(10f, 30f);
                                lookingAtFriendly = false;
                                lastEnt = null;
                            }
                            else
                            {
                                lookingAtFriendly = true;
                            }
                        }
                    }
                    else
                    {
                        lookingAtFriendly = false;
                        lastEnt = null;
                    }
                }
                else
                    lookingAtFriendly = false;
            }
        }
        if (lastEnt)
        {
            lookAtIntensity = Mathf.Lerp(lookAtIntensity, 1f, lookAtLerp * Time.deltaTime);
            if (lookAtTarget != Vector3.zero)
                lookAtTarget = Vector3.Lerp(lookAtTarget, lastEnt.transform.position + (lastEnt.height * Vector3.up), lookAtTrackLerp * Time.deltaTime);
            else
                lookAtTarget = lastEnt.transform.position + (lastEnt.height * Vector3.up);
        }
        else
        {
            lookAtTarget = Vector3.Lerp(lookAtTarget, transform.position + (height*Vector3.up) + (1f*mesh.transform.forward), lookAtTrackLerp * Time.deltaTime);
            lookAtIntensity = Mathf.Lerp(lookAtIntensity, 0f, lookAtLerp * Time.deltaTime);
            lookingAtFriendly = false;
        }
    }

    public override void OnDeath(Damage killingDamage)
    {
        base.OnDeath(killingDamage);
        if (GetTeam() != GameController.instance.playerTeam)
            DungeonController.instance.dungeonState.killedEnemies += 1;
    }

    protected virtual void Flinch(Damage damage)
    {
        if (flinchDuration > 0f)
        {
            var flinchAction = new FlinchAction(this, flinchDuration, moveDuringFlinch, flinchTagging);
            if (GetActionPriority() <= ActionPriority.Override)
                QueueAction(flinchAction);
        }
    }
    protected override void Start()
    {
        base.Start();
        if (GetTeam() != GameController.instance.playerTeam)
            DungeonController.instance.dungeonState.spawnedEnemies += 1;
        damageEvent += Flinch;
    }
    public void Subscribe(MonoBehaviour behavior)
    {
        if (subscribedEntities.IndexOf(behavior) < 0)
            subscribedEntities.Add(behavior);
    }

    public void SendEvent(string ev)
    {
        foreach(var element in subscribedEntities)
        {
            element.SendMessage(ev);
        }
    }
    protected virtual void RotateCharacter()
    {
        if (IsAlive())
        {

            var vel = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);

            if (vel.magnitude > Vector3.kEpsilon)
            {
                var movementQuaternion = Quaternion.LookRotation(vel.normalized);

                mesh.transform.rotation = Quaternion.Slerp(mesh.transform.rotation, movementQuaternion, rotationLerp * Time.deltaTime);
            }
        }
    }

    public bool IsMoving()
    {
        var vel = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);

        if (vel.magnitude > Vector3.kEpsilon)
            return true;
        return false;
    }

    public Vector3 FlatVelocity()
    {
        var vel = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
        return vel;
    }

    public float GetRotationY()
    {
        return mesh.transform.rotation.eulerAngles.y;
    }

    public Quaternion GetRotation()
    {
        return mesh.transform.rotation;
    }

    public void SetRotation(float rotationY)
    {
        mesh.transform.rotation = Quaternion.Euler(mesh.transform.rotation.eulerAngles.x, rotationY, mesh.transform.rotation.eulerAngles.z);
    }

    public void SetRotation(Quaternion rotation)
    {
        mesh.transform.rotation = rotation;
    }

    public ActionPriority GetActionPriority()
    {
        if (currentAction == null)
            return (ActionPriority)(-1);
        return currentAction.priority;
    }

    public bool ActionBusy()
    {
        if (currentAction != null || queuedActions.Count > 0)
            return true;
        return false;
    }

    public void CancelAction()
    {
        if (currentAction != null && currentAction.interruptible)
        {
            currentAction.Cancel();
            currentAction = null;
        }
        ClearActionQueue();
    }

    public void QueueAction(Action action)
    {
        if (currentAction == null)
            queuedActions.Add(action);
        else
        {
            if (currentAction.priority <= action.priority)
            {
                if (action.priority == ActionPriority.Override && currentAction.priority <= ActionPriority.Override)
                    CancelAction();
                queuedActions.Add(action);
            }
            else
            {
                if (action.priority > currentAction.priority)
                {
                    CancelAction();
                    queuedActions.Add(action);
                }
            }
        }
    }

    public void ClearActionQueue()
    {
        queuedActions.Clear();
    }

    public bool CanMoveAction()
    {
        if (currentAction == null)
            return true;
        if (currentAction.movable)
            return true;
        return false;
    }

    void ProcessActions()
    {
        if (IsAlive())
        {
            if (currentAction == null)
            {
                if (queuedActions.Count > 0)
                {
                    currentAction = queuedActions[0];
                    currentAction.Enter();
                    queuedActions.RemoveAt(0);
                }
            }
            if (currentAction != null)
            {
                var actionResult = currentAction.Tick();
                if (actionResult)
                    currentAction = null;
            }
        }
    }

    protected virtual void Update()
    {
        EffectLoop();
        DashLoop();
        if (lookAt)
            lookAtLoop();
        RotateCharacter();
        ProcessActions();
    }


    public void ResetDash()
    {
        dashRecovering = false;
        currentDashTime = dashTime;
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if (!IsAlive() || !CanMoveAction())
            movementVector = Vector3.zero;
        //test input
        rigidBody.velocity -= movementVector.x * Vector3.right * acceleration * Time.deltaTime;
        rigidBody.velocity -= movementVector.z * Vector3.forward * acceleration * Time.deltaTime;
        var dashiSpeed = dashSpeed;
        foreach(var element in effects)
        {
            dashiSpeed *= element.sprintSpeedMultiplier;
        }
        var currentTopSpeed = maxSpeed;
        if (dashing && CanDash())
            currentTopSpeed *= dashiSpeed;
        if (currentAction != null)
            currentTopSpeed *= currentAction.speedBuff;

        //cap speed
        if (rigidBody.velocity.magnitude >= currentTopSpeed)
        {
            rigidBody.velocity = rigidBody.velocity.normalized * currentTopSpeed;
        }

        //deaccelerate
        var xVel = rigidBody.velocity.x;
        var velSign = Mathf.Sign(rigidBody.velocity.x);
        var movementSign = Mathf.Sign(movementVector.x);
        if (velSign != -movementSign && xVel != 0f && movementVector.x != 0f)
            xVel = 0f;
        else
        {
            if (rigidBody.velocity.x != 0f && movementVector.x == 0f)
            {
                xVel = Mathf.Abs(rigidBody.velocity.x);
                xVel -= deacceleration * Time.deltaTime;
                if (xVel <= 0f)
                    xVel = 0f;
                xVel *= velSign;
            }
        }

        var zVel = rigidBody.velocity.z;
        velSign = Mathf.Sign(rigidBody.velocity.z);
        movementSign = Mathf.Sign(movementVector.z);
        if (velSign != -movementSign && zVel != 0f && movementVector.z != 0f)
            zVel = 0f;
        else
        {
            if (rigidBody.velocity.z != 0f && movementVector.z == 0f)
            {
                zVel = Mathf.Abs(rigidBody.velocity.z);
                zVel -= deacceleration * Time.deltaTime;
                if (zVel <= 0f)
                    zVel = 0f;
                zVel *= velSign;
            }
        }

        rigidBody.velocity = new Vector3(xVel, rigidBody.velocity.y, zVel);
    }
}
