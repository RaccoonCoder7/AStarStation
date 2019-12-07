using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 연결된 역 정보가 저장될 클래스
public class ConnStation
{
    // 역 이름
    private string stationName;
    // 연결된 역과의 거리
    private int dist;

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
