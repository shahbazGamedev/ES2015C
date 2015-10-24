using UnityEngine;
using System.Collections;

public class Resource : RTSObject {

    public float capacity;                          // Capacitat del recurs

    protected float amountLeft;                     // Indica la cantitat que queda del recurs
    protected ResourceType resourceType;            // Indica el tipus de recurs

	private BoxCollider boxCollider;			// Referencia al component BoxCollider.

    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();

		FittedBoxCollider ();

        resourceType = ResourceType.Unknown;
        amountLeft = capacity;
    }

    /*** Metodes publics ***/

    // Metode per extreure una quantitat al recurs
    public void Remove(float amount)
    {
        amountLeft -= amount;
        if (amountLeft < 0) amountLeft = 0;
    }

    // Metode per obtenir si el recurs es buit o no
    public bool isEmpty()
    {
        return amountLeft <= 0;
    }

    // Metode per obtenir el tipus de recurs
    public ResourceType GetResourceType()
    {
        return resourceType;
    }

    // Metode per calcular la vida actual del recurs
    protected override void CalculateCurrentHealth()
    {
        healthPercentage = amountLeft / capacity;
    }

	/*** Metodes privats ***/

	// Calcula el boxCollider del recurs
	private void FittedBoxCollider ()
	{
		Transform transform = this.gameObject.transform;
		Quaternion rotation = transform.rotation;
		transform.rotation = Quaternion.identity;
		
		boxCollider = transform.GetComponent<BoxCollider> ();
		
		if (boxCollider == null) {
			transform.gameObject.AddComponent<BoxCollider> ();
			boxCollider = transform.GetComponent<BoxCollider> ();
		}
		
		Bounds bounds = new Bounds (transform.position, Vector3.zero);
		
		ExtendBounds (transform, ref bounds);
		
		boxCollider.center = bounds.center - transform.position;
		boxCollider.size = new Vector3 (bounds.size.x / transform.localScale.x, bounds.size.y / transform.localScale.y, bounds.size.z / transform.localScale.z);
		
		transform.rotation = rotation;
	}
}
