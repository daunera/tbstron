using UnityEngine;
using System.Data.SQLite;
using System.IO;

public class DBConnector : MonoBehaviour
{
    private SQLiteConnection connection;

    void Start()
    {
        string DBPath = Path.Combine(Directory.GetParent(Application.dataPath).ToString(), "DataBases", "mydb.db");
        if (!Directory.Exists(Directory.GetParent(DBPath).ToString()))
        {
            Directory.CreateDirectory(Directory.GetParent(DBPath).ToString());
        }
        bool DBExist = File.Exists(DBPath);

        connection = new SQLiteConnection($@"Data Source={DBPath};Version=3;");
        connection.Open();

        if (!DBExist)
        {
            SQLiteCommand command = connection.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "CREATE TABLE IF NOT EXISTS 'highscores' ( " +
                              "  'id' INTEGER PRIMARY KEY, " +
                              "  'name' TEXT NOT NULL, " +
                              "  'score' INTEGER NOT NULL" +
                              ");";

            command.ExecuteNonQuery();
        }
    }

    void OnDestroy()
    {
        connection.Close();
    }
}
