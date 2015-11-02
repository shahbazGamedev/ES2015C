using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : RTSObject
{
	protected float maxBuildProgress = 10.0f;	// Maxim progres de construccio
	protected GameObject creationUnit = null;	// Objecte que indica la unitat a crear actual

	protected Vector3 spawnPoint;               // Punt de creacio de les unitats
	protected Queue<string> buildQueue;         // Cua de construccio del edifici

	private BoxCollider boxCollider;			// Referencia al component BoxCollider.
	  
	public bool needsBuilding = false;         // Indica si necesita se construit

	private static int layer1 = 11; 
	private static int layer2 = 10;
	private static int layermask1 = 1 << layer1;
	private static int layermask2 = 1 << layer2;
	protected int mask = layermask1 | layermask2;


	/*** Metodes per defecte de Unity ***/

	protected override void Awake ()
	{
		base.Awake ();

		spawnPoint = new Vector3 (transform.position.x + 10, 0.0f, transform.position.z + 10);
		buildQueue = new Queue<string> ();
		gameObject.layer = 10;
		// Calculem la dimensio del BoxCollider
		FittedBoxCollider();
		needsBuilding=true;
		currentBuildProgress = 0.0f; // Progres actual de la construccio
	}

	protected override void Start ()
	{
		base.Start ();
	}

	protected override void Update ()
	{
		base.Update ();
		ProcessBuildQueue ();
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
		return currentBuildProgress / maxBuildProgress;
	}

	// Metode que obte si esta en construccio
	public bool UnderConstruction ()
	{
		return needsBuilding;
	}

	// Metode que va construint el edifici
	public void Construct (int amount)
	{
		hitPoints = hitPoints + amount;
		if (hitPoints >= maxHitPoints) {
			hitPoints = maxHitPoints;
			needsBuilding = false;
		}
	}
	
	public override void PerformAction(string actionToPerform)
	{
		base.PerformAction(actionToPerform);
		CreateUnit(actionToPerform);
	}


	/*** Metodes interns accessibles per les subclasses ***/

	// Metode per crear unitats
	protected virtual void CreateUnit (string unitName)
	{
		if (creationUnit != null) {
			bool spawned = false;
			int maximumSpawn = 5; //tenemos cinco intentos para instanciar una unidad. si no se instancian hay que mover las viejas 
			Vector3 point = spawnPoint; 
		
			while (spawned == false && maximumSpawn>0) {
				if (Physics.CheckSphere (point, 0.4f, mask)) {
					point = new Vector3 (point.x - 5, 0.0f, point.z); //si ya hay algo provamos en otra posicion
					Debug.Log("Habia algo en la posicion"+point);
				} else {
					spawned = true;
					float food = owner.GetResourceAmount (RTSObject.ResourceType.Food);
					if (food >= 20) {
						GameObject unitClone = (GameObject)Instantiate (creationUnit, point, Quaternion.identity);
						unitClone.GetComponent<RTSObject> ().owner = owner;
						owner.resourceAmounts [RTSObject.ResourceType.Food] = food - 20;
					} else {
						Debug.Log ("Not enough food");
					}
				}
				maximumSpawn--;
			}
			creationUnit = null;
		}
	}

	// Metode per administrar el progres de construccio de la cua
	protected void ProcessBuildQueue ()
	{
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
		
		boxCollider.center = bounds.center - transform.position;
		boxCollider.size = new Vector3 (bounds.size.x / transform.localScale.x, bounds.size.y / transform.localScale.y, bounds.size.z / transform.localScale.z);
		
		transform.rotation = rotation;
	}
}
