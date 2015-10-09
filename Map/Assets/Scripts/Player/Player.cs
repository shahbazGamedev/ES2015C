﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Player : MonoBehaviour {
    public string username;
    public bool human;
    public RTSObject SelectedObject { get; set; }
    public int startMoney, startPower;
    public Color teamColor;

    private bool findingPlacement = false;

    private HUD hud;

    void Start () {
        hud = GetComponentInChildren<HUD>();
    }
	
	void Update () {
	
	}

    /// <summary>
    /// Called when the RTS element selection begins.
    /// </summary>
    private void OnSelectionStart(RTSObject obj)
    {
        if (hud != null)
            hud.SetDisplayObject(obj);
    }

    /// <summary>
    /// Called when the RTS element selection ends.
    /// </summary>
    private void OnSelectionEnd(RTSObject obj)
    {
        if (hud != null)
            hud.SetDisplayObject(null);
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
}