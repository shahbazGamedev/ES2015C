using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class HUDNumberElementsofType : HUDElementEnemy
{

    //public Player artificialIntelligence;
    public PlayerCivilization civilitzation;
    public Text textComponent;

    // Use this for initialization
    //GameObject prefab;
    void Start () {
        GameObject menu;
        //artificialIntelligence = GameObject.Find("EnemyPlayer1").GetComponent<Player>();
        civilitzation = GameObject.Find("EnemyPlayer1").GetComponent<Player>().civilization;
    }

    void Awake () {
        textComponent = GetComponent <Text> ();

    }

    void Update() {
        //int aux = (int)artificialIntelligence2.resourceAmounts[RTSObject.ResourceType.Food];

        if (textComponent == null)
            return;
        if (civilitzation.ToString() == "Hittites") {
            textComponent.text = "Hittites";
        }
        else if (civilitzation.ToString() == "Persians") {
            textComponent.text = "Persians";
        }
        else if (civilitzation.ToString() == "Sumerians") {
            textComponent.text = "Sumerians";
        }
        else if (civilitzation.ToString() == "Yamato") {
            textComponent.text = "Yamato";
        }
        else {
            textComponent.text = "Error";
        }


        /*
         Hittites,
         Persians,
         Sumerians,
         Yamato
        */
        
    }
}