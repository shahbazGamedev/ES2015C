﻿using UnityEngine;
using Pathfinding;
using System;
using System.Linq;


public class CivilUnit : Unit
{

    public float capacity, collectionAmount, depositAmount; // Dades sobre la recolecció
    public bool llegado = false;                            //he llegado a mi destino
    public int state;                                       //estado de recoleccion

	public Vector3 constructionPoint = Vector3.zero;		// Posicio on crear el edifici
	public Building currentProject = null;  				// Building actual de construccio
	protected GameObject creationBuilding = null;			// Objecte que anem a crear
	protected GameObject creationBuildingConstruction = null; //Edifici que anem a crear, en construccio

    public bool harvesting = false;      					// Indicadors d'estat de la unitat
	public bool building = false;

    //private float currentLoad = 0.0f, currentDeposit = 0.0f;    // Contadors en temps real de la recolecció
    private ResourceType harvestType;                       // Tipus de recolecció
    private Resource resourceDeposit;                       // Recurs de la recolecció
	private TownCenterBuilding resourceStore;				// Edifici on es deposita la recolecció
    private float amountBuilt = 0.0f;                       // Porcentatge de construcció feta
	//public int mask = 1024;								// 10000001 checks default and obstacles

	                      
	
	private static int layer1 = 0;
	private static int layer2 = 10;
	private static int layermask1 = 1 << layer1;
	private static int layermask2 = 1 << layer2;
	private int finalmask = layermask1 | layermask2;
	
    /*** Metodes per defecte de Unity ***/

    /* CODI COMENTAT - NO FA RES I DONA PROBLEMES AL INICIALITZAR (MERGE 03/11/2015, COMENTAT PER JOAN BRUGUERA)

    public GameObject o;

    public CivilUnit(string asset, Vector3 coords) {
       o = Instantiate(Resources.Load(asset), coords, Quaternion.identity) as GameObject;
    }
    */

	protected override void Awake()
    {
		base.Awake();
        objectName = "Civil Unit";
		gameObject.tag = "civil";
        capacity = 50;
    }
	
    protected override void Update()
    {
        base.Update();
        if (!moving)
        {
            if (harvesting)
            {
                // tot el que implica la recoleccio de recursos
                if(state == 0){ //No hacer nada
                    //Idle();
                }
                if(state == 1){ //Vaciar en el almacen
                    Vaciar();
                }
                if(state == 2){ //Recolectar el recurso
                    Recolectar();
                }
                if(state == 3){ //Ir hacia el almacen
                    IrVaciar();
                }
                if(state == 4){ //Ir hacia el recurso
                    IrRecolectar();
                } 
            }
			else if (building)
			{
				if (currentProject && currentProject.UnderConstruction()) //Si tenemos un proyecto y lo estamos construyendo 
				{
					currentProject.Construct(baseBuildSpeed);
				}
				else if (currentProject && currentProject.UnderConstruction() == false) //Si tenemos un proyecto y se ha acabado de construir
				{
					Destroy(currentProject.gameObject);
					Debug.Log("Destruimos el edificio en construccion");
					currentProject=null;
					building=false;
					CreateFinishedBuilding();
				}
				else if (creationBuildingConstruction != null)
				{
					if (constructionPoint != Vector3.zero)
					{
						CreateBuilding ();
					}
				}
				else
				{
					currentProject = null;
					building = false;
				}
			}
        }
    }

    protected override void OnGUI()
    {
        base.OnGUI();
    }

    /*** Metodes publics ***/

    /// <summary>
    /// Get the current type of resource that the unit is harvesting. If the unit is not harvesting any resource, returns null.
    /// </summary>
    /// <returns>The type of the resource being harvested, or null.</returns>
    public ResourceType? GetHarvestType()
    {
        // TODO: Implement this method properly
        return harvestType;
    }

    /// <summary>
    /// Get the current amount of resource that the unit has harvested. If the unit is not harvesting any resource, returns null.
    /// </summary>
    /// <returns>The amount of resource that has been harvested, or null.</returns>
    public float? GetHarvestAmount()
    {
        // TODO: Implement this method properly
        return collectionAmount;
    }

    /*** Metodes interns accessibles per les subclasses ***/

    // Metode que crea el edifici
    /// <summary>
    /// Starts the building location selection sequence, where the user has to click
    /// on the map in order to select the place where the building should be built.
    /// </summary>
    /// <param name="creationBuildingResource">Name of the resource for the finished building.</param>
    /// <param name="creationBuildingConstructionResource">Name of the resource for the in-progress building</param>
    protected void StartBuildingLocationSelection(string creationBuildingResource, string creationBuildingConstructionResource)
    {
        // If the unit is already working on another building, don't start, because that
        // would corrupt our internal state (creationBuilding, etc.)
        if (building)
        {
            HUDInfo.message = "This unit is already working on another building.";
            return;
        }

        // Load the complete building resource
        var creationBuildingTmp = Resources.Load<GameObject>(creationBuildingResource) as GameObject;
        if (creationBuildingTmp == null || creationBuildingTmp.GetComponent<Building>() == null)
        {
            Debug.LogError("Could not load resource '" + creationBuildingResource + "' to start building location selection.");
            return;
        }

        // Load the in-construction building resource
        var creationBuildingConstructionTmp = Resources.Load<GameObject>(creationBuildingConstructionResource);
        if (creationBuildingConstructionTmp == null || creationBuildingConstructionTmp.GetComponent<Building>() == null)
        {
            Debug.LogError("Could not load resource '" + creationBuildingResource + "' to start building location selection.");
            return;
        }

        // Set up the unit state to work on a building
        HUDInfo.message = "Select the site where you want to build the building";
        building = true;
        constructionPoint = Vector3.zero;
        currentProject = null;
        creationBuilding = creationBuildingTmp;
        creationBuildingConstruction = creationBuildingConstructionTmp;
    }

