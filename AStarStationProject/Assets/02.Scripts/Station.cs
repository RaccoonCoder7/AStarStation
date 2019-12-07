using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 역의 세부정보가 저장될 클래스
public class Station
{
    // 역의 id
    private string id;
    // 역이름
    private string stationName;
    // 부모역 이름
    private string parentName = "";
    // 역 위치
    private Vector2 pos;
    // 역이 가진 호선
    private List<int> lines = new List<int>();
    // 역과 연결된 역정보
    private List<ConnStation> connStationList = new List<ConnStation>();
    // 환승정보
    private Dictionary<string, int> transferDic = new Dictionary<string, int>();

    // f,g,h 를 저장
    private float f = 0;
    private float g = 0;
    private float h = 0;

    public void SetG(float g)
    {
        this.g = g;
    }

    public float GetG()
    {
        return this.g;
    }

    public void SetH(float h)
    {
        this.h = h;
    }

    public float GetH()
    {
        return this.h;
    }

    public void SetF(float f)
    {
        this.f = f;
    }

    public float GetF()
    {
        return this.f;
    }

    public void SetStationName(string stationName)
    {
        this.stationName = stationName;
    }

    public string GetStationName()
    {
        return this.stationName;
    }

    public void SetParentName(string parentName)
    {
        this.parentName = parentName;
    }

    public string GetParentName()
    {
        return this.parentName;
    }

    public void SetPos(Vector2 pos)
    {
        this.pos = pos;
    }

    public Vector2 GetPos()
    {
        return this.pos;
    }

    public void AddLines(int line)
    {
        this.lines.Add(line);
    }

    public void SetLines(List<int> lines)
    {
        this.lines = lines;
    }

    public List<int> GetLines()
    {
        return this.lines;
    }

    public void AddConnStationList(ConnStation connStation)
    {
        this.connStationList.Add(connStation);
    }

    public void SetConnStationList(List<ConnStation> connStationList)
    {
        this.connStationList = connStationList;
    }

    public List<ConnStation> GetConnStationList()
    {
        return this.connStationList;
    }

    public void AddTransferDic(string key, int value)
    {
        this.transferDic.Add(key, value);
    }

    public void SetTransferDic(Dictionary<string, int> transferDic)
    {
        this.transferDic = transferDic;
    }

    public Dictionary<string, int> GetTransferDic()
    {
        return this.transferDic;
    }

    public void SetId(string id)
    {
        this.id = id;
    }

    public string GetId()
    {
        return this.id;
    }
}
