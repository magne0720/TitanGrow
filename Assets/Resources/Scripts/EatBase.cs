﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatBase : MonoBehaviour {

    public int eatPoint;

    public void SetEatPoint(int i)
    {
        eatPoint = i;
    }
    public int GetEatPoint()
    {
        return eatPoint;
    }

}
