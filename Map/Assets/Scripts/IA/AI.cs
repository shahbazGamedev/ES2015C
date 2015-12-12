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
    private List<GameObject> archers;
    private List<GameObject> cavalry;
    private int civilhouse = 0;
    private Vector3 position, coords, armyPos;
    private Vector3 spawnPos;
    private bool housesBuilt = false, armyBuilt=false, towerBuilt=false, academyBuilt=false;
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
        CivilsRecollect();
        if (townCenters.Count==0) {
            civils[0].GetComponent<CivilUnit>().building = true;
            civils[0].GetComponent<CivilUnit>().CreateBuildingIA(RTSObjectFactory.GetObjectTemplate(RTSObjectType.BuildingTownCenter, civilitzation), coords);
            civils[0].GetComponent<CivilUnit>().building = false;
            civils[0].GetComponent<CivilUnit>().StartHarvest(null, true, "food");
        }

        if (artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] >= 100 && civils.Count < 4) {
            CreateNewCivil(false); ;            
        }

        /*ArmyBuilding*/
        else if (civils.Count < 4 &&
                artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] >= 100 &&
                civils.Count < 4 &&
                armyBuilt==false)
        {
            /*armyPos = new Vector3(townCenters[0].transform.position.x - 35, 0, townCenters[0].transform.position.z + 15);
            civils[3].GetComponent<CivilUnit>().CreateBuildingIA(RTSObjectFactory.GetObjectTemplate(RTSObjectType.BuildingTownCenter, civilitzation), coords);
            armyBuilt = true;*/
        }



        /*Academy*/
        else if (civils.Count < 4 &&
             artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] >= 400 &&
             armyBuilt == true && 
             academyBuilt == false) {

            /*spawnPos = new Vector3(spawnPos.x, 0, spawnPos.z - 30);

            civils[3].GetComponent<CivilUnit>().building = true;
            civils[3].GetComponent<CivilUnit>().CreateBuildingIA(RTSObjectFactory.GetObjectTemplate(RTSObjectType.BuildingCivilHouse, civilitzation), spawnPos);
            spawnPos = new Vector3(spawnPos.x - 10, 0, spawnPos.z);

            civils[3].GetComponent<CivilUnit>().CreateBuildingIA(RTSObjectFactory.GetObjectTemplate(RTSObjectType.BuildingCivilHouse, civilitzation), spawnPos);
            civils[3].GetComponent<CivilUnit>().building = false;
            civils[3].GetComponent<CivilUnit>().StartHarvest(null, true, "wood");
            housesBuilt = true;*/
        }

        /*houses*/
        else if (civils.Count < 4 &&
               artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] >= 100 &&
               armyBuilt == true && 
               academyBuilt == true &&
               housesBuilt == false) {

           /* armyPos = new Vector3(townCenters[0].transform.position.x - 35, 0, townCenters[0].transform.position.z+15);

            civils[3].GetComponent<CivilUnit>().building = true;
            armyBuilding = RTSObjectFactory.GetObjectTemplate(RTSObjectType.BuildingArmyBuilding, civilitzation);
            civils[3].GetComponent<CivilUnit>().CreateBuildingIA(armyBuilding, armyPos);
            civils[3].GetComponent<CivilUnit>().building = false;

            civils[3].GetComponent<CivilUnit>().StartHarvest(null, true, "food");
            armyBuilt = true;*/
        }

        /*tower*/
        else if (civils.Count < 4 &&
            artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] >= 250 &&
            armyBuilt == true &&
            academyBuilt == true &&
            housesBuilt == true &&
            towerBuilt == false) {
      
            /*spawnPos = new Vector3(townCenters[0].transform.position.x + 20, 0, townCenters[0].transform.position.z);

            civils[3].GetComponent<CivilUnit>().building = true;
            civils[3].GetComponent<CivilUnit>().CreateBuildingIA(RTSObjectFactory.GetObjectTemplate(RTSObjectType.BuildingWallTower, civilitzation), spawnPos);

            spawnPos = new Vector3(townCenters[0].transform.position.x, 0, townCenters[0].transform.position.z+20);

            civils[3].GetComponent<CivilUnit>().building = true;
            civils[3].GetComponent<CivilUnit>().CreateBuildingIA(RTSObjectFactory.GetObjectTemplate(RTSObjectType.BuildingWallTower, civilitzation), spawnPos);
            civils[3].GetComponent<CivilUnit>().building = false;

            towerBuilt = true;*/
        }


        /*Soldat*/
        if (civils.Count < 4 &&
            artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] >= 100 &&
            soldiers.Count <= archers.Count*2 &&
            soldiers.Count <= cavalry.Count*02 &&
            armyBuilt == true) {

            CreateNewWarrior();
        }

        /*Arquer*/
        if (civils.Count < 4 &&
            artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] >= 175 &&
            soldiers.Count/2 > archers.Count && 
            armyBuilt == true) {
                CreateNewArcher();
         }

        /*Cavall*/
        if (civils.Count < 4 &&
            artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] >= 250 &&
            soldiers.Count/2 > cavalry.Count &&
            archers.Count > cavalry.Count &&
            armyBuilt==true)
        {
            CreateNewArcher();
        }


    //_______________________________

    // Dependiendo de lo que se marque por teclado se aumentaran o disminiuirán los recursos de la IA.
    // Esto se crea para tener un acceso más fácil a que todo funciona.
        if (Input.GetKey (KeyCode.G) && Input.GetKey(KeyCode.UpArrow)) {
            artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Gold] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Gold] + 1000;
            HUDInfo.insertMessage(string.Format("It has increased the resource 'Gold' of AI 1000 units.")); 
        }

        if (Input.GetKey (KeyCode.G) && Input.GetKey (KeyCode.DownArrow)) {
            if (artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Gold] >= 1000) {
                artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Gold] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Gold] - 1000; 
                HUDInfo.insertMessage(string.Format("It has decreased the resource 'Gold' of AI 1000 units.")); 
            }
        }    

        if (Input.GetKey (KeyCode.F) && Input.GetKey(KeyCode.UpArrow)) {
            artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] + 1000; 
            HUDInfo.insertMessage(string.Format("It has increased the resource 'Food' of AI 1000 units."));
        }

        if (Input.GetKey (KeyCode.F) && Input.GetKey (KeyCode.DownArrow)) {
            if (artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] >= 1000) {
                artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] - 1000; 
                HUDInfo.insertMessage(string.Format("It has decreased the resource 'Food' of AI 1000 units.")); 
            }
        }     

        if (Input.GetKey (KeyCode.W) && Input.GetKey(KeyCode.UpArrow)) {
            artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] + 1000; 
            HUDInfo.insertMessage(string.Format("It has increased the resource 'Wood' of AI 1000 units."));
        }

        if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.DownArrow)) {
            if (artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] >= 1000) {
                artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] - 1000; 
                HUDInfo.insertMessage(string.Format("It has decreased the resource 'Wood' of AI 1000 units.")); 
            }
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

    private void CivilsRecollect() {
            int len = civils.Count;
            switch (len) {
                case 1:                 
                    civils[0].GetComponent<CivilUnit>().StartHarvest(null, true, "food");
                    break;
                case 2:
                    civils[0].GetComponent<CivilUnit>().StartHarvest(null, true, "food");
                    civils[1].GetComponent<CivilUnit>().StartHarvest(null, true, "food");
                    break;
                case 3:
                    civils[0].GetComponent<CivilUnit>().StartHarvest(null, true, "food");
                    civils[1].GetComponent<CivilUnit>().StartHarvest(null, true, "food");
                    civils[2].GetComponent<CivilUnit>().StartHarvest(null, true, "wood");
                    break;
                case 4:
                    civils[0].GetComponent<CivilUnit>().StartHarvest(null, true, "food");
                    civils[1].GetComponent<CivilUnit>().StartHarvest(null, true, "food");
                    civils[2].GetComponent<CivilUnit>().StartHarvest(null, true, "wood");
                    civils[3].GetComponent<CivilUnit>().StartHarvest(null, true, "food");
                    break;


            }

        }
}
