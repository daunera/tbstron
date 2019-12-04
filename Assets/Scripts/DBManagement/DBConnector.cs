using UnityEngine;
using System.Data.SQLite;
using System.IO;
using System.Data;
using System;

public class DBConnector : MonoBehaviour
{
    private static string ConnectionString;

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

            CreatePlayers(connection);

            CreateAchievements(connection);

            CreateCharacters(connection);

            CreateEnemies(connection);

            CreateMaps(connection);
        }
    }

    private void CreatePlayers(SQLiteConnection connection)
    {
        string querry = $"CREATE TABLE IF NOT EXISTS {Player.TableName} ( " +
                                  "  'id' INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                  "  'name' TEXT NOT NULL " +
                                  ");";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.ExecuteNonQuery();
        }
    }

    private void CreateAchievements(SQLiteConnection connection)
    {
        string querry = $"CREATE TABLE IF NOT EXISTS {Achievement.TableName} ( " +
                      "  'id' INTEGER PRIMARY KEY AUTOINCREMENT, " +
                      "  'name' TEXT NOT NULL, " +
                      "  'text' TEXT NOT NULL, " +
                      "  'treshold' INTEGER NOT NULL, " +
                      "  'AchievementType' INTEGER NOT NULL " +
                      ");";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.ExecuteNonQuery();
        }

        querry = $"CREATE TABLE IF NOT EXISTS {Achievement.TableName_Values} ( " +
          "  'playerId' INTEGER NOT NULL, " +
          "  'achievementId' INTEGER NOT NULL, " +
          "  'progress' INTEGER NOT NULL DEFAULT 0, " +
          "  PRIMARY KEY('playerId', 'achievementId'), " +
          $"  FOREIGN KEY('playerId') REFERENCES {Player.TableName}('id'), " +
          $"  FOREIGN KEY('achievementId') REFERENCES {Achievement.TableName}('id') " +
          ");";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.ExecuteNonQuery();
        }


        querry = $"INSERT INTO {Achievement.TableName} (Name, Text, Treshold, AchievementType)" +
            $" Values(@name, @text, @treshold, @achievementtype)";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.Prepare();
            command.Parameters.AddWithValue("@name", "Beginner Player");
            command.Parameters.AddWithValue("@text", "Play a set amount of games");
            command.Parameters.AddWithValue("@treshold", 1);
            command.Parameters.AddWithValue("@achievementtype", enAchievementType.GamesPlayed);
            command.ExecuteNonQuery();
        }

        querry = $"INSERT INTO {Achievement.TableName} (Name, Text, Treshold, AchievementType)" +
            $" Values(@name, @text, @treshold, @achievementtype)";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.Prepare();
            command.Parameters.AddWithValue("@name", "Intermediate Player");
            command.Parameters.AddWithValue("@text", "Play a set amount of games");
            command.Parameters.AddWithValue("@treshold", 10);
            command.Parameters.AddWithValue("@achievementtype", enAchievementType.GamesPlayed);
            command.ExecuteNonQuery();
        }

        querry = $"INSERT INTO {Achievement.TableName} (Name, Text, Treshold, AchievementType)" +
            $" Values(@name, @text, @treshold, @achievementtype)";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.Prepare();
            command.Parameters.AddWithValue("@name", "Advanced Player");
            command.Parameters.AddWithValue("@text", "Play a set amount of games");
            command.Parameters.AddWithValue("@treshold", 100);
            command.Parameters.AddWithValue("@achievementtype", enAchievementType.GamesPlayed);
            command.ExecuteNonQuery();
        }

        querry = $"INSERT INTO {Achievement.TableName} (Name, Text, Treshold, AchievementType)" +
            $" Values(@name, @text, @treshold, @achievementtype)";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.Prepare();
            command.Parameters.AddWithValue("@name", "Beginner Champion");
            command.Parameters.AddWithValue("@text", "Win a set amount of games");
            command.Parameters.AddWithValue("@treshold", 1);
            command.Parameters.AddWithValue("@achievementtype", enAchievementType.GamesWon);
            command.ExecuteNonQuery();
        }

        querry = $"INSERT INTO {Achievement.TableName} (Name, Text, Treshold, AchievementType)" +
            $" Values(@name, @text, @treshold, @achievementtype)";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.Prepare();
            command.Parameters.AddWithValue("@name", "Intermediate Champion");
            command.Parameters.AddWithValue("@text", "Win a set amount of games");
            command.Parameters.AddWithValue("@treshold", 10);
            command.Parameters.AddWithValue("@achievementtype", enAchievementType.GamesWon);
            command.ExecuteNonQuery();
        }

        querry = $"INSERT INTO {Achievement.TableName} (Name, Text, Treshold, AchievementType)" +
            $" Values(@name, @text, @treshold, @achievementtype)";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.Prepare();
            command.Parameters.AddWithValue("@name", "Advanced Champion");
            command.Parameters.AddWithValue("@text", "Win a set amount of games");
            command.Parameters.AddWithValue("@treshold", 100);
            command.Parameters.AddWithValue("@achievementtype", enAchievementType.GamesWon);
            command.ExecuteNonQuery();
        }

        querry = $"INSERT INTO {Achievement.TableName} (Name, Text, Treshold, AchievementType)" +
            $" Values(@name, @text, @treshold, @achievementtype)";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.Prepare();
            command.Parameters.AddWithValue("@name", "Beginner Killer");
            command.Parameters.AddWithValue("@text", "Kill a set amount of enemies");
            command.Parameters.AddWithValue("@treshold", 1);
            command.Parameters.AddWithValue("@achievementtype", enAchievementType.EnemiesSlain);
            command.ExecuteNonQuery();
        }

        querry = $"INSERT INTO {Achievement.TableName} (Name, Text, Treshold, AchievementType)" +
            $" Values(@name, @text, @treshold, @achievementtype)";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.Prepare();
            command.Parameters.AddWithValue("@name", "Intermediate Killer");
            command.Parameters.AddWithValue("@text", "Kill a set amount of enemies");
            command.Parameters.AddWithValue("@treshold", 10);
            command.Parameters.AddWithValue("@achievementtype", enAchievementType.EnemiesSlain);
            command.ExecuteNonQuery();
        }

        querry = $"INSERT INTO {Achievement.TableName} (Name, Text, Treshold, AchievementType)" +
            $" Values(@name, @text, @treshold, @achievementtype)";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.Prepare();
            command.Parameters.AddWithValue("@name", "Advanced Killer");
            command.Parameters.AddWithValue("@text", "Kill a set amount of enemies");
            command.Parameters.AddWithValue("@treshold", 100);
            command.Parameters.AddWithValue("@achievementtype", enAchievementType.EnemiesSlain);
            command.ExecuteNonQuery();
        }
    }

    private void CreateCharacters(SQLiteConnection connection)
    {
        string querry = $"CREATE TABLE IF NOT EXISTS {Character.TableName} ( " +
                      "  'id' INTEGER PRIMARY KEY AUTOINCREMENT, " +
                      "  'name' TEXT NOT NULL, " +
                      "  'color' TEXT NOT NULL, " +
                      "  'achievementid' INTEGER,  " +
                      $"  FOREIGN KEY('achievementid') REFERENCES {Achievement.TableName}('id')" +
                      ");";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.ExecuteNonQuery();
        }

        querry = $"INSERT INTO {Character.TableName} (Name, color, achievementid) Values(@name, @color, @achievementid)";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.Prepare();
            command.Parameters.AddWithValue("@name", "FriendOBot");
            command.Parameters.AddWithValue("@color", "lightblue");
            command.Parameters.AddWithValue("@achievementid", null);
            command.ExecuteNonQuery();
        }

        querry = $"INSERT INTO {Character.TableName} (Name, color, achievementid) Values(@name, @color, @achievementid)";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.Prepare();
            command.Parameters.AddWithValue("@name", "Flamethrower");
            command.Parameters.AddWithValue("@color", "red");
            command.Parameters.AddWithValue("@achievementid", null);
            command.ExecuteNonQuery();
        }

        querry = $"INSERT INTO {Character.TableName} (Name, color, achievementid) Values(@name, @color, @achievementid)";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.Prepare();
            command.Parameters.AddWithValue("@name", "MoonMoon");
            command.Parameters.AddWithValue("@color", "green");
            command.Parameters.AddWithValue("@achievementid", null);
            command.ExecuteNonQuery();
        }

        querry = $"INSERT INTO {Character.TableName} (Name, color, achievementid) Values(@name, @color, @achievementid)";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.Prepare();
            command.Parameters.AddWithValue("@name", "Assaultron");
            command.Parameters.AddWithValue("@color", "yellow");
            command.Parameters.AddWithValue("@achievementid", 2);
            command.ExecuteNonQuery();
        }
        querry = $"INSERT INTO {Character.TableName} (Name, color, achievementid) Values(@name, @color, @achievementid)";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.Prepare();
            command.Parameters.AddWithValue("@name", "Obliterator");
            command.Parameters.AddWithValue("@color", "purple");
            command.Parameters.AddWithValue("@achievementid", 3);
            command.ExecuteNonQuery();
        }
    }

    private void CreateEnemies(SQLiteConnection connection)
    {
        string querry = $"CREATE TABLE IF NOT EXISTS {Enemy.TableName} ( " +
              "  'id' INTEGER PRIMARY KEY AUTOINCREMENT, " +
              "  'name' TEXT NOT NULL, " +
              "  'achievementid' INTEGER,  " +
              $"  FOREIGN KEY('achievementid') REFERENCES {Achievement.TableName}('id')" +
              ");";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.ExecuteNonQuery();
        }

        querry = $"INSERT INTO {Enemy.TableName} (Name, achievementid) Values(@name, @achievementid)";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.Prepare();
            command.Parameters.AddWithValue("@name", "DummyAi");
            command.Parameters.AddWithValue("@achievementid", null);
            command.ExecuteNonQuery();
        }

        querry = $"INSERT INTO {Enemy.TableName} (Name, achievementid) Values(@name, @achievementid)";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.Prepare();
            command.Parameters.AddWithValue("@name", "SurvivalAi");
            command.Parameters.AddWithValue("@achievementid", null);
            command.ExecuteNonQuery();
        }

        querry = $"INSERT INTO {Enemy.TableName} (Name, achievementid) Values(@name, @achievementid)";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.Prepare();
            command.Parameters.AddWithValue("@name", "ChaserAi");
            command.Parameters.AddWithValue("@achievementid", null);
            command.ExecuteNonQuery();
        }
    }

    private void CreateMaps(SQLiteConnection connection)
    {
        string querry = $"CREATE TABLE IF NOT EXISTS {Map.TableName} ( " +
                      "  'id' INTEGER PRIMARY KEY AUTOINCREMENT, " +
                      "  'name' TEXT NOT NULL, " +
                      "  'height' INTEGER NOT NULL, " +
                      "  'width' INTEGER NOT NULL, " +
                      "  'achievementid' INTEGER,  " +
                      $"  FOREIGN KEY('achievementid') REFERENCES {Achievement.TableName}('id')" +
                      ");";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.ExecuteNonQuery();
        }

        querry = $"INSERT INTO {Map.TableName} (Name, height, width, achievementid)" +
            $" Values(@name, @height, @width, @achievementid)";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.Prepare();
            command.Parameters.AddWithValue("@name", "Basic Map");
            command.Parameters.AddWithValue("@height", 20);
            command.Parameters.AddWithValue("@width", 20);
            command.Parameters.AddWithValue("@achievementid", null);
            command.ExecuteNonQuery();
        }

        querry = $"INSERT INTO {Map.TableName} (Name, height, width, achievementid)" +
            $" Values(@name, @height, @width, @achievementid)";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.Prepare();
            command.Parameters.AddWithValue("@name", "Basic Walls Map");
            command.Parameters.AddWithValue("@height", 50);
            command.Parameters.AddWithValue("@width", 100);
            command.Parameters.AddWithValue("@achievementid", null);
            command.ExecuteNonQuery();
        }
    }

    public static void SetPlayer(string playerName)
    {
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            PlayerManager.Instance.Player = new Player(connection, playerName);
        }
    }

    public static void RefreshUnlocks()
    {
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            PlayerManager.Instance.Player.RefreshUnlocks(connection);
        }
    }

    internal static void Save()
    {
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            PlayerManager.Instance.Player.Save(connection);
        }
    }
}
