using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : RTSObject
{
	protected GameObject creationUnit = null;	// Objecte que indica la unitat a crear actual

	protected Vector3 spawnPoint;               // Punt de creacio de les unitats
	protected Queue<string> buildQueue;         // Cua de construccio del edifici

	private BoxCollider boxCollider;			// Referencia al component BoxCollider.
	  
	public bool needsBuilding = false;         // Indica si necesita se construit

	private static int layer1 = 11; 
	private static int layer2 = 10;
	private static int layermask1 = 1 << layer1;
	private static int layermask2 = 1 << layer2;
	private int mask = layermask1 | layermask2;

    public float visi = 60f;

    public GameObject finishedModel; // TODOXXX

    /*** Metodes per defecte de Unity ***/

    protected override void Awake()
    {
        base.Awake();
        spawnPoint = new Vector3(transform.position.x + 10, 0.0f, transform.position.z + 10);
        buildQueue = new Queue<string>();
        gameObject.layer = 10;
        // Calculem la dimensio del BoxCollider
        FittedBoxCollider();
		ent.Range = visi;

    }

    protected override void Update()
    {
        base.Update();
        ProcessBuildQueue();
    }

    protected override void OnGUI()
    {
        base.OnGUI();
        if (needsBuilding)
            DrawBuildProgress();
    }

    /*** Metodes publics ***/

    // Metode que obte el porcentatge de construccio actual
    public float getBuildPercentage()
    {
        return hitPoints / maxHitPoints;
    }

    // Metode que obte si esta en construccio
    public override bool CanBeBuilt()
    {
        return needsBuilding;
    }

    // Metode que va construint el edifici
    public void Construct(int amount)
    {
		hitPoints += amount;
        if (hitPoints >= maxHitPoints)
        {
            hitPoints = maxHitPoints;
            needsBuilding = false;
        }
    }

    public override void PerformAction(string actionToPerform)
    {
        base.PerformAction(actionToPerform);
        CreateUnit(actionToPerform);
    }

    protected virtual void CreateUnit(string unitName)
    {
        if (creationUnit != null)
        {
            bool spawned = false;
            int maximumSpawn = 5; //tenemos cinco intentos para instanciar una unidad. si no se instancian hay que mover las viejas 
            Vector3 point = spawnPoint;

            while (spawned == false && maximumSpawn > 0)
            {
                if (Physics.CheckSphere(point, 0.4f, mask))
                {
                    point = new Vector3(point.x - 5, 0.0f, point.z); //si ya hay algo provamos en otra posicion
                    Debug.Log("Habia algo en la posicion" + point);
                }
                else
                {
					spawned = true;
					GameObject unitClone = (GameObject)Instantiate(creationUnit, point, Quaternion.identity);
					unitClone.SetActive(false);
					float food = owner.GetResourceAmount (RTSObject.ResourceType.Food);
					if (food >= unitClone.GetComponent<Unit>().cost) {
						unitClone.SetActive(true);
						unitClone.GetComponent<RTSObject>().owner = owner;
						owner.resourceAmounts [RTSObject.ResourceType.Food] -= unitClone.GetComponent<Unit>().cost;
					}
					else
					{
						HUDInfo.message = "Not enough food (" + unitClone.GetComponent<Unit>().cost + ") to create a new " + unitClone.GetComponent<Unit>().name;
						Destroy(unitClone);
					}
                }
                maximumSpawn--;
            }
            creationUnit = null;
        }
    }

    // Metode per administrar el progres de construccio de la cua
    protected void ProcessBuildQueue()
    {
    }

    /*** Metodes privats ***/

    // Dibuixa el progres de construccio
    private void DrawBuildProgress()
    {
    }

    // Calcula el boxCollider del edifici
    private void FittedBoxCollider()
    {
        Transform transform = this.gameObject.transform;
        Quaternion rotation = transform.rotation;
        transform.rotation = Quaternion.identity;

        boxCollider = transform.GetComponent<BoxCollider>();

        if (boxCollider == null)
        {
            transform.gameObject.AddComponent<BoxCollider>();
            boxCollider = transform.GetComponent<BoxCollider>();
        }

        Bounds bounds = new Bounds(transform.position, Vector3.zero);

        ExtendBounds(transform, ref bounds);

		boxCollider.center = new Vector3((bounds.center.x - transform.position.x) / transform.localScale.x, (bounds.center.y - transform.position.y) / transform.localScale.y, (bounds.center.z - transform.position.z) / transform.localScale.z);
        boxCollider.size = new Vector3(bounds.size.x / transform.localScale.x, bounds.size.y / transform.localScale.y, bounds.size.z / transform.localScale.z);

        transform.rotation = rotation;
    }

}
