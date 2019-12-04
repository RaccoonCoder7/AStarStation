using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private void FGHCalculation(Station st)
    {
        ConnStation connSt = nowStation.GetConnStationList().Find(item => item.GetStationName().Equals(st.GetStationName()));
        G = nowStation.GetG() + connSt.GetDist();

        if (st.GetLines().Find(item => item.Equals(nowLine)).Equals(null))
        {
            int equalLine = Enumerable.Intersect(nowStation.GetLines(), st.GetLines()).First();
            string numToNum = equalLine > nowLine ? nowLine + "to" + equalLine : equalLine + "to" + nowLine;
            G += nowStation.GetTransferDic()[numToNum];
        }

        H = Vector2.Distance(st.GetPos(), destination.GetPos());
        F = G + H;
        st.SetG(G);
        st.SetG(H);
        st.SetG(F);
        st.SetParentName(nowStation.GetStationName());
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
            nowStation = GetNearestStation();
        }
    }

    private void SetStationLists(Station st) // 탐색시작시 호출하는 메소드
    {
        // 현재역을 클로즈리스트에 넣음
        // 클로즈리스트에 전에 오픈리스트에서 제거
        openedList.Remove(nowStation);
        closedList.Add(nowStation);
        
        foreach (ConnStation connStation in nowStation.GetConnStationList())
        {
            // 현재역의 인접역이 닫힌목록에 있으면 True 없으면 False
            bool nowStationInClosedList = (closedList.Find(item => item.GetStationName().Equals(connStation.GetStationName())) != null);
            // 현재역의 인접역이 열린목록에 있으면 True 없으면 False
            bool nowStationInOpenedList = (openedList.Find(item => item.GetStationName().Equals(connStation.GetStationName())) != null);
            
            // 
            if (nowStationInClosedList) continue;
            // 열린목록에 있으면 경로개선메소드 호출
            else if (nowStationInOpenedList) CheckRouteImproveRequired(nowStation);
            // 열린목록에 없으면 열린목록에 추가하는 메소드 호출
            else AddNowStationToOpenList(nowStation);
        }
    }

    private void CheckRouteImproveRequired(Station st) // 경로개선 메소드
    {
        float originalG = openedList.Find(item => item.GetStationName().Equals(st.GetStationName())).GetG();
        float nowG = st.GetG();
        // 기존 오픈리스트에 있던 station G값보다 현재 station G값이 작으면 기존 station 삭제, 현재 station 추가
        if(originalG > nowG) {
            openedList.Remove(stationData.GetStation(st.GetStationName()));
            openedList.Add(st);
        }
    }

    private void AddNowStationToOpenList(Station st)
    {
        FGHCalculation(st);
        openedList.Add(st);
    }

    private Station GetNearestStation()
    {
        float highestF = (from openData in openedList select openData.GetF()).Min();
        return openedList.Find(item => item.GetF().Equals(highestF));
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
