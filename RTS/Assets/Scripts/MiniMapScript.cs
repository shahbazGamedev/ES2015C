using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MiniMapScript : MonoBehaviour {
    public GameObject[] trackedObjects;//Per posar els objectes que volem que es mostrin al minimapa
    List<GameObject> miniMapObjects; //Es objectes que estan dins del minimapa
    public GameObject redColor; //El que es mostrara al minimapa

    // Use this for initialization
    void Start () {
        createMiniMapObjects();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void createMiniMapObjects()
    {
        miniMapObjects = new List<GameObject>();    //Inicialització de la llista d'objectes que sortiran al minimapa
        foreach (GameObject o in trackedObjects)
        { //Anem recorrent la llista dels objectes que volem que sortin al minimapa
            GameObject k = Instantiate(redColor, o.transform.position, Quaternion.identity) as GameObject; //A la posicio de l'objecte hi afegim el color varmellS
            miniMapObjects.Add(k);
        }
    }
}
