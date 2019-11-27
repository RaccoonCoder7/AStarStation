using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StationDataReader : MonoBehaviour
{
    Station station;
    Vector2 pos;
    ConnStation connStation;
    public Station[] nodeStationInfos;

    string debugLog;

    void Start()
    {
        List<Dictionary<string, object>> csvData = CSVReader.Read("stationinfo");
        List<Dictionary<string, object>> csvConnDistenceDatas = CSVReader.Read("stationdistance");

        for (var i = 0; i < csvData.Count; i++)
        {


            for (int nodeCount = 0; nodeCount < nodeStationInfos.Length; nodeCount++)
            {
                if (nodeStationInfos[nodeCount] && nodeStationInfos[nodeCount].name.Equals(csvData[i]["stationName"].ToString()))
                {
                    station = nodeStationInfos[nodeCount].GetComponent<Station>();
                    Debug.Log(nodeStationInfos[nodeCount].GetStationName());
                }
            }
            
            pos = new Vector2(float.Parse(csvData[i]["stationPosX"].ToString()), float.Parse(csvData[i]["stationPosY"].ToString()));
            string[] splitLines = csvData[i]["lines"].ToString().Split('&');
            string[] splitConnStations = csvData[i]["connectedStations"].ToString().Split('/');
            string[] splitChageDistences = csvData[i]["changeLineDistenceInfos"].ToString().Split('/');
            
            station.SetId(csvData[i]["id"].ToString());
            debugLog += csvData[i]["id"].ToString() + " + ";
            station.SetStationName(csvData[i]["stationName"].ToString());
            debugLog += csvData[i]["stationName"].ToString() + " + ";
            station.SetPos(pos);
            debugLog += pos + " + ";

            foreach (string splitLine in splitLines)
            {
                station.AddLines(int.Parse(splitLine));
                debugLog += splitLine + " + ";
            }

            foreach (string splitConnStation in splitConnStations)
            {
                connStation = new ConnStation();

                connStation.SetStationName(splitConnStation);
                debugLog += splitConnStation + " + ";
                foreach (Dictionary<string, object> csvConnDistenceData in csvConnDistenceDatas)
                {
                    if (csvConnDistenceData["stationName1"].ToString().Equals(splitConnStation))
                    {
                        if (csvConnDistenceData["stationName2"].ToString().Equals(csvData[i]["stationName"].ToString()))
                        {
                            connStation.SetDist(int.Parse((float.Parse(csvConnDistenceData["distence"].ToString() ) * 1000).ToString()));
                            debugLog += csvConnDistenceData["distence"].ToString() + " + ";
                        }
                    }
                    else if (csvConnDistenceData["stationName2"].ToString().Equals(splitConnStation))
                    {
                        if (csvConnDistenceData["stationName1"].ToString().Equals(csvData[i]["stationName"].ToString()))
                        {
                            connStation.SetDist(int.Parse((float.Parse(csvConnDistenceData["distence"].ToString()) * 1000).ToString()));
                            debugLog += csvConnDistenceData["distence"].ToString() + " + ";
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

                    station.AddTransferDic(splitChageDistenceDetail[0], int.Parse(splitChageDistenceDetail[1].ToString()));
                    debugLog += splitChageDistenceDetail[0] + " + ";
                    debugLog += splitChageDistenceDetail[1].ToString() + " + ";

                }
            }
            catch
            {
                Debug.Log("ok error : splitChageDistence is null");
            }

            Debug.Log(debugLog);
            debugLog = "";
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
