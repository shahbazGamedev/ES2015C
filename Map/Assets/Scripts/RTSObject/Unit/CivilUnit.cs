using UnityEngine;
using Pathfinding;
using System;
using System.Linq;


public class CivilUnit : Unit
{

    public float capacity, collectionAmount, depositAmount; // Dades sobre la recolecció
    public bool llegado = false;                            //he llegado a mi destino
    public int state;                                       //estado de recoleccion

    public bool harvesting = false;                         // Indicadors d'estat de la unitat

    /// <summary>
    /// true if we are in building location selection mode.
    /// </summary>
    public bool waitingForBuildingLocationSelection = false;
    protected Vector3 constructionPoint = Vector3.zero;
    protected GameObject creationBuilding = null;           // Objecte que anem a crear
    protected GameObject creationBuildingConstruction = null; //Edifici que anem a crear, en construccio
    /// <summary>
    /// Object used to show the preview to the user and detect overlaps and unbuildable places.
    /// </summary>
    protected GameObject creationCollisionDetectorObject;

    protected bool building = false;                        // true if the unit has a building project assigned
    protected Building currentProject = null;  				// Building actual de construccio

    //private float currentLoad = 0.0f, currentDeposit = 0.0f;    // Contadors en temps real de la recolecció
    private ResourceType harvestType;                       // Tipus de recolecció
    private Resource resourceDeposit;                       // Recurs de la recolecció
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
                // tot el que implica la recoleccio de recursos
                if(state==0){ //No hacer nada
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
		anim.SetBool ("IsFarming", harvesting && state == 2 && harvestType == ResourceType.Food);
		if(currentlySelected && harvesting && state == 2 && harvestType == ResourceType.Food && farmingSound && !audio.isPlaying)
		{
			audio.PlayOneShot (farmingSound);
		}
		anim.SetBool ("IsMining", harvesting && state == 2 && harvestType == ResourceType.Gold);
		if(currentlySelected && harvesting && state == 2 && harvestType == ResourceType.Gold && miningSound && !audio.isPlaying)
		{
			audio.PlayOneShot (miningSound);
		}
		anim.SetBool ("IsWoodCutting", harvesting && state == 2 && harvestType == ResourceType.Wood);
		if(currentlySelected && harvesting && state == 2 && harvestType == ResourceType.Wood && woodCuttingSound && !audio.isPlaying)
		{
			audio.PlayOneShot (woodCuttingSound);
		}
		anim.SetBool ("IsBuilding", building && currentProject && currentProject.CanBeBuilt() && currentProject.inConstruction);
		if(currentlySelected && building && currentProject && currentProject.CanBeBuilt() && currentProject.inConstruction && buildingSound && !audio.isPlaying)
		{
			audio.PlayOneShot (buildingSound);
		}
	}

    // Metode que crea el edifici
    /// <summary>
    /// Starts the building location selection sequence, where the user has to click
    /// on the map in order to select the place where the building should be built.
    /// </summary>
    /// <param name="creationBuildingPath">Name of the resource for the finished building.</param>
    /// <param name="creationBuildingConstructionPath">Name of the resource for the in-progress building</param>
    protected void StartBuildingLocationSelection(string creationBuildingPath, string creationBuildingConstructionPath)
    {
        // Destroy old collision detector object if any
        if (creationCollisionDetectorObject != null)
        {
            Destroy(creationCollisionDetectorObject);
        }

        // Load the complete building resource
        var creationBuildingTmp = Resources.Load<GameObject>(creationBuildingPath) as GameObject;
        if (creationBuildingTmp == null || creationBuildingTmp.GetComponent<Building>() == null)
        {
			HUDInfo.insertMessage("Could not load resource '" + creationBuildingPath + "' to start building location selection.");
            return;
        }

        // Load the in-construction building resource
        var creationBuildingConstructionTmp = Resources.Load<GameObject>(creationBuildingConstructionPath);
        if (creationBuildingConstructionTmp == null)
        {
			HUDInfo.insertMessage("Could not load resource '" + creationBuildingConstructionPath + "' to start building location selection.");
            return;
        }

        // Set up the unit state to work on a building
		HUDInfo.insertMessage("Select the site where you want to build the selected building " + creationBuildingTmp.name);
        waitingForBuildingLocationSelection = true;
        constructionPoint = Vector3.zero;
        creationBuilding = creationBuildingTmp;
        creationBuildingConstruction = creationBuildingConstructionTmp;

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
            creationBuildingConstruction = null;
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

        newProject.ReplaceChildWithChildFromGameObjectTemplate(creationBuildingConstruction);

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
        //ClosestPointOnBounds retorna el punto mas cercano del collider del objeto respecto al transform que le pasas
        var closestPointResourceStore = resourceStore.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
        SetNewPath(closestPointResourceStore, false);
        state = 1;
        
    }
    public void Recolectar(){
        Collect(resourceDeposit);
        if(collectionAmount >= capacity){ //Ir a Vaciar deposito
           state = 3;
        } 
    }
    public void IrRecolectar(){
        //ClosestPointOnBounds retorna el punto mas cercano del collider del objeto respecto al transform que le pasas
        var closestPointResource = resourceDeposit.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
        SetNewPath(closestPointResource, false); //mi objetivo ahora es target = recurso (RTSObject)
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
