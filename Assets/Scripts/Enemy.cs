using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	#region Fields
	public float MinSpeed;
	public float MaxSpeed;
	private float currentSpeed;
	private float currentAngle;
	private float x, y, z;
	#endregion
	
	
	// Use this for initialization
	void Start () {
		SetPositionAndSpeed();
	}
	
	// Update is called once per frame
	void Update () {
		float amtToMove = currentSpeed * Time.deltaTime;
		transform.Translate(Vector3.down * amtToMove);
		
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
		currentSpeed = Random.Range(MinSpeed, MaxSpeed);	//Zufallszahl zwischen MinSpeed und MaxSpeed
		currentAngle = Random.Range(0f, 360f);
		x = Random.Range(-6f, 6f);
		y = 7.0f;
		z = 0.0f;
		transform.position = new Vector3(x, y, z);
		transform.localEulerAngles = new Vector3(0, 0, currentAngle);
	}
}
