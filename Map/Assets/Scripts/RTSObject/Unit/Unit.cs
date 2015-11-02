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
	public float visibility = 20f;
	private TerrainFoW tf;

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

		if (Input.GetKey (KeyCode.U)) {
			visibility = 400f;
		}


		explore ();

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

	private void explore(){
		tf = GameObject.FindObjectOfType(typeof(TerrainFoW)) as TerrainFoW;
		
		Vector3 vec = transform.position;
		Debug.Log(vec);
		tf.ExploreArea (vec, visibility);
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

    /// <summary>
    /// Gets the attack strengh that the unit has when it is attacking.
    /// </summary>
    /// <returns>The number of attack sthengh points.</returns>
    public float GetAttackStrengh()
    {
        // TODO: Implement this method properly
        return 321;
    }

    /// <summary>
    /// Gets the defense points that the unit has when it is being attacked.
    /// </summary>
    /// <returns>The number of defense points.</returns>
    public float GetDefense()
    {
        // TODO: Implement this method properly
        return 654;
    }

    /// <summary>
    /// Gets the distance at which the unit can attack, if it is a range unit (e.g. an archer). Otherwise, null.
    /// </summary>
    /// <returns>The distance at which the unit can attack, or null.</returns>
    public float? GetAttackRange()
    {
        // TODO: Implement this method properly
        return 987;
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
