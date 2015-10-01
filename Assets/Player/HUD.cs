using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
    private GameObject hudObjectName;
    private GameObject hudObjectHealth;

    private RTSObject displayObject;

	/// <summary>
    /// Initialize the HUD behaviour.
    /// </summary>
	void Start ()
    {
        hudObjectName = transform.Find("HUDObjectName").gameObject;
        hudObjectHealth = transform.Find("HUDObjectHealth").gameObject;

        displayObject = null;

        UpdateObjectInformationInHud();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Set the object whose information is displayed in the HUD.
    /// </summary>
    /// <param name="displayObject">The object to display in the HUD.</param>
    public void SetDisplayObject(RTSObject newDisplayObject)
    {
        displayObject = newDisplayObject;
        UpdateObjectInformationInHud();
    }

    /// <summary>
    /// Updates the information about the selected object that is displayed in the HUD.
    /// </summary>
    void UpdateObjectInformationInHud()
    {
        // Update the selected object information
        if (displayObject != null)
        {
            hudObjectName.GetComponent<Text>().text = displayObject.objectName;

            hudObjectHealth.GetComponent<Slider>().minValue = 0;
            hudObjectHealth.GetComponent<Slider>().maxValue = displayObject.maxHitPoints;
            hudObjectHealth.GetComponent<Slider>().value = displayObject.hitPoints;
        }
        else
        {
            hudObjectName.GetComponent<Text>().text = "-";

            hudObjectHealth.GetComponent<Slider>().minValue = 0;
            hudObjectHealth.GetComponent<Slider>().maxValue = 0;
            hudObjectHealth.GetComponent<Slider>().value = 0;
        }
    }
}
