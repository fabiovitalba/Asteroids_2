using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour {

	//private int buttonWidth = 200;
	//private int buttonHeight = 50;
	
	public Texture backgroundTexture;
	
	void OnGUI()	{
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);
		
		/*if (GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2, Screen.height / 2 - buttonHeight / 2, buttonWidth, buttonHeight), "You Won!\nPress to Play again!"))	{
			//Reset Player
			Player.Score = 0;
			Player.Lives = 3;
			Player.WeaponScore = 0;
			Player.Missed = 0;
			
			//Load Level
			Application.LoadLevel("level1");
		}*/
		
		if (Input.anyKeyDown)	{
			//Reset Player
			Player.Score = 0;
			Player.Lives = 3;
			Player.WeaponScore = 0;
			Player.Missed = 0;
			
			Application.LoadLevel("level1");
		}
		
	}
}
