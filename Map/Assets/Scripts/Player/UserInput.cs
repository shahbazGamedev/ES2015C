using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UserInput : MonoBehaviour
{

    private Player player;

    // Inicialitzem
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    // Actualitzem a cada frame
    void Update()
    {
        if (player && player.human)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) OpenPauseMenu();

            bool leftClick = Input.GetMouseButtonDown(0);
            bool rightClick = Input.GetMouseButtonDown(1);
            if (leftClick || rightClick)
                HandleMouseClick(leftClick, rightClick);

            MoveCamera();
            RotateCamera();
            MouseActivity();
        }
    }

    private void OpenPauseMenu()
    {
    }

    private void HandleMouseClick(bool leftClick, bool rightClick)
    {
        EventSystem eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        if (eventSystem.IsPointerOverGameObject()) // Click on UI element
        {
            HUDElement element = null;

            // Check if the currently selected element is an element from the HUD
            if (eventSystem.currentSelectedGameObject != null)
            {
                element = eventSystem.currentSelectedGameObject.GetComponent<HUDElement>();
            }

            if (element != null && leftClick)
            {
                // Notify the element about the click event
                element.HandleClick();
            }
        }
        else // Click on non-UI element
        {
            RTSObject targetRtsElement = null;

            // Cast a ray in the direction clicked by the user to detect the currently clicked element,
            // and if it is a RTS element, then save its reference
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            Ray ray = camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject objectHit = hit.collider.gameObject;
                targetRtsElement = objectHit.GetComponent<RTSObject>();
            }

            if (leftClick)
            {
                // Call the class to change the currently selected RTS element.
                // Note that this will be null if the user clicked on nothing or a non-RTS element object.
                player.ChangeSelectedRtsObject(targetRtsElement);
            }
            else if (rightClick)
            {
                if (player.SelectedObject != null && player.SelectedObject.CanAttack() &&
                    targetRtsElement != null && targetRtsElement.owner != null &&
                    player.Team.IsEnemyOf(targetRtsElement.owner.Team))
                {
                    // If the player clicked over an unit or building and the selected unit
                    // can attack, start the attacking sequence
                    player.SelectedObject.AttackObject(targetRtsElement);
                }
                else if (player.SelectedObject != null && player.SelectedObject.CanMove())
                {
                    // Otherwise, if the unit can move, start the movement sequence
                    // TODO: This is inconsistent. All RTSObjects have the CanMove() API,
                    // but only Unit has the setNewPath() and movement implementation.
                    // It would make sense to implement movement as part of object,
                    // since, e.g. resources (animals) may be able to move.
                    Unit selectedUnit = player.SelectedObject as Unit;
                    if (selectedUnit != null)
                    {
                        selectedUnit.setNewPath(hit.point);
                    }
                }
            }
        }
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
