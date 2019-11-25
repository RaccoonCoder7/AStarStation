using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationData : MonoBehaviour
{
    public List<Station> stations = new List<Station>();
    
    // station search
    public Station GetStation(string id)
    {
        return stations.Find(item => item.GetId().Equals(id));
    }
}
