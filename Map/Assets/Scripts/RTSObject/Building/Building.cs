using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using System;
using System.Linq;

public class Building : RTSObject
{
	/// <summary>Time required to spawn a new unit.</summary>
	const float unitSpawnTime = 3.0f;

	/// <summary>GameObject for the unit that is currently in the building queue. null if not spawning units.</summary>
	private GameObject unitQueue = null;
	/// <summary>Time, in seconds, until the next unit is spawned from the building.</summary>
	private float timeToNextSpawn = 0.0f;
	/// <summary>Object used to display the progress of the building queue's spawn.</summary>
	private GameObject spawnProgressObject = null;

	/// <summary>Units that can be spawned from this building.</summary>
	protected RTSObjectType[] spawnableUnits = new RTSObjectType[0];
	protected Vector3 spawnPoint;               // Punt de creacio de les unitats
	protected Queue<string> buildQueue;         // Cua de construccio del edifici

	private BoxCollider boxCollider;			// Referencia al component BoxCollider.
	  
	public bool needsBuilding = false;         	// Indica si necesita ser construit
	public bool inConstruction = false;			// Indica si s'esta construint ara mateix
	private bool demolished = false;
	private static int layer1 = 11;
	private static int layer2 = 10;
	private static int layermask1 = 1 << layer1;
	private static int layermask2 = 1 << layer2;
	private int mask = layermask1 | layermask2;
	public float visi = 60f;

	/// <summary>For in-construction buildings, reference to the finished building model.</summary>
	public GameObject finishedModel;
	public GameObject constructionModel;
	public GameObject demolishedModel;
	/// <summary>Number of seconds that this building will take to be built at the default building factor.</summary>
	public float buildingTime = 5.0f;
	/// <summary>While building, number of "partial" hit points, to build at a consistent rate.</summary>
	private float fractionalHitPoints = 0.0f;

	// Para el clip
	


	/*** Metodes per defecte de Unity ***/

	protected override void Awake ()
	{
		base.Awake ();
		spawnPoint = new Vector3 (transform.position.x + 10, 0.0f, transform.position.z + 10);
		buildQueue = new Queue<string> ();
		gameObject.layer = 10;
		// Calculem la dimensio del BoxCollider
		FittedBoxCollider ();
		ent.Range = visi;
	}

	protected override void Update ()
	{
		base.Update ();
		ProcessBuildQueue ();
		
		if (!inConstruction && hitPoints < maxHitPoints) {
			
		}
	}

	protected override void OnGUI ()
	{
		base.OnGUI ();
		if (needsBuilding)
			DrawBuildProgress ();
	}

	/*** Metodes publics ***/

	// Metode que obte el porcentatge de construccio actual
	public float getBuildPercentage ()
	{
		return hitPoints / maxHitPoints;
	}

	// Metode que obte si esta en construccio
	public override bool CanBeBuilt ()
	{
		return needsBuilding;
	}

	// Metode que va construint el edifici
	public void Construct (float timeFactor)
	{
		inConstruction = true;
		// Increment the number of hit point, taking into account the fractional hit points
		float newHitPoints = hitPoints + fractionalHitPoints + (maxHitPoints / buildingTime) * timeFactor;
		// Split again into whole and fractional hit points
		hitPoints = (int)newHitPoints;
		fractionalHitPoints = newHitPoints - (int)newHitPoints;

		if (hitPoints >= maxHitPoints) { // Building is completed?
			// Clamp hit points at maximum
			hitPoints = maxHitPoints;
			fractionalHitPoints = 0;

			// Mark as completed
			needsBuilding = false;
			inConstruction = false;
			demolished = false;
			if (tag == "civilHouse") {
				owner.maxPopulation += 5;
			}
		}
	}

	public override Action[] GetActions ()
	{
		if (CanBeBuilt ())
			return new Action[0];

		return spawnableUnits.Select (type => new Action
        {
            Name = RTSObjectTypeExt.GetObjectName(type),
            Icon = RTSObjectFactory.GetObjectIconSprite(type, owner.civilization),
            CostResource = ResourceType.Food,
            Cost = RTSObjectFactory.GetObjectCost(type, owner.civilization)
        }).ToArray ();
	}

