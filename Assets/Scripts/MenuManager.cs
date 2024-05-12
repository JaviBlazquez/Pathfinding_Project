using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System.Xml.Serialization;

public class MenuManager : MonoBehaviour
{
    private string dbUri = "URI=file:SavedData.sqlite";
    private string SQL_DELETE_DATA = "DELETE FROM Data";
    private string SQL_DELETE_ITEMS = "DELETE FROM Items";
    private string SQL_DELETE_TIME = "DELETE FROM Timer";
    private List<DataFromDB> datafromdb;
    private Data playerData;

    // Start is called before the first frame update
    void Start()
    {
        datafromdb = new List<DataFromDB>();
        playerData = new Data();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Level1");
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
        Debug.Log("start");
        return dbConnection;
    }

    private void DeleteData(IDbConnection dbConnection)
    {
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = SQL_DELETE_TIME;
        dbCommand.ExecuteNonQuery();
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = SQL_DELETE_ITEMS;
        dbCommand.ExecuteNonQuery();
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = SQL_DELETE_DATA;
        dbCommand.ExecuteNonQuery();
    }

    private void GenerateJSONFile(IDbConnection dbConnection)
    {
        IDbCommand dbCmd = dbConnection.CreateCommand();
        dbCmd.CommandText = "SELECT Data.GameId,Data.Coins,Data.Health,Timer.Minutes,Timer.Seconds" + 
        ",Items.LavaBoots,Items.WaterBoots FROM Data,Items,Timer WHERE Data.SpecialItems = Items.ItemsId" +
        " and Data.Time = Timer.TimerId";
        IDataReader reader = dbCmd.ExecuteReader();
        bool lava;
        bool water;
        while (reader.Read())
        {
            if (reader.GetString(5) == "True")
            {
                lava = true;
            }
            else { lava = false; }
            if (reader.GetString(6) == "True")
            {
                water = true;
            }
            else { water = false; }
            DataFromDB db = new DataFromDB(reader.GetInt32(0),reader.GetInt32(1),
                reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), lava,water);
            datafromdb.Add(db);
            playerData.data.Add(db);
        }
        foreach(DataFromDB db in datafromdb)
        {
             Debug.Log(db.ToString());
        }
        FileManager.WriteToFile("DataFromGame.json", JsonUtility.ToJson(playerData));
        datafromdb.Clear();
        playerData.data.Clear();
    }

    private void GenerateXMLFile(IDbConnection dbConnection)
    {
        IDbCommand dbCmd = dbConnection.CreateCommand();
        dbCmd.CommandText = "SELECT Data.GameId,Data.Coins,Data.Health,Timer.Minutes,Timer.Seconds" +
        ",Items.LavaBoots,Items.WaterBoots FROM Data,Items,Timer WHERE Data.SpecialItems = Items.ItemsId" +
        " and Data.Time = Timer.TimerId";
        IDataReader reader = dbCmd.ExecuteReader();
        bool lava;
        bool water;
        while (reader.Read())
        {
            if (reader.GetString(5) == "True")
            {
                lava = true;
            }
            else { lava = false; }
            if (reader.GetString(6) == "True")
            {
                water = true;
            }
            else { water = false; }
            DataFromDB db = new DataFromDB(reader.GetInt32(0), reader.GetInt32(1),
                reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), lava, water);
            datafromdb.Add(db);
            playerData.data.Add(db);
        }
        foreach (DataFromDB db in datafromdb)
        {
            Debug.Log(db.ToString());
        }
        XmlSerializer serializer = new XmlSerializer(typeof(Data));
        using (FileStream stream = new FileStream("PlayerData.xml", FileMode.Create))
        {
            serializer.Serialize(stream, playerData);
        }
        datafromdb.Clear();
        playerData.data.Clear();
    }

    private void UpdateHealth(IDbConnection dbConnection)
    {
        string command = "UPDATE Data SET Health = 4 WHERE Health < 4;";
        command = command.Remove(command.Length - 1, 1);
        command += ";";
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = command;
        dbCommand.ExecuteNonQuery();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GenerateXML()
    {
        IDbConnection dbConnection = OpenDataBase();
        GenerateXMLFile(dbConnection);
        Debug.Log("Xml file created");
        CloseDB(dbConnection);
    }
    public void GenerateJSON()
    {
        IDbConnection dbConnection = OpenDataBase();
        GenerateJSONFile(dbConnection);
        Debug.Log("Json file created");
        CloseDB(dbConnection);
    }
    public void DeleteDataBase()
    {
        IDbConnection dbConnection = OpenDataBase();
        DeleteData(dbConnection);
        Debug.Log("Data Deleted");
        CloseDB(dbConnection);
    }

    public void RestoreHealth()
    {
        IDbConnection dbConnection = OpenDataBase();
        UpdateHealth(dbConnection);
        Debug.Log("Health restored");
        CloseDB(dbConnection);
    }
}
