using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float ProjectileSpeed;
	public GameObject ExplosionPrefab;
	
	private Enemy enemy;
	
	// Use this for initialization
	void Start () {
		enemy = (Enemy)GameObject.Find("Enemy").GetComponent("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
		float amtToMove = ProjectileSpeed * Time.deltaTime;
		
		transform.Translate(Vector3.up * amtToMove);
		
		//Wenn eine gewisse Höhe erreicht wird, Objekt löschen um unnötige Projektile nichtmehr zu berrechnen.
		if (transform.position.y > 6.4f)	{
			Destroy (gameObject);	//kleines gameObject, dass nicht die gesamte Klasse, sondern nur diese Instanz gelöscht wird.
		}
	}
	
	void OnTriggerEnter(Collider otherObject)	{
		if (otherObject.tag == "enemy")	{
			//otherObject.gameObject.transform.position = new Vector3(otherObject.transform.position.x, 7.0f, otherObject.transform.position.z);
			Player.Score += 100;
			Player.WeaponScore += 100;
			if (Player.Score > 15000)	{
				Application.LoadLevel("Win");
			}
			
			//Enemy enemy = (Enemy)otherObject.gameObject.GetComponent("Enemy");
			Instantiate(ExplosionPrefab, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), enemy.transform.rotation);
			
			//Game gets more difficult -> Increasing Speed
			enemy.MinSpeed += 0.5f;
			enemy.MaxSpeed += 1f;
			
			enemy.SetPositionAndSpeed();
			Destroy(gameObject);
		}
	}
}
