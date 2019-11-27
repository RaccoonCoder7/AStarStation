using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    public List<Station> routeList = new List<Station>();
    public GameObject ring;

    private GameObject rings;
    private int nowLine;
    private int G;
    private int F;
    private int H;
    private List<Station> openedList = new List<Station>();
    private List<Station> closedList = new List<Station>();
    private Station nowStation;
    private Station destination;
    private CameraMove camMove;

    void Start()
    {
        camMove = Camera.main.GetComponent<CameraMove>();
        rings = new GameObject("rings");
    }

    public void DestroyRings()
    {
        Destroy(rings);
        rings = new GameObject("rings");
    }

    public IEnumerator SearchPath(Station start, Station end)
    {
        // TODO: 계산중임을 표시할 무언가
        // TODO: 알고리즘 구현
        // TODO: RouteAnim 실행하기
        yield return null;
    }

    private IEnumerator RouteAnim()
    {
        foreach (Station item in routeList)
        {
            Vector3 pos = item.transform.position;
            Instantiate(ring, pos, Quaternion.identity, rings.transform);
            pos.z = -10;
            pos = camMove.ChangeToMaxPos(pos);
            camMove.transform.position = pos;
            yield return new WaitForSeconds(0.33f);
        }
    }

}
