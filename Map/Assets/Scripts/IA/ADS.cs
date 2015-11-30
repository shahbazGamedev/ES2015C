using UnityEngine;
using System.Collections;

public class ADS : RTSObject
{
    GameObject elnombrequequieras;
    RaycastHit hit;
    RTSObject targetlocal;
    enum aiState { wandering, chasing, attacking }
    aiState estado;
    int speed;
    float dist;
    ArrayList objetivosLocal;

    // Use this for initialization
    void Start()
    {
        speed = 2;
        hit = new RaycastHit();
        estado = aiState.wandering;
        this.owner = gameObject.GetComponent<RTSObject>().owner;
        GetComponent<RTSObject>().owner = GameObject.Find("EnemyPlayer1").GetComponent<Player>();
        elnombrequequieras = GameObject.Find("EnemyPlayer1");
        objetivosLocal = elnombrequequieras.GetComponent<Player>().objetivos;
    }

    // Update is called once per frame
    void Update()
    {

        detection();
        stateMachine();
        
        if (objetivosLocal.Count > 0) //se mira si hay objetivos o no
        {
            estado = aiState.attacking;
        }
        else
        {
            estado = aiState.wandering;
        }
    }

    public void Chase()
    {
        //transform.LookAt(target.transform);
        //transform.Translate(0, 0, 1 * Time.deltaTime * Speed);
    }

    public void Attack()
    {
        if (objetivosLocal.Count > 0) { 
            targetlocal = (RTSObject)objetivosLocal[0];
            transform.LookAt(targetlocal.transform);
            GetComponent<MovimientoAleatorioCivil>().enabled = false;
            dist = Vector3.Distance(transform.position, targetlocal.transform.position);
            if (targetlocal.GetComponent<RTSObject>().hitPoints == 0)
            {
                objetivosLocal.Remove(targetlocal);
            }
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
    }

    public void Wander()
    {
        GetComponent<MovimientoAleatorioCivil>().enabled = true;
    }

    public void detection()
    {
        //Debug.Log("VOY MIRANDO");
        if ((Physics.Raycast(transform.position, transform.position + new Vector3(0, 0, 40), out hit, 10)) || (Physics.Raycast(transform.position, transform.position + new Vector3(0, 0, -40), out hit, 10))
            || (Physics.Raycast(transform.position, transform.position + new Vector3(40, 0, 0), out hit, 10)) || (Physics.Raycast(transform.position, transform.position + new Vector3(-40, 0, 0), out hit, 10))
            || (Physics.Raycast(transform.position, transform.position + new Vector3(40, 0, 40), out hit, 10)) || (Physics.Raycast(transform.position, transform.position + new Vector3(40, 0, -40), out hit, 10))
            || (Physics.Raycast(transform.position, transform.position + new Vector3(-40, 0, 40), out hit, 10)) || (Physics.Raycast(transform.position, transform.position + new Vector3(-40, 0, -40), out hit, 10)))
        {
            if (hit.collider.gameObject.GetComponent<RTSObject>().owner != this.owner)
            { // compruebo si el owner es de otro equipo
                if (!objetivosLocal.Contains(hit.collider.gameObject.GetComponent<RTSObject>())) // compruebo si el objetivo ya esta en la lista 
                {
                    objetivosLocal.Add(hit.collider.gameObject.GetComponent<RTSObject>()); // añado el objetivo a la lista de objetivos
                    Debug.Log("HE DETECTADO UN OBJETO DE CLASE: " + hit.collider.tag);
                    Debug.Log("TENGO EL TARGET AÑADIDO-->" + targetlocal);
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
