using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MiniMapScript : MonoBehaviour {
    List <GameObject> trackedBlueObjects;//Per posar els objectes que volem que es mostrin al minimapa
    List<GameObject> trackedRedObjects;//Per posar els objectes que volem que es mostrin al minimapa
    List<GameObject> miniMapObjects; //Es objectes que estan dins del minimapa
    public GameObject redColor; //El que es mostrara al minimapa
    public GameObject blueColor; //El que es mostrara al minimapa
    public GameObject cylinder;//testeig

    // Use this for initialization
    void Start () {
        trackedBlueObjects = new List<GameObject>();
        trackedRedObjects = new List<GameObject>();

        addEnemyGameObject(cylinder);//testeig
        createMiniMapObjects();

    }
	
	// Update is called once per frame
	void Update () {
    }

    void createMiniMapObjects()
    {
        miniMapObjects = new List<GameObject>();    //Inicialització de la llista d'objectes que sortiran al minimapa
      

        foreach (GameObject o in trackedRedObjects)
        { //Anem recorrent la llista dels objectes que volem que sortin al minimapa
            GameObject k = Instantiate(redColor, o.transform.position, Quaternion.identity) as GameObject; //A la posicio de l'objecte hi afegim el color varmellS
            miniMapObjects.Add(k);
        }

        foreach (GameObject o in trackedBlueObjects)
        { //Anem recorrent la llista dels objectes que volem que sortin al minimapa
            GameObject k = Instantiate(blueColor, o.transform.position, Quaternion.identity) as GameObject; //A la posicio de l'objecte hi afegim el color varmellS
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

    void UpdateMap() {
        foreach (GameObject o in trackedRedObjects)
        { //Anem recorrent la llista dels objectes que volem que sortin al minimapa
            GameObject k = Instantiate(redColor, o.transform.position, Quaternion.identity) as GameObject; //A la posicio de l'objecte hi afegim el color varmellS
            miniMapObjects.Remove(k);
        }

        foreach (GameObject o in trackedBlueObjects)
        { //Anem recorrent la llista dels objectes que volem que sortin al minimapa
            GameObject k = Instantiate(blueColor, o.transform.position, Quaternion.identity) as GameObject; //A la posicio de l'objecte hi afegim el color varmellS
            miniMapObjects.Remove(k);
        }


        foreach (GameObject o in trackedRedObjects)
        { //Anem recorrent la llista dels objectes que volem que sortin al minimapa
            GameObject k = Instantiate(redColor, o.transform.position, Quaternion.identity) as GameObject; //A la posicio de l'objecte hi afegim el color varmellS
            miniMapObjects.Add(k);
        }

        foreach (GameObject o in trackedBlueObjects)
        { //Anem recorrent la llista dels objectes que volem que sortin al minimapa
            GameObject k = Instantiate(blueColor, o.transform.position, Quaternion.identity) as GameObject; //A la posicio de l'objecte hi afegim el color varmellS
            miniMapObjects.Add(k);
        }
    }
}
