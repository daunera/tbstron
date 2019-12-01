using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Data.SQLite;
using System.Data;
using System.Linq;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;

    public List<Character> UnlockedCharacters;

    public List<Enemy> UnlockedEnemies;

    public List<Character> SelectedCharacters;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        UnlockedCharacters = new List<Character>();
    }

    public void LoadUnlockedCharacters(SQLiteConnection connection, List<Achievement> completedAchievements)
    {
        UnlockedCharacters = new List<Character>();
        DataTable dt = new DataTable();
        string querry = $"SELECT * from {Character.TableName} " +
            $" WHERE achievementid is Null " +
            $" OR achievementid in ({string.Join(", ", completedAchievements.Select(x => x.Id))})";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            using (SQLiteDataReader dr = command.ExecuteReader())
            {
                dt.Load(dr);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UnlockedCharacters.Add(new Character(dt.Rows[i]));
                }
            }
        }
    }

    public void LoadUnlockedEnemies(SQLiteConnection connection, List<Achievement> completedAchievements)
    {
        UnlockedEnemies = new List<Enemy>();
        DataTable dt = new DataTable();
        string querry = $"SELECT * from {Enemy.TableName} " +
            $" WHERE achievementid is Null " +
            $" OR achievementid in ({string.Join(", ", completedAchievements.Select(x => x.Id))})";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            using (SQLiteDataReader dr = command.ExecuteReader())
            {
                dt.Load(dr);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UnlockedEnemies.Add(new Enemy(dt.Rows[i]));
                }
            }
        }
    }
}
