using UnityEngine;
using System.Collections;

public class ADS : RTSObject {

    public int Speed;
    RaycastHit hit;
    RTSObject targetlocal;
    //state setup
    public bool colision = false;
    enum aiState { wandering, chasing, attacking }
    aiState estado;

    // Use this for initialization
    void Start () {
        Speed = 2;
        hit = new RaycastHit();
        estado = aiState.wandering;
        this.owner = gameObject.GetComponent<RTSObject>().owner;
        GetComponent<RTSObject>().owner = GameObject.Find("EnemyPlayer1").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update () {

        stateMachine();

    }

    public void Chase() {
        //transform.LookAt(currentDestination);
        //transform.LookAt(target.transform);
        //transform.Translate(0, 0, 1 * Time.deltaTime * Speed);
    }

    public void Attack()
    {
        GetComponent<MovimientoAleatorioCivil>().enabled = false;
        //transform.Translate(0, 0, 0);
        if (targetlocal != null)
        {
            AttackObject(targetlocal);
        }
        targetlocal = null;

        estado = aiState.wandering;

        //Pendiente de poner la logica de ataque.
    }

    public void Wander()
    {
        GetComponent<MovimientoAleatorioCivil>().enabled = true;
        detection();
    }

    public void detection()
    {
        //Debug.Log("VOY MIRANDO");
        if ((Physics.Raycast(transform.position, transform.position + new Vector3(0, 0, 40), out hit, 10)) || (Physics.Raycast(transform.position, transform.position + new Vector3(0, 0, -40), out hit, 10))
            || (Physics.Raycast(transform.position, transform.position + new Vector3(40, 0, 0), out hit, 10)) || (Physics.Raycast(transform.position, transform.position + new Vector3(-40, 0, 0), out hit, 10))
            || (Physics.Raycast(transform.position, transform.position + new Vector3(40, 0, 40), out hit, 10)) || (Physics.Raycast(transform.position, transform.position + new Vector3(40, 0, -40), out hit, 10))
            || (Physics.Raycast(transform.position, transform.position + new Vector3(-40, 0, 40), out hit, 10)) || (Physics.Raycast(transform.position, transform.position + new Vector3(-40, 0, -40), out hit, 10)))
        {
            if(hit.collider.gameObject.GetComponent<RTSObject>().owner != this.owner) { 
                targetlocal = hit.collider.gameObject.GetComponent<RTSObject>();
                estado = aiState.attacking;

                Debug.Log("HE DETECTADO UN OBJETO DE CLASE: " + hit.collider.tag);
                Debug.Log("TENGO EL TARGET Y VOY A ATACAR -->" + targetlocal + "i");
            }
        }
    }
    
    public void stateMachine() { 
        switch (estado)
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
