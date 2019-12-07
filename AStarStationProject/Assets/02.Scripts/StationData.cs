using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 역의 정보를 모두 저장하고있는 클래스
public class StationData : MonoBehaviour
{
    public List<Station> stations;
    
    // 원하는 역정보를 역의 이름을 통해서 가져오는 메소드
    public Station GetStation(string stationName)
    {
        return stations.Find(item => item.GetStationName().Equals(stationName));
    }
}
