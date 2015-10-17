using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : RTSObject {

    public float maxBuildProgress = 10.0f;      // Maxim progres de construccio

    protected Vector3 spawnPoint;               // Punt de creacio de les unitats
    protected Queue<string> buildQueue;         // Cua de construccio del edifici

    private float currentBuildProgress = 0.0f;  // Progres actual de la construccio
    private bool needsBuilding = false;         // Indica si necesita se construit
	
	public Transform civil;
	public int mask = 129; //10000001 checks default and obstacles

    /*** Metodes per defecte de Unity ***/

    protected override void Awake()
    {
        base.Awake();
        spawnPoint = new Vector3(transform.position.x + 10, 0.0f, transform.position.z + 10);
        buildQueue = new Queue<string>();
        gameObject.layer = 10;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        ProcessBuildQueue();
    }

    protected override void OnGUI()
    {
        base.OnGUI();
        if (needsBuilding) DrawBuildProgress();
    }

    /*** Metodes publics ***/

    // Metode que obte el porcentatge de construccio actual
    public float getBuildPercentage()
    {
        return currentBuildProgress / maxBuildProgress;
    }

    // Metode que obte si esta en construccio
    public bool UnderConstruction()
    {
        return needsBuilding;
    }

    // Metode que va construint el edifici
    public void Construct(int amount)
    {
        hitPoints += amount;
        if (hitPoints >= maxHitPoints)
        {
            hitPoints = maxHitPoints;
            needsBuilding = false;
        }
    }

    /*** Metodes interns accessibles per les subclasses ***/

    // Metode per crear unitats
    protected void CreateUnit(string unitName)
    {
		bool spawned = false;
		int maximumSpawn = 5; //no podemos instanciar más de 5 unidades a la vez. Para instanciar más hay que mover las otras
		Vector3 point = spawnPoint; 
		
		if (unitName.Equals("CivilUnit")) {		
			while (spawned == false && maximumSpawn>0) {
				if (Physics.CheckSphere (point, 0.1f, mask)) {
					point = new Vector3(point.x + 10, 0.0f, point.z + 10); //si ya hay algo provamos en otra posicion
				} else {
					spawned = true;
					Instantiate(civil, point, Quaternion.identity);
				}
				maximumSpawn--;
			}
			 		
		}	
    }

    // Metode per administrar el progres de construccio de la cua
    protected void ProcessBuildQueue()
    {
    }

    /*** Metodes privats ***/

    // Dibuixa el progres de construccio
    private void DrawBuildProgress()
    {
    }
}
