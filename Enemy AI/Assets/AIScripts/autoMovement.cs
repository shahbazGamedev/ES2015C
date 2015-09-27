using UnityEngine;
using System.Collections;

public class autoMovement : MonoBehaviour {
	void Update() {
		transform.Translate(Vector3.forward * Time.deltaTime);
		transform.Translate(Vector3.up * Time.deltaTime, Space.World);
	}
}
