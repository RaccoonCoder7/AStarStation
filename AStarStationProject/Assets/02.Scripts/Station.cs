using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station
{
    private string id;
    private string stationName;
    private string parentId;
    private Vector2 pos;
    private List<int> lines = new List<int>();
    private List<ConnStation> connStationList = new List<ConnStation>();
    private Dictionary<string, int> transferDic = new Dictionary<string, int>();
    private float f;
    private float g;
    private float h;

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

    public void SetParentId(string parentId)
    {
        this.parentId = parentId;
    }

    public string GetParentId()
    {
        return this.parentId;
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
