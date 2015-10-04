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

    // DisplayObject.actions and DisplayObject.player is not implemented yet.
    // Use a mock instead and it will require to be changed later.
    private Player DisplayObjectPlayerMock;
    private string[] DisplayObjectActionsMock;

    void Start()
    {
        DisplayObjectPlayerMock = transform.root.GetComponent<Player>();
        DisplayObjectActionsMock = new string[] { "Move", "Attack" };

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

        if (DisplayObject != null &&
            DisplayObjectPlayerMock == transform.root.GetComponent<Player>() &&
            ActionIndex < DisplayObjectActionsMock.Length)
        {
            textComponent.text = DisplayObjectActionsMock[ActionIndex];
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
        if (DisplayObject != null && ActionIndex < DisplayObjectActionsMock.Length)
        {
            DisplayObject.PerformAction(DisplayObjectActionsMock[ActionIndex]);
        }
    }
}
