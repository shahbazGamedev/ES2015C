using UnityEngine;
using System.Collections;
using System;

public class CitizenAI : MonoBehaviour
{

    public int AIState;
    public bool autoHarvest;
    Player citizenAI;
    public bool advanced;
    string civilName;

    RaycastHit hit;
    float distancia;
    RTSObject targetlocal;
    public Animator anim;

    // Use this for initialization
    void Start()
    {
        citizenAI = GameObject.Find("EnemyPlayer1").GetComponent<Player>();
        this.tag = "civil";
        civilName = GetComponent<CivilUnit>().objectName;

        hit = new RaycastHit();
        targetlocal = null;
        anim = gameObject.GetComponent<Animator>();
        distancia = 0f;

        this.advanced = false;
        autoHarvest = false;
        // Desconectamos el script CitizenAI.cs para el Jugador Player (humano)
        if (gameObject.GetComponent<RTSObject>().owner.human)
        {
            Debug.Log("SOY " + gameObject.name + ", HUMAN ESTA: " + GetComponent<RTSObject>().owner.human + " Y CITIZEN_AI ESTA DISABLED: ");
            GetComponent<CitizenAI>().enabled = false;
        }
        else
        {
            Debug.Log("SOY " + gameObject.name + ", HUMAN ESTA: " + GetComponent<RTSObject>().owner.human + " Y CITIZEN_AI ESTA ENABLED: ");
            GetComponent<CitizenAI>().enabled = true;
        }

        advanced = GetComponent<CivilUnit>().objectName.Equals("Hittite Civil Axe") ||
                GetComponent<CivilUnit>().objectName.Equals("Persian Civil Axe") ||
                GetComponent<CivilUnit>().objectName.Equals("Sumerian Civil Axe") ||
                GetComponent<CivilUnit>().objectName.Equals("Yamato Civil Axe") ||

                GetComponent<CivilUnit>().objectName.Equals("Hittite Civil Rack") ||
                GetComponent<CivilUnit>().objectName.Equals("Persian Civil Rack") ||
                GetComponent<CivilUnit>().objectName.Equals("Sumerian Civil Rack") ||
                GetComponent<CivilUnit>().objectName.Equals("Yamato Civil Rack") ||

                GetComponent<CivilUnit>().objectName.Equals("Hittite Civil Pick") ||
                GetComponent<CivilUnit>().objectName.Equals("Persian Civil Pick") ||
                GetComponent<CivilUnit>().objectName.Equals("Sumerian Civil Pick") ||
                GetComponent<CivilUnit>().objectName.Equals("Yamato Civil Pick");
        // Si se trata de un Civil Advanced, duplicamos la capacidad y la velocidad de recolección.
        if (advanced)
        {
            Debug.Log("SOY " + gameObject.name + " y ME CONVIERTO A ADVANCED");
            //GetComponent<CivilUnit>().harvestingSpeed = 10; ******************************
            GetComponent<CivilUnit>().capacity = 100.0f;
        }
        else
        {
            //GetComponent<CivilUnit>().harvestingSpeed = 5; ******************************
            GetComponent<CivilUnit>().capacity = 50.0f;
        }

        this.AIState = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Physics.SphereCast(transform.position, 15f, transform.forward, out hit, 20)) // sistema de deteccion en esfera
        {
            try
            {
                if (hit.collider != null && hit.collider.gameObject.GetComponent<RTSObject>().owner.human == true && hit.collider.tag == "mility")
                {
                    targetlocal = hit.collider.gameObject.GetComponent<RTSObject>();
                }
            }
            catch (NullReferenceException e)
            {
            }
        }

        if (targetlocal != null)
        {
            
            distancia = Vector3.Distance(transform.position, targetlocal.transform.position);
            if (distancia < 40 && distancia > 0 && targetlocal != null)
            {
                //transform.LookAt(targetlocal.transform);
                anim.SetBool("IsWalking", true);
                transform.Translate(-targetlocal.transform.position * 3 * Time.deltaTime);
            }
            else
            {
                targetlocal = null;
            }
        }
        else {

            switch (this.AIState)
            {
                case 0:
                    /*
                    GetComponent<CitizenAI>().enabled = true;
                    autoHarvest = true;
                    FreeHarvesting();
                    */
                    break;
                case 1:
                    autoHarvest = false;
                    GetWood();
                    break;
                case 2:

                    autoHarvest = false;
                    GetFood();

                    break;


                case 3:

                    autoHarvest = false;
                    GetGold();

                    break;
                case 4: // Desactiva el script CitizenAI para iniciar la construcción.
                        /*
                        autoHarvest = false;
                        GetComponent<CivilUnit>().StopHarvest();
                        GetComponent<CitizenAI>().enabled = false; 
                        */
                    break;
            }
        }
    }

    public void GetWood()
    {
        //Debug.Log ("VOY A OBTENER MADERA");
        //GetComponent<CivilUnit>().StartHarvest(null, "wood"); ******************************
    }
    public void GetFood()
    {
        //Debug.Log ("VOY A OBTENER COMIDA");

        //GetComponent<CivilUnit>().StartHarvest(null, "food"); ******************************
    }
    public void GetGold()
    {
        //Debug.Log ("VOY A OBTENER ORO");

        //GetComponent<CivilUnit>().StartHarvest(null, "gold"); ******************************
    }

    public void FreeHarvesting()
    {

        if (citizenAI.resourceAmounts[RTSObject.ResourceType.Wood] < 2000 && autoHarvest)
        {
            Debug.Log("FALTA MADERA");
            //gameObject.GetComponent<CivilUnit>().StartHarvest(null, "wood"); ******************************
        }
        else if (citizenAI.resourceAmounts[RTSObject.ResourceType.Wood] >= 2000 &&
                 citizenAI.resourceAmounts[RTSObject.ResourceType.Food] < 2000 && autoHarvest)
        {
            Debug.Log("FALTA COMIDA");
            //gameObject.GetComponent<CivilUnit>().StartHarvest(null, "food"); ******************************
        }
        else if (citizenAI.resourceAmounts[RTSObject.ResourceType.Wood] >= 2000 &&
                 citizenAI.resourceAmounts[RTSObject.ResourceType.Food] >= 2000 &&
                 citizenAI.resourceAmounts[RTSObject.ResourceType.Gold] < 2000 && autoHarvest)
        {
            Debug.Log("FALTA ORO");
            //gameObject.GetComponent<CivilUnit>().StartHarvest(null, "gold"); ******************************
        }
        else
        {
            this.AIState = 4;
        }
    }
}