    public void CreateBuilding()
    {
        // Initialize the object to build, so we can access its cost and colliders
        var creationBuildingConstructionProject =
            (GameObject)Instantiate(creationBuildingConstruction, constructionPoint, Quaternion.identity);

        // Check if there are enough resources available
        float woodAvailable = owner.GetResourceAmount(RTSObject.ResourceType.Wood);
        float woodCost = creationBuildingConstructionProject.GetComponent<Building>().cost;

        if (woodAvailable < woodCost)
        {
            Destroy(creationBuildingConstructionProject);
            HUDInfo.message = string.Format("Not enough wood ({0}) to construct the {1}",
                creationBuildingConstructionProject.GetComponent<Building>().cost,
                creationBuildingConstructionProject.GetComponent<Building>().objectName);

            building = false;
            constructionPoint = Vector3.zero;
            currentProject = null;
            creationBuilding = null;
            creationBuildingConstruction = null;
            return;
        }
        Debug.Log("Tenemos suficiente madera");

        // Verify that the building is not overlapping another building
        // To do this, since Unity doesn't offer us much help, we do the following:
        // We calculate a bounding sphere of the building and find nearby colliders.
        // For those which correspond to buildings (which will be BoxColliders), we
        // check for collissions one by one to check if there is any overlap
        var buildingBoxCollider = creationBuildingConstructionProject.GetComponent<BoxCollider>();
        var tentativeSphereCenter = buildingBoxCollider.bounds.center;
        var tentativeSphereRadius = Math.Max(buildingBoxCollider.bounds.extents.x,
            Math.Max(buildingBoxCollider.bounds.extents.y, buildingBoxCollider.bounds.extents.z));
        var potentialColliders = Physics.OverlapSphere(tentativeSphereCenter, tentativeSphereRadius);
        foreach (BoxCollider otherCollider in potentialColliders.Where(
            c => c != buildingBoxCollider && c.gameObject.GetComponent<Building>() != null).Cast<BoxCollider>())
        {
            if (buildingBoxCollider.bounds.Intersects(otherCollider.bounds))
            {
                Destroy(creationBuildingConstructionProject);
                HUDInfo.message = "We can not build because there are other buildings nearby";

                constructionPoint = Vector3.zero;
                return;
            }
        }

        // Start the building project
        currentProject = creationBuildingConstructionProject.GetComponent<Building> ();
		currentProject.hitPoints = 0;
		currentProject.needsBuilding = true;
		currentProject.owner = owner;

		var guo = new GraphUpdateObject (currentProject.GetComponent<BoxCollider> ().bounds);
		guo.updatePhysics = true;
		AstarPath.active.UpdateGraphs (guo);

		owner.resourceAmounts [RTSObject.ResourceType.Wood] -= currentProject.cost;
		SetNewPath(constructionPoint);
    }
	
	public void CreateFinishedBuilding()
	{
		creationBuilding = (GameObject)Instantiate (creationBuilding, constructionPoint, Quaternion.identity);
		currentProject = creationBuilding.GetComponent<Building> ();
		currentProject.owner = owner;
		var guo = new GraphUpdateObject (currentProject.GetComponent<BoxCollider> ().bounds);
		guo.updatePhysics = true;
		AstarPath.active.UpdateGraphs (guo);
		SetNewPath(constructionPoint);		
		constructionPoint = Vector3.zero;
		creationBuilding = null;
		currentProject=null;
		creationBuildingConstruction = null;
		constructionPoint = Vector3.zero;
	}

    /*** Metodes privats ***/

    // Metode que cridem per a començar a recolectar
    public void StartHarvest(Resource resource)
    {
        resourceDeposit = resource;
        harvestType = resourceDeposit.GetResourceType();
        harvesting = true;
        state = 4;
    }

    // Metode de recolecció
    public void Collect(Resource resourceDeposit)
    {
        //GetComponent<Animation>().Play (attack.name); 
        if(!resourceDeposit.isEmpty()){
            resourceDeposit.Remove(Mathf.Round(5*Time.deltaTime));    //resta esto del arbol (ej)
            collectionAmount += Mathf.Round(5*Time.deltaTime); //se lo suma al recolector
        }
    }

    // Metode per depositar els recursos al edifici resourceStore
    public void Deposit(Resource resourceDeposit)
    {
		//vaciarme
		owner.resourceAmounts[harvestType] += collectionAmount;
        collectionAmount = 0;
    }

    public void Vaciar(){
        Deposit(resourceDeposit);
        state = 4; //como ya he vaciado, vuelvo al recurso
    }
    public void IrVaciar(){
		if (resourceStore == null){
			resourceStore = findTownCenter();
		}
        SetNewPath(resourceStore.transform.position);
        state = 1;
    }
    public void Recolectar(){
        Collect(resourceDeposit);
        if(collectionAmount >= capacity){ //Ir a Vaciar deposito
           state = 3;
        } 
    }
    public void IrRecolectar(){
        SetNewPath(resourceDeposit.transform.position); //mi objetivo ahora es target = recurso (RTSObject)
        state = 2;
    }

	private TownCenterBuilding findTownCenter(){
        GameObject[] centers;
		centers = GameObject.FindGameObjectsWithTag("townCenter");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in centers){
            Vector3 diff = go.transform.position - position;
            float curDistance =  diff.sqrMagnitude;
            if(curDistance < distance){
                closest = go;
                distance = curDistance;
            }
        }
        return closest.GetComponent<TownCenterBuilding>();
    }

}
