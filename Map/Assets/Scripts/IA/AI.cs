using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AI : MonoBehaviour {

    // Use this for initialization
    List<Unit> civils;
    //List<GameObject> townCenters;
    int townCenters;
    List<GameObject> soldiers;
    Vector3 position;
    Unit civil;
    bool building=false;
    int i;

    void Start () {
        position = new Vector3(110,0,200);
        //civil= Instantiate(Resources.Load("yamato_civil", typeof(GameObject)), position, Quaternion.identity) as GameObject;
        civil = Instantiate(Resources.Load("yamato_civil", typeof(GameObject)), position, Quaternion.identity) as Unit;
        civils.Add(civil);
    }
	
	// Update is called once per frame
	void Update () {
        buildTownCenter();
        build("farm","food");
        build("mine", "metal");
        build("woodCutter", "tree");
    }


    void buildTownCenter() {
        //Si no tinc cap centro urbano, tenir-ne una ha de ser la meva prioritat
        if (townCenters == 0) {

            while ( i < civils.Count && building == true) // Loop with for.
            {
                // civilian.buildTownCenter();  //Encara no està implementat
                townCenters++;
                building = true;
            }


        }
    }

    void build(String building, String resource) {

        GameObject food;
        GameObject farm;
        GameObject townCenter;
        Vector3 buildingPosition;
        Vector3 unitPosition;
        Vector3 closestDistance;
        float totalDist;

        foreach (Unit civilian in civils)
        {
            unitPosition = civilian.transform.position; //Agafo la posicio del civil
            townCenter = civilian.FindClosest("townCenter"); //Retorna el TownCenter més proper
            buildingPosition = townCenter.transform.position;//Agafo la posicio
            closestDistance = new Vector3(unitPosition.x - buildingPosition.x,0,unitPosition.z - buildingPosition.z);
            totalDist = (float)Math.Sqrt(closestDistance.x*closestDistance.x+closestDistance.z*closestDistance.z);//i calculo  la distancia euclidania

            if (totalDist > 50) { //Si les distancia és més petita de 50 no val la pena anar a construir una farm, ja es pot anar al TownCenter
                farm = civilian.FindClosest(building);
                buildingPosition = farm.transform.position;
                closestDistance = new Vector3(unitPosition.x - buildingPosition.x, 0, unitPosition.z - buildingPosition.z);
                totalDist = (float)Math.Sqrt(closestDistance.x * closestDistance.x + closestDistance.z * closestDistance.z);//i calculo  la distancia euclidania

                if (totalDist>50) {
                    food = civilian.FindClosest(resource);
                    buildingPosition = food.transform.position;
                    closestDistance = new Vector3(unitPosition.x - buildingPosition.x, 0, unitPosition.z - buildingPosition.z);
                    totalDist = (float)Math.Sqrt(closestDistance.x * closestDistance.x + closestDistance.z * closestDistance.z);//i calculo  la distancia euclidania

                    if (totalDist < 50) {
                        //civilian.buildFarm();        Encara no esta implementat aquest mètode
                    }

                }
            }

        }
    }

}
