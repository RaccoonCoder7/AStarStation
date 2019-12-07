using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// A* 알고리즘을 이용해 최단경로를 구하는 클래스
public class AStar : MonoBehaviour
{
    public List<Transform> routeList = new List<Transform>();
    public GameObject ring;
    public GameObject panel;

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
    private TouchMgr touchMgr;
    private StationData stationData;
    private bool isEnd;
    private bool isSearching;

    void Start()
    {
        camMove = Camera.main.GetComponent<CameraMove>();
        rings = new GameObject("rings");
        stationData = FindObjectOfType<StationData>();
        touchMgr = FindObjectOfType<TouchMgr>();
    }

    public void DestroyRings()
    {
        Destroy(rings);
        rings = new GameObject("rings");
    }

    // 각역의 F, G, H 값을 구하는 함수
    private void FGHCalculation(Station st)
    {
        // 현재역과 인접역의 거리 + 지금까지 온 거리
        ConnStation connSt = nowStation.GetConnStationList().Find(item => item.GetStationName().Equals(st.GetStationName()));
        G = nowStation.GetG() + connSt.GetDist();

        // 환승할경우 환승거리를 더해줌
        if (st.GetLines().Find(item => item.Equals(nowLine)).Equals(0) && !nowLine.Equals(0))
        {
            int equalLine = Enumerable.Intersect(nowStation.GetLines(), st.GetLines()).First();
            string numToNum = equalLine > nowLine ? nowLine + "to" + equalLine : equalLine + "to" + nowLine;
            G += nowStation.GetTransferDic()[numToNum];
        }

        // 인접역과 목적지의 직선거리
        H = Vector2.Distance(st.GetPos(), destination.GetPos());

        F = G + H;
        st.SetG(G);
        st.SetH(H);
        st.SetF(F);

        st.SetParentName(nowStation.GetStationName());
    }

    public IEnumerator SearchPath(string start, string end)
    {
        isEnd = false;
        panel.SetActive(true);
        nowStation = stationData.GetStation(start);
        destination = stationData.GetStation(end);
        openedList.Add(nowStation);

        while (!isEnd)
        {
            if (isSearching)
            {
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                yield return StartCoroutine("LoopSearch");
            }
        }

        List<Station> finalList = closedList;
        routeList = ChangeStToTr(GetFinalRouteList());
        panel.SetActive(false);
        yield return StartCoroutine("RouteAnim");
        ResetApp();
        touchMgr.canTouch = true;
        yield return null;
    }

    private void ResetApp()
    {
        routeList = new List<Transform>();
        nowLine = 0;
        openedList = new List<Station>();
        closedList = new List<Station>();
        nowStation = null;
        destination = null;
        G = 0;
        F = 0;
        H = 0;
    }

    private List<Transform> ChangeStToTr(List<Station> finalList)
    {
        List<Transform> trList = new List<Transform>();
        foreach (Station st in finalList)
        {
            trList.Add(GameObject.Find(st.GetStationName()).transform);
        }
        return trList;
    }

    private IEnumerator LoopSearch()
    {
        isSearching = true;
        // 현재역을 클로즈리스트에 넣음
        // 클로즈리스트에 전에 오픈리스트에서 제거
        closedList.Add(nowStation);
        openedList.Remove(nowStation);
        foreach (ConnStation cs in nowStation.GetConnStationList())
        {
            Station st = stationData.GetStation(cs.GetStationName());
            SetStationLists(st);
            if (st.GetStationName().Equals(destination.GetStationName()))
            {
                destination.SetParentName(nowStation.GetStationName());
                isEnd = true;
                StopCoroutine("LoopSearch");
                break;
            }
        }
        if (openedList.Count == 0)
        {
            destination.SetParentName(nowStation.GetStationName());
            isEnd = true;
            StopCoroutine("LoopSearch");
        }
        if (!isEnd)
        {
            Station nearestStation = GetNearestStation();
            Debug.Log("now: "+nowStation.GetStationName()+" nearest : " + nearestStation.GetStationName()+" nowline: "+nowLine);
            if (nearestStation.GetParentName().Equals(nowStation.GetStationName()))
            {
                nowLine = Enumerable.Intersect(nowStation.GetLines(), nearestStation.GetLines()).First();
            }
            else
            {
                Station parentStation = stationData.GetStation(nearestStation.GetParentName());
                nowLine = Enumerable.Intersect(parentStation.GetLines(), nearestStation.GetLines()).First();
            }
            Debug.Log("nowLine: "+nowLine);

            nowStation = nearestStation;
        }
        yield return null;
        isSearching = false;
    }

