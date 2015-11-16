using UnityEngine;
using System.Collections;

public class AIResources : MonoBehaviour{
	public float wood;
	public float food;
	public float gold;
	
	public AIResources (){
		
		this.wood = 1000;
		this.food = 1000;
		this.gold = 1000;
	}

	void Start(){

        this.wood = 1000;
        this.food = 1000;
        this.gold = 1000;

    }
	void Update(){}
}

