using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class GameManager : Singleton<GameManager>
{
    private float prevTime;
    private float prevSaveTime;
    public int coins = 0;
    public bool LavaWalkers = false;
    public bool WaterWalkers = false;
    private int maxcoins = 10;
    public int health = 3;
    public int minutos;
    public int segundos;
    private string dbUri = "URI=file:SavedData.sqlite";
    private string SQL_COUNT_ITEMS = "SELECT count(*) FROM Items";
    private string SQL_COUNT_TIME = "SELECT count(*) FROM Timer";
    private string SQL_CREATE_DATA = "CREATE TABLE IF NOT EXISTS Data " +
        "(GameId INTEGER UNIQUE NOT NULL PRIMARY KEY," +
        " Coins INTEGER ," +
        " Health INTEGER  ," +
        " Time INTEGER REFERENCES Timer ," +
        "SpecialItems INTEGER REFERENCES Items);";
    private string SQL_CREATE_TIMER = "CREATE TABLE IF NOT EXISTS Timer " +
        "(TimerId INTEGER UNIQUE NOT NULL PRIMARY KEY," +
        "Minutes INTEGER," +
        "Seconds INTEGER);";
    private string SQL_CREATE_ITEMS = "CREATE TABLE IF NOT EXISTS Items" +
        "(ItemsId INTEGER UNIQUE NOT NULL PRIMARY KEY" +
        ", WaterBoots BOOL ," +
        " LavaBoots BOOL);";
    // Start is called before the first frame update
    void Start()
    {
        prevTime = Time.realtimeSinceStartup;
        prevSaveTime = prevTime;
        //CloseDB();
    }

    private void CloseDB(IDbConnection dbConnection)
    {
        Debug.Log("end");
        dbConnection.Close();
    }

    private IDbConnection OpenDataBase()
    {
        IDbConnection dbConnection = new SqliteConnection(dbUri);
        dbConnection.Open();
        return dbConnection;
    }

    private void AddData(IDbConnection dbConnection)
    {
        string command = "INSERT INTO Items (WaterBoots,LavaBoots) VALUES ";
        command += $"('{WaterWalkers}','{LavaWalkers}'),";
        command = command.Remove(command.Length - 1, 1);
        command += ";";
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = command;
        dbCommand.ExecuteNonQuery();
        command = "INSERT INTO Timer (Minutes,Seconds) VALUES ";
        command += $"('{minutos}','{segundos}'),";
        command = command.Remove(command.Length - 1, 1);
        command += ";";
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = command;
        dbCommand.ExecuteNonQuery();
        command = "INSERT INTO Data (Coins, Health, Time, SpecialItems) VALUES ";
        int IdTime = CountNumberElementsTimer(dbConnection);
        int IdItem = CountNumberElementsItems(dbConnection);
        command += $"('{coins}','{health}','{IdTime}','{IdItem}'),";
        command = command.Remove(command.Length - 1, 1);
        command += ";";
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = command;
        dbCommand.ExecuteNonQuery();
    }

    private void InitializaDB(IDbConnection dbConnection)
    {
        IDbCommand dbCmd = dbConnection.CreateCommand();
        dbCmd.CommandText = SQL_CREATE_DATA + SQL_CREATE_TIMER + SQL_CREATE_ITEMS;
        dbCmd.ExecuteReader();
    }

    private int CountNumberElementsTimer(IDbConnection dbConnection)
    {
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = SQL_COUNT_TIME;
        IDataReader reader = dbCommand.ExecuteReader();
        reader.Read();
        return reader.GetInt32(0);
    }
    private int CountNumberElementsItems(IDbConnection dbConnection)
    {
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = SQL_COUNT_ITEMS;
        IDataReader reader = dbCommand.ExecuteReader();
        reader.Read();
        return reader.GetInt32(0);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(coins == 5)
        {
            Destroy(GameObject.Find("puenteLevadizo"));
        }
        if(coins == maxcoins)*/
        {
            Destroy(GameObject.Find("secretDoor"));
        }
        if(health == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        float currentTime = Time.realtimeSinceStartup;
        if(currentTime > prevSaveTime + 30)
        {
            prevSaveTime += 30;
            Debug.Log("start");
            IDbConnection dbConnection = OpenDataBase();
            InitializaDB(dbConnection);
            AddData(dbConnection);
            CloseDB(dbConnection);
            //SaveData();

        }
    }
}
