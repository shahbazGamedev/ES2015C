﻿using UnityEngine;
using System.Collections;

public class ADS : MonoBehaviour
{
    GameObject enlace;
    RaycastHit hit;
    RTSObject targetlocal;
    enum aiState { wandering, chasing, attacking }
    aiState estado;
    int speed;
    float dist;

    // Use this for initialization
    void Start()
    {
        if (!this.gameObject.GetComponent<RTSObject>().owner.human) {  //  Se comprueba si el propietario es el jugador(human)
            GetComponent<MovimientoAleatorioCivil>().enabled = true;
            GetComponent<ADS>().enabled = true;
            speed = 2;
            hit = new RaycastHit();
            estado = aiState.wandering;
            enlace = GameObject.Find("EnemyPlayer1");
        }
        else {
            GetComponent<MovimientoAleatorioCivil>().enabled = false;
            GetComponent<ADS>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        detection(); //funcion de deteccion del entorno
        stateMachine(); //funcion de maquina de estados
        
        if (enlace.GetComponent<Player>().objetivos.Count > 0) //se mira si hay objetivos o no
        {
            estado = aiState.attacking; //estado de atacar
        }
        else
        {
            estado = aiState.wandering; //estado de vagar por el mapa
        }
    }

    public void Chase()
    {
        //transform.LookAt(target.transform);
        //transform.Translate(0, 0, 1 * Time.deltaTime * Speed);
    }

    public void Attack() // funcion que controla el ataque
    {
        if (enlace.GetComponent<Player>().objetivos.Count > 0) {
            targetlocal = (RTSObject)enlace.GetComponent<Player>().objetivos[0];
            if (targetlocal != null) { 
                transform.LookAt(targetlocal.transform);
                GetComponent<MovimientoAleatorioCivil>().enabled = false; // desactivo el movimiento aleatorio
                dist = Vector3.Distance(transform.position, targetlocal.transform.position);
                if (targetlocal.GetComponent<RTSObject>().hitPoints == 0)
                {
                    enlace.GetComponent<Player>().objetivos.Remove(targetlocal);
                }
                if (dist > 2)
                {
                    transform.Translate(0, 0, 1 * speed * Time.deltaTime);
                }
                else
                {
                    transform.Translate(0, 0, 0);
                    GetComponent<RTSObject>().AttackObject(targetlocal);
                }
            }
        }
    }

    public void Wander()
    {
        GetComponent<MovimientoAleatorioCivil>().enabled = true; // Activo el movimiento aleatorio
    }

    public void detection()
    {
        //Debug.Log("VOY MIRANDO");
        if (Physics.SphereCast(transform.position, 15f, transform.forward, out hit, 20)) // sistema de deteccion en esfera
        { 
            if (hit.collider != null && hit.collider.gameObject.GetComponent<RTSObject>().owner.human)
            { 
                if (!enlace.GetComponent<Player>().objetivos.Contains(hit.collider.gameObject.GetComponent<RTSObject>())) // compruebo si el objetivo ya esta en la lista de objetivos
                {
                    enlace.GetComponent<Player>().objetivos.Add(hit.collider.gameObject.GetComponent<RTSObject>()); // añado el objetivo a la lista de objetivos
                    //Debug.Log("HE DETECTADO UN OBJETO DE CLASE: " + hit.collider.tag);
                    //Debug.Log("TENGO EL TARGET AÑADIDO-->" + targetlocal);
                }
            }
        }
    }

    public void stateMachine()
    {
        switch (estado) // maquina de estados
        {
            case aiState.wandering:
                Wander();
                break;
            case aiState.chasing:
                Chase();
                break;
            case aiState.attacking:
                Attack();
                break;
        }
    }
}
