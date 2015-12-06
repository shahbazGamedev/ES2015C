using UnityEngine;
using Pathfinding;
using System;
using System.Linq;


public class CivilUnit : Unit
{

	public float collectionAmount, capacity = 50.0f;		// Dades sobre la recolecció
	private int harvestingState;                            // Estat recoleccio, 1: recolectant, 2: deixant
	
	public bool harvesting = false;                         // Indicadors d'estat de la unitat
	public bool collecting = false;

    /// <summary>Buildings that can be created by this civil unit.</summary>
    protected RTSObjectType[] buildableBuildings = new RTSObjectType[0];

    /// <summary>
    /// true if we are in building location selection mode.
    /// </summary>
    public bool waitingForBuildingLocationSelection = false;
    protected Vector3 constructionPoint = Vector3.zero;
    protected GameObject creationBuilding = null;           // Objecte que anem a crear
    protected GameObject creationBuildingConstruction = null;
    /// <summary>
    /// 
    /// Object used to show the preview to the user and detect overlaps and unbuildable places.
    /// </summary>
    protected GameObject creationCollisionDetectorObject;

    public bool building = false;                        // true if the unit has a building project assigned
    protected Building currentProject = null;  				// Building actual de construccio
	
	private ResourceType harvestType = ResourceType.Unknown;    // Tipus de recolecció
	private string resourceTag = "";
    private Resource resourceDeposit = null;                    // Recurs de la recolecció
	private TownCenterBuilding resourceStore;				// Edifici on es deposita la recolecció
    private float amountBuilt = 0.0f;                       // Porcentatge de construcció feta
	//public int mask = 1024;								// 10000001 checks default and obstacles

