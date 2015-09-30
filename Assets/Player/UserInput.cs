using UnityEngine;
using System.Collections;

public class UserInput : MonoBehaviour
{

    private Player player;

    // Inicialitzem
    void Start()
    {
        player = transform.root.GetComponent<Player>();
    }

    // Actualitzem a cada frame
    void Update()
    {
        if (player && player.human)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) OpenPauseMenu();
            if (Input.GetMouseButtonDown(0)) // If the left mouse button is clicked...
            {
                CheckSelectionChangeByMouse();
            }
            MoveCamera();
            RotateCamera();
            MouseActivity();
        }
    }

    private void OpenPauseMenu()
    {
    }

    private void CheckSelectionChangeByMouse()
    {
        RTSObject objectRtsElement = null;

        // Cast a ray in the direction clicked by the user to detect the currently clicked element,
        // and if it is a RTS element, then save its reference
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        Ray ray = camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHit = hit.collider.gameObject;
            objectRtsElement = objectHit.GetComponent<RTSObject>();
        }
        // Call the class to change the currently selected RTS element.
        // Note that this will be null if the user clicked on nothing or a non-RTS element object.
        player.ChangeSelectedRtsObject(objectRtsElement);
    }

    private void MoveCamera()
    {
    }

    private void RotateCamera()
    {
    }

    private void MouseActivity()
    {
    }
}
