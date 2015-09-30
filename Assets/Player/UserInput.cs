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
            MoveCamera();
            RotateCamera();
            MouseActivity();
        }
    }

    private void OpenPauseMenu()
    {
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
