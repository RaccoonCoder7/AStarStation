using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    private string id;
    private string stationName;
    private string parentId;
    private Vector2 pos;
    private int line;
    private List<ConnStation> connStationList;
    private Dictionary<string, int> transferDic = new Dictionary<string, int>();

    void Start()
    {
        
    }

    public string GetId()
    {
        return this.id;
    }
}
