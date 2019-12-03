using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationData : MonoBehaviour
{
    public List<Station> stations;
    
    // station search
    public Station GetStation(string stationName)
    {
        return stations.Find(item => item.GetStationName().Equals(stationName));
    }
}
