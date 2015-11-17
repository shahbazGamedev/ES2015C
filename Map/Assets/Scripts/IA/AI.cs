using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AI : RTSObject
{

    // Use this for initialization

    private Player artificialIntelligence;
    private List<GameObject> civils;
    public List<GameObject> townCenters;
    private List<CivilUnit> soldiers;
    private Vector3 position, closestDistance,coords;
    private float totalDist;
    private GameObject civil, center;
    private GameObject tree;
    private bool building = false;
    private int i = 0;
    public AIResources resources;




    void Start()
    {
        resources = new AIResources();
        artificialIntelligence = gameObject.AddComponent<Player>();
        townCenters = new List <GameObject>();
        civils = new List<GameObject>();
        Vector3 coords = new Vector3(453.51f, 0f, 435.28f);
        CreateNewCivil(coords);
       coords = new Vector3(450f, 0f, 434f);
        CreateNewCivil(coords);
       // coords = new Vector3(448f, 0f, 432f);
       // CreateNewCivil(coords);
        BuildTownCenter(true);
        StartRecollecting(civils[0],"tree");
<<<<<<< HEAD
        StartRecollecting(civils[1], "food");
        //StartRecollecting(civils[2], "food");

=======
        StartRecollecting(civils[1], "tree");
        StartRecollecting(civils[2], "food");
   
>>>>>>> refs/remotes/origin/dev_TeamD
    }

    // Update is called once per frame
    void Update() {
        if (townCenters[0]==null) {
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

    

    private void BuildWareHouse(GameObject civilian) {

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
                position = new Vector3(civilian.transform.position.x + 10, 0.0f, civilian.transform.position.z -20);
                GameObject centerClone = (GameObject)Instantiate(Resources.Load("Prefabs/Hittite_CivilHouse"), position, Quaternion.identity);
                centerClone.GetComponent<RTSObject>().owner = artificialIntelligence;
               // artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] - 50;
            //}
       //}
    }


    private void BuildTownCenter(Boolean resourceFree) {
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

    private void CreateNewCivil(Vector3 coords) {
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