	public override void PerformAction (string actionToPerform)
	{
		base.PerformAction (actionToPerform);
		CreateUnit (RTSObjectTypeExt.GetObjectTypeFromName (actionToPerform));
	}

	private void CreateUnit (RTSObjectType objectType)
	{
		GameObject creationUnit = RTSObjectFactory.GetObjectTemplate (objectType, owner.civilization);
		if (creationUnit == null)
			return;

		AddUnitToCreationQueue (creationUnit);
	}

	private void AddUnitToCreationQueue (GameObject creationUnit)
	{
		// Sanity check
		if (creationUnit == null)
			throw new ArgumentNullException ("creationUnit");

		if ((owner.population + 1) > owner.maxPopulation) {
			HUDInfo.insertMessage ("Not enough capacity of population to create a new unit, please create a Civil House to increase it");
			return;
		}

		// Check if the queue is full (currently units can't be queued, only one unit is supported at a time)
		if (unitQueue != null) {
			HUDInfo.insertMessage ("A unit is already in the queue. Please wait " + 
				Math.Round (timeToNextSpawn, 1) + " seconds for the spawn.");
			return;
		}

		// Add the unit to the queue. The rest will be done from Update().
		unitQueue = creationUnit;
		timeToNextSpawn = unitSpawnTime;
		var spawnProgressObjectPrefab = Resources.Load<GameObject> ("BuildingUnitSpawnProgressBar");
		spawnProgressObject = Instantiate (spawnProgressObjectPrefab);
		spawnProgressObject.transform.parent = transform;
		spawnProgressObject.transform.localPosition = new Vector3 (0.0f,
            GetComponent<BoxCollider> ().size.y + 5.0f / transform.localScale.y, 0.0f);
	}

	/// <summary>
	/// Updates the building's unit spawn queue status.
	/// </summary>
	private void ProcessBuildQueue ()
	{
		// If there are no units in the queue, we're done!
		if (unitQueue == null)
			return;

		// Update time until next unit is spawned
		timeToNextSpawn = Math.Max (timeToNextSpawn - Time.deltaTime, 0.0f);
		if (timeToNextSpawn != 0.0f) { // Not spawning yet
			spawnProgressObject.GetComponent<BuildingUnitSpawnProgressBar> ()
                .UpdateProgressBar (1.0f - timeToNextSpawn / unitSpawnTime);
			return;
		}

		// Remove the unit from the building's creation queue
		var unitToSpawn = unitQueue;
		unitQueue = null;
		Destroy (spawnProgressObject);

		// Figure out spawn point
		Vector3 actualSpawnPoint = GetNextSpawnPoint ();
		if (actualSpawnPoint == Vector3.zero) {
			HUDInfo.insertMessage ("Building spawn space is full, please move units to create new ones.");
			return;
		}

		// Create the unit itself
		GameObject unitClone = (GameObject)Instantiate (unitToSpawn, actualSpawnPoint, Quaternion.identity);
		float food = owner.GetResourceAmount (RTSObject.ResourceType.Food);
		if (food >= unitClone.GetComponent<Unit> ().cost) {
			unitClone.GetComponent<RTSObject> ().owner = owner;
			owner.resourceAmounts [RTSObject.ResourceType.Food] -= unitClone.GetComponent<Unit> ().cost;
			owner.population++;
			makeCreationSound ();
		} else {
			HUDInfo.insertMessage ("Not enough food (" + unitClone.GetComponent<Unit> ().cost + ") to create a new " + unitClone.GetComponent<Unit> ().name);
			Destroy (unitClone);
		}
	}

	/// <summary>
	/// Get the next available spawn point for an unit in the max.
	/// </summary>
	/// <returns>The position of the next spawn point for this building, or Vector3.zero if no point is available.</returns>
	private Vector3 GetNextSpawnPoint ()
	{
		// Spawn attempts before considering the unit cannot spawn the units
		const int maximumSpawnAttempts = 5;

		return Enumerable.Range (0, maximumSpawnAttempts)
            .Select (n => new Vector3 (spawnPoint.x - 5.0f * n, spawnPoint.y, spawnPoint.z))
            .FirstOrDefault (sp => !Physics.CheckSphere (sp, 0.4f, mask));
	}

	/*** Metodes privats ***/

