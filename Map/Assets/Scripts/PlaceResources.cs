using UnityEngine;
using System.Collections;
using Pathfinding;

public class PlaceResources : MonoBehaviour
{

    // Use this for initialization
    private int i, j, k; //s'utilitzen per recorrer el fors(PlaceForests)
    private float y;
    GameObject tree;


    Vector3 coords;

    void Start()
    {
        //Crides a la funció
        //tree
        //PlaceRandomResources(30, "arbolYamato",500,500);
        PlaceGroup(10, 30, "arbolYamato",500,500);
        PlaceResourcesFromXToZ("arbolYamato",500,500,130,130,150,150);
        PlaceResourcesFromXToZ("arbolYamato", 500, 500, 400, 420, 420, 430);

        PlaceRandomResources(30, "Bush", 500,500);
        //PlaceGroup(10, 30, "Bush",500,500);
        PlaceResourcesFromXToZ("Bush", 500,500,130,100,150,120);
        PlaceResourcesFromXToZ("Bush", 500, 500, 400, 470, 420, 480);
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void PlaceRandomResources(int numberOfResources, string asset,float width, float depth)//numero de recursos,prefab del recurs, amplada del mapa, profunditat del mapa
    {
        if (asset == "arbolYamato")
        {
            y = -1.6f;
        }
        else if (asset == "Bush")
        {
            y = 1.77f;
        }

        for (i = 0; i < numberOfResources; i++)
        {
            double randomDepth = Random.Range(0, depth);
            double randomWidth = Random.Range(0, width);
            coords = new Vector3((float)randomWidth, y, (float)randomDepth);
            if (!Physics.CheckSphere(coords, 1))     //Comprova si hi ha algun altre objecte a on es crea l'arbre
            {
				tree = (GameObject)Instantiate(Resources.Load("Prefabs/" + asset, typeof(GameObject)), coords, Quaternion.identity); //Crear l'arbre
                var guo = new GraphUpdateObject(tree.GetComponent<Collider>().bounds);
                guo.updatePhysics = true;
                AstarPath.active.UpdateGraphs(guo);
            }
        }
    }


    void PlaceGroup(int numGroups, int maxElementsInGroup, string asset, float width, float depth)
    {
        if(asset == "arbolYamato") {
            y = -1.6f;
        }
        else if(asset == "Bush") {
            y = 1.77f;
        }
        for (i = 0; i < numGroups; i++) //Numero de boscos que tindra el mapa
        {
            double randomDepth = Random.Range(0, depth); //agafa un numero aleatori, dins de les mides del mapa
            double randomWidth = Random.Range(0, width);

            coords = new Vector3((float)randomWidth, y, (float)randomDepth); //Es creen les coordenades a partir dels numeros anteriors

            int numberOfElementsX = Random.Range(0, maxElementsInGroup);  //Numero aleatori entre 0 i el parametre(controla quans arbres i pot haver a un bosc)
            int numberOfElementsY = Random.Range(0, maxElementsInGroup);

            //Va reccorent per crear el bosc
            for (j = 0; j < numberOfElementsX; j++)
            {
                j = j + 2;// es per augmentar l'espai entre arbres
                for (k = 0; k < numberOfElementsY; k++)
                {
                    coords = new Vector3((float)randomWidth + j, y, (float)randomDepth + k);
                    if (coords.x < 500 && coords.z < 500)
                    { //controla que els arbres estiguin dins les mides del mapa
                        if (!Physics.CheckSphere(coords, 1))
                        {
                            int placeTree = Random.Range(0, 2);  //Només posa un arbre si no surt el 0, es per que els boscos quedin més realistes
                            if (placeTree != 0)
                            {
								tree=(GameObject)Instantiate(Resources.Load("Prefabs/" + asset, typeof(GameObject)), coords, Quaternion.identity); //Crear l'arbre
                                var guo = new GraphUpdateObject(tree.GetComponent<Collider>().bounds);
                                guo.updatePhysics = true;
                                AstarPath.active.UpdateGraphs(guo);
                            }
                        }
                    }
                    k = k + 2;
                }
            }

        }

    }

    void PlaceResourcesFromXToZ( string asset, float width, float depth,float coordXStart, float coordZStart, float coordXEnd, float coordZEnd)//prefab del recurs, amplada del mapa, profunditat del mapa, i coordeandes d'inici i fi 
    {
        if (asset == "arbolYamato")
        {
            y = -1.6f;
        }
        else if (asset == "Bush")
        {
            y = 1.77f;
        }
        float coordX,coordZ;
        for (coordX = coordXStart; coordX < coordXEnd; coordX++) {
            for (coordZ=coordZStart; coordZ < coordZEnd; coordZ++) {
                int placeTree = Random.Range(0, 2);  //Només posa un arbre si no surt el 0, es per que els boscos quedin més realistes
                if (placeTree == 0)
                {
                    coords = new Vector3((float)coordX, y, (float)coordZ);
                    if (!Physics.CheckSphere(coords, 1))     //Comprova si hi ha algun altre objecte a on es crea l'arbre
                    {
						tree = (GameObject)Instantiate(Resources.Load("Prefabs/" + asset, typeof(GameObject)), coords, Quaternion.identity); //Crear l'arbre
                        var guo = new GraphUpdateObject(tree.GetComponent<Collider>().bounds);
                        guo.updatePhysics = true;
                        AstarPath.active.UpdateGraphs(guo);
                    }
                }
                coordZ=coordZ+2;
            }
            coordX=coordX + 2; 
        }
    }



}
