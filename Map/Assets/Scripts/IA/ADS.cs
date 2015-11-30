using UnityEngine;
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
        speed = 2;
        hit = new RaycastHit();
        estado = aiState.wandering;
        GetComponent<RTSObject>().owner = GameObject.Find("EnemyPlayer1").GetComponent<Player>();
        enlace = GameObject.Find("EnemyPlayer1");
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

    public void Attack()
    {
        if (enlace.GetComponent<Player>().objetivos.Count > 0) { 
            targetlocal = (RTSObject)enlace.GetComponent<Player>().objetivos[0];
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

    public void Wander()
    {
        GetComponent<MovimientoAleatorioCivil>().enabled = true; // Activo el movimiento aleatorio
    }

    public void detection()
    {
        //Debug.Log("VOY MIRANDO");
        if ((Physics.Raycast(transform.position, transform.position + new Vector3(0, 0, 40), out hit, 10)) || (Physics.Raycast(transform.position, transform.position + new Vector3(0, 0, -40), out hit, 10))
             || (Physics.Raycast(transform.position, transform.position + new Vector3(40, 0, 0), out hit, 10)) || (Physics.Raycast(transform.position, transform.position + new Vector3(-40, 0, 0), out hit, 10))
             || (Physics.Raycast(transform.position, transform.position + new Vector3(40, 0, 40), out hit, 10)) || (Physics.Raycast(transform.position, transform.position + new Vector3(40, 0, -40), out hit, 10))
             || (Physics.Raycast(transform.position, transform.position + new Vector3(-40, 0, 40), out hit, 10)) || (Physics.Raycast(transform.position, transform.position + new Vector3(-40, 0, -40), out hit, 10)))
         {
            if (hit.collider.gameObject.GetComponent<RTSObject>().owner != GetComponent<RTSObject>().owner)
            { // compruebo si el owner es de otro equipo
                if (!enlace.GetComponent<Player>().objetivos.Contains(hit.collider.gameObject.GetComponent<RTSObject>())) // compruebo si el objetivo ya esta en la lista 
                {
                    enlace.GetComponent<Player>().objetivos.Add(hit.collider.gameObject.GetComponent<RTSObject>()); // añado el objetivo a la lista de objetivos
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
