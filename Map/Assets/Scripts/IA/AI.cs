using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AI : MonoBehaviour {

    // Use this for initialization
    List<Unit> civils;
    int townCenters;
    List<GameObject> soldiers;
    Vector3 position;
    Unit civil;

    private int food, metal, wood;
    private bool building=false;
    private int i = 0;

    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
    }


    private void buildTownCenter() {
        //Si no tinc cap centro urbano, tenir-ne una ha de ser la meva prioritat
        if (townCenters == 0 && food==100 && wood==100) { //Es comproven els recursos

            while ( i < civils.Count && building == true) // Loop with for.
            {
                // civilian.buildTownCenter();  //Encara no està implementat
                food = food - 100;
                wood = wood - 100;
                townCenters++;
                building = true;
            }


        }
    }

    private void build(String building) {

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

        foreach (Unit civilian in civils)
        {
            unitPosition = civilian.transform.position; //Agafo la posicio del civil
            townCenter = civilian.FindClosest("townCenter"); //Retorna el TownCenter més proper
            buildingPosition = townCenter.transform.position;//Agafo la posicio
            closestDistance = new Vector3(unitPosition.x - buildingPosition.x, 0, unitPosition.z - buildingPosition.z);
            totalDist = (float)Math.Sqrt(closestDistance.x * closestDistance.x + closestDistance.z * closestDistance.z);//i calculo  la distancia euclidania
            if (wood == 50 && food == 100)
            {
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
                            //civilian.build(building);        Encara no esta implementat aquest mètode
                            wood = wood - 50;
                            food = food - 50;
                        }

                    }
                }

            }
        }
    }

    public void incFood(int num) {
        food = food +num;
    }

    public void incWood(int num) {
        wood = wood + num;
    }

    public void incMetal(int num) {
        metal = metal+num;
    }

    public int getFood() {
        return food;
    }

    public int getWood()
    {
        return wood;
    }

    public int getMetal() {
        return metal;
    }


}
