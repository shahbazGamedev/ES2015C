using UnityEngine;
using Pathfinding;

public class CivilUnit : Unit
{

    public float capacity, collectionAmount, depositAmount; // Dades sobre la recolecció
    public Building resourceStore;                          // Edifici on es deposita la recolecció
    public int buildSpeed;                                  // Velocitat de construcció

    private bool harvesting = false, emptying = false, building = false;    // Indicadors d'estat de la unitat
    private float currentLoad = 0.0f, currentDeposit = 0.0f;    // Contadors en temps real de la recolecció
    private ResourceType harvestType;                       // Tipus de recolecció
    private Resource resourceDeposit;                       // Recurs de la recolecció
    private Building currentProject;                        // Edifici actual en construcció
    private float amountBuilt = 0.0f;                       // Porcentatge de construcció feta
	public int mask = 1024; //10000001 checks default and obstacles
	
	private static int layer1 = 0;
	private static int layer2 = 10;
	private static int layermask1 = 1 << layer1;
	private static int layermask2 = 1 << layer2;
	private int finalmask = layermask1 | layermask2;
	
	
	public Transform townCenter;

    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
        objectName = "Civil Unit";
		actions = new string[] { "TownCenter" };                 //Accions que pot fer la unitat civil
    }

    protected override void Update()
    {
        base.Update();
        if (!moving)
        {
            if (harvesting || emptying)
            {
                // tot el que implica la recoleccio de recursos
            }
            else if (building && currentProject && currentProject.UnderConstruction())
            {
                // tot el que implica la construccio d'edificis
            }
        }
    }

    protected override void OnGUI()
    {
        base.OnGUI();
    }

    /*** Metodes privats ***/

    // Metode que crea el edifici
    private void CreateBuilding(string buildingName)
    {
        building = true;
		Vector3 point = new Vector3(transform.position.x + 10, 0.0f, transform.position.z + 10);
		
		if (buildingName.Equals("TownCenter")) {		
			if (Physics.CheckSphere (point, 0.8f, finalmask)) {
				Debug.Log("No podemos construir porque hay otros edificios cerca");
			} else {
				float wood = owner.GetResourceAmount(RTSObject.ResourceType.Wood);
				if (wood >= 100) {
					Transform centerClone = (Transform)Instantiate(townCenter, point, Quaternion.identity);
					centerClone.GetComponent<RTSObject>().owner=owner;
					var guo = new GraphUpdateObject(centerClone.GetComponent<Collider>().bounds);
					guo.updatePhysics = true;
					AstarPath.active.UpdateGraphs (guo);
					owner.resourceAmounts[RTSObject.ResourceType.Wood]=wood-100;
				} else {
					Debug.Log("Not enough wood");
				}
			}
		}			 			
    }

    // Metode que cridem per a començar a recolectar
    private void StartHarvest(Resource resource, Building store)
    {
        resourceDeposit = resource;
        resourceStore = store;
        harvesting = true;
        emptying = false;
    }

    // Metode de recolecció
    private void Collect()
    {

    }

    // Metode per depositar els recursos al edifici resourceStore
    private void Deposit()
    {

    }

    /// <summary>
    /// Get the current type of resource that the unit is harvesting. If the unit is not harvesting any resource, returns null.
    /// </summary>
    /// <returns>The type of the resource being harvested, or null.</returns>
    public ResourceType? GetHarvestType()
    {
        // TODO: Implement this method properly
        return ResourceType.Wood;
    }

    /// <summary>
    /// Get the current amount of resource that the unit has harvested. If the unit is not harvesting any resource, returns null.
    /// </summary>
    /// <returns>The amount of resource that has been harvested, or null.</returns>
    public float? GetHarvestAmount()
    {
        // TODO: Implement this method properly
        return 999;
    }
	
	public override void PerformAction(string actionToPerform) {
		base.PerformAction(actionToPerform);
        CreateBuilding(actionToPerform);
	}
}
