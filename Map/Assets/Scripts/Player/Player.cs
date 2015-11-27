using Pathfinding;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Player : MonoBehaviour {
    public string username;
    public bool human;
    public float initialFood, initialGold, initialWood;
    public PlayerCivilization civilization;

    private bool findingPlacement = false;

    /// <summary>
    /// Gets the team that this player belongs to.
    /// </summary>
    public Team Team
    {
        get
        {
            return GetComponentInParent<Team>();
        }
    }

    /// <summary>
    /// Which object does the player have selected for executing actions over it?
    /// </summary>
    public RTSObject SelectedObject { get; set; }

    /// <summary>
    /// Amount of each resource that can be collected by the player.
    /// </summary>
    public Dictionary<RTSObject.ResourceType, float> resourceAmounts;

    void Start ()
    {
        // Set the initial resource amounts
        resourceAmounts = new Dictionary<RTSObject.ResourceType, float>();
        resourceAmounts[RTSObject.ResourceType.Food] = initialFood;
        resourceAmounts[RTSObject.ResourceType.Gold] = initialGold;
        resourceAmounts[RTSObject.ResourceType.Wood] = initialWood;

		Texture2D cursorTexture = Resources.Load("HUD/Cursors/cursor_normal") as Texture2D;
		Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);

        // Load player civilization from menu parameters
        var menuGameParametersObject = GameObject.Find("MenuGameParameters");
        var menuGameParameters = (menuGameParametersObject != null) ?
            menuGameParametersObject.GetComponent<MenuGameParameters>() : null;

        if (menuGameParameters != null)
        {
            civilization = human
                ? menuGameParameters.SelectedHumanCivilization
                : menuGameParameters.SelectedEnemyCivilization;

            Debug.LogFormat("The user has choosen the {0} civilization in the menu for the {1} player.",
                civilization, human ? "human" : "enemy");
        }
        else
        {
            Debug.Log("Can't find the menu game parameters object (either it's not correctly " +
                "configured, or you launched the game directly from the campaign scene). Setting defaults.");
        }

        // Spawn initial elements
        SpawnInitialTownCenter();
        SpawnInitialCivilUnit();
        SpawnInitialMilitaryUnit();
    }

    /// <summary>
    /// Spawn the initial player town center.
    /// </summary>
    private void SpawnInitialTownCenter()
    {
        var townCenterSpawnPointTransform = transform.FindChild("TownCenterSpawnPoint");
        if (townCenterSpawnPointTransform == null)
        {
            Debug.LogFormat("Can't find the town center spawn point for {0}.", username);
            return;
        }

        var townCenterSpawnPoint = townCenterSpawnPointTransform.position;

        var townCenterTemplate = RTSObjectFactory.GetObjectTemplate(RTSObjectType.BuildingTownCenter, civilization, true);
        if (townCenterTemplate == null)
            return;

        var townCenter = (GameObject)Instantiate(townCenterTemplate, townCenterSpawnPoint, Quaternion.identity);
        townCenter.GetComponent<RTSObject>().owner = this;
        townCenter.transform.parent = transform; // Should have no effect, but easier for debugging

        var guo = new GraphUpdateObject(townCenter.GetComponent<BoxCollider>().bounds);
        guo.updatePhysics = true;
        AstarPath.active.UpdateGraphs(guo);
    }

    /// <summary>
    /// Spawn the initial player civil unit.
    /// </summary>
    private void SpawnInitialCivilUnit()
    {
        var civilUnitSpawnPointTransform = transform.FindChild("CivilUnitSpawnPoint");
        if (civilUnitSpawnPointTransform == null)
        {
            Debug.LogFormat("Can't find the civil unit spawn point for {0}.", username);
            return;
        }

        var civilUnitSpawnPoint = civilUnitSpawnPointTransform.position;

        var civilUnitTemplate = RTSObjectFactory.GetObjectTemplate(RTSObjectType.UnitCivil, civilization, true);
        if (civilUnitTemplate == null)
            return;

        var civilUnit = (GameObject)Instantiate(civilUnitTemplate, civilUnitSpawnPoint, Quaternion.identity);
        civilUnit.GetComponent<RTSObject>().owner = this;
        civilUnit.transform.parent = transform; // Should have no effect, but easier for debugging
    }



    /// <summary>
    /// Spawn the initial player military unit.
    /// </summary>
    private void SpawnInitialMilitaryUnit()
    {
        var militaryUnitSpawnPointTransform = transform.FindChild("MilitaryUnitSpawnPoint");
        if (militaryUnitSpawnPointTransform == null)
        {
            Debug.LogFormat("Can't find the military unit spawn point for {0}.", username);
            return;
        }

        var militaryUnitSpawnPoint = militaryUnitSpawnPointTransform.position;

        var militaryUnitTemplate = RTSObjectFactory.GetObjectTemplate(RTSObjectType.UnitWarrior, civilization, true);
        if (militaryUnitTemplate == null)
            return;

        var militaryUnit = (GameObject)Instantiate(militaryUnitTemplate, militaryUnitSpawnPoint, Quaternion.identity);
        militaryUnit.GetComponent<RTSObject>().owner = this;
        militaryUnit.transform.parent = transform; // Should have no effect, but easier for debugging
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
