using UnityEngine;
using System.Collections;

public class PlaceResources : MonoBehaviour
{

    // Use this for initialization
    private int i, j, k; //s'utilitzen per recorrer el fors(PlaceForests)

    int width = 500;//mides del mapa
    int depth = 500;

    Vector3 coords;

    void Start()
    {
        PlaceRandomResources(70, "arbolYamato");
        PlaceGroup(10, 30, "arbolYamato");
    }

    // Update is called once per frame
    void Update()
    {
    }

    void PlaceRandomResources(int numberOfResources, string asset)
    {

        for (i = 0; i < numberOfResources; i++)
        {
            double randomDepth = Random.Range(0, depth);
            double randomWidth = Random.Range(0, width);
            coords = new Vector3((float)randomWidth, -1.6f, (float)randomDepth);
            if (Physics.CheckSphere(coords, 1))     //Comprova si hi ha algun altre objecte a on es crea l'arbre
            {
            }
            else
            {
                Instantiate(Resources.Load(asset, typeof(GameObject)), coords, Quaternion.identity); //Crear l'arbre
            }
            //Instantiate(Resources.Load(asset, typeof(GameObject)), coords, Quaternion.identity); //Crear l'arbre
        }
    }


    void PlaceGroup(int numGroups, int maxElementsInGroup, string PrefabResource)
    {

        for (i = 0; i < numGroups; i++) //Numero de boscos que tindra el mapa
        {
            double randomDepth = Random.Range(0, depth); //agafa un numero aleatori, dins de les mides del mapa
            double randomWidth = Random.Range(0, width);

            coords = new Vector3((float)randomWidth, -1.6f, (float)randomDepth); //Es creen les coordenades a partir dels numeros anteriors

            int numberOfElementsX = Random.Range(0, maxElementsInGroup);  //Numero aleatori entre 0 i el parametre(controla quans arbres i pot haver a un bosc)
            int numberOfElementsY = Random.Range(0, maxElementsInGroup);

            //Va reccorent per crear el bosc
            for (j = 0; j < numberOfElementsX; j++)
            {
                j = j + 2;// es per augmentar l'espai entre arbres
                for (k = 0; k < numberOfElementsY; k++)
                {
                    coords = new Vector3((float)randomWidth + j, -1.6f, (float)randomDepth + k);
                    if (coords.x < 500 && coords.z < 500)
                    { //controla que els arbres estiguin dins les mides del mapa
                        if (Physics.CheckSphere(coords, 1))
                        {
                        }
                        else
                        {
                            int prova = Random.Range(0,2);
                            if (prova!=0) {
                                Instantiate(Resources.Load(PrefabResource, typeof(GameObject)), coords, Quaternion.identity); //Crear l'arbre
                            }
                        }
                    }
                    k = k + 2;
                }
            }

        }

    }
}
