using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays the amount of attack range that a unit has.
/// </summary>
public class HUDUnitAttackRange : HUDElement
{
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
    /// Update the attack strength that is shown in the HUD.
    /// </summary>
	void Update()
    {
        float? attackRange = null;
        if (DisplayObject is Unit)
        {
            Unit displayUnit = (Unit)DisplayObject;
            attackRange = displayUnit.GetAttackRange();
        }

        if (attackRange.HasValue)
        {
            textComponent.enabled = true;
            textComponent.text = attackRange.ToString();
        }
        else
        {
            textComponent.enabled = false;
        }
    }
}
