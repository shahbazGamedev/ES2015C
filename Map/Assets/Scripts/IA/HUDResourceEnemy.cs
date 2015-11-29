﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Shows the available quantity for a given resource on the HUD.
/// </summary>
public class HUDResourceEnemy : HUDElementEnemy
{
    /// <summary>
    /// The type of the resource to show the quantity of.
    /// </summary>
    public RTSObject.ResourceType resourceType;

    private Text textComponent;

    void Start()
    {
        textComponent = GetComponent<Text>();
        if (textComponent == null)
        {
            Debug.Log("Script of type " + GetType().Name + " without a Text component won't work.");
        }
    }

    /// <summary>
    /// Updates the quantity of the resource in the HUD.
    /// </summary>
    void Update()
    {
        if (textComponent == null)
            return;

        // TODO: Find the quantity of the resource in the player and update it
        textComponent.text = EnemyPlayer1.GetResourceAmount(resourceType).ToString();
    }
}