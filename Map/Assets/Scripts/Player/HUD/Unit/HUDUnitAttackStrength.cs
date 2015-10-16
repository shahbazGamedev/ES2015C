using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays the amount of attack strength that a unit has.
/// </summary>
public class HUDUnitAttackStrength : HUDElement
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
	void Update ()
    {
        if (DisplayObject is Unit)
        {
            Unit displayUnit = (Unit)DisplayObject;
            textComponent.enabled = true;
            textComponent.text = displayUnit.GetAttackStrengh().ToString();
        }
        else
        {
            textComponent.enabled = false;
        }
	}
}
