using System;
using System.Data;
using System.Drawing;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Character
{
    public const string TableName = "Characters";

    public int Id { get; set; }
    public string Name { get; set; }  
    public UnityEngine.Color Color { get; set; }
    public int? AchievementId { get; set; }
    public Sprite Sprite { get; set; }

    public Character()
    {
        Id = 0;
        Name = "Inactive";      
        Color = UnityEngine.Color.black;
        AchievementId = null;
        Sprite = null;
    }

    public Character(DataRow row)
    {
        Id = Convert.ToInt32(row[nameof(Id)]);
        Name = Convert.ToString(row[nameof(Name)]);
        System.Drawing.Color color = System.Drawing.Color.FromName(Convert.ToString(row[nameof(Color)]));
        Color = new UnityEngine.Color(color.R, color.G, color.B, color.A);
        if (row[nameof(AchievementId)] is DBNull)
        {
            AchievementId = null;
        }
        else
        {
            AchievementId = Convert.ToInt32(row[nameof(AchievementId)]);
        }

        Sprite = Resources.Load<Sprite>(Path.Combine("Images", "CharacterImages", Name));
    }
}