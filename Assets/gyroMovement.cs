using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GyroMovement : MonoBehaviour {

    public float speed = 1.0f;
    public Button startButton;

    Vector2 rotation = new Vector2(0, 0);
    float zAngle = 0;

    Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        Input.gyro.enabled = true;

        rb2d = GetComponent<Rigidbody2D>();
	}
    
    public void ResetVelocity()
    {
        rb2d.velocity = new Vector2(0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        UpdateRotation();

        Vector3 rightRotation = transform.right * rotation.x + transform.up * rotation.y;
        rb2d.velocity += new Vector2(rightRotation.x, rightRotation.y) * speed * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0, 0, zAngle);
        // Debug.Log(Input.gyro.rotationRate);
    }

    float ConvertAngle(float angle)
    {
        if (angle > 180) return angle - 360;
        return angle;
    }

    void UpdateRotation()
    {
        rotation.x = ConvertAngle(Input.gyro.rotationRate.x);
        rotation.y = ConvertAngle(Input.gyro.rotationRate.y);
        zAngle += Input.gyro.rotationRate.z;
    }
}