	public AudioClip farmingSound;
	public AudioClip miningSound;
	public AudioClip woodCuttingSound;
	public AudioClip buildingSound;
	
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
        baseBuildFactor = 1.0f;
    }
	
    protected override void Update()
    {
        base.Update();

        if (!moving)
        {
            if (harvesting)
            {
				if (harvestingState == 1)
				{
					var closestPointResource = resourceDeposit.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
					var distanceToResource = (closestPointResource - transform.position).magnitude;
					if (distanceToResource <= 4)
					{
						Collect ();
					}
				}
				else if (harvestingState == 2)
				{
					var closestPointResourceStore = resourceStore.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
					var distanceToTownCenter = (closestPointResourceStore - transform.position).magnitude;
					if (distanceToTownCenter <= 4)
					{
						Deposit ();
					}
				}
            }
			else if (building)
			{
                if (currentProject == null)
                {
                    AssignBuildingProject(null);
                }
				else if (currentProject.CanBeBuilt()) //Si tenemos un proyecto y lo estamos construyendo 
				{
                    // Check that the building is close enough to the unit to build it
                    var closestPointInBuilding = currentProject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                    var distanceToBuilding = (closestPointInBuilding - transform.position).magnitude;

                    if (distanceToBuilding <= 5)
                    {
                        currentProject.Construct(Time.deltaTime * baseBuildFactor);
                    }
                    else
                    {
						HUDInfo.insertMessage("Civil unit can't reach the target location, deassigning building project.");
                        AssignBuildingProject(null);
                    }
				}
				else  // Si tenemos un proyecto y se ha acabado de construir
				{
					CreateFinishedBuilding();
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
    /// Check if the unit is currently harvesting.
    /// </summary>
    /// <returns>true if the unit is harvesting, false otherwise.</returns>
    public override bool IsHarvesting()
    {
        return harvesting;
    }

    /// <summary>
    /// Get the current type of resource that the unit is harvesting.
    /// </summary>
    /// <returns>The type of the resource being harvested.</returns>
    public override ResourceType GetHarvestType()
    {
        if (!harvesting)
            throw new InvalidOperationException("Called GetHarvestType when unit is not harvesting.");

        return harvestType;
    }

    /// <summary>
    /// Get the current amount of resource that the unit has harvested.
    /// </summary>
    /// <returns>The amount of resource that has been harvested.</returns>
    public override float GetHarvestAmount()
    {
        if (!harvesting)
            throw new InvalidOperationException("Called GetHarvestAmount when unit is not harvesting.");

        return collectionAmount;
    }

    public override bool IsBuilding()
    {
        return building;
    }

    public override void AssignBuildingProject(Building newProject)
    {
        building = (newProject != null);
        currentProject = newProject;

        // Move to unit to the object to build
        if (newProject != null)
        {
            SetNewPath(newProject.GetComponent<Collider>().ClosestPointOnBounds(transform.position), false);
        }
    }

    /*** Metodes interns accessibles per les subclasses ***/

	protected override void Animating ()
	{
		base.Animating ();
		anim.SetBool ("IsFarming", harvesting && collecting && harvestType == ResourceType.Food);
		if(currentlySelected && harvesting && collecting && harvestType == ResourceType.Food && farmingSound && !audio.isPlaying)
		{
			audio.PlayOneShot (farmingSound);
		}
		anim.SetBool ("IsMining", harvesting && collecting && harvestType == ResourceType.Gold);
		if(currentlySelected && harvesting && collecting && harvestType == ResourceType.Gold && miningSound && !audio.isPlaying)
		{
			audio.PlayOneShot (miningSound);
		}
		anim.SetBool ("IsWoodCutting", harvesting && collecting && harvestType == ResourceType.Wood);
		if(currentlySelected && harvesting && collecting && harvestType == ResourceType.Wood && woodCuttingSound && !audio.isPlaying)
		{
			audio.PlayOneShot (woodCuttingSound);
		}
		anim.SetBool ("IsBuilding", building && currentProject && currentProject.CanBeBuilt() && currentProject.inConstruction);
		if(currentlySelected && building && currentProject && currentProject.CanBeBuilt() && currentProject.inConstruction && buildingSound && !audio.isPlaying)
		{
			audio.PlayOneShot (buildingSound);
		}
	}

    public override Action[] GetActions()
    {
        return buildableBuildings.Select(type => new Action
        {
            Name = RTSObjectTypeExt.GetObjectName(type),
            Icon = RTSObjectFactory.GetObjectIconSprite(type, owner.civilization),
            CostResource = ResourceType.Wood,
            Cost = RTSObjectFactory.GetObjectCost(type, owner.civilization)
        }).ToArray();
    }

    public override void PerformAction(string actionToPerform)
    {
        base.PerformAction(actionToPerform);
        CreateBuilding(RTSObjectTypeExt.GetObjectTypeFromName(actionToPerform));
    }

    private void CreateBuilding(RTSObjectType objectType)
    {
        GameObject buildingPrefabTemplate = RTSObjectFactory.GetObjectTemplate(objectType, owner.civilization);
        if (buildingPrefabTemplate == null)
            return;

        StartBuildingLocationSelection(buildingPrefabTemplate);
    }

    // Metode que crea el edifici
    /// <summary>
    /// Starts the building location selection sequence, where the user has to click
    /// on the map in order to select the place where the building should be built.
    /// </summary>
    /// <param name="creationBuildingTmp">GameObject of the resource for the finished building.</param>
    private void StartBuildingLocationSelection(GameObject creationBuildingTmp)
    {
        // Destroy old collision detector object if any
        if (creationCollisionDetectorObject != null)
        {
            Destroy(creationCollisionDetectorObject);
        }

        // Set up the unit state to work on a building
		HUDInfo.insertMessage("Select the site where you want to build the selected building " + creationBuildingTmp.name);
        waitingForBuildingLocationSelection = true;
        constructionPoint = Vector3.zero;
        creationBuilding = creationBuildingTmp;

        // Create the building preview and overlap detector object
        creationCollisionDetectorObject = (GameObject)Instantiate(creationBuilding, Vector3.zero, Quaternion.identity);
        creationCollisionDetectorObject.name = creationCollisionDetectorObject.name + "_CollisionDetector";
        creationCollisionDetectorObject.AddComponent<BuildingOverlapDetector>();

        // Remove all components of the preview / overlap detector object except
        // - The transform, since this is basic and can't be removed
        // - The collider, because we need it to detect the overlaps with other objects
        // - The rigidbody, because otherwise, the object will be considered to have a 
        //   "static collider" and the physics engine will not compute calculation correctly
        //   after the object has moved
        foreach (Component component in creationCollisionDetectorObject.GetComponents<Component>())
        {
            if (!(component is Transform) && !(component is Collider) && !(component is Rigidbody))
            {
                Destroy(component);
            }
        }

        // Add our script to manage the preview object
        creationCollisionDetectorObject.AddComponent<BuildingOverlapDetector>();
    }

    /// <summary>
    /// Sets the location of the building on the building location selector to the current position.
    /// </summary>
    public void SetBuildingLocation()
    {
        if (creationCollisionDetectorObject.GetComponent<BuildingOverlapDetector>().IsBuildable)
        {
            constructionPoint = creationCollisionDetectorObject.transform.position;
            Destroy(creationCollisionDetectorObject); // To avoid collisions with the new object

            // Create the "on construction" building
            CreateOnConstructionBuilding();

            // Exit location selection mode
            waitingForBuildingLocationSelection = false;
            constructionPoint = Vector3.zero;
            creationBuilding = null;
            creationCollisionDetectorObject = null;
        }
        else
        {
			HUDInfo.insertMessage("The building cannot be placed in the selected location.");
        }
    }

    public void CreateOnConstructionBuilding()
    {
        // Initialize the object to build, so we can access its cost and colliders
        var creationBuildingConstructionProject =
            (GameObject)Instantiate(creationBuilding, constructionPoint, Quaternion.identity);

        // Check if there are enough resources available
        float woodAvailable = owner.GetResourceAmount(RTSObject.ResourceType.Wood);
        float woodCost = creationBuildingConstructionProject.GetComponent<Building>().cost;

        if (woodAvailable < woodCost)
        {
            Destroy(creationBuildingConstructionProject);
			HUDInfo.insertMessage(string.Format("Not enough wood ({0}) to construct the {1}",
                creationBuildingConstructionProject.GetComponent<Building>().cost,
                creationBuildingConstructionProject.GetComponent<Building>().objectName));

            return;
        }

        // Start the building project
        var newProject = creationBuildingConstructionProject.GetComponent<Building> ();
        newProject.hitPoints = 0;
        newProject.needsBuilding = true;
        newProject.owner = owner;
        newProject.finishedModel = creationBuilding;

        if (newProject.constructionModel != null)
        {
            newProject.changeModel("construction");
        }
        else
        {
            HUDInfo.insertMessage("WARNING: No on-construction model for building " + newProject.objectName + ".");
        }

        // Update physics
		var guo = new GraphUpdateObject (newProject.GetComponent<BoxCollider> ().bounds);
		guo.updatePhysics = true;
		AstarPath.active.UpdateGraphs (guo);

        // Substract resources needed for the building
		owner.resourceAmounts [RTSObject.ResourceType.Wood] -= newProject.cost;
        // Assign the building project to this unit
        AssignBuildingProject(newProject);
    }
	
	public void CreateFinishedBuilding()
	{
		/*
        currentProject.ReplaceChildWithChildFromGameObjectTemplate(currentProject.finishedModel);

        var guo = new GraphUpdateObject (currentProject.GetComponent<BoxCollider> ().bounds);
		guo.updatePhysics = true;
		AstarPath.active.UpdateGraphs (guo);*/
		currentProject.GetComponent<Building>().changeModel("finished");

        AssignBuildingProject(null);
	}

    /*** Metodes privats ***/


	// Metode que cridem per a començar a recolectar
	public void StartHarvest(Resource resource, string tag)
	{
		resourceDeposit = resource;
		if (resourceDeposit == null)
		{
			resourceDeposit = FindResource(tag);
		}
		resourceTag = resourceDeposit.tag;
		ResourceType newHarvestType = resourceDeposit.GetResourceType();
		// Si tenim en el civil ple o el nou recurs es diferent al anterior anem a Buidar
		if (collectionAmount >= 50.0f && harvestType == newHarvestType)
		{
			goToLeaving ();
		}
		else
		{
			if (harvestType != newHarvestType)
			{
				collectionAmount = 0.0f;
			}
			goToResourcing();
		}
		harvestType = newHarvestType;
		harvesting = true;
	}
	
	public void StopHarvest(){
		harvesting = false;
		collecting = false;
		resourceDeposit = null;
	}
	
	// Metode de recolecció
	private void Collect()
	{
		if (collectionAmount >= capacity)
		{
			collecting = false;
			collectionAmount = capacity;
			goToLeaving ();
		}
		else if (resourceDeposit && !resourceDeposit.isEmpty ())
		{
			collecting = true;
			resourceDeposit.Remove (5 * Time.deltaTime);
			collectionAmount += (5 * Time.deltaTime);
		}
		else
		{
			StopHarvest();
			StartHarvest(null, resourceTag);
		}
	}
	
	// Metode per depositar els recursos al edifici towncenter i que es sumi al jugador
	private void Deposit()
	{
		owner.resourceAmounts[harvestType] += collectionAmount;
		collectionAmount = 0.0f;
		goToResourcing();
	}
	
	private void goToResourcing(){
		var closestPointResource = resourceDeposit.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
		SetNewPath(closestPointResource, false);
		harvestingState = 1;
	}
	
	private void goToLeaving(){
		if (resourceStore == null)
		{
			resourceStore = findTownCenter();
		}
		var closestPointResourceStore = resourceStore.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
		SetNewPath(closestPointResourceStore, false);
		harvestingState = 2;
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
			if (go.GetComponent<TownCenterBuilding>() && go.GetComponent<TownCenterBuilding>().owner == owner && curDistance < distance)
			{
				closest = go;
				distance = curDistance;
			}
		}
		return closest.GetComponent<TownCenterBuilding>();
	}
	
	
	private Resource FindResource(string tag)
	{
		GameObject[] centers;
		centers = GameObject.FindGameObjectsWithTag(tag);
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in centers)
		{
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (go.GetComponent<Resource>() && go.GetComponent<Resource>().amountLeft > 1 && curDistance < distance)
			{
				closest = go;
				distance = curDistance;
			}
		}
		return closest.GetComponent<Resource>();
	}



    public void CreateBuildingIA(GameObject building, Vector3 coords)
    {
            creationBuilding = (GameObject)Instantiate(building, coords, Quaternion.identity);
            creationBuildingConstruction = (GameObject)Instantiate(building, coords, Quaternion.identity);
            creationBuildingConstruction.SetActive(false);
            float wood = owner.GetResourceAmount(RTSObject.ResourceType.Wood);
            if (wood >= creationBuildingConstruction.GetComponent<Building>().cost)
            {
                Debug.Log("Tenemos suficiente madera");
                creationBuildingConstruction.SetActive(true);
                currentProject = creationBuildingConstruction.GetComponent<Building>();
                currentProject.hitPoints = 0;
                currentProject.needsBuilding = true;
                currentProject.owner = owner;
                var guo = new GraphUpdateObject(currentProject.GetComponent<BoxCollider>().bounds);
                guo.updatePhysics = true;
                AstarPath.active.UpdateGraphs(guo);
                owner.resourceAmounts[RTSObject.ResourceType.Wood] -= currentProject.cost;
                SetNewPath(coords,false);                    
        }
    }






}
