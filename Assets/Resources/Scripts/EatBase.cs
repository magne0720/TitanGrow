using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 巨人が食べることの可能なものの基底クラス
/// 食べられるときにそのポイントを受け渡す
/// </summary>
public class EatBase : MonoBehaviour {

    protected int HP;
    protected float MySpeed;
    protected Vector3 MyPosition;
    protected Vector3 TargetPosition;
    protected Vector3 MyDirection;     //自身の向いている方向    
    protected int eatPoint;
    protected Vector3 ForcePosition;
    protected float force = 0;

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
