using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnStation
{
    private string stationName;
    private int dist;

    void Start()
    {
        
    }

    public void SetStationName(string stationName)
    {
        this.stationName = stationName;
    }

    public string GetStationName()
    {
        return this.stationName;
    }

    public void SetDist(int dist)
    {
        this.dist = dist;
    }

    public int GetDist()
    {
        return this.dist;
    }
}
