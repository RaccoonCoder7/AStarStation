using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 시작화면을 없애주는 클래스(컴포넌트)
public class SelfActiveF : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
        }
    }
}
