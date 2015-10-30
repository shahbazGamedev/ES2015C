﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using Pathfinding;

public class Unit : RTSObject
{
	public Avatar unitAvatar;				// Referencia al avatar del component Animator.

	protected float moveSpeed = 5;             	// Velocitat de moviment

	private CharacterController characterController;	// Referencia al component CharacterController.
	private Seeker seeker;					// Referencia al component Seeker.
	private Vector3 targetPosition;         // Indica el vector3 del objectiu
	private GameObject targetObject;        // Indica el objecte del objectiu

	private Quaternion aimRotation;
	private Path path;
	private float nextWaypointDistance = 3.0f;
	private int currentWaypoint = 0;

	protected bool moving;                  // Indica si esta movent-se 
	protected bool running;					// Indica si esta corrent


	/*** Metodes per defecte de Unity ***/

	protected override void Awake ()
	{
		base.Awake ();
		seeker = gameObject.AddComponent<Seeker> ();
		anim = gameObject.AddComponent<Animator>();
		// Calculem la dimensio del CharacterController
		FittedCharacterCollider();
		
		// Asignem les propietats el avatar del Animator
		anim.avatar = unitAvatar;
	}

	protected override void Start ()
	{
		base.Start ();
	}

	protected override void Update ()
	{
		base.Update ();

		// If the unit is currently moving, call the function to update
		// the position in the path the unit is following
		if (path != null && currentWaypoint < path.vectorPath.Count)
        {
			moveToPosition ();
		}
        else if (aiming)
		{
			
		}
	}

	protected override void OnGUI ()
	{
		base.OnGUI ();
	}

	/*** Metodes publics ***/

	/// <summary>
	/// Tells if the unit can attack
	/// </summary>
	/// <returns>Boolean saying if the units can attack or not.</returns>
	public override bool CanAttack ()
	{
		return false;
	}

	/// <summary>
	/// Tells if the unit can move
	/// </summary>
	/// <returns>Boolean saying if the units can move or not.</returns>
	public override bool CanMove ()
	{
		return true;
	}

    /// <summary>
    /// Gets the movement speed of a unit.
    /// </summary>
    /// <returns>The movement speed of a unit.</returns>
    public override float GetMovementSpeed()
    {
        return moveSpeed;
    }

	/// <summary>
	/// Tells the unit to move to the given position, by generating and
	/// following a route to the desired position.
	/// </summary>
	/// <param name="target">The position we want the unit to move to.</param>
	protected override void SetNewPath (Vector3 target)
	{
		// We're starting movement, so start the walking animation
		moving = true;

		targetPosition = target;
		seeker.StartPath (transform.position, targetPosition, OnPathComplete);
	}

    /// <summary>
    /// Tell the unit to cancel the movement path to the given position.
    /// </summary>
    protected override void CancelPath()
    {
        moving = false;
        path = null;
    }

    /// <summary>
    /// Tell the unit to cancel the movement path to the given position.
    /// </summary>
    protected override bool HasPath()
    {
        return moving;
    }

    public GameObject FindClosest (string tag)
	{
		
		GameObject[] TaggedObjects = GameObject.FindGameObjectsWithTag (tag); //Retorna una llista amb els objectes que tenen el tag tag
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		
		foreach (GameObject go in TaggedObjects) { //recorre la llista dels objectes i caculca quin es el més proper
			position = (go.transform.position - position);
			var curDistance = position.sqrMagnitude;
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}
		
		return closest;    //retorna l'objecte més proper
	}

	/*** Metodes interns accessibles per les subclasses ***/

	protected override void Animating ()
	{
		base.Animating ();
		anim.SetBool ("IsWalking", moving);
		anim.SetBool ("IsRunning", running);
	}
	
	// Metode per disparar
	protected override void UseWeapon()
	{
		base.UseWeapon();
	}
	
	// Metode per apuntar
	protected override void AimAtTarget()
	{
		base.AimAtTarget();
		aimRotation = Quaternion.LookRotation(target.transform.position - transform.position);
	}

	/*** Metodes privats ***/

	private void OnPathComplete (Path newPath)
	{
        // We need to be careful and check the 'moving' variable here, since
        // if we started calculating a path, and cancelled it quickly, we will still receive
        // the OnPathComplete() event even tough the movement was cancelled.
        // Note that the case where we start a new path, cancel it, and start a new one
        // quickly works correctly, since in this case the Seeker will cancel the first path
		if (moving && !newPath.error)
        {
			path = newPath;
			currentWaypoint = 0;
		}
	}

	private void moveToPosition ()
	{
		// Set the rotation of the model to point in the direction of the target
		Quaternion newRotation = Quaternion.LookRotation (targetPosition - transform.position);
		newRotation.x = 0f;
		newRotation.z = 0f;
		transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * 10);

		// Tell the controller to move in the straight-line direction
		// between the current position and the waypoint
		Vector3 dir = (path.vectorPath [currentWaypoint] - transform.position).normalized;
		dir = dir * GetMovementSpeed();
		characterController.SimpleMove (dir);

		// Have we reached the waypoint in the path?
		if (Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]) < nextWaypointDistance) {
			// Advance to the next waypoint
			currentWaypoint++;

			// If we have reached the last waypoint in the path,
			// then we need to stop the walking animation
			if (path.vectorPath.Count == currentWaypoint)
            {
                CancelPath();
			}
		}
	}

	// Calcula el characterController de la unitat
	private void FittedCharacterCollider ()
	{
		Transform transform = this.gameObject.transform;
		Quaternion rotation = transform.rotation;
		transform.rotation = Quaternion.identity;
		
		characterController = transform.GetComponent<CharacterController> ();
		
		if (characterController == null) {
			transform.gameObject.AddComponent<CharacterController> ();
			characterController = transform.GetComponent<CharacterController> ();
		}
		
		Bounds bounds = new Bounds (transform.position, Vector3.zero);
		
		ExtendBounds (transform, ref bounds);
		
		characterController.center = new Vector3(bounds.center.x - transform.position.x, bounds.center.y - transform.position.y + 0.25f, bounds.center.z - transform.position.z);
		characterController.radius = bounds.size.x / 2;
		characterController.height = bounds.size.y / transform.localScale.y;
		
		transform.rotation = rotation;
	}
}
