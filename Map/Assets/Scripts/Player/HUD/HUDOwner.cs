﻿using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Behaviour to display the name of the player who owns a given RTSObject.
/// </summary>
public class HUDOwner : HUDElement
{
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
    /// Updates the object action in the HUD.
    /// </summary>
    protected override void UpdateObjectInformationInHud()
    {
        if (textComponent == null)
            return;

        if (DisplayObject != null)
        {
            if (DisplayObject.GetPlayer() != null)
            {
                textComponent.text = DisplayObject.GetPlayer().username;
            }
            else
            {
                textComponent.text = "No owner";
            }
        }
        else
        {
            textComponent.text = "";
        }
    }
}