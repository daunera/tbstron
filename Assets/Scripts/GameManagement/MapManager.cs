using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    public List<Map> UnlockedMaps;
    public Map SelectedMap;

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
        UnlockedMaps = new List<Map>();
    }

    public void LoadUnlockedMaps(SQLiteConnection connection, List<Achievement> completedAchievements)
    {
        UnlockedMaps = new List<Map>();
        DataTable dt = new DataTable();
        string querry = $"SELECT * from {Map.TableName} " +
            $" WHERE achievementid is Null " +
            $" OR achievementid in ({string.Join(", ", completedAchievements.Select(x => x.Id))})";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            using (SQLiteDataReader dr = command.ExecuteReader())
            {
                dt.Load(dr);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UnlockedMaps.Add(new Map(dt.Rows[i]));
                }
            }
        }
    }
}
