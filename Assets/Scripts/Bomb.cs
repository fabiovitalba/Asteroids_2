using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {
	public float detonationTime;
	public AudioClip explosion;
	
	public GameObject ExplosionPrefab;
	
	private Enemy enemy;

	// Use this for initialization
	void Start () {
		detonationTime *= 60;
		detonationTime += Time.frameCount;
		enemy = (Enemy)GameObject.Find("Enemy").GetComponent("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.frameCount > detonationTime)	{
			AudioSource.PlayClipAtPoint(explosion, transform.position);
			Instantiate(ExplosionPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
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
