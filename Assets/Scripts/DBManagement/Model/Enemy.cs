using System;
using System.Collections;
using System.Data;

public class Enemy
{
    public const string TableName = "Enemies";

    public int Id { get; set; }
    public string Name { get; set; }
    public int? AchievementId { get; set; }

    public Enemy()
    {
        Id = 0;
        Name = "Inactive";
        AchievementId = null;
    }

    public Enemy(DataRow row)
    {
        Id = Convert.ToInt32(row[nameof(Id)]);
        Name = Convert.ToString(row[nameof(Name)]);
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
