using UnityEngine;
using System.Collections;

public class ADS : RTSObject {

    RaycastHit hit;
    RTSObject targetlocal;
    enum aiState { wandering, chasing, attacking }
    aiState estado;
    int speed;
    float dist;

    // Use this for initialization
    void Start () {
        speed = 2;
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
        //transform.LookAt(target.transform);
        //transform.Translate(0, 0, 1 * Time.deltaTime * Speed);
    }

    public void Attack()
    {

        if (targetlocal != null)
        {
            GetComponent<MovimientoAleatorioCivil>().enabled = false;
            dist = Vector3.Distance(transform.position, targetlocal.transform.position);
            transform.LookAt(targetlocal.transform);
            if (dist > 2)
            {
                transform.Translate(0, 0, 1 * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(0, 0, 0);
                AttackObject(targetlocal);
            }            
        }

        if (targetlocal == null)
        {
            estado = aiState.wandering;
        }

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
