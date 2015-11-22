using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Placeholder class that is used to display the building preview and detect collisions when placing a building.
/// </summary>
public class BuildingOverlapDetector : MonoBehaviour
{
    private readonly Color buildableColor = new Color(0.0f, 1.0f, 0.0f, 0.8f);
    private readonly Color nonBuildableColor = new Color(1.0f, 0.0f, 0.0f, 0.5f);

    private const bool drawBoundingBox = false;

    /// <summary>
    /// Number of OnCollisionEnter calls minus number of OnCollisionExit calls.
    /// </summary>
    private int collisionCount = 0;

    /// <summary>
    /// Height of the currently mouse-overed terrain.
    /// </summary>
    private float currentTerrainHeight = 0.0f;

    /// <summary>
    /// Checks if the overlap detector is currently colliding.
    /// </summary>
    public bool IsBuildable
    {
        get
        {
            return collisionCount == 0 && Math.Abs(currentTerrainHeight) < 2.0f;
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

        // Enable alpha mode (transparency) for each material, for the partially visible effect
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            foreach (Material m in r.materials)
            {
                // http://sassybot.com/blog/swapping-rendering-mode-in-unity-5-0/
                m.SetFloat("_Mode", 2);
                m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                m.EnableKeyword("_ALPHABLEND_ON");
            }
        }
    }

    void Update()
    {
        // Update the overlap detector position to the position specified by the mouse
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        Ray ray = camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, (1 << 9))) // Raycast against ground (terrain) only
        {
            currentTerrainHeight = hit.point.y;

            // Adjust the weight so the bottom of the collider touching the ground, to avoid collisions with it
            // Add a few units of extra height because otherwise, if the unit is exactly
            // on the floor, the rigidbody will detect a collision with the floor always
            var hitPoint = new Vector3(hit.point.x, hit.point.y - 
                (2*GetComponent<BoxCollider>().center.y - GetComponent<BoxCollider>().size.y) + 0.35f, hit.point.z);
            transform.position = hitPoint;
        }
        else
        {
            currentTerrainHeight = Mathf.NegativeInfinity;
        }

        // If the option is enabled, show the bounding box of the object instead
        if (drawBoundingBox)
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);

            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(go.GetComponent<BoxCollider>());
            go.transform.parent = transform;
            go.transform.localPosition = GetComponent<BoxCollider>().center;
            go.transform.localScale = GetComponent<BoxCollider>().size;
        }

        // Paint the object with a color according to if it's buildable or not
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            foreach (Material m in r.materials)
            {
                m.color = IsBuildable ? buildableColor : nonBuildableColor;
            }
        }
    }
}
