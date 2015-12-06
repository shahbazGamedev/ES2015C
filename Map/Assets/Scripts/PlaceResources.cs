using UnityEngine;
using System.Collections;
using Pathfinding;

public class PlaceResources : MonoBehaviour
{
	private Terrain terrain;
	private float terrainWidth; 		// terrain size (x)
	private float terrainLength; 		// terrain size (z)
	private float terrainPosX; 			// terrain position x
	private float terrainPosZ; 			// terrain position z


    Vector3 coords;

    void Start()
    {
		terrain = Terrain.activeTerrain;
		terrainWidth = terrain.terrainData.size.x;
		terrainLength = terrain.terrainData.size.z;
		terrainPosX = terrain.transform.position.x;
		terrainPosZ = terrain.transform.position.z;

        PlaceRandomResources(50, "arbolYamato");
		PlaceGroup(20, 20, "arbolYamato");
		//PlaceResourcesFromXToZ("arbolYamato", 130, 130, 150, 150);
		//PlaceResourcesFromXToZ("arbolYamato", 400, 420, 420, 430);

        PlaceRandomResources(30, "Bush");
        PlaceGroup(20, 10, "Bush");
		//PlaceResourcesFromXToZ("Bush", 130, 100, 150, 120);
		//PlaceResourcesFromXToZ("Bush", 400, 470, 420, 480);

		PlaceRandomResources(40, "Goldmine");

    }

    private void PlaceRandomResources(int numberOfResources, string asset)
    {
        for (int i = 0; i < numberOfResources; i++)
        {
			Vector3 coords = getRandomMapPosition();
			while (Physics.CheckSphere(coords, 1, 9) || coords.y > 1){
				coords = getRandomMapPosition();
			}
			instatiateObject(asset, coords);
        }
    }


	private void PlaceGroup(int numGroups, int maxElementsInGroup, string asset)
    {
		for (int i = 0; i < numGroups; i++)
        {
			Vector3 iniCoords = getRandomMapPosition();
			while (Physics.CheckSphere(iniCoords, 1, 9) || iniCoords.y > 1){
				iniCoords = getRandomMapPosition();
			}

			//Numero aleatori entre 0 i el parametre
            int numberOfElementsX = Random.Range(0, maxElementsInGroup);
            int numberOfElementsY = Random.Range(0, maxElementsInGroup);

            //Va reccorent per crear el bosc
            for (int j = 0; j < numberOfElementsX; j++)
            {
                j = j + 2;// es per augmentar l'espai entre arbres
				for (int k = 0; k < numberOfElementsY; k++)
                {
					Vector3 coords = new Vector3(iniCoords.x + j, iniCoords.y, iniCoords.z + k);
					//controla que els arbres estiguin dins les mides del mapa
					if (coords.x < terrain.terrainData.size.x && coords.z < terrain.terrainData.size.z)
                    {
						if (!Physics.CheckSphere(coords, 1, 9) && coords.y < 1)
                        {
                            int placeTree = Random.Range(0, 2);  //Només posa un arbre si no surt el 0, es per que els boscos quedin més realistes
                            if (placeTree != 0)
                            {
								instatiateObject(asset, coords);
                            }
                        }
                    }
                    k = k + 2;
                }
            }
        }
    }

	private Vector3 getRandomMapPosition()
	{
		float posx = Random.Range (terrainPosX, terrainPosX + terrainWidth);
		float posz = Random.Range (terrainPosZ, terrainPosZ + terrainLength);
		Vector3 tmppos = new Vector3 (posx, 0, posz);
		float posy = terrain.SampleHeight (tmppos);
		return new Vector3 (posx, posy, posz);
	}

	private void instatiateObject(string asset, Vector3 coords){
		GameObject resource = (GameObject)Instantiate(Resources.Load("Prefabs/" + asset, typeof(GameObject)), coords, Quaternion.identity); //Crear l'arbre
		if (GameObject.Find ("Map")) {
			resource.transform.parent = GameObject.Find ("Map").transform;
		}
		var guo = new GraphUpdateObject(resource.GetComponent<Collider>().bounds);
		guo.updatePhysics = true;
		AstarPath.active.UpdateGraphs(guo);
	}

	private void PlaceResourcesFromXToZ(string asset, float coordXStart, float coordZStart, float coordXEnd, float coordZEnd)//prefab del recurs, amplada del mapa, profunditat del mapa, i coordeandes d'inici i fi 
    {
        float coordX, coordZ;
        for (coordX = coordXStart; coordX < coordXEnd; coordX++)
        {
            for (coordZ = coordZStart; coordZ < coordZEnd; coordZ++)
            {
                int placeTree = Random.Range(0, 2);  //Només posa un arbre si no surt el 0, es per que els boscos quedin més realistes
                if (placeTree == 0)
                {
                    coords = new Vector3((float)coordX, 0, (float)coordZ);
					if (!Physics.CheckSphere(coords, 1, 9) && coords.y < 1)     //Comprova si hi ha algun altre objecte a on es crea l'arbre
                    {
						instatiateObject(asset, coords);
                    }
                }
                coordZ = coordZ + 2;
            }
            coordX = coordX + 2;
        }
    }



}