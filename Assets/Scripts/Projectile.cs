using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float ProjectileSpeed;
	
	// Use this for initialization
	void Start () {
	
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
}
