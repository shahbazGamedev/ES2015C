using UnityEngine;
using System.Collections;

public class PlaceResources : MonoBehaviour {

    // Use this for initialization
    Vector3 position = new Vector3(95, 2.5f, 160f);
    Vector3 position2 = new Vector3(85, -1.650108f, 166.89f);
    void Start () {
        GameObject tree = Instantiate(Resources.Load("arbolYamato1", typeof(GameObject)), position, Quaternion.identity) as GameObject ;
        GameObject Tree2 = Instantiate(Resources.Load("arbolYamato1", typeof(GameObject)), position2, Quaternion.identity) as GameObject;

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
