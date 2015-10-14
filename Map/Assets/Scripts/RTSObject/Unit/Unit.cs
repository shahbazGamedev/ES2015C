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

        // If the unit is currently moving, call the function to update
        // the position in the path the unit is following
        if (path != null && currentWaypoint < path.vectorPath.Count)
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

    /// <summary>
    /// Tells the unit to move to the given position, by generating and
    /// following a route to the desired position.
    /// </summary>
    /// <param name="target">The position we want the unit to move to.</param>
    public void setNewPath(Vector3 target)
    {
        // We're starting movement, so start the walking animation
        moving = true;

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
        // Set the rotation of the model to point in the direction of the target
        Quaternion newRotation = Quaternion.LookRotation(targetPosition - transform.position);
        newRotation.x = 0f;
        newRotation.z = 0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10);

        // Tell the controller to move in the straight-line direction
        // between the current position and the waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir = dir * moveSpeed * Time.deltaTime;
        controller.SimpleMove(dir);

        // Have we reached the waypoint in the path?
        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
        {
            // Advance to the next waypoint
            currentWaypoint++;

            // If we have reached the last waypoint in the path,
            // then we need to stop the walking animation
            if (path.vectorPath.Count == currentWaypoint)
            {
                moving = false;
            }
        }
    }
}
