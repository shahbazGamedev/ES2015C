using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RTSObject : MonoBehaviour
{

    // Variables publiques generals
	public string objectName = "Generic RTS Object";     // Nom del objecte
	public int cost = 50, hitPoints = 100, maxHitPoints = 100; // Cost, punts de vida i vida maxima
    /// <summary>Default movement speed. Leave at zero if the object can't move.</summary>
    protected float baseMoveSpeed = 0;
	
	protected float currentBuildProgress = 10.0f;
	protected int baseBuildSpeed=0;
    /// <summary>Default attack strength. Leave at zero if the object can't attack.</summary>
    protected int baseAttackStrength = 0;
    /// <summary>Default number of hits per second of the object. Leave at zero if the object can't attack.</summary>
    protected float baseAttackSpeed = 0.0f;
    /// <summary>Default attack strength. Leave at zero if the object isn't a range unit.</summary>
    protected int baseAttackRange = 0;
    /// <summary>Default attack defense. Leave at null if the object can't defend.</summary>
    protected int? baseDefense = 0;                 // Punt de atac, Habilitat defensa, Habilitat atac
    public enum ResourceType { Gold, Wood, Food, Unknown }	// Declarem els tipus de recursos
    public Player owner;                            // A quin player correspon

    // Variables accessibles per a les subclasses
    protected string[] actions = { };               // Accions que pot realitzar
	protected bool currentlySelected = false;       // Indica si esta seleccionat
    protected float healthPercentage = 1.0f;        // Percentatge de vida
    protected RTSObject target = null;              // Posible objectiu
    protected bool aiming = false;
    /// <summary>true if the unit is attacking or moving into position to attack another unit.</summary>
    protected bool attacking = false;
    /// <summary>If attacking, position where the unit is programmed to move in order to attack another unit.</summary>
    protected Vector3 programmedAttackPosition;
    /// <summary>If attacking, number of seconds remaining until the unit takes another hit to the target.</summary>
    protected float remainingTimeToAttack = 0;

    /// <summary>true if the unit is dying (it has zero hit points and it playing the dead animation).</summary>
    protected bool dying = false;
    /*
    protected bool deadAnimationStarted = false;
    protected bool deadAnimationFinished = false;
    */
    /// <summary>Default time between the object having 0 HP and the object being destroyed.</summary>
    private const float DefaultDeadTime = 3.0f;
    /// <summary>Number of seconds until the unit is considered dead and it disappears from the map.</summary>
    protected float remainingTimeToDead = 0;

    protected List<RTSObject> nearbyObjects;        // Llista de objectes propers

    protected Animator anim;                        // Referencia al component Animator.
	protected Rigidbody rigbody;					// Referenica al component Rigidbody

	private int ObjectId { get; set; }               // Identificador unic del objecte
    private float currentWeaponChargeTime;

    /*** Metodes per defecte de Unity ***/

    protected virtual void Awake()
    {
		rigbody = gameObject.AddComponent<Rigidbody> ();
		rigbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
		if (currentlySelected) updateSelection ();
        if (attacking) PerformAttack();
        if (dying && UpdateDeadObject()) return; ;
        if (this != null && anim && anim.runtimeAnimatorController) Animating();
    }

    protected virtual void OnGUI()
    {
    }

    /*** Metodes publics ***/

	// Metode per declarar la seleccio del objecte
	public virtual void SetSelection(bool selected)
	{
		currentlySelected = selected;
	}

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
        return (!dying && baseMoveSpeed != 0);
    }

    /// <summary>
    /// Gets the movement speed of a object.
    /// </summary>
    /// <returns>The movement speed of a object.</returns>
    public virtual float GetMovementSpeed()
    {
        if (!CanMove())
            throw new InvalidOperationException("Called GetMovementSpeed over an object that can't move.");

        return baseMoveSpeed;
    }

    /// <summary>
    /// Tells the object to start a free (non-attack) movement to the
    /// given position.
    /// </summary>
    /// <param name="target">The position we want the object to move to.</param>
    public void MoveTo(Vector3 target)
    {
        if (!CanAttack())
            throw new InvalidOperationException("Called MoveTo over an object that can't move.");

        if (attacking)
        {
            EndAttack();
        }

        SetNewPath(target);
    }

    /// <summary>
    /// Tells the object to move to the given position, by generating and
    /// following a route to the desired position.
    /// </summary>
    /// <param name="target">The position we want the object to move to.</param>
    protected virtual void SetNewPath(Vector3 target)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Tell the object to cancel the movement path to the given position.
    /// </summary>
    protected virtual void CancelPath()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns true if the unit has programmed a movement path to a given position.
    /// </summary>
    /// <returns>true if the unit has a movement path, false otherwise.</returns>
    protected virtual bool HasPath()
    {
        throw new NotImplementedException();
    }
	
	public virtual bool CanBuild()
	{
		return (baseBuildSpeed !=0);
	}
	
	public virtual bool CanBeBuilt()
	{
		return (currentBuildProgress<10.0f);
	}

    /// <summary>
    /// Tells if the object can attack.
    /// </summary>
    /// <returns>Boolean saying if the object can attack or not.</returns>
    public virtual bool CanAttack()
    {
        return (!dying && baseAttackStrength != 0 && baseAttackSpeed != 0.0f);
    }

    /// <summary>
    /// Begins an attack on the given target object.
    /// To stop the current attack sequence, pass null to this method.
    /// </summary>
    /// <param name="target">The target of the attack. If null is given, the attack will stop.</param>
    public void AttackObject(RTSObject target)
    {
        if (!CanAttack())
            throw new InvalidOperationException("Called AttackObject over an object that can't attack.");

        if (target != null)
        {
            if (!target.CanBeAttacked())
                throw new InvalidOperationException("Called AttackObject over a target object that can't defend.");

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
        if (!CanAttack())
            throw new InvalidOperationException("Called GetAttackStrength over an object that can't attack.");

        return baseAttackStrength;
    }

    /// <summary>
    /// Gets the attack strengh that the unit has when it is attacking, in attack/second.
    /// </summary>
    /// <returns>The number of attacks per second of this unit.</returns>
    public virtual float GetAttackSpeed()
    {
        if (!CanAttack())
            throw new InvalidOperationException("Called GetAttackStrength over an object that can't attack.");

        return baseAttackSpeed;
    }

    /// <summary>
    /// Gets the distance at which the unit can attack, if it is a range unit (e.g. an archer).
    /// Otherwise, returns zero if the unit is not a range unit.
    /// </summary>
    /// <returns>The distance at which the unit can attack, or zero.</returns>
    public virtual float GetAttackRange()
    {
        if (!CanAttack())
            throw new InvalidOperationException("Called GetAttackRange over an object that can't attack.");

        return baseAttackRange;
    }

    /// <summary>
    /// Tell if the object can be attacked by military units.
    /// </summary>
    /// <returns>Boolean saying if the object can be attacked or not.</returns>
    public virtual bool CanBeAttacked()
    {
        return (!dying && baseDefense.HasValue);
    }

    /// <summary>
    /// Gets the defense points that the unit has when it is being attacked.
    /// </summary>
    /// <returns>The number of defense points.</returns>
    public virtual int GetDefense()
    {
        if (!CanBeAttacked())
            throw new InvalidOperationException("Called GetDefense over an object that can't defend.");

        return baseDefense.Value;
    }

    /// <summary>
    /// Substract health points from this object.
    /// </summary>
    /// <param name="damage">The number of health points to substract to this object.</param>
    public void TakeDamage(int damage)
    {
        hitPoints = Math.Max(hitPoints - damage, 0);
        if (hitPoints == 0)
        {
            BeginDead();
        }
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

	// Metode que actualitza segons la posicio del objecte el indicador de seleccio
	private void updateSelection(){

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
        target = newTarget;
        attacking = true;
        remainingTimeToAttack = 0;

        PerformAttack();
    }
    
    /// <summary>
    /// Cancels an attack sequence on the given target.
    /// </summary>
    private void EndAttack()
    {
        // If we were moving to the target to attack it, cancel this move
        if (CanMove() && HasPath())
        {
            CancelPath();
        }

        // Disable the attack status
        target = null;
        attacking = false;
        remainingTimeToAttack = 0;
    }
    
    /// <summary>
    /// If the object is performing an attack, inflicts damage to the target according
    /// to the attack speed, strength and target defense, or moves closer to the target
    /// in order to perform the attack.
    /// </summary>
    private void PerformAttack()
    {
        // If the enemy is dead (or already destroyed), cancel the attack action
        // http://answers.unity3d.com/questions/13840/how-to-detect-if-a-gameobject-has-been-destroyed.html
        if (target == null || target.hitPoints == 0 || !target.CanBeAttacked())
        {
            EndAttack();
            return;
        }

        // Calculate the distance to the nearest point in the target
        var targetAttackPosition = target.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
        var distanceToTarget = (targetAttackPosition - transform.position).magnitude;
        var targetInRange = (distanceToTarget < GetAttackRange() + AttackRangeTolerance);

        if (targetInRange)
        {
            // Cancel movement if we've just reached the target
            if (CanMove() && HasPath())
            {
                CancelPath();
            }

            // Check if enough time has passed to hit the object again
            remainingTimeToAttack -= Time.deltaTime;
            while (remainingTimeToAttack <= 0)
            {
                // Take damage according to the same formula used in AOE2
                var finalAttackPointsPerSec = Math.Max(GetAttackStrength() - target.GetDefense(), 1);
                target.TakeDamage(finalAttackPointsPerSec);

                remainingTimeToAttack += 1.0f/GetAttackSpeed();
            }
        }
        else
        {
            // If we need to and the unit can't move, warn the user that the attack can't be done
            if (!CanMove())
            {
                Debug.Log(string.Format("{0} can't attack {1}, because it is {2}m away",
                    objectName, target.objectName, distanceToTarget));
                EndAttack();
                return;
            }

            // If necessary, move closer to the attack target. We also need to periodically
            // check if the target has moved and readjust our path accordingly
            if (!HasPath() || (HasPath() && (programmedAttackPosition - targetAttackPosition).magnitude > 10))
            {
                // Move to a point near the target to attack
                var attackPosition = targetAttackPosition - (targetAttackPosition -
                    transform.position).normalized * AttackRangeTolerance / 4;
                SetNewPath(attackPosition);
                
                programmedAttackPosition = attackPosition;
            }
        }
    }

    /// <summary>
    /// Begins the dead transition.
    /// </summary>
    private void BeginDead()
    {
        // Make sure that we don't attack while dying
        if (attacking)
        {
            EndAttack();
        }

        if (!dying)
        {
            // Begin the "dying" state
            dying = true;
            remainingTimeToDead = DefaultDeadTime;

            // Enable fade mode for each material, for the fade-out animation
            foreach (Renderer r in GetComponentsInChildren<Renderer>())
            {
                foreach (Material m in r.materials)
                {
                    // http://sassybot.com/blog/swapping-rendering-mode-in-unity-5-0/
                    m.SetFloat("_Mode", 2);
                    m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    m.EnableKeyword("_ALPHABLEND_ON");
                }
            }
        }
    }

    /// <summary>
    /// Checks if the required time between the unit having zero health points and the unit disappearing has elapsed.
    /// </summary>
    private bool UpdateDeadObject()
    {
        remainingTimeToDead -= Time.deltaTime;

        // Do a fade-out animation over the object
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            foreach (Material m in r.materials)
            {
                Color color = m.color;
                color.a = (remainingTimeToDead / DefaultDeadTime);
                m.color = color;
            }
        }

        // If the dead timer has elapsed, destroy the object
        if (remainingTimeToDead <= 0)
        {
            Destroy(gameObject);
            return true;
        }

        return false;
    }

    /// <summary>
    /// This method is called when the object is destroyed.
    /// In this case, we need to update the paths for A* to no longer consider our destroyed object.
    /// </summary>
    public void OnDestroy()
    {
        // Calculate the bounds of our object and tell A*Pathfinder to update that area
        Bounds bounds = new Bounds(transform.position, Vector3.zero);
        ExtendBounds(transform, ref bounds);
        if (AstarPath.active != null) // Can be null when the game is closed
        {
            AstarPath.active.UpdateGraphs(bounds);
        }
    }
}
