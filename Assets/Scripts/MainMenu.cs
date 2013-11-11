using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	private string instructionText = "Instructions:\nPress Left and Right Arrows to move.\nPress Space to shoot.\nUse the Shift-Key to place a Bomb.";
	//private int buttonWidth = 200;
	//private int buttonHeight = 50;
	
	public Texture backgroundTexture;
	
	void OnGUI()	{
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);
		GUI.Label(new Rect(10, 10, 250, 200), instructionText);
		
		/*if (GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2, Screen.height / 2 - buttonHeight / 2, buttonWidth, buttonHeight), "Start Game"))	{
			Application.LoadLevel("level1");
			//Hier kann man entweder mit der Nummer der Reihenfolge der Szene im Build ansteuern, oder einfach mit dem Namen als String der Szene
		}*/
		
		if (Input.anyKeyDown)	{
			Application.LoadLevel("level1");
		}
		
	}
	
}
