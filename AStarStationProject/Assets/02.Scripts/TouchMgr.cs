using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMgr : MonoBehaviour
{
    public GameObject startImg;
    public GameObject endImg;

    private RaycastHit2D hit;
    private int stationLayer;
    private string nowStation;
    private string destination;
    private AStar aStar;

    void Start()
    {
        stationLayer = LayerMask.NameToLayer("Station");
        aStar = FindObjectOfType<AStar>();
        startImg = Instantiate(startImg);
        startImg.SetActive(false);
        endImg = Instantiate(endImg);
        endImg.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                TouchStation(hit.collider.name);
                return;
            }
            TouchOther();
        }
    }

    private void TouchStation(string name)
    {
        if (!startImg.activeSelf)
        {
            startImg.SetActive(true);
            startImg.transform.position = GetModifiedPos(name);
            nowStation = name;
            return;
        }
        if (!endImg.activeSelf)
        {
            if (name.Equals(nowStation))
            {
                TouchOther();
                return;
            }
            endImg.SetActive(true);
            endImg.transform.position = GetModifiedPos(name);
            destination = name;
            aStar.StartCoroutine(aStar.SearchPath(nowStation, destination));
            return;
        }
        TouchOther();
    }

    private void TouchOther()
    {
        if (!startImg.activeSelf)
        {
            return;
        }
        nowStation = null;
        startImg.SetActive(false);
        if (!endImg.activeSelf)
        {
            return;
        }
        aStar.DestroyRings();
        destination = null;
        endImg.SetActive(false);
    }

    private Vector3 GetModifiedPos(string name)
    {
        Vector3 pos = GameObject.Find(name).transform.position;
        pos.x += 0.2f;
        pos.y += 0.4f;
        return pos;
    }
}
