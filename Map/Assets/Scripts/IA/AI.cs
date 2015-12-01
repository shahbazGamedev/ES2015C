using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AI : MonoBehaviour
{

    // Use this for initialization

    private Player artificialIntelligence;
    private List<GameObject> civils;
    private List<GameObject> townCenters;
    private List<GameObject> soldiers;
    private int civilhouse = 0;
    private Vector3 position, coords, armyPos;
    private Vector3 spawnPos;
    private bool housesBuilt = false, armyBuilt=false, towerBuilt=false;
    private int i, z = 0;
    private GameObject armyBuilding;
    private PlayerCivilization civilitzation;


    private int prova =1;

    void Start()
    {
        artificialIntelligence = GameObject.Find("EnemyPlayer1").GetComponent<Player>();
        civilitzation = GameObject.Find("EnemyPlayer1").GetComponent<Player>().civilization;
        soldiers = new List<GameObject>();
        townCenters = new List<GameObject>();
        civils = new List<GameObject>();
        Vector3 coords = new Vector3(453.51f, 0f, 435.28f);        
        i = 1;
        BuildTownCenter(coords,true);
        CreateNewCivil(true);
        spawnPos = townCenters[0].transform.position;
        civils[0].GetComponent<CivilUnit>().StartHarvest(null, true,"food");
    }

    // Update is called once per frame
    void Update()
    {
        if (townCenters.Count==0) {
            civils[0].GetComponent<CivilUnit>().building = true;
            civils[0].GetComponent<CivilUnit>().CreateBuildingIA(RTSObjectFactory.GetObjectTemplate(RTSObjectType.BuildingTownCenter, civilitzation), coords);
            civils[0].GetComponent<CivilUnit>().building = false;
            civils[0].GetComponent<CivilUnit>().StartHarvest(null, true, "food");
        }

        if (artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] > 100 && civils.Count < 3) {
            CreateNewCivil(false); ;
            civils[i].GetComponent<CivilUnit>().StartHarvest(null, true, "food");
            i++;
        }
        else if (civils.Count >= 3 &&
                artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] > 100 &&
                civils.Count < 5)
        {
            CreateNewCivil(false);
            civils[i].GetComponent<CivilUnit>().StartHarvest(null, true, "wood");
            i++;
        }

        //Encara no hi ha or al mapa
        /* else if (civils.Count>=5 &&
                 artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] > 100 &&
                 civils.Count<7) {
             CreateNewCivil(false);
             StartRecollecting(civils[i], "gold");
             i++;
         }*/

        else if (civils.Count == 5 &&
             artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] > 100 &&
             housesBuilt == false) {

            CreateNewCivil(false);
            spawnPos = new Vector3(spawnPos.x, 0, spawnPos.z - 30);

            civils[i].GetComponent<CivilUnit>().building = true;
            civils[i].GetComponent<CivilUnit>().CreateBuildingIA(RTSObjectFactory.GetObjectTemplate(RTSObjectType.BuildingCivilHouse, civilitzation), spawnPos);
            spawnPos = new Vector3(spawnPos.x - 10, 0, spawnPos.z);

            civils[i].GetComponent<CivilUnit>().CreateBuildingIA(RTSObjectFactory.GetObjectTemplate(RTSObjectType.BuildingCivilHouse, civilitzation), spawnPos);
            civils[i].GetComponent<CivilUnit>().building = false;
            civils[i].GetComponent<CivilUnit>().StartHarvest(null, true, "wood");
            i++;
            housesBuilt = true;
        }

        else if (civils.Count == 6 &&
               artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] > 100 &&
               armyBuilt == false) {

            CreateNewCivil(false);
            armyPos = new Vector3(townCenters[0].transform.position.x - 35, 0, townCenters[0].transform.position.z+15);

            civils[i].GetComponent<CivilUnit>().building = true;
            armyBuilding = RTSObjectFactory.GetObjectTemplate(RTSObjectType.BuildingArmyBuilding, civilitzation);
            civils[i].GetComponent<CivilUnit>().CreateBuildingIA(armyBuilding, armyPos);
            civils[i].GetComponent<CivilUnit>().building = false;

            civils[i].GetComponent<CivilUnit>().StartHarvest(null, true, "food");
            i++;
            armyBuilt = true;
        }

        else if (armyBuilt==true &&
            artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] > 100 &&
            towerBuilt==false &&
            civils.Count == 7) {
      
            CreateNewCivil(false);
            spawnPos = new Vector3(townCenters[0].transform.position.x + 20, 0, townCenters[0].transform.position.z);

            civils[i].GetComponent<CivilUnit>().building = true;
            civils[i].GetComponent<CivilUnit>().CreateBuildingIA(RTSObjectFactory.GetObjectTemplate(RTSObjectType.BuildingWallTower, civilitzation), spawnPos);

            spawnPos = new Vector3(townCenters[0].transform.position.x, 0, townCenters[0].transform.position.z+20);

            civils[i].GetComponent<CivilUnit>().building = true;
            civils[i].GetComponent<CivilUnit>().CreateBuildingIA(RTSObjectFactory.GetObjectTemplate(RTSObjectType.BuildingWallTower, civilitzation), spawnPos);
            civils[i].GetComponent<CivilUnit>().building = false;

            i++;
            towerBuilt = true;
        }



        else if (soldiers.Count >= 10 &&
            artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] > 100 &&
            civils.Count < 7) {
            CreateNewCivil(false);
            civils[i].GetComponent<CivilUnit>().StartHarvest(null, true, "food");
            i++;
        }

        if (armyBuilt==true &&
            artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] > 100 &&
            soldiers.Count<10) {
                CreateNewWarrior();
         }

        if (armyBuilt == true &&
            artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] > 100 &&
            soldiers.Count < 15 &&
            soldiers.Count >=10)
        {
            CreateNewArcher();
        }

        if (armyBuilt == true &&
           artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] > 100 &&
           soldiers.Count < 20 && 
           soldiers.Count>=15)
        {
            CreateNewCavalry();
        }

        if (armyBuilt == true &&
          artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] > 525 &&
          soldiers.Count >= 20)
        {
            CreateNewCavalry();
            CreateNewArcher();
            CreateNewWarrior();
        }

    }



   /* private void BuildWareHouse(GameObject civilian)
    {

        //if (artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] > 50)
        //{
        tree = civilian.GetComponent<CivilUnit>().FindClosest("wood");
        GameObject center = civilian.GetComponent<CivilUnit>().FindClosest("townCenter");


        /* closestDistance = new Vector3(civil.GetComponent<CivilUnit>().transform.position.x - tree.transform.position.x, 0, civil.GetComponent<CivilUnit>().transform.position.z - tree.transform.position.z);
         totalDist = (float)Math.Sqrt(closestDistance.x * closestDistance.x + closestDistance.z * closestDistance.z);
         Vector3 positionCenter = new Vector3(center.transform.position.x, 0.0f, center.transform.position.z);
         Vector3 closestCenter = new Vector3(civil.GetComponent<CivilUnit>().transform.position.x - center.transform.position.x, 0, civil.GetComponent<CivilUnit>().transform.position.z - center.transform.position.z);
         float totalCenter = (float)Math.Sqrt(closestDistance.x * closestDistance.x + closestDistance.z * closestDistance.z);*/

        //if (totalDist < 50f && totalCenter > 100f) {
        //position = new Vector3(civilian.transform.position.x + 10, 0.0f, civilian.transform.position.z - 20);
        //GameObject centerClone = (GameObject)Instantiate(RTSObjectFactory.GetObjectTemplate(RTSObjectType.BuildingCivilHouse,civilitzation), position, Quaternion.identity);
        //centerClone.GetComponent<RTSObject>().owner = artificialIntelligence;
        // artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] - 50;
        //}
        //}
    //}


    private void BuildTownCenter(Boolean resourceFree)
    { 
        foreach (GameObject civilian in civils)
        {          
                position = new Vector3(civilian.transform.position.x + 10, 0.0f, civilian.transform.position.z + 10);
                var townCenter = Instantiate(RTSObjectFactory.GetObjectTemplate(RTSObjectType.BuildingTownCenter, civilitzation), coords, Quaternion.identity) as GameObject;
                townCenter.GetComponent<RTSObject>().owner = artificialIntelligence;
                if (resourceFree == false)
                {
                    artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] - 100; //resta fusta 
                }
                townCenters.Add(townCenter);
        }
    }




    private void BuildTownCenter(Vector3 coords,Boolean resourceFree)
    {
            var townCenter = Instantiate(RTSObjectFactory.GetObjectTemplate(RTSObjectType.BuildingTownCenter, civilitzation), coords, Quaternion.identity) as GameObject;
            townCenter.GetComponent<RTSObject>().owner = artificialIntelligence;
            if (resourceFree == false)
            {
                artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] - 100; //resta fusta 
            }
            townCenters.Add(townCenter);
    }


    private void CreateNewCivil(Boolean resourceFree)
    {
        if (prova ==1) {
            coords = new Vector3(townCenters[0].transform.position.x - 10, 0.4f, townCenters[0].transform.position.z - 10);
            prova = 2;
        }
        else if (prova == 2){
            coords = new Vector3(townCenters[0].transform.position.x - 15, 0.4f, townCenters[0].transform.position.z - 10);
            prova = 3;
        }
        else if (prova == 3)
        {
            coords = new Vector3(townCenters[0].transform.position.x - 15, 0.4f, townCenters[0].transform.position.z - 15);
            prova = 1;
        }
         
                if (resourceFree == false) {
                    artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] - 100;
                }
                GameObject civil = Instantiate(RTSObjectFactory.GetObjectTemplate(RTSObjectType.UnitCivil, civilitzation), coords, Quaternion.identity) as GameObject;
                civil.GetComponent<CivilUnit>().owner = artificialIntelligence;
                civils.Add(civil);
            
    }



    private void CreateNewWarrior()
    {

        coords = new Vector3(armyPos.x - 10, 0.4f, armyPos.z - 10 + z * 2);
        artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] - 100;

        GameObject civil = Instantiate(RTSObjectFactory.GetObjectTemplate(RTSObjectType.UnitWarrior, civilitzation), coords, Quaternion.identity) as GameObject;
        civil.GetComponent<Unit>().owner = artificialIntelligence;
        soldiers.Add(civil);
        if (z >= 10)
        {
            z = 0;
        }
        else {
            z++;
        }
    }



    private void CreateNewArcher()
    {
<<<<<<< HEAD
        civilian.GetComponent<CivilUnit>().GoTo(position, false);
        civilian.GetComponent<CivilUnit>().StartHarvest(tree.GetComponent<Resource>());
=======
>>>>>>> dev_teamD5

        coords = new Vector3(armyPos.x - 15, 0.4f, armyPos.z - 10 + z * 2);
        artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] - 170;

        GameObject civil = Instantiate(RTSObjectFactory.GetObjectTemplate(RTSObjectType.UnitArcher, civilitzation), coords, Quaternion.identity) as GameObject;
        civil.GetComponent<Unit>().owner = artificialIntelligence;
        soldiers.Add(civil);
        if (z >= 8)
        {
            z = 0;
        }
        else
        {
            z++;
        }
    }

    private void CreateNewCavalry()
    {

        coords = new Vector3(armyPos.x - 20, 0.4f, armyPos.z - 10 + z * 3);
        artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] - 250;

        GameObject civil = Instantiate(RTSObjectFactory.GetObjectTemplate(RTSObjectType.UnitCavalry, civilitzation), coords, Quaternion.identity) as GameObject;
        civil.GetComponent<Unit>().owner = artificialIntelligence;
        soldiers.Add(civil);
        if (z >= 8)
        {
            z = 0;
        }
        else
        {
            z++;
        }
    }


}
