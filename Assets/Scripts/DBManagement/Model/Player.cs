using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using UnityEngine;

public class Player
{
    public const string TableName = "Players";

    public int Id { get; set; }

    public string Name { get; set; }

    public List<Achievement> Achievements = new List<Achievement>();

    public Player(SQLiteConnection connection, string playerName)
    {
        bool playerNameExist = false;

        string querry = $"SELECT * from {Player.TableName} WHERE NAME LIKE @name";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.Prepare();
            command.Parameters.AddWithValue("@name", playerName);

            using (SQLiteDataReader dr = command.ExecuteReader())
            {
                playerNameExist = dr.HasRows;
            }
        }

        if (!playerNameExist)
        {
            CreatePlayer(connection, playerName);
        }

        LoadPlayer(connection, playerName);
    }

    private void CreatePlayer(SQLiteConnection connection, string playerName)
    {
        string querry = $"INSERT INTO {Player.TableName} (Name) Values(@name)";
        long id = 0;
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.Prepare();
            command.Parameters.AddWithValue("@name", playerName);
            command.ExecuteScalar();
            id = command.Connection.LastInsertRowId;
        }

        DataTable dt = new DataTable();
        querry = $"SELECT id from {Achievement.TableName}";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            using (SQLiteDataReader dr = command.ExecuteReader())
            {
                dt.Load(dr);
            }
        }

        foreach (DataRow row in dt.Rows)
        {
            querry = $"INSERT INTO {Achievement.TableName_Values} (PlayerId, AchievementId) Values(@playerId, @achievementId)";
            using (SQLiteCommand command = new SQLiteCommand(querry, connection))
            {
                command.Prepare();
                command.Parameters.AddWithValue("@playerid", id);
                command.Parameters.AddWithValue("@achievementId", row[nameof(Achievement.Id)]);

                command.ExecuteNonQuery();
            }
        }
    }

    private void LoadPlayer(SQLiteConnection connection, string playerName)
    {
        DataTable dt = new DataTable();
        string querry = $"SELECT * from {Player.TableName} WHERE NAME LIKE @name";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.Prepare();
            command.Parameters.AddWithValue("@name", playerName);

            using (SQLiteDataReader dr = command.ExecuteReader())
            {
                dt.Load(dr);
                Id = Convert.ToInt32(dt.Rows[0][nameof(Id)]);
                Name = Convert.ToString(dt.Rows[0][nameof(Name)]);
            }
        }

        dt = new DataTable();
        querry = $"SELECT * from {Achievement.TableName} a LEFT JOIN {Achievement.TableName_Values} b ON a.id = b.AchievementId where b.PlayerId = @playerId";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.Prepare();
            command.Parameters.AddWithValue("@playerid", Id);

            using (SQLiteDataReader dr = command.ExecuteReader())
            {
                dt.Load(dr);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Achievements.Add(new Achievement(dt.Rows[i]));
                }
            }
        }
    }

    public void RefreshUnlocks(SQLiteConnection connection)
    {
        CharacterManager.Instance.LoadUnlockedCharacters(connection, CompletedAchievements());
        CharacterManager.Instance.LoadUnlockedEnemies(connection, CompletedAchievements());
        MapManager.Instance.LoadUnlockedMaps(connection, CompletedAchievements());
    }

    private List<Achievement> CompletedAchievements()
    {
        return Achievements.Where(x => x.Progress >= x.Treshold).ToList();
    }

    public void Save(SQLiteConnection connection)
    {
        string querry = $"Update {Player.TableName} SET Name = @name, Where Id = @Id";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.Prepare();
            command.Parameters.AddWithValue("@name", Name);
            command.Parameters.AddWithValue("@Id", Id);
            command.ExecuteNonQuery();
        }

        foreach (Achievement achievement in Achievements)
        {
            achievement.Save(connection, Id);
        }
    }
}
