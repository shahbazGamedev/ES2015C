using UnityEngine;
using System.Collections;

public class Resource : RTSObject {

    public float capacity;                          // Capacitat del recurs

    protected float amountLeft;                     // Indica la cantitat que queda del recurs
    protected ResourceType resourceType;            // Indica el tipus de recurs

    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
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
}
