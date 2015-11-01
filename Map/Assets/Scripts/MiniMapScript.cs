using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MiniMapScript : MonoBehaviour {
    List<GameObject> trackedBlueObjects;//Per posar els objectes que volem que es mostrin al minimapa
    List<GameObject> trackedRedObjects;//Per posar els objectes que volem que es mostrin al minimapa
    List<GameObject> miniMapObjects; //Es objectes que estan dins del minimapa
    public GameObject redColor; //El que es mostrara al minimapa
    public GameObject blueColor; //El que es mostrara al minimapa
    public GameObject cylinder;//testeig
    private Vector3 unitPosition;
    private float nextActionTime = 0.0f;
    public float period = 0.5f;

    // Use this for initialization
    public void Start() {
        miniMapObjects = new List<GameObject>();
        trackedBlueObjects = new List<GameObject>();
        trackedRedObjects = new List<GameObject>();
        addAllyGameObject(cylinder);//testeig
        /*Vector3 coords = new Vector3((float)120f, 0f, (float)150f);
        Object o = Instantiate(Resources.Load("yamato_civil", typeof(GameObject)), coords, Quaternion.identity);
        addAllyGameObject((GameObject)o);*/
        createMiniMapObjects();

    }

    // Update is called once per frame
    void Update() {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            UpdateMap();
        }
       // UpdateMap();
    }

    void createMiniMapObjects()
    {

        foreach (GameObject o in trackedRedObjects)
        { //Anem recorrent la llista dels objectes que volem que sortin al minimapa
            GameObject k = Instantiate(redColor, o.transform.position, Quaternion.identity) as GameObject; //A la posicio de l'objecte hi afegim el color vermellS
            miniMapObjects.Add(k);
        }

        foreach (GameObject o in trackedBlueObjects)
        { //Anem recorrent la llista dels objectes que volem que sortin al minimapa
            GameObject k = Instantiate(blueColor, o.transform.position, Quaternion.identity) as GameObject; //A la posicio de l'objecte hi afegim el color blau
            miniMapObjects.Add(k);
        }

    }

    void addEnemyGameObject(GameObject go) {
        trackedRedObjects.Add(go);
    }

    void deleteEnemyGameObject(GameObject go) {
        trackedRedObjects.Remove(go);
    }

    void addAllyGameObject(GameObject go)
    {
        trackedBlueObjects.Add(go);
    }

    void deleteAllyGameObject(GameObject go)
    {
        trackedBlueObjects.Remove(go);
    }


    public void UpdateMap() {
        foreach (GameObject o in miniMapObjects) {
            Destroy(o);    
        }
        
        foreach (GameObject o in trackedRedObjects)
        { //Anem recorrent la llista dels objectes que volem que sortin al minimapa
            GameObject k = Instantiate(redColor, o.transform.position, Quaternion.identity) as GameObject; //A la posicio de l'objecte hi afegim el color vermellS
            miniMapObjects.Add(k);
        }

        foreach (GameObject o in trackedBlueObjects)
        { //Anem recorrent la llista dels objectes que volem que sortin al minimapa
            GameObject k = Instantiate(blueColor, o.transform.position, Quaternion.identity) as GameObject; //A la posicio de l'objecte hi afegim el color blau
            miniMapObjects.Add(k);
        }
    }
}
