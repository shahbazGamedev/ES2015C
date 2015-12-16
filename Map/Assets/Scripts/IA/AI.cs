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
    private int z = 0, civilian = 1, numWall = 1;
    private float CoordX, CoordZ;
    private Vector3 coords, armyPos;
    private Vector3 spawnPos;
    private bool housesBuilt = false, armyBuilt=false, towerBuilt=false, academyBuilt=false;
    private PlayerCivilization civilitzation;

    public int numberTownCenters = 0;
    public int numberCivils = 0;
    public int numberArmyBuilidngs = 0;
    public int numberAcedemies = 0;
    public int numberCivilHouse = 0;
    public int numberWallTower = 0;
    public int numberWall = 0;
    public int numberWarrior = 0;
    public int numberArcher = 0;
    public int numberCavalry = 0;
    public int numberWallEntrance = 0;
    public int numberArcherAdvanced = 0;
    public int numberCavalryAdvanced = 0;
    public int numberWarriorAdvanced = 0;
    public int numberUniversity = 0;




    //private GameObject menu;


    void Start()
    {
        artificialIntelligence = GameObject.Find("EnemyPlayer1").GetComponent<Player>();
        civilitzation = GameObject.Find("EnemyPlayer1").GetComponent<Player>().civilization;
        soldiers = new List<GameObject>();
        archers = new List<GameObject>();
        cavalry = new List<GameObject>();
        townCenters = new List<GameObject>();
        civils = new List<GameObject>();
        Vector3 coords = new Vector3(453.51f, 0f, 435.28f);        
        BuildTownCenter(coords,true);

        coords = new Vector3(453.51f-10f, 0f, 435.28f-10f);
        //GameObject civil = Instantiate(RTSObjectFactory.GetObjectTemplate(RTSObjectType.UnitCivil, civilitzation), coords, Quaternion.identity) as GameObject;
        //civil.GetComponent<CivilUnit>().owner = artificialIntelligence;
        //civils.Add(civil);
        CreateNewCivil(true);
        spawnPos = townCenters[0].transform.position;
	
    }


    // Update is called once per frame
    void Update()
    {
        CivilsRecollect();

        
        /*TownCenter*/
        if (townCenters.Count == 0)
            {
                

                GameObject townCenter = RTSObjectFactory.GetObjectTemplate(RTSObjectType.BuildingTownCenter, civilitzation);
                townCenters.Add(townCenter);
                spawnPos = new Vector3(450f,0,150f);
                civils[3].GetComponent<CivilUnit>().CreateOnConstructionBuildingAI(townCenter, spawnPos);

                

            }

            if (artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] >= 100 && civils.Count < 4)
            {

                CreateNewCivil(false);

            }

            /*ArmyBuilding*/
            if (civils.Count >= 4 &&
                    artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] >= 100 &&
                    armyBuilt == false)
            {
                armyBuilt = true;
                armyPos = new Vector3(spawnPos.x, 0, spawnPos.z + 20);
                GameObject armyBuilding = RTSObjectFactory.GetObjectTemplate(RTSObjectType.BuildingArmyBuilding, civilitzation);
                civils[3].GetComponent<CivilUnit>().CreateOnConstructionBuildingAI(armyBuilding, armyPos);  

                //aumentamos numero de ArmyBuilidngs contruidas
                numberArmyBuilidngs = numberArmyBuilidngs + 1;            
            }



            /*Academy*/
            if (civils.Count >= 4 &&
                 artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] >= 400 &&
                 armyBuilt == true &&
                 academyBuilt == false)
            {

                academyBuilt = true;
                spawnPos = new Vector3(townCenters[0].transform.position.x - 30, 0, townCenters[0].transform.position.z + 15);
                GameObject armyBuilding = RTSObjectFactory.GetObjectTemplate(RTSObjectType.BuildingAcademy, civilitzation);
                civils[3].GetComponent<CivilUnit>().CreateOnConstructionBuildingAI(armyBuilding, spawnPos);

                //aumentamos numero de academys contruidas
                numberAcedemies = numberAcedemies + 1;
            }

            /*houses*/
            if (civils.Count >= 4 &&
                   artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] >= 100 &&
                   armyBuilt == true &&
                   academyBuilt == true &&
                   housesBuilt == false)
            {
                housesBuilt = true;
                spawnPos = new Vector3(townCenters[0].transform.position.x + 70, -46, townCenters[0].transform.position.z + 100);
                GameObject civilHouse = RTSObjectFactory.GetObjectTemplate(RTSObjectType.BuildingCivilHouse, civilitzation);
                civils[3].GetComponent<CivilUnit>().CreateOnConstructionBuildingAI(civilHouse, spawnPos);


                //aumentamos numero de civilhouse contruidas
                numberCivilHouse = numberCivilHouse + 1;

            }

            /*tower*/
            if (civils.Count >= 4 &&
                artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] >= 250 &&
                armyBuilt == true &&
                academyBuilt == true &&
                housesBuilt == true &&
                towerBuilt == false)
            {

                towerBuilt = true;
                spawnPos = new Vector3(townCenters[0].transform.position.x - 25, 0, townCenters[0].transform.position.z - 25);
                GameObject wallTower = RTSObjectFactory.GetObjectTemplate(RTSObjectType.BuildingWallTower, civilitzation);
                civils[3].GetComponent<CivilUnit>().CreateOnConstructionBuildingAI(wallTower, spawnPos);

                //aumentamos numero de wallTower contruidas
                numberWallTower = numberWallTower + 1;

            }

            /*Wall*/
            if (civils.Count >= 4 &&
                artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] >= 50 &&
                armyBuilt == true &&
                academyBuilt == true &&
                housesBuilt == true &&
                towerBuilt == true &&
                numWall <= 8)
            {

                numWall++;
                if (numWall <= 5)
                {
                    CoordX = townCenters[0].transform.position.x - 28 + (5f * numWall);
                    CoordZ = townCenters[0].transform.position.z - 25;
                }          
                /*else{
                    CoordZ = townCenters[0].transform.position.z - 25 + (5f * (numWall - 5));
                }*/
                spawnPos = new Vector3(CoordX, 0, CoordZ);
                GameObject wall = RTSObjectFactory.GetObjectTemplate(RTSObjectType.BuildingWall, civilitzation);
                civils[3].GetComponent<CivilUnit>().CreateOnConstructionBuildingAI(wall, spawnPos);

                //aumentamos numero de wall contruidas
                numberWall = numberWall + 1;

            }


            /*Soldats*/
             if(civils.Count >= 4 &&
                artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] >= 100 &&
                soldiers.Count<10 &&              
                armyBuilt == true)
            {

                GameObject warrior = RTSObjectFactory.GetObjectTemplate(RTSObjectType.UnitWarrior, civilitzation);
                CreateNewWarrior();
        }


             /*Arquers*/
            if (civils.Count >=  4 &&
                 artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] >= 175 &&
                 soldiers.Count>=10 && 
                 archers.Count<=5 &&
                 armyBuilt == true)
            {

                GameObject archer = RTSObjectFactory.GetObjectTemplate(RTSObjectType.UnitArcher, civilitzation);
                CreateNewArcher(); ;

            }

            /*Cavall*/
            if (civils.Count >= 4 &&
                 artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] >= 250 &&
                 soldiers.Count >= 10 &&
                 archers.Count >= 5 &&
                 armyBuilt == true)
            {
                GameObject cavalry = RTSObjectFactory.GetObjectTemplate(RTSObjectType.UnitCavalry, civilitzation);
                CreateNewCavalry();

        }

    
        //_______________________________

        // Dependiendo de lo que se marque por teclado se aumentaran o disminiuirán los recursos de la IA.
        // Esto se crea para tener un acceso más fácil a que todo funciona.
        if (Input.GetKey (KeyCode.G) && Input.GetKey(KeyCode.UpArrow)) {
            artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Gold] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Gold] + 50;
            HUDInfo.insertMessage(string.Format("It has increased the resource 'Gold' of AI 50 units.")); 
        }

        if (Input.GetKey (KeyCode.G) && Input.GetKey (KeyCode.DownArrow)) {
            if (artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Gold] >= 1000) {
                artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Gold] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Gold] - 50; 
                HUDInfo.insertMessage(string.Format("It has decreased the resource 'Gold' of AI 50 units.")); 
            }
        }    

        if (Input.GetKey (KeyCode.F) && Input.GetKey(KeyCode.UpArrow)) {
            artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] + 50; 
            HUDInfo.insertMessage(string.Format("It has increased the resource 'Food' of AI 50 units."));
        }

        if (Input.GetKey (KeyCode.F) && Input.GetKey (KeyCode.DownArrow)) {
            if (artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] >= 1000) {
                artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] - 50; 
                HUDInfo.insertMessage(string.Format("It has decreased the resource 'Food' of AI 50 units.")); 
            }
        }     

        if (Input.GetKey (KeyCode.W) && Input.GetKey(KeyCode.UpArrow)) {
            artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] + 50; 
            HUDInfo.insertMessage(string.Format("It has increased the resource 'Wood' of AI 50 units."));
        }

        if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.DownArrow)) {
            if (artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] >= 1000) {
                artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Wood] - 50; 
                HUDInfo.insertMessage(string.Format("It has decreased the resource 'Wood' of AI 50 units.")); 
            }
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

        //aumentamos numero de numberTownCenters contruidas
        numberTownCenters = numberTownCenters + 1;

    }


    private void CreateNewCivil()
    {
        GameObject[] centers;
        centers = GameObject.FindGameObjectsWithTag("townCenter");
        foreach (GameObject army in centers)
        {
            if (army.GetComponent<Building>().owner == artificialIntelligence)
            {
                GameObject civilUnit = RTSObjectFactory.GetObjectTemplate(RTSObjectType.UnitCivil, civilitzation);
                army.GetComponent<Building>().CreateUnitAI(civilUnit);
            }
        }

        GameObject[] enemySoldiers = GameObject.FindGameObjectsWithTag("civil");
        civils.Clear();
        foreach (GameObject soldier in enemySoldiers)
        {
            if (soldier.GetComponent<RTSObject>().owner == artificialIntelligence.GetComponent<Player>())
            {
                civils.Add(soldier);

            }
        }

    }
    private void CreateNewCivil(Boolean resourceFree)
    {
        if (civilian == 1)
        {
            coords = new Vector3(townCenters[0].transform.position.x - 10, 0.4f, townCenters[0].transform.position.z - 10);
            civilian = 2;
        }
        else if (civilian == 2)
        {
            coords = new Vector3(townCenters[0].transform.position.x - 15, 0.4f, townCenters[0].transform.position.z - 10);
            civilian = 3;
        }
        else if (civilian == 3)
        {
            coords = new Vector3(townCenters[0].transform.position.x - 15, 0.4f, townCenters[0].transform.position.z - 15);
            civilian = 4;         
        }
        else if (civilian == 4)
            
        {
        coords = new Vector3(townCenters[0].transform.position.x - 10, 0.4f, townCenters[0].transform.position.z - 15);
         civilian = 1;
           
        }

        if (resourceFree == false)
        {
         artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] - 100;
        }
        GameObject civil = Instantiate(RTSObjectFactory.GetObjectTemplate(RTSObjectType.UnitCivil, civilitzation), coords, Quaternion.identity) as GameObject;
        civil.GetComponent<CivilUnit>().owner = artificialIntelligence;
        civils.Add(civil);

        numberCivils = numberCivils +1;
    }


    private void CreateNewWarrior()
    {

        coords = new Vector3(armyPos.x - 10, 0.4f, armyPos.z - 10 + z * 2);
        artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] - 100;

        GameObject civil = Instantiate(RTSObjectFactory.GetObjectTemplate(RTSObjectType.UnitWarrior, civilitzation), coords, Quaternion.identity) as GameObject;
        civil.GetComponent<Unit>().owner = artificialIntelligence;
        civil.AddComponent<ADS>();
        civil.AddComponent<MovimientoAleatorioCivil>();
        soldiers.Add(civil);

        numberWarrior = numberWarrior + 1;
        if (z >= 10)
        {
            z = 0;
        }
        else
        {
            z++;
        }
    }



    private void CreateNewArcher()
    {
        coords = new Vector3(armyPos.x - 15, 0.4f, armyPos.z - 10 + z * 2);
        artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] = artificialIntelligence.resourceAmounts[RTSObject.ResourceType.Food] - 170;

        GameObject civil = Instantiate(RTSObjectFactory.GetObjectTemplate(RTSObjectType.UnitArcher, civilitzation), coords, Quaternion.identity) as GameObject;
        civil.GetComponent<Unit>().owner = artificialIntelligence;
        civil.AddComponent<ADS>();
        civil.AddComponent<MovimientoAleatorioCivil>();
        archers.Add(civil);

        numberArcher = numberArcher + 1;
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
        civil.AddComponent<ADS>();
        civil.AddComponent<MovimientoAleatorioCivil>();
        cavalry.Add(civil);

        numberCavalry = numberCavalry + 1;
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
			civils[0].GetComponent<CivilUnit>().GetComponent<CitizenAI> ().AIState = 2;
                break;
            case 2:
			civils[0].GetComponent<CivilUnit>().GetComponent<CitizenAI> ().AIState = 2;
			civils[1].GetComponent<CivilUnit>().GetComponent<CitizenAI> ().AIState = 2;
                break;
            case 3:
			civils[0].GetComponent<CivilUnit>().GetComponent<CitizenAI> ().AIState = 2;
			civils[1].GetComponent<CivilUnit>().GetComponent<CitizenAI> ().AIState = 2;
            civils[2].GetComponent<CivilUnit>().GetComponent<CitizenAI> ().AIState = 1;
                break;
            case 4:
			civils[0].GetComponent<CivilUnit>().GetComponent<CitizenAI> ().AIState = 2;
			civils[1].GetComponent<CivilUnit>().GetComponent<CitizenAI> ().AIState = 2;
			civils[2].GetComponent<CivilUnit>().GetComponent<CitizenAI> ().AIState = 1;
                //civils[3].GetComponent<CivilUnit>().StartHarvest(null, true, "food");
                break;
            }
        }
   
}
