﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

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

            if (leftClick)
            {
                // Call the class to change the currently selected RTS element.
                // Note that this will be null if the user clicked on nothing or a non-RTS element object.
                player.ChangeSelectedRtsObject(objectRtsElement);
            }
            else if (rightClick)
            {
                // If a unit is currently selected, move it to the point clicked by the player
                Unit selectedUnit = player.SelectedObject as Unit;
                if (selectedUnit != null)
                {
                    selectedUnit.setNewPath(hit.point);
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