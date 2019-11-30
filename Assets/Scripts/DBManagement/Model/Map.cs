using UnityEngine;
using UnityEditor;
using System;
using System.Data;

public class Map 
{
    public const string TableName = "Maps";

    public int Id { get; set; }

    public string Name { get; set; }

    public int Height { get; set; }

    public int Width { get; set; }

    public int? AchievementId { get; set; }

    public Map(DataRow row)
    {
        Id = Convert.ToInt32(row[nameof(Id)]);
        Name = Convert.ToString(row[nameof(Name)]);
        Height = Convert.ToInt32(row[nameof(Height)]);
        Width = Convert.ToInt32(row[nameof(Width)]);
        if (row[nameof(AchievementId)] is DBNull)
        {
            AchievementId = null;
        }
        else
        {
            AchievementId = Convert.ToInt32(row[nameof(AchievementId)]);
        }
    }
}