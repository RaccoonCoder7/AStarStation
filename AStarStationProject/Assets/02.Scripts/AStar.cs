using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    public List<Station> routeList = new List<Station>();
    public GameObject ring;

    private List<GameObject> rings = new List<GameObject>();
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
        StartCoroutine("RouteAnim");
    }

    public void DestroyRings()
    {
        foreach (GameObject r in rings)
        {
            Destroy(r);
        }
        rings = new List<GameObject>();
    }

    public IEnumerator SearchPath(Station start, Station end)
    {
        yield return null;
    }

    private IEnumerator RouteAnim()
    {
        foreach (Station item in routeList)
        {
            Vector3 pos = item.transform.position;
            GameObject clone = Instantiate(ring, pos, Quaternion.identity);
            rings.Add(clone);
            pos.z = -10;
            pos = camMove.ChangeToMaxPos(pos);
            camMove.transform.position = pos;
            yield return new WaitForSeconds(0.33f);
        }
    }

}
