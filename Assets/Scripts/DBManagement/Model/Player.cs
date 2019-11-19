using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Player
{
    public static string TableName = "Players";

    public int Id { get; set; }

    public string Name { get; set; }

    public Player(DataRow row)
    {
        Id = Convert.ToInt32(row[nameof(Id)]);
        Name = Convert.ToString(row[nameof(Name)]);
    }    
}
