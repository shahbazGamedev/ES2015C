using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MiniMapScript : MonoBehaviour {
    List<GameObject> trackedBlueObjects;//Per posar els objectes que volem que es mostrin al minimapa
    List<GameObject> trackedRedObjects;//Per posar els objectes que volem que es mostrin al minimapa
    List<GameObject> miniMapObjects; //Es objectes que estan dins del minimapa
    public GameObject redColor; //El que es mostrara al minimapa
    public GameObject blueColor; //El que es mostrara al minimapa
    private Vector3 unitPosition;
    private float nextActionTime = 0.0f;
    public float period = 0.5f;
    Vector3 pos = new Vector3(0, 0f, 0);

    // Use this for initialization
    public void Start() {
        Vector3 coords = new Vector3(114f, 0f, 134f);
		Transform civil = Instantiate(Resources.Load("Prefabs/Yamato_civil"), coords, Quaternion.identity) as Transform;
        //redColor = new GameObject();
        createMiniMapRed((civil));
        //addAllyGameObject((GameObject)o);

    }

    // Update is called once per frame
    void Update() {
    }



    void createMiniMapRed(Transform go)
    {
        GameObject redColor = Instantiate(Resources.Load("MinimapRed"),pos, Quaternion.identity) as GameObject;
        redColor.transform.parent = go;
    }

    void createMiniMapBlue(Transform go)
    {
        GameObject k = Instantiate(blueColor, go.transform.position, Quaternion.identity) as GameObject;
        //var go = Instantiate(prefabEffect) as GameObject;
        k.transform.parent = go.transform;
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
