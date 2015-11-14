using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AI : MonoBehaviour
{

    // Use this for initialization

    private Player artificialIntelligence;
    private List<GameObject> civils;
    public int townCenters;
    private List<CivilUnit> soldiers;
    private Vector3 position,closestDistance;
    private float totalDist;
    private GameObject civil, center;
    private GameObject tree;
    private float time;
    private bool building=false;
    private int i = 0;

    void Start()
    {
        artificialIntelligence = gameObject.AddComponent<Player>();
        townCenters = 0;
        civils = new List<GameObject>();
        Vector3 coords = new Vector3(453.51f, 0f, 435.28f);
        CreateNewCivil(coords);
        BuildTownCenter(true);

        foreach (GameObject civilian in civils)
        {         
            BuildWareHouse(civilian);
        }    
    }
	
	// Update is called once per frame
	void Update () {
    }



    private void BuildWareHouse(GameObject civilian) {

        tree = civilian.GetComponent<CivilUnit>().FindClosest("tree");
        center = civilian.GetComponent<CivilUnit>().FindClosest("townCenter");

        Vector3 positionTree = new Vector3(tree.transform.position.x, 0.0f, tree.transform.position.z);
        closestDistance = new Vector3(civil.GetComponent<CivilUnit>().transform.position.x - tree.transform.position.x, 0, civil.GetComponent<CivilUnit>().transform.position.z - tree.transform.position.z);
        totalDist = (float)Math.Sqrt(closestDistance.x * closestDistance.x + closestDistance.z * closestDistance.z);

        Vector3 positionCenter = new Vector3(center.transform.position.x, 0.0f, center.transform.position.z);
        Vector3 closestCenter = new Vector3(civil.GetComponent<CivilUnit>().transform.position.x - center.transform.position.x, 0, civil.GetComponent<CivilUnit>().transform.position.z - center.transform.position.z);
        float totalCenter = (float)Math.Sqrt(closestDistance.x * closestDistance.x + closestDistance.z * closestDistance.z);

        if (totalDist < 50f && totalCenter > 100f){  
            position = new Vector3(civilian.transform.position.x+10, 0.0f, civilian.transform.position.z+10);
            GameObject centerClone = (GameObject)Instantiate(Resources.Load("Prefabs/Hittite_CivilHouse"), position, Quaternion.identity);
            centerClone.GetComponent<RTSObject>().owner = artificialIntelligence;
        }
    }


    private void BuildTownCenter(Boolean resourceFree) {
        //Si no tinc cap centro urbano, tenir-ne una ha de ser la meva prioritat 
        foreach (GameObject civilian in civils)
        {
            if (building == false) // Loop with for.
            {
                position = new Vector3(civilian.transform.position.x + 10, 0.0f, civilian.transform.position.z + 10);
                GameObject centerClone = (GameObject)Instantiate(Resources.Load("Prefabs/Hittite_TownCenter"), position, Quaternion.identity);
                centerClone.GetComponent<RTSObject>().owner = artificialIntelligence;
                if (resourceFree == false)
                {
                    artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] - 100; //resta fusta 
                }   
                townCenters++;
                building = true;
            }
        }
    }

    private void CreateNewCivil(Vector3 coords) {
        civil= Instantiate(Resources.Load("Prefabs/Hittite_civil"), coords, Quaternion.identity) as GameObject;
        civil.GetComponent<CivilUnit>().owner = artificialIntelligence;
        civils.Add(civil);
        coords = new Vector3(455f, 0f, 436f);
    }


}
