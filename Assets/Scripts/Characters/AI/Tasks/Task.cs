using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task
{
    public AICharacterComponent owner;
    public int priority;

    protected bool RouteTo(Vector3 position, float minDistance, GameObject target = null)
    {
        var dist = Vector3.Distance(owner.transform.position, position);
        var heading = (owner.transform.position - position).normalized;
        heading.y = 0f;
        heading = heading.normalized;
        if (dist > minDistance)
        {
            /*
            var off = 0.15f;
            var colCheckOrigin = owner.transform.position + (-heading * off) + (Vector3.up * 0.1f);
            var colCheckLength = 3f;
            var ray = new Ray(colCheckOrigin, -heading);
            Debug.DrawLine(colCheckOrigin, colCheckOrigin + (-heading*colCheckLength/2f), Color.green, 1f);
            Debug.DrawLine(colCheckOrigin + (-heading * colCheckLength / 2f), colCheckOrigin + (-heading * colCheckLength), Color.red, 1f);
            RaycastHit hit;
            var rCast = Physics.Raycast(ray, out hit, colCheckLength);
            if (rCast)
            {
                
                if (hit.collider.gameObject != target && hit.collider.gameObject != owner.gameObject)
                {
                    Debug.DrawLine(hit.collider.transform.position, hit.collider.transform.position + (Vector3.up * 5f), Color.yellow, 1f);

                    //var away = -hit.normal;
                    var away = (hit.point - owner.transform.position).normalized;
                    //var away = Quaternion.FromToRotation(-heading, -hit.normal) * Vector3.forward;
                    away.y = 0f;
                    if (away.magnitude > Vector3.kEpsilon)
                    {
                        away = away.normalized;
                        away = Vector3.Lerp(heading, away, 0.5f).normalized;
                    }
                    else
                        away = heading;
                    //var away = (owner.transform.position -hit.point).normalized;
                    Debug.DrawLine(hit.collider.transform.position, hit.collider.transform.position + (away * 5f), Color.magenta, 1f);
                    Debug.Log(away.ToString());
                    //away = Vector3.Lerp(heading, away, 0.5f).normalized;
                    //Debug.DrawLine(owner.transform.position, owner.transform.position + (away * 1f), Color.yellow, 1f);
                    heading = away;
                }
                
            }*/
            owner.movementVector = heading;
            return false;
        }
        else
        {
            owner.movementVector = Vector3.zero;
            owner.SetRotation(Quaternion.LookRotation(-heading).eulerAngles.y);
            return true;
        }
    }
    public Task(AICharacterComponent owner, int priority)
    {
        this.owner = owner;
        this.priority = priority;
    }
    public abstract bool Tick();
}
