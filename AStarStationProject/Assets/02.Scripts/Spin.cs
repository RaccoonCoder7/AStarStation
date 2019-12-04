﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    void Start()
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