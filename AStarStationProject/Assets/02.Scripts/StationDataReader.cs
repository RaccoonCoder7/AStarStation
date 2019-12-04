using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StationDataReader : MonoBehaviour
{
    private Station station;
    private Vector2 pos;
    private ConnStation connStation;
    private StationData stationData;

    void Start()
    {
        stationData = FindObjectOfType<StationData>();
        List<Dictionary<string, object>> csvData = CSVReader.Read("stationinfo");
        List<Dictionary<string, object>> csvConnDistenceDatas = CSVReader.Read("stationdistance");
        stationData.stations = new List<Station>();

        for (var i = 0; i < csvData.Count; i++)
        {
            station = new Station();
            
            pos = new Vector2(float.Parse(csvData[i]["stationPosX"].ToString()), float.Parse(csvData[i]["stationPosY"].ToString()));
            string[] splitLines = csvData[i]["lines"].ToString().Split('&');
            string[] splitConnStations = csvData[i]["connectedStations"].ToString().Split('/');
            string[] splitChageDistences = csvData[i]["changeLineDistenceInfos"].ToString().Split('/');
            
            station.SetId(csvData[i]["id"].ToString());
            station.SetStationName(csvData[i]["stationName"].ToString());
            station.SetPos(pos);

            foreach (string splitLine in splitLines)
            {
                station.AddLines(int.Parse(splitLine));
            }

            foreach (string splitConnStation in splitConnStations)
            {
                connStation = new ConnStation();

                connStation.SetStationName(splitConnStation);
                foreach (Dictionary<string, object> csvConnDistenceData in csvConnDistenceDatas)
                {
                    if (csvConnDistenceData["stationName1"].ToString().Equals(splitConnStation))
                    {
                        if (csvConnDistenceData["stationName2"].ToString().Equals(csvData[i]["stationName"].ToString()))
                        {
                            connStation.SetDist(int.Parse((float.Parse(csvConnDistenceData["distence"].ToString() ) * 1000).ToString()));
                        }
                    }
                    else if (csvConnDistenceData["stationName2"].ToString().Equals(splitConnStation))
                    {
                        if (csvConnDistenceData["stationName1"].ToString().Equals(csvData[i]["stationName"].ToString()))
                        {
                            connStation.SetDist(int.Parse((float.Parse(csvConnDistenceData["distence"].ToString()) * 1000).ToString()));
                        }
                    }
                }

                station.AddConnStationList(connStation);
            }

            try
            {
                foreach (string splitChageDistence in splitChageDistences)
                {
                    string[] splitChageDistenceDetail = splitChageDistence.Split('-');
                    connStation = new ConnStation();

                    station.AddTransferDic(splitChageDistenceDetail[0], int.Parse(splitChageDistenceDetail[1].ToString()) * 18);

                }
            }
            catch
            {
                Debug.Log("ok error : splitChageDistence is null");
            }

            stationData.stations.Add(station);
        }
    }
}
