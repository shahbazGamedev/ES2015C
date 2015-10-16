using UnityEngine;

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


    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
        objectName = "Civil Unit";
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
    private void CreateBuilding(Building project)
    {
        currentProject = project;
        building = true;
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
}
