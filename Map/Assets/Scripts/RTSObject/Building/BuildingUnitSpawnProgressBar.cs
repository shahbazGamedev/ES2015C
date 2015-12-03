using UnityEngine;

/// <summary>Used to display the progress bar for unit spawns in buildings.</summary>
public class BuildingUnitSpawnProgressBar : MonoBehaviour {
    
    void Start()
    {
        UpdateProgressBar(0.0f);
        FaceCamera();
    }

	void Update ()
    {
        FaceCamera();
    }

    /// <summary>
    /// Make sprite always face the camera
    /// http://answers.unity3d.com/questions/536208/game-texture-always-face-camera.html
    /// </summary>
    void FaceCamera()
    {
        transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward);
    }

    /// <summary>
    /// Update the progress bar status. Linear between 0.0f (empty) and 1.0f (full).
    /// </summary>
    /// <param name="spawnProgress">Spawn progress of the unit.</param>
    public void UpdateProgressBar(float spawnProgress)
    {
        var frontPlaneObject = transform.GetChild(0);
        frontPlaneObject.localScale = new Vector3(spawnProgress, 1.0f, 1.0f);
    }
}
