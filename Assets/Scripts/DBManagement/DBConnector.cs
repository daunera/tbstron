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

        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {

                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = $"CREATE TABLE IF NOT EXISTS {Player.TableName} ( " +
                                  "  'id' INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                  "  'name' TEXT NOT NULL " +
                                  ");";

                command.ExecuteNonQuery();
            }
        }
    }

    public static Player SetPlayer(string playerName)
    {
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            bool playerNameExist = false;
            DataTable dt = new DataTable();

            string querry = $"SELECT * from {Player.TableName} WHERE NAME LIKE @name LIMIT 1";
            using (SQLiteCommand command = new SQLiteCommand(querry, connection))
            {
                command.Prepare();
                command.Parameters.AddWithValue("@name", playerName);

                using (SQLiteDataReader dr = command.ExecuteReader())
                {
                    playerNameExist = dr.HasRows;
                    dt.Load(dr);
                }
            }

            if (!playerNameExist)
            {
                querry = $"INSERT INTO {Player.TableName} (Name) Values(@name)";
                using (SQLiteCommand command = new SQLiteCommand(querry, connection))
                {
                    command.Prepare();
                    command.Parameters.AddWithValue("@name", playerName);
                    command.ExecuteNonQuery();
                }

                querry = $"SELECT * from {Player.TableName} WHERE NAME LIKE @name";
                using (SQLiteCommand command = new SQLiteCommand(querry, connection))
                {
                    command.Prepare();
                    command.Parameters.AddWithValue("@name", playerName);

                    using (SQLiteDataReader dr = command.ExecuteReader())
                    {
                        dt.Load(dr);
                    }
                }
            }

            Player = new Player(dt.Rows[0]);
        }

        return Player;
    }
}
