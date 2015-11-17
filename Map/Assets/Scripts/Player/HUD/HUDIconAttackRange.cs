﻿using UnityEngine;
using UnityEngine.UI;

public class HUDIconAttackRange : HUDElement
{
    private Image imageComponent;

    void Start()
    {
        imageComponent = GetComponent<Image>();
        if (imageComponent == null)
        {
            Debug.Log("Script of type " + GetType().Name + " without a Text component won't work.");
        }
    }

    /// <summary>
    /// Update the icon related to the selected unit.
    /// </summary>
	void Update()
    {
        if (DisplayObject != null && DisplayObject.objectIconAttackRange != null)
        {
            imageComponent.enabled = true;
            imageComponent.sprite = DisplayObject.objectIconAttackRange;
        }
        else
        {
            imageComponent.enabled = false;
            imageComponent.sprite = null;
        }
    }
}