using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays the icon related to amount of attack range that a unit has.
/// </summary>
public class HUDUnitAttackRangeIcon : HUDElement
{
    private Image imageComponent;

    void Start()
    {
        imageComponent = GetComponent<Image>();
        if (imageComponent == null)
        {
            Debug.Log("Script of type " + GetType().Name + " without a Text component won't work.");
        }
    }

    /// <summary>
    /// Update the icon related to the attack range that is shown in the HUD.
    /// </summary>
	void Update()
    {
        imageComponent.enabled = (DisplayObject != null && 
            DisplayObject.CanAttack() && DisplayObject.GetAttackRange() != 0);
    }
}