using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Behaviour to control the actions that a player can do over a RTSObject.
/// </summary>
public class HUDAction : HUDElement
{
    /// <summary>
    /// The index of the action in the object to display.
    /// </summary>
    public int ActionIndex;

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
    void Update()
    {
        if (textComponent == null)
            return;

        if (DisplayObject != null &&
            DisplayObject.IsOwnedBy(Player) &&
            ActionIndex < DisplayObject.GetActions().Length)
        {
            textComponent.text = DisplayObject.GetActions()[ActionIndex];
        }
        else
        {
            textComponent.text = "";
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
