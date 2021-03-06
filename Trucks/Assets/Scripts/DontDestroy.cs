﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (GameObject.FindObjectsOfType<DontDestroy> ().Length == 1) {
			
			GameObject.DontDestroyOnLoad (this.gameObject);
		} 
		else 
		{
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level1")) 
		{
			Destroy (gameObject);
		}
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2"))
        {
            Destroy(gameObject);
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level3"))
        {
            Destroy(gameObject);
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level4"))
        {
            Destroy(gameObject);
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("procedural"))
        {
            Destroy(gameObject);
        }
    }
}
