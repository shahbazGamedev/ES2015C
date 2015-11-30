using UnityEngine;
using System.Collections;

public class ADS : Unit {

    public int Speed;
    public Vector3 currentDestination,a;
    
    Vector3 fwd;
    RaycastHit hit;
    private Vector3 previousPosition;


    //state setup
    public bool colision = false;
    enum aiState { wandering, chasing, attacking }
    aiState estado;

    // Use this for initialization
    void Start () {
        Speed = 2;
        currentDestination = new Vector3(0, 0, 0);
        hit = new RaycastHit();
        previousPosition = transform.position;
        estado = aiState.wandering;
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
        transform.Translate(0, 0, 0);
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
        if ((Physics.Raycast(transform.position, transform.position + new Vector3(0, 0, 15), out hit, 10)) || (Physics.Raycast(transform.position, transform.position + new Vector3(0, 0, -15), out hit, 10))
            || (Physics.Raycast(transform.position, transform.position + new Vector3(15, 0, 0), out hit, 10)) || (Physics.Raycast(transform.position, transform.position + new Vector3(-15, 0, 0), out hit, 10))
            || (Physics.Raycast(transform.position, transform.position + new Vector3(14, 0, 14), out hit, 10)) || (Physics.Raycast(transform.position, transform.position + new Vector3(14, 0, -14), out hit, 10))
            || (Physics.Raycast(transform.position, transform.position + new Vector3(-14, 0, 14), out hit, 10)) || (Physics.Raycast(transform.position, transform.position + new Vector3(-14, 0, -14), out hit, 10)))
        {
            //if(hit.collider.gameObject.GetComponent<RTSObject>().owner) { 

        Debug.Log("HE DETECTADO UN OBJETO DE CLASE: " + hit.collider.tag);

            //}
            //estado = aiState.attacking;
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
