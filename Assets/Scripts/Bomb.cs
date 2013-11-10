using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {
	public float detonationTime;
	public AudioClip explosion;
	public GameObject ExplosionPrefab;

	// Use this for initialization
	void Start () {
		detonationTime *= 60;
		detonationTime += Time.frameCount;
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
			Enemy enemy = (Enemy)otherObject.gameObject.GetComponent("Enemy");
			Instantiate(ExplosionPrefab, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), enemy.transform.rotation);
			Instantiate(ExplosionPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
			enemy.SetPositionAndSpeed();
			Destroy(gameObject);
			Player.Score += 100;
			Player.WeaponScore += 100;
		}
	}
}
