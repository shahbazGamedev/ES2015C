using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	/// Tells if the object can attack
	/// </summary>
	/// <returns>Boolean saying if the object can attack or not.</returns>
    public virtual bool CanAttack()
    {
        return false;
    }

	/// <summary>
	/// Tells if the object can move
	/// </summary>
	/// <returns>Boolean saying if the object can move or not.</returns>
    public virtual bool CanMove()
    {
        return false;
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

    // Metode que inicialitza el atac a un objecte
    private void BeginAttack(RTSObject target)
    {
        this.target = target;
        attacking = true;
        PerformAttack();
    }

    // Metode que realitza el atac
    private void PerformAttack()
    {
        if (!target)
        {
            attacking = false;
            return;
        }
    }
}
