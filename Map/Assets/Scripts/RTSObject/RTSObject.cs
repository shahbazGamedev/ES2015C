﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RTSObject : MonoBehaviour
{

    // Variables publiques generals
	public string objectName = "Generic RTS Object";     // Nom del objecte
	public int cost = 50, hitPoints = 100, maxHitPoints = 100; // Cost, punts de vida i vida maxima
	public int hitDamage = 10, defense = 0, attack = 0;		// Punt de atac, Habilitat defensa, Habilitat atac
	public enum ResourceType { Gold, Wood, Food, Unknown }	// Declarem els tipus de recursos
    public Player owner;                            // A quin player correspon

    // Variables accessibles per a les subclasses
    protected string[] actions = { };               // Accions que pot realitzar
    protected float healthPercentage = 1.0f;        // Percentatge de vida
    protected RTSObject target = null;              // Posible objectiu
    protected float remainingTimeToAttack = 0;
    protected bool attacking = false, movingIntoPosition = false, aiming = false;   // Booleans dels tres estats comuns a tots els objectes
    protected List<RTSObject> nearbyObjects;        // Llista de objectes propers

    protected Animator anim;                        // Referencia al component Animator.

	private int ObjectId { get; set; }               // Identificador unic del objecte
    private float currentWeaponChargeTime;

    /*** Metodes per defecte de Unity ***/

    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
        if (attacking && movingIntoPosition) CheckMovedIntoPosition();
        if (attacking && !movingIntoPosition) PerformAttack();
        if (anim && anim.runtimeAnimatorController) Animating();
    }

    protected virtual void OnGUI()
    {
    }

    /*** Metodes publics ***/

    // Metode per obtenir les accions del objecte
    public string[] GetActions()
    {
        return actions;
    }

    // Metode per realitzar la accio corresponent
    public virtual void PerformAction(string actionToPerform)
    {
    }

    // Metode per declarar accions al fer click al boto del ratoli
    public virtual void MouseClick(GameObject hitObject, Vector3 hitPoint, Player controller)
    {
    }

    // Metode per saber si el Player es el propietari del objecte
    public bool IsOwnedBy(Player owner)
    {
        if (this.owner && this.owner.Equals(owner))
            return true;
        return false;
    }

	/// <summary>
	/// Tells if the object can change its position.
	/// </summary>
	/// <returns>Boolean saying if the object can move or not.</returns>
    public virtual bool CanMove()
    {
        return false;
    }

    /// <summary>
    /// Tells the object to start a free (non-attack) movement to the
    /// given position.
    /// </summary>
    /// <param name="target">The position we want the unit to move to.</param>
    public void MoveTo(Vector3 target)
    {
        if (attacking)
        {
            EndAttack();
        }

        SetNewPath(target);
    }

    /// <summary>
    /// Tells the unit to move to the given position, by generating and
    /// following a route to the desired position.
    /// </summary>
    /// <param name="target">The position we want the unit to move to.</param>
    protected virtual void SetNewPath(Vector3 target)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Tell the unit to cancel the movement path to the given position.
    /// </summary>
    protected virtual void CancelPath()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Tells if the object can attack.
    /// </summary>
    /// <returns>Boolean saying if the object can attack or not.</returns>
    public virtual bool CanAttack()
    {
        return false;
    }

    /// <summary>
    /// Begins an attack on the given target object.
    /// To stop the current attack sequence, pass null to this method.
    /// </summary>
    /// <param name="target">The target of the attack. If null is given, the attack will stop.</param>
    public void AttackObject(RTSObject target)
    {
        if (target != null)
        {
            BeginAttack(target);
        }
        else
        {
            EndAttack();
        }
    }

    /// <summary>
    /// Gets the attack strengh that the unit has when it is attacking.
    /// </summary>
    /// <returns>The number of attack strength points.</returns>
    public virtual int GetAttackStrength()
    {
        throw new InvalidOperationException("GetAttackStength must be implemented on objects that can attack.");
    }

    /// <summary>
    /// Gets the attack strengh that the unit has when it is attacking, in attack/second.
    /// </summary>
    /// <returns>The number of attacks per second of this unit.</returns>
    public virtual float GetAttackSpeed()
    {
        throw new InvalidOperationException("GetAttackSpeed must be implemented on objects that can attack.");
    }

    /// <summary>
    /// Gets the distance at which the unit can attack, if it is a range unit (e.g. an archer).
    /// Otherwise, returns zero if the unit is not a range unit.
    /// </summary>
    /// <returns>The distance at which the unit can attack, or zero.</returns>
    public virtual float GetAttackRange()
    {
        throw new InvalidOperationException("GetAttackRange must be implemented on objects that can attack.");
    }

    /// <summary>
    /// Tell if the object can be attacked by military units.
    /// </summary>
    /// <returns>Boolean saying if the object can be attacked or not.</returns>
    public virtual bool CanBeAttacked()
    {
        return false;
    }

    /// <summary>
    /// Gets the defense points that the unit has when it is being attacked.
    /// </summary>
    /// <returns>The number of defense points.</returns>
    public virtual int GetDefense()
    {
        throw new InvalidOperationException("GetDefense must be implemented on objects that can defend.");
    }

    // Metode per extreure vida al objecte
    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0) Destroy(gameObject);
    }

	/*** Metodes interns accessibles per les subclasses ***/
	
	// Funcio auxiliar per al calcul del BoxCollider i el CharacterController
	protected void ExtendBounds (Transform t, ref Bounds b)
	{
		Renderer rend = t.GetComponent<Renderer> ();
		if (rend != null) {
			b.Encapsulate (rend.bounds.min);
			b.Encapsulate (rend.bounds.max);
		}
		
		foreach (Transform t2 in t) {
			ExtendBounds (t2, ref b);
		}
	}
	
	// Metode per calcular la vida actual del objecte
	protected virtual void CalculateCurrentHealth()
	{
		healthPercentage = (float)hitPoints / (float)maxHitPoints;
	}
	
	// Metode que usem per animar el objecte
	protected virtual void Animating()
	{
		anim.SetBool("IsAttacking", attacking);
		anim.SetBool("IsDead", hitPoints <= 0);
	}
	
	// Metode per disparar
	protected virtual void UseWeapon()
	{
		currentWeaponChargeTime = 0.0f;
	}
	
	// Metode per apuntar
	protected virtual void AimAtTarget()
	{
		aiming = true;
	}

    /*** Metodes privats ***/

    // Metode que dibuixa el GUI del objecte
    private void DrawGUI()
    {
    }

    /// <summary>
    /// Since objects can't overlap, we have to give them some tolerance
    /// for the attacks to be possible for non-range units.
    /// </summary>
    const float AttackRangeTolerance = 5;

    /// <summary>
    /// Begins an attack sequence on the given target.
    /// </summary>
    /// <param name="newTarget">The target of the attack.</param>
    private void BeginAttack(RTSObject newTarget)
    {
        // Calculate the distance to the nearest point in the target
        var targetAttackPosition = newTarget.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
        var distanceToTarget = (targetAttackPosition - transform.position).magnitude;

        // Check if we need to move to the target to attack it, and if we
        // need to and the unit can't move, warn the user that the attack can't be done
        bool needToMoveToTarget = (distanceToTarget >= GetAttackRange() + AttackRangeTolerance);
        if (needToMoveToTarget && !CanMove())
        {
            Debug.Log(string.Format("{0} can't attack {1}, because it is {2}m away",
                objectName, newTarget.objectName, distanceToTarget));
            return;
        }

        // Enable the attack status
        target = newTarget;
        attacking = true;
        movingIntoPosition = needToMoveToTarget;
        remainingTimeToAttack = 0;

        if (needToMoveToTarget)
        {
            // Move to a point near the target to attack
            var attackPosition = targetAttackPosition - (targetAttackPosition -
                transform.position).normalized * AttackRangeTolerance / 4;
            SetNewPath(attackPosition);
        }
        else
        {
            // Begin attacking immediately if we are close enough the target
            PerformAttack();
        }
    }
    
    /// <summary>
    /// Cancels an attack sequence on the given target.
    /// </summary>
    private void EndAttack()
    {
        // If we were moving to the target to attack it, cancel this move
        if (attacking && movingIntoPosition)
        {
            CancelPath();
        }

        // Disable the attack status
        target = null;
        attacking = false;
        movingIntoPosition = false;
        remainingTimeToAttack = 0;
    }

    /// <summary>
    /// If the object is moving into position in order to perform an attack,
    /// checks if the object is close enough to the target to begin the attack.
    /// </summary>
    private void CheckMovedIntoPosition()
    {
        // If the enemy is dead (or already destroyed), cancel the attack action
        // http://answers.unity3d.com/questions/13840/how-to-detect-if-a-gameobject-has-been-destroyed.html
        if (target == null || target.hitPoints == 0)
        {
            EndAttack();
            return;
        }

        // Check if we are close enough of the enemy to begin the attack sequence
        var targetAttackPosition = target.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
        var distanceToTarget = (targetAttackPosition - transform.position).magnitude;

        bool needToMoveToTarget = (distanceToTarget >= GetAttackRange() + AttackRangeTolerance);

        if (!needToMoveToTarget)
        {
            movingIntoPosition = false;
            CancelPath();
        }
    }
    
    /// <summary>
    /// If the object is performing an attack, inflicts damage to the target according
    /// to the attack speed, strength and target defense.
    /// </summary>
    private void PerformAttack()
    {
        // If the enemy is dead (or already destroyed), cancel the attack action
        // http://answers.unity3d.com/questions/13840/how-to-detect-if-a-gameobject-has-been-destroyed.html
        if (target == null || target.hitPoints == 0)
        {
            EndAttack();
            return;
        }

        // Check if enough time has passed to hit the object again
        remainingTimeToAttack -= Time.deltaTime;
        while (remainingTimeToAttack <= 0)
        {
            // Take damage according to the same formula used in AOE3
            var finalAttackPointsPerSec = Math.Max(GetAttackStrength() - target.GetDefense(), 1);
            target.TakeDamage(finalAttackPointsPerSec);

            remainingTimeToAttack += GetAttackSpeed();
        }
    }
}
