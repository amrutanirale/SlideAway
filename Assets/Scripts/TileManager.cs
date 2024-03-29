﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TileManager : MonoBehaviour
{

	public GameObject[] tilePrefab;

	public GameObject currentTile;

	private static TileManager instance;

	private Stack<GameObject> leftTiles = new Stack<GameObject> ();

	public Stack<GameObject> LeftTiles {
		get { return leftTiles; }
		set { leftTiles = value; }   
	}

	private Stack<GameObject> topTiles = new Stack<GameObject> ();

	public Stack<GameObject> TopTiles {
		get { return topTiles; }
		set { topTiles = value; }  
	}

	public static TileManager Instance {
		get {
			if (instance == null) {
				instance = GameObject.FindObjectOfType<TileManager> ();
			} 
			return instance;
		}
	}


	// Use this for initialization
	void Start ()
	{
		CreateTiles (100);

		for (int i = 0; i < 50; i++) {
			SpawnTile ();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	    
	}

	public void CreateTiles (int amount)
	{
		for (int i = 0; i < amount; i++) {
			leftTiles.Push (Instantiate (tilePrefab [0]));
			topTiles.Push (Instantiate (tilePrefab [1]));
			leftTiles.Peek ().name = "LeftTile";
			leftTiles.Peek ().SetActive (false);
			topTiles.Peek ().name = "TopTile";
			topTiles.Peek ().SetActive (false);
		}
	}

	public void SpawnTile ()
	{
		if (leftTiles.Count == 0 || topTiles.Count == 0) {
			CreateTiles (10);
		}

		int randomIndex = Random.Range (0, 2);

		if (randomIndex == 0) {
			GameObject tmp = leftTiles.Pop ();
			tmp.SetActive (true);
			tmp.transform.position = currentTile.transform.GetChild (0).transform.GetChild (randomIndex).position;
			currentTile = tmp;
		} else if (randomIndex == 1) {
			GameObject tmp = topTiles.Pop ();
			tmp.SetActive (true);
			tmp.transform.position = currentTile.transform.GetChild (0).transform.GetChild (randomIndex).position; 
			currentTile = tmp;
		}

		int spawnPickup = Random.Range (0, 10);

		if (spawnPickup == 0) {
			currentTile.transform.GetChild (1).gameObject.SetActive (true);
		}
 

		//currentTile = (GameObject)Instantiate (leftTilePrefab, currentTile.transform.GetChild (0).transform.GetChild (0).position, Quaternion.identity);
		//currentTile = (GameObject)Instantiate (tilePrefab [randomIndex], currentTile.transform.GetChild (0).transform.GetChild (randomIndex).position, Quaternion.identity);
	}

	public void ResetGame ()
	{
		//Application.LoadLevel (Application.loadedLevel);
		UnityEngine.SceneManagement.SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
