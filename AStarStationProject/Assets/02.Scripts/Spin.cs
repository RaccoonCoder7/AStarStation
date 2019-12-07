using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 탐색중에 표시하는 인디케이터의 애니메이션을 관리하는 클래스
public class Spin : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine("SpinMark");
    }

    private IEnumerator SpinMark()
    {
        while (true)
        {
            transform.Rotate(0, 0, 15);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
