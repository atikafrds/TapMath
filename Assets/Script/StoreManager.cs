using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour {

	private string connectionString;
	private List<Item> itemList = new List<Item>();
    public GameObject itemPrefab;
    public Transform StoreItemParent;
	public Sprite newSprite;
    public int scoreMemory;

	// Use this for initialization
	void Start () {
		connectionString = "URI=file:" + Application.dataPath + "/myDatabase.sqlite";
		CreateTable ();
        ShowItems();
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
				string query = "SELECT ID, NAME, PRICE, OWNERSHIP FROM ITEMS";
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
			GameObject tempObject = Instantiate(itemPrefab);
			Item tempItem = itemList[i];
			tempObject.GetComponent<StoreItemScript>().setItem(tempItem.getName(), tempItem.getPrice());
			tempObject.transform.SetParent(StoreItemParent);
            tempObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
			if (tempItem.getOwnership() == 1) {
                tempObject.GetComponentInChildren<Button>().GetComponent<Image>().sprite = newSprite;
                tempObject.GetComponentInChildren<Button>().interactable = false;
            }
			tempObject.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickButton(tempItem, tempObject.GetComponentInChildren<Button>()));
        }
    }

	public void OnClickButton(Item item, Button button) {
        if (PlayerPrefs.HasKey("scoreMemory")) {
			scoreMemory = PlayerPrefs.GetInt("scoreMemory");
        } else {
            scoreMemory = 0;
        }
		Debug.Log("Previous score: " + scoreMemory);
		if (scoreMemory >= item.getPrice()) {
			scoreMemory -= item.getPrice();
            button.GetComponent<Image>().sprite = newSprite;
            button.interactable = false;
            using (IDbConnection dbConnection = new SqliteConnection(connectionString)) {
                dbConnection.Open();
                using (IDbCommand dbCommand = dbConnection.CreateCommand()) {
					string query = "UPDATE ITEMS SET OWNERSHIP = 1 WHERE ID = " + item.getID();
                    dbCommand.CommandText = query;
                    dbCommand.ExecuteScalar();
                    dbConnection.Close();
                }
            }
        }
		Debug.Log("After score: " + scoreMemory);
		PlayerPrefs.SetInt("scoreMemory", scoreMemory);
    }
}
