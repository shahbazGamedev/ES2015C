using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]
public class MovimientoAleatorioCivil : MonoBehaviour
{
    public float speed = 1;
    public float directionChangeInterval = 6;
    public float maxHeadingChange = 500;
    public Animator anim;

    CharacterController controller;
    float heading;
    Vector3 targetRotation;

    Vector3 forward
    {
        get { return transform.TransformDirection(Vector3.forward); }
    }

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        // Set random initial rotation
        heading = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, heading, 0);

        StartCoroutine(NewHeadingRoutine());
    }

    void Update()
    {
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
        controller.SimpleMove(forward * speed);
        anim.SetBool("IsWalking", true);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag != "Boundary")
        {
            return;
        }

        // Bounce off the wall using angle of reflection
        var newDirection = Vector3.Reflect(forward, hit.normal);
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, newDirection);
        heading = transform.eulerAngles.y;
        NewHeading();
    }

    /// <summary>
    /// Calculates a new direction to move towards.
    /// </summary>
    void NewHeading()
    {
        var floor = Mathf.Clamp(heading - maxHeadingChange, 0, 360);
        var ceil = Mathf.Clamp(heading + maxHeadingChange, 0, 360);
        heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
    }

    /// <summary>
    /// Repeatedly calculates a new direction to move towards.
    /// Use this instead of MonoBehaviour.InvokeRepeating so that the interval can be changed at runtime.
    /// </summary>
    IEnumerator NewHeadingRoutine()
    {
        while (true)
        {
            NewHeading();
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }
}