	// Dibuixa el progres de construccio
	private void DrawBuildProgress ()
	{
	}

	// Calcula el boxCollider del edifici
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

		boxCollider.center = new Vector3 ((bounds.center.x - transform.position.x) / transform.localScale.x, (bounds.center.y - transform.position.y) / transform.localScale.y, (bounds.center.z - transform.position.z) / transform.localScale.z);
		boxCollider.size = new Vector3 (bounds.size.x / transform.localScale.x, bounds.size.y / transform.localScale.y, bounds.size.z / transform.localScale.z);

		transform.rotation = rotation;
	}
	
	public void changeModel (string estat)
	{



		if (estat == "finished") {
			ReplaceChildWithChildFromGameObjectTemplate (finishedModel);
			AudioClip normal = Resources.Load ("Sounds/Building/Ta_Da") as AudioClip;
			audio.PlayOneShot (normal);
		} else if (estat == "demolished") {
			//var exp = demolishedModel.GetComponent<ParticleSystem>();
			ReplaceChildWithChildFromGameObjectTemplate (demolishedModel);
			AudioClip semi = Resources.Load ("Sounds/Building/Pickaxe" ) as AudioClip;
			audio.PlayOneShot (semi);
			//exp.Play();
			//Destroy(gameObject, exp.duration);
		} else if (estat == "construction") {
			ReplaceChildWithChildFromGameObjectTemplate (constructionModel);
			AudioClip onconst = Resources.Load ("Sounds/Building/Fire_Burning") as AudioClip;
			audio.PlayOneShot (onconst);
		}

		var guo = new GraphUpdateObject (this.GetComponent<BoxCollider> ().bounds);
		guo.updatePhysics = true;
		AstarPath.active.UpdateGraphs (guo);		
	}
	
	public void getModels (string fModel, string cModel, string dModel)
	{
		Debug.Log ("get prefabs of Building");
		finishedModel = Resources.Load<GameObject> (fModel) as GameObject;
		constructionModel = Resources.Load<GameObject> (cModel) as GameObject;
		demolishedModel = Resources.Load<GameObject> (dModel) as GameObject;
		
		if (finishedModel == null || finishedModel.GetComponent<Building> () == null) {
			Debug.Log ("Could not load resource '" + fModel);
		}
		
		if (constructionModel == null) {
			Debug.Log ("Could not load resource '" + cModel);
		}
		
		if (demolishedModel == null) {
			Debug.Log ("Could not load resource '" + dModel);
		}
	}
	
	public override void TakeDamage (int damage)
	{
		base.TakeDamage (damage);
		if (hitPoints > 0 && hitPoints < maxHitPoints && !demolished) {
			changeModel ("demolished");
			demolished = true;
		}
	}


    public virtual void CreateUnitAI(GameObject unit)
    {
        if (unit != null)
        {
            bool spawned = false;
            int maximumSpawn = 5; //tenemos cinco intentos para instanciar una unidad. si no se instancian hay que mover las viejas 
            Vector3 point = spawnPoint;

            while (spawned == false && maximumSpawn > 0)
            {
                if (Physics.CheckSphere(point, 0.4f, mask))
                {
                    point = new Vector3(point.x - 5, 0.0f, point.z); //si ya hay algo provamos en otra posicion
                }
                else
                {
                    spawned = true;
                    GameObject unitClone = (GameObject)Instantiate(unit, point, Quaternion.identity);
                    unitClone.SetActive(false);
                    float food = owner.GetResourceAmount(RTSObject.ResourceType.Food);
                    if (food >= unitClone.GetComponent<Unit>().cost)
                    {
                        unitClone.SetActive(true);
                        unitClone.GetComponent<RTSObject>().owner = GameObject.Find("EnemyPlayer1").GetComponent<Player>(); 
                        owner.resourceAmounts[RTSObject.ResourceType.Food] -= unitClone.GetComponent<Unit>().cost;
                    }
                    else
                    {
                        HUDInfo.insertMessage("Not enough food (" + unitClone.GetComponent<Unit>().cost + ") to create a new " + unitClone.GetComponent<Unit>().name);
                        Destroy(unitClone);
                    }
                }
                maximumSpawn--;
            }
            unit = null;
        }
    }
}
