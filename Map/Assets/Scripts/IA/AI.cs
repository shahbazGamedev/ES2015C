using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AI : Player {

    // Use this for initialization
    List<CivilUnit> civils;
    int townCenters;
    List<CivilUnit> soldiers;
    Vector3 position;
    CivilUnit civil;

    private bool building=false;
    private int i = 0;

    void Start () {
        townCenters = 0;
       civils = new List<CivilUnit>();
       CreateNewCivil();
    }
	
	// Update is called once per frame
	void Update () {
        buildTownCenter();
    }


    private void buildTownCenter() {
        //Si no tinc cap centro urbano, tenir-ne una ha de ser la meva prioritat
        if (civils.Count == 0 && townCenters==0) {
            
        }

        else {  
            foreach (CivilUnit civilian in civils)
                if (building == false) // Loop with for.
                {
                    civilian.CreateBuilding("TownCenter"); 
                    townCenters++;
                    building = true;
                }
        }
        
    }

    private void CreateNewCivil() {
        Vector3 coords = new Vector3(114f, 0f, 111f);
        civil = new CivilUnit("yamato_civil",coords);
        //GameObject civil= Instantiate(Resources.Load("yamato_civil"), coords, Quaternion.identity) as GameObject;
        //civils.Add(civil);
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

        foreach (CivilUnit civilian in civils)
        {
            unitPosition = civilian.transform.position; //Agafo la posicio del civil
            townCenter = civilian.FindClosest("townCenter"); //Retorna el TownCenter més proper
            buildingPosition = townCenter.transform.position;//Agafo la posicio
            closestDistance = new Vector3(unitPosition.x - buildingPosition.x, 0, unitPosition.z - buildingPosition.z);
            totalDist = (float)Math.Sqrt(closestDistance.x * closestDistance.x + closestDistance.z * closestDistance.z);//i calculo  la distancia euclidania
   
                if (totalDist > 50)
                { //Si les distancia és més petita de 50 no val la pena anar a construir una farm, ja es pot anar al TownCenter
                    buildingGameObject = civilian.FindClosest(building);
                    buildingPosition = buildingGameObject.transform.position;
                    closestDistance = new Vector3(unitPosition.x - buildingPosition.x, 0, unitPosition.z - buildingPosition.z);
                    totalDist = (float)Math.Sqrt(closestDistance.x * closestDistance.x + closestDistance.z * closestDistance.z);//i calculo  la distancia euclidania

                    if (totalDist > 50)
                    {
                        resourceGameObject = civilian.FindClosest(resource);
                        buildingPosition = resourceGameObject.transform.position;
                        closestDistance = new Vector3(unitPosition.x - buildingPosition.x, 0, unitPosition.z - buildingPosition.z);
                        totalDist = (float)Math.Sqrt(closestDistance.x * closestDistance.x + closestDistance.z * closestDistance.z);//i calculo  la distancia euclidania

                        if (totalDist < 50)
                        {
                            civilian.CreateBuilding(building); //Encara no esta implementat aquest mètode
                        }

                    }
                

            }
        }
    }

}