    private void SetStationLists(Station st) // 탐색시작시 호출하는 메소드
    {
        // 현재역의 인접역이 닫힌목록에 있으면 True 없으면 False
        bool nowStationInClosedList = (closedList.Find(item => item.GetStationName().Equals(st.GetStationName())) != null);
        // 현재역의 인접역이 열린목록에 있으면 True 없으면 False
        bool nowStationInOpenedList = (openedList.Find(item => item.GetStationName().Equals(st.GetStationName())) != null);

        // 닫힌목록에 있으면 무시
        if (nowStationInClosedList) return;
        // 열린목록에 있으면 경로개선메소드 호출
        else if (nowStationInOpenedList) CheckRouteImproveRequired(st);
        // 열린목록에 없으면 열린목록에 추가하는 메소드 호출
        else AddNowStationToOpenList(st);
    }

    private void CheckRouteImproveRequired(Station st) // 경로개선 메소드
    {
        FGHCalculation(st);
        // Debug.Log("find: " + openedList.Find(item => item.GetStationName().Equals(st.GetStationName())));
        float originalG = openedList.Find(item => item.GetStationName().Equals(st.GetStationName())).GetG();
        float nowG = st.GetG();
        // 기존 오픈리스트에 있던 station G값보다 현재 station G값이 작으면 기존 station 삭제, 현재 station 추가
        if (originalG > nowG)
        {
            openedList.Remove(stationData.GetStation(st.GetStationName()));
            openedList.Add(st);
        }
    }

    // openList에 FGH를 계산하여 추가하는 메소드
    private void AddNowStationToOpenList(Station st)
    {
        FGHCalculation(st);
        openedList.Add(st);
    }

    // openList중 가장 가까운 역의 정보를 반환하는 메소드
    private Station GetNearestStation()
    {
        float highestF = (from openData in openedList select openData.GetF()).Min();
        Station s = openedList.Find(item => item.GetF().Equals(highestF));
        return openedList.Find(item => item.GetF().Equals(highestF));
    }

    // closeList에 저장된 역들을 목적지부터 부모 역을 따라 저장하여 최단루트 역리스트를 반환하는 메소드
    private List<Station> GetFinalRouteList()
    {
        List<Station> finalList = new List<Station>();
        // 목적지 역 저장
        finalList.Add(destination);
        Station nextStation = destination;
        // 출발역에 도달했을때 종료
        while (!closedList[0].GetStationName().Equals(nextStation.GetStationName()))
        {
            finalList.Add(closedList.Find(item => item.GetStationName().Equals(nextStation.GetParentName())));
            nextStation = finalList[finalList.Count - 1];
        }

        // 출발역 저장
        finalList.Add(nextStation);

        return finalList;
    }

    private IEnumerator RouteAnim()
    {
        for (int i = routeList.Count - 1; i >= 0; i--)
        {
            Vector3 pos = routeList[i].position;
            Instantiate(ring, pos, Quaternion.identity, rings.transform);
            pos.z = -10;
            pos = camMove.ChangeToMaxPos(pos);
            camMove.transform.position = pos;
            yield return new WaitForSeconds(0.33f);
        }
    }

}
