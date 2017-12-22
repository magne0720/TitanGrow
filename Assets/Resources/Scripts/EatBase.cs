using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatBase : MonoBehaviour {

    public int HP;
    public float MySpeed;
    public Vector3 MyPosition;
    public Vector3 TargetPosition;
    public Vector3 MyDirection;     //自身の向いている方向    
    public int eatPoint;
    public Vector3 ForcePosition;
    public float force = 0;

    public void SetEatPoint(int i)
    {
        eatPoint = i;
    }
    public int GetEatPoint()
    {
        return eatPoint;
    }
    public void AddForce(Vector3 dir,float f)
    {
        ForcePosition = dir;
        force = f;
    }

}
