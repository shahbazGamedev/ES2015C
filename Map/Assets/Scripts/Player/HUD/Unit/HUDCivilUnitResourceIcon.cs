using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays the icon related to the harvested amount of a resource that a civil unit has.
/// </summary>
public class HUDCivilUnitResourceIcon : HUDElement
{
    private Image imageComponent;

    /// <summary>
    /// The type of the resource that was being harvested the last update (to save resources).
    /// </summary>
    private RTSObject.ResourceType? currentHarvestType = RTSObject.ResourceType.Gold;

    /// <summary>
    /// The sprites corresponding to each type of resource.
    /// </summary>
    private Dictionary<RTSObject.ResourceType, Sprite> resourceSprites;

    void Start()
    {
        resourceSprites = new Dictionary<RTSObject.ResourceType, Sprite>();
        resourceSprites[RTSObject.ResourceType.Gold] = Resources.Load<Sprite>("HUD/Resource/GoldIcon");
        resourceSprites[RTSObject.ResourceType.Food] = Resources.Load<Sprite>("HUD/Resource/FoodIcon");
        resourceSprites[RTSObject.ResourceType.Wood] = Resources.Load<Sprite>("HUD/Resource/WoodIcon");

        imageComponent = GetComponent<Image>();
        if (imageComponent == null)
        {
            Debug.Log("Script of type " + GetType().Name + " without a Text component won't work.");
        }
    }

    /// <summary>
    /// Update the icon related to the harvested amount of a resource that a civil unit has.
    /// </summary>
	void Update()
    {
        // Try to get the harvest type from the selected unit
        RTSObject.ResourceType? harvestType = null;
        if (DisplayObject != null && DisplayObject is CivilUnit)
        {
            CivilUnit displayUnit = (CivilUnit)DisplayObject;
            harvestType = displayUnit.GetHarvestType();
        }

        // Don't load the icon again if the type hasn't changed, to save resources
        if (currentHarvestType == harvestType)
            return;
        currentHarvestType = harvestType;

        // The resource type has changed, so update the displayed sprite
        if (harvestType != null && resourceSprites.ContainsKey(harvestType.Value))
        {
            imageComponent.enabled = true;
            imageComponent.sprite = resourceSprites[harvestType.Value];
        }
        else
        {
            imageComponent.enabled = false;
            imageComponent.sprite = null;
        }
    }
}