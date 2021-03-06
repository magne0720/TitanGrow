﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbed : MonoBehaviour {

    public Vector3 defaultScale = Vector3.zero;
    // Use this for initialization
    void Start()
    {
        defaultScale = transform.lossyScale;
    }

    // Update is called once per frame
    void Update()
    {
        //大きさ固定
        Vector3 lossScale = transform.lossyScale;
        Vector3 localScale = transform.localScale;
        transform.localScale = new Vector3(
                localScale.x / lossScale.x * defaultScale.x,
                localScale.y / lossScale.y * defaultScale.y,
                localScale.z / lossScale.z * defaultScale.z
        );
    }
}
