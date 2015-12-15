using UnityEngine;
using System.Collections;

public class CitizenAI : AI {
    
    public int AIState; 
    public bool autoHarvest;
    Player citizenAI;
    public bool advanced;
    string civilName;
    
    // Use this for initialization
    void Start () {
        citizenAI = GameObject.Find("EnemyPlayer1").GetComponent<Player>();

        this.tag = "civil";
        civilName = GetComponent<CivilUnit> ().objectName;
        
        this.advanced = false;
        autoHarvest = false;
        // Desconectamos el script CitizenAI.cs para el Jugador Player (humano)
        if(gameObject.GetComponent<RTSObject>().owner.human){
            //Debug.Log ("SOY "+gameObject.name+", HUMAN ESTA: "+GetComponent<RTSObject>().owner.human+" Y CITIZEN_AI ESTA DISABLED: ");
            GetComponent<CitizenAI>().enabled = false;
        }else{
            //Debug.Log ("SOY "+gameObject.name+", HUMAN ESTA: "+GetComponent<RTSObject>().owner.human+" Y CITIZEN_AI ESTA ENABLED: ");
            GetComponent<CitizenAI>().enabled = true;
        }
        
        advanced = GetComponent<CivilUnit> ().objectName.Equals ("Hittite Civil Axe") ||
                GetComponent<CivilUnit> ().objectName.Equals ("Persian Civil Axe") ||
                GetComponent<CivilUnit> ().objectName.Equals ("Sumerian Civil Axe") ||
                GetComponent<CivilUnit> ().objectName.Equals ("Yamato Civil Axe") ||
                
                GetComponent<CivilUnit> ().objectName.Equals ("Hittite Civil Rack") ||
                GetComponent<CivilUnit> ().objectName.Equals ("Persian Civil Rack") ||
                GetComponent<CivilUnit> ().objectName.Equals ("Sumerian Civil Rack") ||
                GetComponent<CivilUnit> ().objectName.Equals ("Yamato Civil Rack") || 
                
                GetComponent<CivilUnit> ().objectName.Equals ("Hittite Civil Pick") ||
                GetComponent<CivilUnit> ().objectName.Equals ("Persian Civil Pick") ||
                GetComponent<CivilUnit> ().objectName.Equals ("Sumerian Civil Pick") ||
                GetComponent<CivilUnit> ().objectName.Equals ("Yamato Civil Pick"); 
        // Si se trata de un Civil Advanced, duplicamos la capacidad y la velocidad de recolección.
        if (advanced) {
            //Debug.Log("SOY "+gameObject.name+" y ME CONVIERTO A ADVANCED");
            GetComponent<CivilUnit> ().harvestingSpeed = 10;
            GetComponent<CivilUnit> ().capacity = 150.0f;
        } else {
            GetComponent<CivilUnit>().harvestingSpeed = 5;
            GetComponent<CivilUnit> ().capacity = 50.0f;
        }
        
        this.AIState = 0;
    }
    // Update is called once per frame
    void Update () {
        switch (this.AIState) {
        case 0: 
            GetComponent<CitizenAI>().enabled = true;
            autoHarvest = true;
            FreeHarvesting();
            this.AIState = 99; // Si yo te contara...
            break;
        case 1:
            autoHarvest = false;
            GetWood();
            this.AIState = 11; // no preguntes...
            break;
        case 2:
            autoHarvest = false;
            GetFood();          
            this.AIState = 22; // Sigue sin preguntar
            break;
        case 3:
            autoHarvest = false;
            GetGold();          
            this.AIState = 33; // Que te he dicho que no preguntes
            break;
        case 4: // Desactiva el script CitizenAI para iniciar la construcción.

            autoHarvest = false;
            GetComponent<CivilUnit>().StopHarvest();
            GetComponent<CitizenAI>().enabled = false; 
            this.AIState = 44; // No insistas

            break;
        }

        if (advanced) {
            //Debug.Log("SOY "+gameObject.name+" y ME CONVIERTO A ADVANCED");
            GetComponent<CivilUnit> ().harvestingSpeed = 15;
            GetComponent<CivilUnit> ().capacity = 150.0f;
        } else {
            GetComponent<CivilUnit>().harvestingSpeed = 5;
            GetComponent<CivilUnit> ().capacity = 50.0f;
        }
    }
    
    public void GetWood(){
        gameObject.GetComponent<CivilUnit>().StartHarvest (null, "wood");
        //Debug.Log ("SOY "+gameObject.GetComponent<CivilUnit>().name + " Y VOY A OBTENER MADERA");


    }
    public void GetFood(){
        //Debug.Log ("SOY "+gameObject.GetComponent<CivilUnit>().name + " Y VOY A OBTENER COMIDA");
        gameObject.GetComponent<CivilUnit>().StartHarvest (null, "food");
    }
    public void GetGold(){
        //Debug.Log ("SOY "+gameObject.GetComponent<CivilUnit>().name + " Y VOY A OBTENER ORO");
        gameObject.GetComponent<CivilUnit>().StartHarvest (null, "gold");
    }
    
    public void FreeHarvesting(){
        
        if (citizenAI.resourceAmounts [RTSObject.ResourceType.Wood] < 2000 && autoHarvest) {
            //Debug.Log("FALTA MADERA");
            gameObject.GetComponent<CivilUnit> ().StartHarvest (null, "wood");
        } else if (citizenAI.resourceAmounts [RTSObject.ResourceType.Wood] >= 2000 && 
                   citizenAI.resourceAmounts [RTSObject.ResourceType.Food] < 2000 && autoHarvest) {
            //Debug.Log("FALTA COMIDA");
            gameObject.GetComponent<CivilUnit> ().StartHarvest (null, "food");
        } else if (citizenAI.resourceAmounts [RTSObject.ResourceType.Wood] >= 2000 && 
                   citizenAI.resourceAmounts [RTSObject.ResourceType.Food] >= 2000 &&
                   citizenAI.resourceAmounts [RTSObject.ResourceType.Gold] < 2000 && autoHarvest) {
            //Debug.Log("FALTA ORO");
            gameObject.GetComponent<CivilUnit> ().StartHarvest (null, "gold");
        } else {
            this.AIState = 4;
        }
    }
}


