﻿using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {
	public float detonationTime;
	public AudioClip laser;

	// Use this for initialization
	void Start () {
		detonationTime *= 60;
		detonationTime += Time.frameCount;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.frameCount > detonationTime)	{
			AudioSource.PlayClipAtPoint(laser, transform.position);
			Destroy (gameObject);	//kleines gameObject, dass nicht die gesamte Klasse, sondern nur diese Instanz gelöscht wird.
		}
	}
}