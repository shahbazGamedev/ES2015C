using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Behaviour to control the actions that a player can do over a RTSObject - button node.
/// </summary>
public class HUDAction : HUDElement
{
    /// <summary>
    /// The index of the action in the object to display.
    /// </summary>
    public int ActionIndex;

    private Text textComponent;

    private Button buttonComponent;

    private Image imageComponent;

    void Start()
    {
        textComponent = GetComponentInChildren<Text>();
        buttonComponent = GetComponentInChildren<Button>();
        imageComponent = GetComponentInChildren<Image>();
        if (textComponent == null || buttonComponent == null || imageComponent == null)
        {
            Debug.Log("Script of type " + GetType().Name + " without a Button and Image component won't work.");
        }
    }

    /// <summary>
    /// Updates the object action in the HUD.
    /// </summary>
    void Update()
    {
        if (textComponent == null || buttonComponent == null || imageComponent == null)
            return;

        if (DisplayObject != null &&
            DisplayObject.IsOwnedBy(Player) &&
            ActionIndex < DisplayObject.GetActions().Length)
        {
            textComponent.text = DisplayObject.GetActions()[ActionIndex];
            buttonComponent.enabled = true;
            imageComponent.enabled = true;
            
        }
        else
        {
            textComponent.text = "";
            buttonComponent.enabled = false;
            imageComponent.enabled = false;
        }
    }

    /// <summary>
    /// Calls the action handler in the RTSObject when the user clicks the action.
    /// </summary>
    public void ExecuteAction()
    {
        if (DisplayObject != null &&
            DisplayObject.IsOwnedBy(Player) &&
            ActionIndex < DisplayObject.GetActions().Length)
        {
            if (DisplayObject.tag == "civil" && DisplayObject.GetComponent<CivilUnit>() && DisplayObject.GetComponent<CivilUnit>().building == false)
            {
                DisplayObject.GetComponent<CivilUnit>().building = true;
				HUDInfo.message = "Select the site where you want to build the building";
            }
            DisplayObject.PerformAction(DisplayObject.GetActions()[ActionIndex]);
        }
    }
}
