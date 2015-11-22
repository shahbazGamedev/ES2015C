using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Placeholder class that is used to detect collisions when placing a building.
/// </summary>
public class BuildingOverlapDetector : MonoBehaviour
{
    /// <summary>
    /// Number of OnCollisionEnter calls minus number of OnCollisionExit calls.
    /// </summary>
    private int collisionCount = 0;

    /// <summary>
    /// Checks if the overlap detector is currently colliding.
    /// </summary>
    public bool IsBuildable
    {
        get
        {
            return collisionCount == 0 && Math.Abs(transform.position.y) <= 2;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        collisionCount++;
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        collisionCount--;
    }

    void Awake()
    {
        // Set our layer to IgnoreRaycast, so we don't detect collisions with ourselves...
        gameObject.layer = 2;
    }

    void Update()
    {
        // Update the overlap detector position to the position specified by the mouse
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        Ray ray = camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, (1 << 9))) // Raycast against ground (terrain) only
        {
            var hitPointOnFloor = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            transform.position = hitPointOnFloor;
        }

        // Draw a box that displays if the object can be drawn and its position
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Destroy(go.GetComponent<BoxCollider>());
        go.transform.parent = transform;
        go.transform.localPosition = GetComponent<BoxCollider>().center;
        go.transform.localScale = GetComponent<BoxCollider>().size;
        go.GetComponent<Renderer>().material.color = (IsBuildable) ? Color.green : Color.red;
    }
}
