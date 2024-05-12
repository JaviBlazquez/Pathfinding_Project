using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DataFromDB
{
    public int Gameid;
    public int coins;
    public int health;
    public int minutes;
    public int seconds;
    public bool lavaboots;
    public bool waterboots;

    public DataFromDB(int Gameid,int coins,int health,int minutes,int seconds,bool lava,bool water)
    {
        this.Gameid = Gameid;
        this.coins = coins;
        this.health = health;
        this.minutes = minutes;
        this.seconds = seconds;
        this.lavaboots = lava;
        this.waterboots = water;
    }

    public DataFromDB()
    {

    }
    public string ToCSV()
    {
        return $"{Gameid}; {coins}; {health}; {minutes}; {seconds}; {lavaboots}; {waterboots}";
    }

    public override string ToString()
    {
        return $"{Gameid},{coins},{health},{minutes},{seconds},{lavaboots},{waterboots}";
    }

}
