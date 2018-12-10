using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ui_script : MonoBehaviour {

	public background_space aScript;

	public void StartGame() 
	{
	
	AudioSource audioData = GetComponent<AudioSource>();
	audioData.Play(0);
	aScript.zoom();
	print("hellooo");
   // SceneManager.LoadScene("lvl");
	}

	public void QuitGame() 
	{
	  print("By");
   // SceneManager.LoadScene("lvl");
	}
}
