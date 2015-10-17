using UnityEngine;
using System.Collections;

public class PlaceResources : MonoBehaviour {

    // Use this for initialization
    //Vector3 position = new Vector3(95, 2.5f, 160f);
    //Vector3 position2 = new Vector3(85, -1.650108f, 166.89f);
    private int i;
    int width = 500;
    int depth = 500;
    Vector3 coords;
    //double rDouble = r.NextDouble() * range;
    void Start () {
        PlaceRandomTrees(100,"arbolYamato1");
        //
        //

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void PlaceRandomTrees(int numberOfAsset,string asset) {
        for (i=0;i<numberOfAsset;i++) {
            double randomDepth = Random.Range(0, depth);
            double randomWidth = Random.Range(0, width);
            coords = new Vector3((float)randomWidth,-1.6f,(float)randomDepth);
            Instantiate(Resources.Load(asset, typeof(GameObject)), coords, Quaternion.identity);

        }
    }
}
