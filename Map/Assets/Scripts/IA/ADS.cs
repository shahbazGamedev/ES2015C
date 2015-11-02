using UnityEngine;
using System.Collections;

public class ADS : MilitaryUnit {

    public int Speed = 2;
    public Vector3 currentDestination = new Vector3(0,0,0);

    //state setup
    public bool colision = false;
    enum aiState { wandering, chasing, attacking }
    aiState estado;

    // Use this for initialization
    void Start () {


    }

    // Update is called once per frame
    void Update () {

        if (colision){
            estado = aiState.chasing;
        }else{
            // patrullar
        }
        StateMachine();

    }

    public void StateMachine(){
        switch (estado) {
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

    public void Chase() {
        transform.LookAt(currentDestination);
        //transform.LookAt(target.transform);
        transform.Translate(0, 0, 1 * Time.deltaTime * Speed);
    }

    public void Attack()
    {

    }

    public void Wander()
    {

    }


}
