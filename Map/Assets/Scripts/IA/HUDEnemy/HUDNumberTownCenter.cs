using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;



/// <summary>
/// Shows the available quantity for a given resource on the HUD.
/// </summary>
public class HUDNumberTownCenter : MonoBehaviour
{
    /// <summary>
    /// The type of the resource to show the quantity of.
    /// </summary>
    public Player artificialIntelligence2;
    //public Player artificialIntelligenceaux;

    public int numberTownCenters2;

    
    Text textComponent;

    void Start()
    {
        //numberTownCenters2 = 100;
        AI aux = GetComponent<AI>();
        numberTownCenters2 = (int) aux.numberTownCenters;
        
        artificialIntelligence2 = GameObject.Find("EnemyPlayer1").GetComponent<Player>();
        //artificialIntelligence2 = aux.artificialIntelligence;

        textComponent = GetComponent<Text>();
        if (textComponent == null)
        {
            Debug.Log("Script of type " + GetType().Name + " without a Text component won't work.");
        }
    }

    void Awake () {
        textComponent = GetComponent <Text> ();

    }

    /// <summary>
    /// Updates the quantity of the resource in the HUD.
    /// </summary>
    void Update()
    {
        if (textComponent == null)
            return;

        // TODO: Find the quantity of the resource in the player and update it
        //textComponent.text = EnemyPlayer1.GetResourceAmount(resourceType).ToString();
        //int myInt = (int)Math.Ceiling(artificialIntelligence2.resourceAmounts[RTSObject.ResourceType.Wood]);

       
            
        //int aux = (int)artificialIntelligence2.resourceAmounts[RTSObject.ResourceType.Gold];
        //textComponent.text = (aux).ToString();
        
        /*AI aux = GetComponent<AI>();
        numberTownCenters2 = (int) aux.numberTownCenters;*/
        


        textComponent.text = "x" + (numberTownCenters2).ToString();

    }
}
