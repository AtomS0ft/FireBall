using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
	private LineRenderer lr;
	private Rigidbody rb;
	Transform tr_camera;
	Vector3 cam_delta;
	[SerializeField]
	Vector3 vel = new Vector3 (10f, 0f, 0); 
	Vector3 connected_point;
	bool connected = false;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		lr = GetComponent<LineRenderer> ();
		//rb.AddForce (Vector3.right * 1000);
		rb.velocity = vel;
		tr_camera = Camera.main.transform;
		cam_delta = transform.position - tr_camera.position;

	}
	
	// Update is called once per frame
	void Update () {
				
		if (connected) {
			lr.enabled = true;
			lr.SetPosition (0, transform.position);
			lr.SetPosition (1, connected_point);
			if (Input.GetKeyDown (KeyCode.Space)) {
				connected = false;
			}		
		} else {
			lr.enabled = false;

			if (Input.GetKeyDown (KeyCode.Space)) {
				//rb.isKinematic = true;
				RaycastHit hit;
				if (Physics.Raycast (transform.position, new Vector3 (0.6f, 1f, 0), out hit)) {
					Debug.DrawLine (transform.position, hit.point, Color.red, 999f);
					//Debug.Log(hit.distance);
					connected_point = hit.point;
					connected = true;
					//Debug.Log(hit.rigidbody.gameObject.name);
				}
			}		
		}
	}
	void FixedUpdate(){
		if (connected) {
			//rb.velocity
			float vel_magnitude = rb.velocity.magnitude;
			Vector3 new_direction = Vector3.Cross ((transform.position - connected_point), Vector3.back).normalized;
			Debug.DrawRay (transform.position, new_direction, Color.green, 999f);
			//rb.isKinematic = true;
			rb.velocity = new_direction * vel_magnitude;
		}			
	}

	void LateUpdate(){
		tr_camera.position = transform.position - cam_delta;
	}
}
