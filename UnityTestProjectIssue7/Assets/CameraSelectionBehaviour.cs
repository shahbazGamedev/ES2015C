using UnityEngine;
using System.Collections;

/// <summary>
/// Manages the RTS element selection events.
/// This script must be assigned to the camera of the scene in which the user can select RTS elements.
/// </summary>
public class CameraSelectionBehaviour : MonoBehaviour {	
	/// <summary>
    /// Update function, called every frame.
    /// </summary>
	void Update () {
        if (Input.GetMouseButtonDown(0)) // If the left mouse button is clicked...
        {
            RTSElement objectRtsElement = null;

            // Cast a ray in the direction clicked by the user to detect the currently clicked element,
            // and if it is a RTS element, then save its reference
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject objectHit = hit.collider.gameObject;
                objectRtsElement = objectHit.GetComponent<RTSElement>();
            }

            // Call the class to change the currently selected RTS element.
            // Note that this will be null if the user clicked on nothing or a non-RTS element object.
            RTSElement.ChangeSelection(objectRtsElement);
        }
    }
}
