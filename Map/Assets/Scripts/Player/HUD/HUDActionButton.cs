using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Behaviour to control the actions that a player can do over a RTSObject - button node.
/// </summary>
public class HUDActionButton : HUDElement
{
    /// <summary>
    /// The index of the action in the object to display.
    /// </summary>
    public int ActionIndex;

    private Button buttonComponent;

    private Image imageComponent;

    void Start()
    {
        buttonComponent = GetComponent<Button>();
        imageComponent = GetComponent<Image>();
        if (buttonComponent == null || imageComponent == null)
        {
            Debug.Log("Script of type " + GetType().Name + " without a Button and Image component won't work.");
        }
    }

    /// <summary>
    /// Updates the object action in the HUD.
    /// </summary>
    void Update()
    {
        if (buttonComponent == null || imageComponent == null)
            return;

        if (DisplayObject != null &&
            DisplayObject.IsOwnedBy(Player) &&
            ActionIndex < DisplayObject.GetActions().Length)
        {
            buttonComponent.enabled = true;
            imageComponent.enabled = true;
            
        }
        else
        {
            buttonComponent.enabled = false;
            imageComponent.enabled = false;
        }
    }

    /// <summary>
    /// Calls the action handler in the RTSObject when the user clicks the action.
    /// </summary>
    public override void HandleClick()
    {
        if (DisplayObject != null &&
            DisplayObject.IsOwnedBy(Player) &&
            ActionIndex < DisplayObject.GetActions().Length)
        {
            if (DisplayObject.tag == "civil" && DisplayObject.GetComponent<CivilUnit>() && DisplayObject.GetComponent<CivilUnit>().building == false)
            {
                DisplayObject.GetComponent<CivilUnit>().building = true;
                
            }
            DisplayObject.PerformAction(DisplayObject.GetActions()[ActionIndex]);
        }
    }
}
