using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    private int nowLine;
    private string destinationID;
    private int G;
    private int F;
    private int H;
    private List<Station> openedList;
    private List<Station> closedList;
    private Station nowStation;

    void Start()
    {
        
    }

}
