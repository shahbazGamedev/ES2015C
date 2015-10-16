using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays the amount of attack range that a military unit has.
/// </summary>
public class HUDMilitaryUnitAttackRange : HUDElement
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
        if (DisplayObject is MilitaryUnit)
        {
            MilitaryUnit displayMilitaryUnit = (MilitaryUnit)DisplayObject;
            if (displayMilitaryUnit.GetAttackRange() != null)
                attackRange = displayMilitaryUnit.GetAttackRange();
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
