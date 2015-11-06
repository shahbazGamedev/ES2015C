using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AI : MonoBehaviour
{

    // Use this for initialization

    Player artificialIntelligence;
    List<GameObject> civils;
    int townCenters;
    List<CivilUnit> soldiers;
    Vector3 position;
    GameObject civil;

    private bool building=false;
    private int i = 0;

    void Start () {
       artificialIntelligence = gameObject.AddComponent<Player>();
       townCenters = 0;
       civils = new List<GameObject>();
       Vector3 coords = new Vector3(453.51f, 0f, 435.28f);
       CreateNewCivil(coords);
    }
	
	// Update is called once per frame
	void Update () {
        buildTownCenter();
    }


    private void buildTownCenter() {
        //Si no tinc cap centro urbano, tenir-ne una ha de ser la meva prioritat 
        foreach (GameObject civilian in civils)
        {
            if (building == false) // Loop with for.
            {
                Vector3 point = new Vector3(civilian.transform.position.x + 10, 0.0f, civilian.transform.position.z + 10);
                GameObject centerClone = (GameObject)Instantiate(Resources.Load("Prefabs/Hittite_TownCenter"), point, Quaternion.identity);
                centerClone.GetComponent<RTSObject>().owner = artificialIntelligence;                      
                artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] - 100; //resta fusta 
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
        /*Transform tree = Instantiate(Resources.Load("Prefabs/arbolYamato"), coords, Quaternion.identity) as Transform;
        if (tree == null)
        {
            Debug.Log("No hi ha cap arbre proper");
        }
        else
        {
            civil.GetComponent<CivilUnit>().GoTo(tree.transform.position);
        }*/
    }

    private void Build(String building) {

        GameObject resourceGameObject;
        GameObject buildingGameObject;
        GameObject townCenter;
        Vector3 buildingPosition;
        Vector3 unitPosition;
        Vector3 closestDistance;
        String resource;
        float totalDist;

        switch (building) {
            case "farm":
                resource = "food";
                break;
            case "woodCutter":
                resource = "wood";
                break;
            case "mine":
                resource = "metal";
                break;
            default:
                resource = null;
                break;

        }

        foreach (GameObject civilian in civils)
        {
            unitPosition = civilian.transform.position; //Agafo la posicio del civil
            townCenter = civilian.GetComponent<CivilUnit>().FindClosest("townCenter"); //Retorna el TownCenter més proper
            buildingPosition = townCenter.transform.position;//Agafo la posicio
            closestDistance = new Vector3(unitPosition.x - buildingPosition.x, 0, unitPosition.z - buildingPosition.z);
            totalDist = (float)Math.Sqrt(closestDistance.x * closestDistance.x + closestDistance.z * closestDistance.z);//i calculo  la distancia euclidania
   
                if (totalDist > 50)
                { //Si les distancia és més petita de 50 no val la pena anar a construir una farm, ja es pot anar al TownCenter
                    buildingGameObject = civilian.GetComponent<CivilUnit>().FindClosest(building);
                    buildingPosition = buildingGameObject.transform.position;
                    closestDistance = new Vector3(unitPosition.x - buildingPosition.x, 0, unitPosition.z - buildingPosition.z);
                    totalDist = (float)Math.Sqrt(closestDistance.x * closestDistance.x + closestDistance.z * closestDistance.z);//i calculo  la distancia euclidania

                    if (totalDist > 50)
                    {
                        resourceGameObject = civilian.GetComponent<CivilUnit>().FindClosest(resource);
                        buildingPosition = resourceGameObject.transform.position;
                        closestDistance = new Vector3(unitPosition.x - buildingPosition.x, 0, unitPosition.z - buildingPosition.z);
                        totalDist = (float)Math.Sqrt(closestDistance.x * closestDistance.x + closestDistance.z * closestDistance.z);//i calculo  la distancia euclidania

                        if (totalDist < 50)
                        {
                        civilian.GetComponent<CivilUnit>().CreateBuilding(building); //Encara no esta implementat aquest mètode
                        }

                    }
                

            }
        }
    }
}
