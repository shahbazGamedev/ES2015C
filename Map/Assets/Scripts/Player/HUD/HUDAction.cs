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

    private Text nameTextComponent;

    private Button buttonComponent;

    private Image actionImageComponent;

    private Text costTextComponent;

    private Image costResourceImageComponent;

    void Start()
    {
        nameTextComponent = transform.GetChild(0).GetComponent<Text>();
        buttonComponent = transform.GetChild(1).GetComponent<Button>();
        actionImageComponent = transform.GetChild(1).GetComponent<Image>();
        costTextComponent = transform.GetChild(2).GetComponent<Text>();
        costResourceImageComponent = transform.GetChild(3).GetComponent<Image>();
        if (nameTextComponent == null || buttonComponent == null || actionImageComponent == null
            || costTextComponent == null || costResourceImageComponent == null)
        {
            Debug.Log("Script of type " + GetType().Name + " without a Button and Image component won't work.");
        }
    }

    /// <summary>
    /// Updates the object action in the HUD.
    /// </summary>
    void Update()
    {
        if (nameTextComponent == null || buttonComponent == null || actionImageComponent == null
            || costTextComponent == null || costResourceImageComponent == null)
            return;

        if (DisplayObject != null &&
            DisplayObject.IsOwnedBy(Player) &&
            ActionIndex < DisplayObject.GetActions().Length)
        {
            var action = DisplayObject.GetActions()[ActionIndex];

            nameTextComponent.text = action.Name;
            buttonComponent.enabled = true;
            actionImageComponent.enabled = true;
            actionImageComponent.sprite = action.Icon;
            costTextComponent.text = action.Cost.ToString();
            costResourceImageComponent.enabled = true;
            costResourceImageComponent.sprite = GetResourceSprite(action.CostResource);

        }
        else
        {
            nameTextComponent.text = "";
            buttonComponent.enabled = false;
            actionImageComponent.enabled = false;
            costTextComponent.text = "";
            costResourceImageComponent.enabled = false;
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
            DisplayObject.PerformAction(DisplayObject.GetActions()[ActionIndex].Name);
        }
    }
}
