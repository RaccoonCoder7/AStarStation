using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StationDataReader : MonoBehaviour
{
    List<Station> stationInfos;
    Station station;
    Vector2 pos;
    ConnStation connStation;

    void Start()
    {
        stationInfos = new List<Station>();
        List<Dictionary<string, object>> csvData = CSVReader.Read("stationinfoTest");
        List<Dictionary<string, object>> csvConnDistenceDatas = CSVReader.Read("stationDistenceTest");

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
                foreach(Dictionary<string, object> csvConnDistenceData in csvConnDistenceDatas)
                {
                    if (csvConnDistenceData["stationName1"].ToString().Equals(splitConnStation) ||
                        csvConnDistenceData["stationName2"].ToString().Equals(splitConnStation))
                    {
                        connStation.SetDist(int.Parse(csvConnDistenceData["distence"].ToString()));
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

                    station.AddTransferDic(splitChageDistenceDetail[0], int.Parse(splitChageDistenceDetail[1].ToString()));
                }
            }
            catch
            {
                Debug.Log("ok error : splitChageDistence is null");
            }            

            stationInfos.Add(station);

            // id,stationName,stationPosX,stationPosY,lines,connectedStations,changeLineDistenceInfos
            //Debug.Log(" id : "+ csvData[i]["id"].ToString() + " stationName : " + csvData[i]["stationName"].ToString() + " stationPosX : " + csvData[i]["stationPosX"].ToString() +
            //    " stationPosY : " + csvData[i]["stationPosY"].ToString() + " lines : " + csvData[i]["lines"].ToString() + " connectedStations : " + csvData[i]["connectedStations"].ToString() +
            //    " changeLineDistenceInfos : " + csvData[i]["changeLineDistenceInfos"].ToString());
            //if (i == 0)
            //{
            //    Debug.Log("stationInfos[i].GetStationName(): " + stationInfos[i].GetStationName() + stationInfos[i].GetPos() + 
            //        stationInfos[i].GetLines()[0] + stationInfos[i].GetTransferDic()["4to6"] + stationInfos[i].GetConnStationList()[0].GetDist());

            //}
        }
    }
}
