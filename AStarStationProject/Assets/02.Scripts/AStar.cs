using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    public List<Station> routeList;
    public GameObject ring;

    private int nowLine;
    private string destinationID;
    private int G;
    private int F;
    private int H;
    private List<Station> openedList;
    private List<Station> closedList;
    private Station nowStation;
    private CameraMove camMove;

    void Start()
    {
        camMove = Camera.main.GetComponent<CameraMove>();
        StartCoroutine("RouteAnim");
    }

    private IEnumerator RouteAnim()
    {
        foreach (Station item in routeList)
        {
            Vector3 pos = item.transform.position;
            Instantiate(ring, pos, Quaternion.identity);
            pos.z = -10;
            pos = camMove.ChangeToMaxPos(pos);
            camMove.transform.position = pos;
            yield return new WaitForSeconds(0.33f);
        }
    }

}
