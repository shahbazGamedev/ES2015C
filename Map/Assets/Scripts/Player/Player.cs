//using Mono.Cecil;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public string username;
    public bool human;
    public float initialFood, initialGold, initialWood;
    public Color teamColor;

    private bool findingPlacement = false;

    /// <summary>
    /// Which object does the player have selected for executing actions over it?
    /// </summary>
    public RTSObject SelectedObject { get; set; }

    /// <summary>
    /// Amount of each resource that can be collected by the player.
    /// </summary>
    public Dictionary<RTSObject.ResourceType, float> resourceAmounts;

    void Start () {
        resourceAmounts = new Dictionary<RTSObject.ResourceType, float>();
        resourceAmounts[RTSObject.ResourceType.Food] = initialFood;
        resourceAmounts[RTSObject.ResourceType.Gold] = initialGold;
        resourceAmounts[RTSObject.ResourceType.Wood] = initialWood;
    }
	
	void Update () {
	
	}

    /// <summary>
    /// Called when the RTS element selection begins.
    /// </summary>
    private void OnSelectionStart(RTSObject obj)
    {
    }

    /// <summary>
    /// Called when the RTS element selection ends.
    /// </summary>
    private void OnSelectionEnd(RTSObject obj)
    {
    }

    /// <summary>
    /// Changes the currently selected RTS element for the current player, or unselects it.
    /// </summary>
    /// <param name="newSelection">The new RTS element to select, or null to unselect any element.</param>
    internal void ChangeSelectedRtsObject(RTSObject newSelection)
    {
        if (newSelection != SelectedObject)
        {
            if (SelectedObject != null)
            {
                OnSelectionEnd(SelectedObject);
            }

            SelectedObject = newSelection;

            if (SelectedObject != null)
            {
                OnSelectionStart(SelectedObject);
            }
        }
    }

    public float GetResourceAmount(RTSObject.ResourceType resourceType)
    {
        if (!resourceAmounts.ContainsKey(resourceType))
            throw new ArgumentOutOfRangeException("resourceType");

        return resourceAmounts[resourceType];
    }
}
