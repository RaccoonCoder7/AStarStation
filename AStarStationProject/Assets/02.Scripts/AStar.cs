using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    public List<Transform> routeList = new List<Transform>();
    public GameObject ring;

    private GameObject rings;
    private int nowLine;
    private float G;
    private float F;
    private float H;
    private List<Station> openedList = new List<Station>();
    private List<Station> closedList = new List<Station>();
    private Station nowStation;
    private Station destination;
    private CameraMove camMove;
    private StationData stationData;

    void Start()
    {
        camMove = Camera.main.GetComponent<CameraMove>();
        rings = new GameObject("rings");
        stationData = FindObjectOfType<StationData>();
    }

    public void DestroyRings()
    {
        Destroy(rings);
        rings = new GameObject("rings");
    }

    public IEnumerator SearchPath(string start, string end)
    {
        // TODO: 계산중임을 표시할 무언가

        nowStation = stationData.GetStation(start);
        destination = stationData.GetStation(end);
        openedList.Add(nowStation);
        LoopSearch();
        List<Station> finalList = GetFinalRouteList();
        // TODO: station리스트를 transform리스트로 변환해야함
        // yeild return RouteAnim() coroutine

        // TODO: RouteAnim 실행하기
        yield return null;
    }

    private void LoopSearch()
    {
        float endTime = Time.time + 10;
        while (endTime < Time.time)
        {
            foreach (ConnStation cs in nowStation.GetConnStationList())
            {
                Station st = stationData.GetStation(cs.GetStationName());
                SetStationLists(st);
                if (st.GetStationName().Equals(destination.GetStationName()))
                {
                    return;
                }
            }
            if (openedList.Count == 0)
            {
                return;
            }
            nowStation = openedList[GetNearestStation()];
        }
    }

    private void SetStationLists(Station st)
    {

    }

    private void CheckRouteImproveRequired(Station st)
    {

    }

    private void AddNowStationToOpenList(Station st)
    {

    }

    private int GetNearestStation()
    {
        int index = 0;
        return index;
    }

    private List<Station> GetFinalRouteList()
    {
        return null;
    }

    private IEnumerator RouteAnim()
    {
        foreach (Transform item in routeList)
        {
            Vector3 pos = item.position;
            Instantiate(ring, pos, Quaternion.identity, rings.transform);
            pos.z = -10;
            pos = camMove.ChangeToMaxPos(pos);
            camMove.transform.position = pos;
            yield return new WaitForSeconds(0.33f);
        }
    }

}
