﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 巨人が食べることの可能なものの基底クラス
/// 食べられるときにそのポイントを受け渡す
/// </summary>
public class EatBase : MonoBehaviour {

    protected int HP;
    public Vector3 MyPosition;
    public Vector3 TargetPosition;
    protected Vector3 MyDirection;     //自身の向いている方向    
    protected int eatPoint;
    public Vector3 ForcePosition;
    public float force = 0, gravity = 0;
    public bool isLand;

    protected Animator anim;

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
    public void SetDirection(Vector3 v)
    {
        MyDirection = v;
    }
    public virtual void Initialize()
    {
        HP=0;
        //MyPosition=new Vector3();
        TargetPosition=MyPosition;
        MyDirection=new Vector3();     //自身の向いている方向    
        eatPoint=0;
        ForcePosition=new Vector3();
        force = 0;
    }

    //移動
    public void Move(float speed = 0)
    {
        //飛ばされの減衰
        if (force > 0) force -= Time.deltaTime * 1.0f;
        else if (force < 0)
        {
            force = 0;
            ForcePosition = Vector3.zero;
        }
        //飛ばされている場合
        gameObject.transform.Translate(ForcePosition * force * Time.deltaTime);

        Vector3 moving = TargetPosition - MyPosition;
        moving.Normalize();
        //通常移動
        gameObject.transform.Translate(moving* speed * Time.deltaTime);
        SetDirection(new Vector3(moving.x,0,moving.z));//必要ない？
        if (Math.Length(moving) >= 1.0f)
        {
            Quaternion q = Quaternion.LookRotation(moving);
            transform.rotation = q;
        }
        //Gravity();


    }
    public void Gravity()
    {


        if (!isLand)
        {
            gravity += Time.deltaTime;

            MyPosition.y -= gravity;
        }
    }
    public void OnCollisionEnter(Collision c)
    {
        if (c.transform.tag == "Ground")
        {
            isLand = true;
        }
    }
    public void OnCollisionExit(Collision c)
    {
        if (c.transform.tag == "Ground")
        {
            isLand = false;
        }
    }
}
