using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMgr : MonoBehaviour
{
    public GameObject startImg;
    public GameObject endImg;

    private RaycastHit2D hit;
    private int stationLayer;
    private Station nowStation;
    private Station destination;
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
                Debug.Log(hit.collider.name);
            }
        }
    }

    private void TouchStation()
    {
        if (!startImg.activeSelf)
        {
            startImg.SetActive(true);
            startImg.transform.position = Vector3.zero; // TODO: station 포지션으로
            nowStation = new Station(); // TODO: station으로
            return;
        }
        if (!endImg.activeSelf)
        {
            endImg.SetActive(true);
            endImg.transform.position = Vector3.zero; // TODO: station 포지션으로
            destination = new Station(); // TODO: station으로
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
}
