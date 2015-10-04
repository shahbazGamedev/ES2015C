using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RTSObject : MonoBehaviour {

    // Variables publiques
    public string objectName = "GenericObject";
    public int cost = 100, sellValue = 10, hitPoints = 100, maxHitPoints = 100;

    // Variables accessibles per a les subclasses
    protected Player player;
    protected string[] actions = { };
    protected bool currentlySelected = false;
    protected Rect playingArea = new Rect(0.0f, 0.0f, 0.0f, 0.0f);
    protected float healthPercentage = 1.0f;
    protected RTSObject target = null;
    protected bool attacking = false, movingIntoPosition = false;
    protected List<RTSObject> nearbyObjects;

    public int ObjectId { get; set; }

    protected virtual void Start () {
	
	}
    
    protected virtual void Update () {
	
	}

    protected virtual void OnGUI()
    {
    }



    /*** Metodes publics ***/

    public virtual void PerformAction(string actionToPerform)
    {
    }

    public virtual void MouseClick(GameObject hitObject, Vector3 hitPoint, Player controller)
    {
        Vector3 currentPosition = transform.position;
    }

    public bool IsOwnedBy(Player owner)
    {
        if (player && player.Equals(owner))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual bool CanAttack()
    {
        return false;
    }

    public virtual bool CanMove()
    {
        return false;
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0) Destroy(gameObject);
    }
}
