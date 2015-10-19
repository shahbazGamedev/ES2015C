using UnityEngine;
using System.Collections;
using Pathfinding;

public class Unit : RTSObject
{

    public float moveSpeed = 200;             // Velocitat de moviment

    public CharacterController controller;
    private Vector3 targetPosition;
    public Seeker seeker;
    private Path path;
    private float nextWaypointDistance = 3.0f;
    private int currentWaypoint = 0;

    protected bool moving;                  // Indica si esta movent-se

    private Vector3 destination;            // Posicio del desti en el mon
    private GameObject destinationTarget;   // Indica el objectiu

    /*** Metodes per defecte de Unity ***/

    protected override void Awake()
    {
        base.Awake();
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
        moveToPosition();
        Animating();
    }

    protected override void OnGUI()
    {
        base.OnGUI();
    }

    /*** Metodes publics ***/

    public void setNewPath(Vector3 target)
    {
        moving = true;
        targetPosition = target;
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);
    }

    /*** Metodes privats ***/

    // Metode que usem per animar la unitat
    private void Animating()
    {
        anim.SetBool("IsWalking", moving);
        anim.SetBool("IsAttacking", attacking);
    }

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

   public GameObject FindClosest(string tag)
    {

        GameObject[] TaggedObjects = GameObject.FindGameObjectsWithTag(tag); //Retorna una llista amb els objectes que tenen el tag tag
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject go in TaggedObjects) //recorre la llista dels objectes i caculca quin es el més proper
        {
            position = (go.transform.position - position);
            var curDistance = position.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        return closest;    //retorna l'objecte més proper
    }
}
