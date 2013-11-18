using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	#region Fields
	public float MinSpeed;
	public float MaxSpeed;
	
	private float currentSpeed;
	private float currentAngle;
	private float x, y, z;
	
	private float MinRotateSpeed = 60f;
	private float MaxRotateSpeed = 120f;
	private float currentRotationSpeed;
	
	private float MinScale = 0.8f;
	private float MaxScale = 2f;
	private float currentScaleX, currentScaleY, currentScaleZ;
	#endregion
	
	
	// Use this for initialization
	void Start () {
		SetPositionAndSpeed();
	}
	
	// Update is called once per frame
	void Update () {
		float rotationSpeed = currentRotationSpeed * Time.deltaTime;
		transform.Rotate(new Vector3(2, -2, -3) * rotationSpeed);
		
		float amtToMove = currentSpeed * Time.deltaTime;
		transform.Translate(Vector3.down * amtToMove, Space.World);
		
		if (transform.position.x < -8f)	{
			SetPositionAndSpeed();
			Player.Missed ++;
		}	else if (transform.position.x > 8f)	{
			SetPositionAndSpeed();
			Player.Missed ++;
		}	
		if (transform.position.y < -5f)	{
			SetPositionAndSpeed();
			Player.Missed ++;
		}	else if (transform.position.y > 7f)	{
			SetPositionAndSpeed();
			Player.Missed ++;
		}
		//If the Enemy falls out of the Field, player gets +1 to Missed
		
	}
	
	public void SetPositionAndSpeed ()	{
		currentRotationSpeed = Random.Range(MinRotateSpeed, MaxRotateSpeed);
		currentSpeed = Random.Range(MinSpeed, MaxSpeed);	//Zufallszahl zwischen MinSpeed und MaxSpeed
		currentScaleX = Random.Range(MinScale, MaxScale);
		currentScaleY = Random.Range(MinScale, MaxScale);
		currentScaleZ = Random.Range(MinScale, MaxScale);
		//currentAngle = Random.Range(0f, 360f);	//Random Angle of comming into the screen
		
		x = Random.Range(-6f, 6f);
		y = 7.0f;
		z = 0.0f;
		transform.position = new Vector3(x, y, z);
		
		//Scaling the Asteroid
		transform.localScale = new Vector3(currentScaleX, currentScaleY, currentScaleZ);
		
		//transform.localEulerAngles = new Vector3(0, 0, currentAngle);	//Comming into the screen
	}
}
