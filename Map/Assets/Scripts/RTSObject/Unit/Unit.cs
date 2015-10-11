using UnityEngine;
using System.Collections;
using Pathfinding;

public class Unit : RTSObject
{

    public float moveSpeed = 20;             // Velocitat de moviment

    public CharacterController controller;
    private Vector3 targetPosition;         // Indica el vector3 del objectiu
    private GameObject targetObject;        // Indica el objecte del objectiu
    public Seeker seeker;
    private Path path;
    private float nextWaypointDistance = 3.0f;
    private int currentWaypoint = 0;

    protected bool moving;                  // Indica si esta movent-se          


    /*** Metodes per defecte de Unity ***/

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<CharacterController>();
        seeker = GetComponent<Seeker>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (path == null || currentWaypoint >= path.vectorPath.Count)
        {
            moving = false;
            return;
        }
        else
        {
            moveToPosition();
        }
    }

    protected override void OnGUI()
    {
        base.OnGUI();
    }

    protected override void Animating()
    {
        base.Animating();
        anim.SetBool("IsWalking", moving);
    }

    /*** Metodes publics ***/

    public override bool CanAttack()
    {
        return true;
    }

    public override bool CanMove()
    {
        return true;
    }

    public void setNewPath(Vector3 target)
    {
        
        targetPosition = target;
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);
    }

    /*** Metodes privats ***/

    private void OnPathComplete(Path newPath)
    {
        if (!newPath.error)
        {
            path = newPath;
            currentWaypoint = 0;
        }
    }

    private void moveToPosition()
    {
        moving = true;
        Quaternion newRotation = Quaternion.LookRotation(targetPosition - transform.position);

        newRotation.x = 0f;
        newRotation.z = 0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10);

        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir = dir * moveSpeed * Time.deltaTime;
        controller.SimpleMove(dir);

        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }
}
