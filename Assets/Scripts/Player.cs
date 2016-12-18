using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {	//Player erbt von MonoBehaviour
	
	public float PlayerSpeed;	//Public Variables are visible & changeable in the Unity3D UI
	public GameObject ProjectilePrefab;
	public GameObject ProjectilePrefab2;
	public GameObject BombPrefab;
	public GameObject ExplosionPrefab;
	
	public static int Score = 0;
	public static int Lives = 3;
	public static int WeaponScore = 0;
	public static int Missed = 0;
	
	private float shipInvisibleTime = 1.5f;
	private float shipMoveOnToScreenSpeed = 5f;
	private float blinkRate = 0.05f;
	private int numberOfTimesToBlink = 10;
	private int blinkCount;
		
	enum State {
		Playing,
		Explosion,
		Invincible
	}
	
	private State state = State.Playing;	//We are playing
	
	
	//vopelj@googlemail.com Tutormail
	
	// Use this for initialization
	void Start () {
		//CAREFUL! While active Game Mode do NOT MAKE CHANGES TO THE GAME. All Changes will be lost on closing the Game mode.
		//transform.position = new Vector3(-6, 5, transform.position.z);
		//transform.position means change the position of the object. The Object gets moved in -6 x, 5 in y and z stays the same.
		
		
	}
	
	// Update is called once per frame
	void Update () {
		if (state != State.Explosion)	{	//As long as the Ship is not exploded, it can move
			float amtToMoveX = Input.GetAxis("Horizontal") * PlayerSpeed * Time.deltaTime;
			float amtToMoveY = Input.GetAxis("Vertical") * PlayerSpeed * Time.deltaTime;
			//CAREFUL! To avoid this function only to be called when a Frame has been created, insert deltaTime.
			//Amount to Move. Speichert den Wert der Achse in amtToMove
			//GetAxis = Smoothes out the Movement, GetAxisRaw = Stops immediately
			
			transform.Translate(Vector3.right * amtToMoveX, Space.World);
			transform.Translate(Vector3.up * amtToMoveY, Space.World);
			//Move Player -> Bewege das Objekt in die Rechte Achse (von der Camera aus) um amtToMove
			//Wichtig: Space.World gibt an, dass das Objekt im Bezug auf die Welt die X und Y Koordinaten ändert und nicht auf sich
			//			selbst bezogen
			
			if (transform.position.x < -7.4f)	{
				transform.position = new Vector3(7.3f, transform.position.y, 0f);	//7.3f is also possible.
			}	else if (transform.position.x > 7.4f)	{
				transform.position = new Vector3(-7.3f, transform.position.y, 0f);
			}	
			if (transform.position.y < -4.7f)	{
				transform.position = new Vector3(transform.position.x, 6.6f, 0f);
			}	else if (transform.position.y > 6.7f)	{
				transform.position = new Vector3(transform.position.x, -4.6f, 0f);
			}
			//Float Cast could also be f at the end of the Variable
			//The Screen ends at -7.4 and 7.4
			
			//Easier method:
			transform.localEulerAngles = new Vector3(0, -Input.GetAxis("Horizontal") * 30, 0);
			
			//Schuss
			if (Input.GetKeyDown("space"))	{	//KeyCode.Space looks better and is easier to use.
				//Damit das Projektil nicht IM Schiff spawnt.
				//Das 0,5 mit localScale.y sagt, dass die position im oberen 3/4 des Modells erstellt wird.
				if (Player.WeaponScore >= 1000)	{
					Vector3 position1 = new Vector3(transform.position.x - 0.5f, transform.position.y + (0.5f * transform.localScale.y), transform.position.z);
					Vector3 position2 = new Vector3(transform.position.x, transform.position.y + (0.5f * transform.localScale.y), transform.position.z);
					Vector3 position3 = new Vector3(transform.position.x + 0.5f, transform.position.y + (0.5f * transform.localScale.y), transform.position.z);
					Instantiate(ProjectilePrefab, position1, Quaternion.AngleAxis(15, Vector3.forward));
					Instantiate(ProjectilePrefab, position2, Quaternion.identity);
					Instantiate(ProjectilePrefab, position3, Quaternion.AngleAxis(345, Vector3.forward));
					
				}	else if (Player.WeaponScore >= 500)	{
					Vector3 position1 = new Vector3(transform.position.x - 0.5f, transform.position.y + (0.5f * transform.localScale.y), transform.position.z);
					Vector3 position2 = new Vector3(transform.position.x + 0.5f, transform.position.y + (0.5f * transform.localScale.y), transform.position.z);
					Instantiate(ProjectilePrefab, position1, Quaternion.identity);
					Instantiate(ProjectilePrefab, position2, Quaternion.identity);
					
				}	else {
					Vector3 position = new Vector3(transform.position.x, transform.position.y + (0.5f * transform.localScale.y), transform.position.z);
					Instantiate(ProjectilePrefab2, position, Quaternion.identity);
				}
			}
			if (Input.GetKeyDown(KeyCode.LeftShift))	{
				Vector3 position = new Vector3(transform.position.x, transform.position.y + (0.5f * transform.localScale.y), transform.position.z);
				
				Instantiate(BombPrefab, position, Quaternion.identity);	//Ein Projektil wird instanziiert.
			}
		}
	}
	
	void OnGUI()	{
		GUI.Label(new Rect(10, 10, 120, 20), "Score: " + Player.Score.ToString());
		GUI.Label(new Rect(10, 30, 120, 20), "Lives: " + Player.Lives.ToString());
		GUI.Label(new Rect(10, 50, 120, 20), "Missed: " + Player.Missed.ToString());
		GUI.Label(new Rect(10, 70, 120, 20), "Weapon Score: " + Player.WeaponScore.ToString());
	}
	
	void OnTriggerEnter(Collider otherObject)	{
		if ((otherObject.tag == "enemy") && (state == State.Playing))	{
			
			Lives--;
			WeaponScore = 0;
			
			Enemy enemy = (Enemy)otherObject.gameObject.GetComponent("Enemy");
			enemy.SetPositionAndSpeed();
			
			//Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
			StartCoroutine(DestroyShip());
		}
	}
	
	IEnumerator DestroyShip()	{
		state = State.Explosion;	//Oh lawd, let me tell you that the Ship is exploding!
		Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
		transform.position = new Vector3(0f, -5.5f, transform.position.z);	//Spawningpoint a little bit lower
		yield return new WaitForSeconds(shipInvisibleTime);
		
		if (Player.Lives > 0)	{
			while (transform.position.y < -2.2)	{
				//Move Ship back to the Standart Position
				float amtToMove = shipMoveOnToScreenSpeed * Time.deltaTime;
				transform.position = new Vector3(0f, transform.position.y + amtToMove, transform.position.z);
				
				yield return 0;	//Boh
			}
			state = State.Invincible;
			while (blinkCount < numberOfTimesToBlink)	{
				//If true, make false -> If false, make true. Rinse and Repeat. ???. Profit!
				gameObject.GetComponent<Renderer>().enabled = !gameObject.GetComponent<Renderer>().enabled;
				if (gameObject.GetComponent<Renderer>().enabled)	{
					blinkCount++;
				}
				yield return new WaitForSeconds(blinkRate);
			}
			blinkCount = 0;
			state = State.Playing;
			
		}	else {
			Application.LoadLevel("Lose");
			
		}
		
	}
}
