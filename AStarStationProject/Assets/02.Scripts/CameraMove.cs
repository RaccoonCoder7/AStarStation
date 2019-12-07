using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자 화면을 제어하는 클래스
public class CameraMove : MonoBehaviour
{
    public float dragSpeedX = -890.0f;
    public float dragSpeedY = -550.0f;
    public float maxX = 5.92f;
    public float maxY = 3.73f;

    private Vector3 prevMousePos;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // 휠을통한 화면확대
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && cam.orthographicSize < 8f)
        {
            cam.orthographicSize += 0.5f;
        }
        // 휠을통한 화면축소
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && cam.orthographicSize > 2f)
        {
            cam.orthographicSize -= 0.5f;
        }
        if (Input.GetMouseButtonDown(0))
        {
            prevMousePos = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        // 화면이동
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - prevMousePos);
        Vector3 move = new Vector3(pos.x * dragSpeedX * Time.deltaTime, pos.y * dragSpeedY * Time.deltaTime, 0);
        move = CheckMaxPos(move);

        transform.Translate(move, Space.World);
        prevMousePos = Input.mousePosition;
    }

    // 화면이 지도로부터 너무 멀어지지 않도록 고정해주는 함수
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

    // 화면 비율에 따른 화면 위치를 반환해주는 함수
    private Vector3 CheckMaxPos(Vector3 move)
    {
        Vector3 pos = transform.position;
        float calMaxX = GetCalculatedMaxX();
        float calMaxY = GetCalculatedMaxY();
        if (move.x > 0 && transform.position.x + move.x > calMaxX)
        {
            pos.x = calMaxX;
            transform.position = pos;
            move.x = 0;
        }
        else if (move.x < 0 && transform.position.x + move.x < -calMaxX)
        {
            pos.x = -calMaxX;
            transform.position = pos;
            move.x = 0;
        }
        if (move.y > 0 && transform.position.y + move.y > calMaxY)
        {
            pos.y = calMaxY;
            transform.position = pos;
            move.y = 0;
        }
        else if (move.y < 0 && transform.position.y + move.y < -calMaxY)
        {
            pos.y = -calMaxY;
            transform.position = pos;
            move.y = 0;
        }
        return move;
    }

    // 화면 X값의 위치를 반환하는 함수
    private float GetCalculatedMaxX()
    {
        return 5 / cam.orthographicSize * maxX;
    }

    // 화면 Y값의 위치를 반환하는 함수
    private float GetCalculatedMaxY()
    {
        return 5 / cam.orthographicSize * maxY;
    }
}
