using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AI : MonoBehaviour
{
    public string username;

    // Use this for initialization

    private Player artificialIntelligence;
    private List<GameObject> civils;
    public List<GameObject> townCenters;
    private List<CivilUnit> soldiers;
    private Vector3 position, closestDistance, coords;
    private float totalDist;
    private GameObject civil, center;
    private GameObject tree;
    private bool building = false;
    private int i = 0;
    public AIResources resources;

    /// <summary>
     /// Which object does the player have selected for executing actions over it?
     /// </summary>
     public RTSObject SelectedObject { get; set; }
 
     /// <summary>
     /// Amount of each resource that can be collected by the player.
     /// </summary>
     public Dictionary<RTSObject.ResourceType, float> resourceAmounts;


    void Start()
    {
        resources = new AIResources();
        artificialIntelligence = gameObject.AddComponent<Player>();
        townCenters = new List<GameObject>();
        civils = new List<GameObject>();
        Vector3 coords = new Vector3(453.51f, 0f, 435.28f);
        CreateNewCivil(coords);
        coords = new Vector3(450f, 0f, 434f);
        CreateNewCivil(coords);
        // coords = new Vector3(448f, 0f, 432f);
        // CreateNewCivil(coords);
        BuildTownCenter(true);
        StartRecollecting(civils[0], "tree");

        StartRecollecting(civils[1], "food");
        //StartRecollecting(civils[2], "food");

        /*
         SpawnInitialEnemyTownCenter();
         SpawnInitialEnemyCivilUnit();
         SpawnInitialEnemyMilitaryUnit();
         */
    }
    //Creación del TownCenter dependiendo de la civilización seleccionada //
     private void SpawnInitialEnemyTownCenter()
     {
         var townCenterSpawnPointTransform = transform.FindChild("TownCenterSpawnPoint");
         if (townCenterSpawnPointTransform == null)
         {
             Debug.LogFormat("Can't find the town center spawn point for {0}.", username);
             return;
         }
 
         var townCenterSpawnPoint = townCenterSpawnPointTransform.position;
 
         //var townCenterTemplate = GetTownCenterTemplateForCivilization(civilization);
         var townCenterTemplate = RTSObjectFactory.GetObjectTemplate(RTSObjectType.BuildingTownCenter, civilization, true);
         if (townCenterTemplate == null)
             return;
 
         var townCenter = (GameObject)Instantiate(townCenterTemplate, townCenterSpawnPoint, Quaternion.identity);
         townCenter.GetComponent<RTSObject>().owner = this;
         townCenter.transform.parent = transform; // Should have no effect, but easier for debugging
 
         var guo = new GraphUpdateObject(townCenter.GetComponent<BoxCollider>().bounds);
         guo.updatePhysics = true;
         AstarPath.active.UpdateGraphs(guo);
     }

     //Creación de la Unidad Civil dependiendo de la civilización seleccionada //
     private void SpawnInitialEnemyCivilUnit()
     {
         var civilUnitSpawnPointTransform = transform.FindChild("CivilUnitSpawnPoint");
         if (civilUnitSpawnPointTransform == null)
         {
             Debug.LogFormat("Can't find the civil unit spawn point for {0}.", username);
             return;
         }
 
         var civilUnitSpawnPoint = civilUnitSpawnPointTransform.position;
 
         //var civilUnitTemplate = GetCivilUnitTemplateForCivilization(civilization);
         var civilUnitTemplate = RTSObjectFactory.GetObjectTemplate(RTSObjectType.UnitCivil, civilization, true);
         if (civilUnitTemplate == null)
             return;
 
         var civilUnit = (GameObject)Instantiate(civilUnitTemplate, civilUnitSpawnPoint, Quaternion.identity);
         civilUnit.GetComponent<RTSObject>().owner = this;
         civilUnit.transform.parent = transform; // Should have no effect, but easier for debugging
     }

     //Creación de la Unidad Militar dependiendo de la civilización seleccionada //
     private void SpawnInitialEnemyMilitaryUnit()
     {
         var militaryUnitSpawnPointTransform = transform.FindChild("MilitaryUnitSpawnPoint");
         if (militaryUnitSpawnPointTransform == null)
         {
             Debug.LogFormat("Can't find the military unit spawn point for {0}.", username);
             return;
         }
 
         var militaryUnitSpawnPoint = militaryUnitSpawnPointTransform.position;
 
         //var militaryUnitTemplate = GetMilitaryUnitTemplateForCivilization(civilization);
         var militaryUnitTemplate = RTSObjectFactory.GetObjectTemplate(RTSObjectType.UnitWarrior, civilization, true);
         if (militaryUnitTemplate == null)
             return;
 
         var militaryUnit = (GameObject)Instantiate(militaryUnitTemplate, militaryUnitSpawnPoint, Quaternion.identity);
         militaryUnit.GetComponent<RTSObject>().owner = this;
         militaryUnit.transform.parent = transform; // Should have no effect, but easier for debugging
     }
 



    // Update is called once per frame
    void Update()
    {
        if (townCenters[0] == null)
        {
            BuildTownCenter(false);
        }

        if (resources.wood >= 150)
        {
            resources.wood -= 150;
            BuildWareHouse(civils[0]);
        }

        if (resources.wood >= 50)
        {
            resources.wood -= 50;
            coords = new Vector3(457f, 0f, 436f);
            CreateNewCivil(coords);
        }
    }



    private void BuildWareHouse(GameObject civilian)
    {

        //if (artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] > 50)
        //{
        tree = civilian.GetComponent<CivilUnit>().FindClosest("tree");
        center = civilian.GetComponent<CivilUnit>().FindClosest("townCenter");


        /* closestDistance = new Vector3(civil.GetComponent<CivilUnit>().transform.position.x - tree.transform.position.x, 0, civil.GetComponent<CivilUnit>().transform.position.z - tree.transform.position.z);
         totalDist = (float)Math.Sqrt(closestDistance.x * closestDistance.x + closestDistance.z * closestDistance.z);
         Vector3 positionCenter = new Vector3(center.transform.position.x, 0.0f, center.transform.position.z);
         Vector3 closestCenter = new Vector3(civil.GetComponent<CivilUnit>().transform.position.x - center.transform.position.x, 0, civil.GetComponent<CivilUnit>().transform.position.z - center.transform.position.z);
         float totalCenter = (float)Math.Sqrt(closestDistance.x * closestDistance.x + closestDistance.z * closestDistance.z);*/

        //if (totalDist < 50f && totalCenter > 100f) {
        position = new Vector3(civilian.transform.position.x + 10, 0.0f, civilian.transform.position.z - 20);
        GameObject centerClone = (GameObject)Instantiate(Resources.Load("Prefabs/Hittite_CivilHouse"), position, Quaternion.identity);
        centerClone.GetComponent<RTSObject>().owner = artificialIntelligence;
        // artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] - 50;
        //}
        //}
    }


    private void BuildTownCenter(Boolean resourceFree)
    {
        //Si no tinc cap centro urbano, tenir-ne una ha de ser la meva prioritat 

        foreach (GameObject civilian in civils)
        {
            if (building == false) // Loop with for.
            {
                position = new Vector3(civilian.transform.position.x + 10, 0.0f, civilian.transform.position.z + 10);
                GameObject centerClone = (GameObject)Instantiate(Resources.Load("Prefabs/Sumerian_TownCenter"), position, Quaternion.identity);
                centerClone.GetComponent<RTSObject>().owner = artificialIntelligence;
                if (resourceFree == false)
                {
                    artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] - 100; //resta fusta 
                }
                townCenters.Add(centerClone);
                building = true;
            }
        }
    }


    private void CreateNewCivil(Vector3 coords)
    {
        civil = Instantiate(Resources.Load("Prefabs/Sumerian_civil"), coords, Quaternion.identity) as GameObject;
        civil.GetComponent<CivilUnit>().owner = artificialIntelligence;
        civils.Add(civil);
    }


    private void recollect(GameObject civilian, Vector3 position)
    {
        civilian.GetComponent<CivilUnit>().GoTo(position);
        civilian.GetComponent<CivilUnit>().StartHarvest(tree.GetComponent<Resource>());

    }

    private void StartRecollecting(GameObject civilian, String resource)
    {
        //foreach (GameObject civilian in civils)
        //{
        tree = civilian.GetComponent<CivilUnit>().FindClosest(resource);

        if (tree == null)
        {
            Debug.Log("L'arbre es null");
        }
        else
        {
            Vector3 position = new Vector3(tree.transform.position.x, 0.0f, tree.transform.position.z);
            recollect(civilian, position);
        }
        //}
    }
}
