using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cars : MonoBehaviour {

	public float movementSpeed;
	public float rotationSpeed;
	public float rotX;
	public float rotY;
	public float rotZ;

	//Step sound
	public float stepTimer;
	public float setStepTimer;

	// Use this for initialization
	void Start () {
		stepTimer = setStepTimer;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (Input.GetKey (KeyCode.LeftShift) && Input.GetKey ("w")) {
			transform.position += transform.TransformDirection (Vector3.forward) * Time.deltaTime * movementSpeed * 2.5f;
			stepTimer -= 1 * Time.deltaTime * 2.5f;
		} else if (Input.GetKey ("w") && !Input.GetKey (KeyCode.LeftShift)) {
			transform.position += transform.TransformDirection (Vector3.forward) * Time.deltaTime * movementSpeed;
			stepTimer -= 1 * Time.deltaTime;
		} else if (Input.GetKey ("s")) {
			transform.position -= transform.TransformDirection (Vector3.forward) * Time.deltaTime * movementSpeed;
			stepTimer -= 1 * Time.deltaTime;
		} else if (Input.GetAxis ("Xbox_LJV") < 0 && Input.GetButton("Xbox_LB") && !Input.GetKey("a") && !Input.GetKey("d")) {
			transform.position -= transform.TransformDirection (Vector3.forward) * Time.deltaTime * movementSpeed * Input.GetAxis ("Xbox_LJV") * 2.5f;
			stepTimer -= 1 * Time.deltaTime * 2.5f;
		} else if (Input.GetAxis ("Xbox_LJV") != 0 && !Input.GetKey("a") && !Input.GetKey("d")) {
			transform.position -= transform.TransformDirection (Vector3.forward) * Time.deltaTime * movementSpeed * Input.GetAxis ("Xbox_LJV");
			stepTimer -= 1 * Time.deltaTime;
		}

		if (Input.GetKey ("a") && !Input.GetKey ("d")) {
			transform.position += transform.TransformDirection (Vector3.left) * Time.deltaTime * movementSpeed;
			if (!Input.GetKey ("w") && !Input.GetKey ("s")) {
				stepTimer -= 1 * Time.deltaTime;
			}
		} else if (Input.GetKey ("d") && !Input.GetKey ("a")) {
			transform.position -= transform.TransformDirection (Vector3.left) * Time.deltaTime * movementSpeed;
			if (!Input.GetKey ("w") && !Input.GetKey ("s")) {
				stepTimer -= 1 * Time.deltaTime;
			}
		} else if (Input.GetAxis ("Xbox_LJH") != 0 && !Input.GetKey("w") && !Input.GetKey("s")) {
			transform.position -= transform.TransformDirection (Vector3.left) * Time.deltaTime * movementSpeed * Input.GetAxis("Xbox_LJH");
			if (!Input.GetKey ("w") && !Input.GetKey ("s") && !Input.GetButton("Xbox_LB")) {
				stepTimer -= 1 * Time.deltaTime;
			}
		}
	}

	void Update () {

		if (stepTimer <= 0) {
			GetComponent<AudioSource> ().Play ();
			stepTimer += setStepTimer;
		}

		if (Input.GetKeyDown (KeyCode.Space) || Input.GetButtonDown("Xbox_A")) {
			if (transform.position.y <= 1.05f) {
				GetComponent<Rigidbody> ().AddForce (Vector3.up * 200);
			}
		}
		if (Input.GetAxis ("Mouse Y") != 0 || Input.GetAxis ("Mouse X") != 0) {
			rotX -= Input.GetAxis ("Mouse Y") * Time.deltaTime * rotationSpeed;
			rotY += Input.GetAxis ("Mouse X") * Time.deltaTime * rotationSpeed;
		} else if (Input.GetAxis ("Xbox_RJV") != 0 || Input.GetAxis ("Xbox_RJH") != 0) {
			rotX += Input.GetAxis ("Xbox_RJH") * Time.deltaTime * rotationSpeed;
			rotY += Input.GetAxis ("Xbox_RJV") * Time.deltaTime * rotationSpeed;
		}
		if (rotX < -90) {
			rotX = -90;
		} else if (rotX > 90) {
			rotX = 90;
		}
		transform.rotation = Quaternion.Euler (0, rotY, 0);
		GameObject.FindWithTag ("MainCamera").transform.rotation = Quaternion.Euler (rotX, rotY, 0);
	}
}