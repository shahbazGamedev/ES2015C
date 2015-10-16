using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays the amount of defense that a military unit has.
/// </summary>
public class HUDMilitaryUnitDefense : HUDElement
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
    /// Update the defense that is shown in the HUD.
    /// </summary>
	void Update()
    {
        if (DisplayObject is MilitaryUnit)
        {
            MilitaryUnit displayMilitaryUnit = (MilitaryUnit)DisplayObject;
            textComponent.enabled = true;
            textComponent.text = displayMilitaryUnit.GetDefense().ToString();
        }
        else
        {
            textComponent.enabled = false;
        }
    }
}
