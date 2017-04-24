using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour {

	private string connectionString;
	private List<Item> itemList = new List<Item>();
	public GameObject backgroundPrefab;
	public Transform backgroundParent;
	public string BgInUse;
	public Sprite Default;
	public Sprite PinkandBlue;
	public Sprite Mozaic;
	public Sprite Deer;

	// Use this for initialization
	void Start () {
		connectionString = "URI=file:" + Application.dataPath + "/myDatabase.sqlite";
		CreateTable ();
		ShowItems();
		if (PlayerPrefs.HasKey("BgInUse")) {
			BgInUse = PlayerPrefs.GetString("BgInUse");
		} else {
			BgInUse = "Default";
		}
	}

	// Update is called once per frame
	void Update () {

	}

	public void CreateTable() {
		using (IDbConnection dbConnection = new SqliteConnection(connectionString)) {
			dbConnection.Open();
			using (IDbCommand dbCommand = dbConnection.CreateCommand()) {
				string query = "CREATE TABLE IF NOT EXISTS items(id INTEGER PRIMARY KEY  NOT NULL, name VARCHAR NOT NULL, price INTEGER NOT NULL, ownership INTEGER NOT NULL  DEFAULT (0))";
				dbCommand.CommandText = query;
				dbCommand.ExecuteScalar ();
				dbConnection.Close();
			}
		}
	}

	private void GetItems() {
		itemList.Clear();
		using (IDbConnection dbConnection = new SqliteConnection(connectionString)) {
			dbConnection.Open();
			using (IDbCommand dbCommand = dbConnection.CreateCommand()) {
				string query = "SELECT ID, NAME, PRICE, OWNERSHIP FROM ITEMS WHERE OWNERSHIP = 1";
				dbCommand.CommandText = query;
				using (IDataReader reader = dbCommand.ExecuteReader()) {
					while (reader.Read()) {
						itemList.Add (new Item (reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3)));
					}
					dbConnection.Close();
					reader.Close();
				}
			}
		}
	}

	private void ShowItems() {
		GetItems ();
		for (int i = 0; i < itemList.Count; i++) {
			GameObject tempObject = Instantiate(backgroundPrefab);
			Item tempItem = itemList[i];
			tempObject.GetComponent<BackgroundScript>().setItem(tempItem.getName());
			tempObject.transform.SetParent(backgroundParent);
			tempObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
			tempObject.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickButton(tempItem, tempObject.GetComponentInChildren<Button>()));
		}
	}

	public void OnClickButton(Item item, Button button) {
		PlayerPrefs.SetString ("BgInUse", item.getName ());
		Debug.Log ("BgInUse: " + PlayerPrefs.GetString ("BgInUse"));
		Sprite selectedSprite;
		if (item.getName ().Equals ("Default")) {
			selectedSprite = Default;
		} else if (item.getName ().Equals ("Pink and Blue")) {
			selectedSprite = PinkandBlue;
		} else if (item.getName ().Equals ("Mozaic")) {
			selectedSprite = Mozaic;
		} else {
			selectedSprite = Deer;
		}
		for (int i = 0; i < GameObject.FindGameObjectsWithTag ("Background").Length; i++) {
			GameObject.FindGameObjectsWithTag ("Background") [i].GetComponent<SpriteRenderer> ().sprite = selectedSprite;
		}
	}
}
