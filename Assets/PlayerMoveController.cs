using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour {
	public float moveSpeed;

	public float sensitivityX;
	public float sensitivityY;
	private Vector2 mouseLook;
	private Vector2 smoothV;

	public Camera camera;
	public GameObject capsule;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (camera) {
			KeyHandler ();
			MouseHandler ();
		}
	}

	void MouseHandler () {
		var mouseDirection = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

		mouseDirection = Vector2.Scale(mouseDirection, new Vector2(sensitivityX, sensitivityY));
		smoothV.x = Mathf.Lerp(smoothV.x, mouseDirection.x, 1f);
		smoothV.y = Mathf.Lerp(smoothV.y, mouseDirection.y, 1f);
		mouseLook += smoothV;

		// Prevent y from going below -90 and above 90
		mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

		camera.transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
		capsule.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, capsule.transform.up);
	}

	void KeyHandler () {
		float xAxisValue = Input.GetAxis("Horizontal");
    	float zAxisValue = Input.GetAxis("Vertical");
		
		Vector3 position = new Vector3(xAxisValue, 0.0f, zAxisValue);
		capsule.transform.Translate(position * moveSpeed);
	}
}
