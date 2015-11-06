using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AI : Player {

    // Use this for initialization
    List<GameObject> civils;
    int townCenters;
    List<CivilUnit> soldiers;
    Vector3 position;
    CivilUnit civil;

    private bool building=false;
    private int i = 0;

    void Start () {
       townCenters = 0;
       civils = new List<GameObject>();
       CreateNewCivil();
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
                GameObject centerClone = (GameObject)Instantiate(Resources.Load("towncenterYamato"), point, Quaternion.identity);
                //civilian.GetComponent<CivilUnit>().CreateBuilding("TownCenter");
                townCenters++;
                building = true;
            }
        }
    }

    private void CreateNewCivil() {
        Vector3 coords = new Vector3(114f, 0f, 111f);
        GameObject civil= Instantiate(Resources.Load("yamato_civil"), coords, Quaternion.identity) as GameObject;

        civils.Add(civil);
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
						civilian.GetComponent<CivilUnit>().PerformAction(building);
                        civilian.GetComponent<CivilUnit>().CreateBuilding(); //Encara no esta implementat aquest mètode
                        }

                    }
                

            }
        }
    }
}
