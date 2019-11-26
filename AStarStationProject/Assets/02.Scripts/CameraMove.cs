using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Vector3 prevMousePos;
    public float dragSpeed = -500.0f;
    public float maxX = 5.92f;
    public float maxY = 3.73f;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            prevMousePos = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - prevMousePos);
        Vector3 move = new Vector3(pos.x * dragSpeed * Time.deltaTime, pos.y * dragSpeed * Time.deltaTime, 0);
        move = CheckMaxPos(move);

        transform.Translate(move, Space.World);
        prevMousePos = Input.mousePosition;
    }

    public Vector3 ChangeToMaxPos(Vector3 originPos)
    {
        if (Mathf.Abs(originPos.x) > maxX)
        {
            if (originPos.x < 0)
            {
                originPos.x = -maxX;
            }
            else
            {
                originPos.x = maxX;
            }
        }
        if (Mathf.Abs(originPos.y) > maxY)
        {
            if (originPos.y < 0)
            {
                originPos.y = -maxY;
            }
            else
            {
                originPos.y = maxY;
            }
        }
        return originPos;
    }

    private Vector3 CheckMaxPos(Vector3 move)
    {
        Vector3 pos = transform.position;
        if (move.x > 0 && transform.position.x + move.x > maxX)
        {
            pos.x = maxX;
            transform.position = pos;
            move.x = 0;
        }
        else if (move.x < 0 && transform.position.x + move.x < -maxX)
        {
            pos.x = -maxX;
            transform.position = pos;
            move.x = 0;
        }
        if (move.y > 0 && transform.position.y + move.y > maxY)
        {
            pos.y = maxY;
            transform.position = pos;
            move.y = 0;
        }
        else if (move.y < 0 && transform.position.y + move.y < -maxY)
        {
            pos.y = -maxY;
            transform.position = pos;
            move.y = 0;
        }
        return move;
    }

}
