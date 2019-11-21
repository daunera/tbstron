using System;
using System.Data;
using System.Data.SQLite;
using UnityEngine;

public class Achievement
{
    public const string TableName = "Achievements_Template";
    public const string TableName_Values = "Achievements_Values";

    public int Id { get; set; }
    public string Name { get; set; }
    public string Text { get; set; }
    public int Progress { get; set; }
    public int Treshold { get; set; }

    public string ProgressText { get { return Progress < Treshold ? $"{Progress} / {Treshold}" : "Completed"; } }

    public Achievement(DataRow row)
    {
        Id = Convert.ToInt32(row[nameof(Id)]);
        Name = Convert.ToString(row[nameof(Name)]);
        Text = Convert.ToString(row[nameof(Text)]);
        Progress = Convert.ToInt32(row[nameof(Progress)]);
        Treshold = Convert.ToInt32(row[nameof(Treshold)]);
    }

    public void Save(SQLiteConnection connection, int playerId)
    {
        string querry = $"Update {Achievement.TableName_Values} SET Progress = @progress  Where Id = @PlayerId AND Achievementid = @AchievementId";
        using (SQLiteCommand command = new SQLiteCommand(querry, connection))
        {
            command.Prepare();           
            command.Parameters.AddWithValue("@progress", Math.Min(Progress, Treshold));
            command.Parameters.AddWithValue("@PlayerId", playerId);
            command.Parameters.AddWithValue("@AchievementId", Id);
            command.ExecuteNonQuery();
        }
    }
}