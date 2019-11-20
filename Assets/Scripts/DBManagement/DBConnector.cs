using UnityEngine;
using System.Data.SQLite;
using System.IO;
using System.Data;
using System;

public class DBConnector : MonoBehaviour
{
    private static string ConnectionString;
    public static Player Player;

    void Awake()
    {
        string DBPath = Path.Combine(Directory.GetParent(Application.dataPath).ToString(), "DataBases", "mydb.db");
        ConnectionString = $@"Data Source={DBPath};Version=3;";
        if (!Directory.Exists(Directory.GetParent(DBPath).ToString()))
        {
            Directory.CreateDirectory(Directory.GetParent(DBPath).ToString());            
        }
        if (!File.Exists(DBPath))
        {
            CreaterDataBase();
        }
    }

    private void CreaterDataBase()
    {
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            string querry = $"CREATE TABLE IF NOT EXISTS {Player.TableName} ( " +
                                  "  'id' INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                  "  'name' TEXT NOT NULL " +
                                  ");";
            using (SQLiteCommand command = new SQLiteCommand(querry, connection))
            {
                command.ExecuteNonQuery();
            }

            querry = $"CREATE TABLE IF NOT EXISTS {Achievement.TableName_Template} ( " +
                      "  'id' INTEGER PRIMARY KEY AUTOINCREMENT, " +
                      "  'name' TEXT NOT NULL, " +
                      "  'text' TEXT NOT NULL, " +
                      "  'treshold' INTEGER NOT NULL " +
                      ");";
            using (SQLiteCommand command = new SQLiteCommand(querry, connection))
            {
                command.ExecuteNonQuery();
            }

            querry = $"INSERT INTO {Achievement.TableName_Template} (Name, Text, Treshold) Values(@name, @text, @treshold)";
            using (SQLiteCommand command = new SQLiteCommand(querry, connection))
            {
                command.Prepare();
                command.Parameters.AddWithValue("@name", "Achievement1");
                command.Parameters.AddWithValue("@text", "Achievement1 text");
                command.Parameters.AddWithValue("@treshold", 10);
                command.ExecuteNonQuery();
            }

            querry = $"CREATE TABLE IF NOT EXISTS {Achievement.TableName_Values} ( " +
                      "  'playerId' INTEGER NOT NULL, " +
                      "  'achievementId' INTEGER NOT NULL, " +
                      "  'progress' INTEGER NOT NULL DEFAULT 0, " +
                      "  PRIMARY KEY('playerId', 'achievementId'), " +
                      $"  FOREIGN KEY('playerId') REFERENCES {Player.TableName}('id'), " +
                      $"  FOREIGN KEY('achievementId') REFERENCES {Achievement.TableName_Template}('id') " +
                      ");";
            using (SQLiteCommand command = new SQLiteCommand(querry, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public static Player SetPlayer(string playerName)
    {
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            Player = new Player(connection, playerName);
        }

        return Player;
    }
}
