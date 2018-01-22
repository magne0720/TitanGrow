using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 巨人が食べることの可能なものの基底クラス
/// 食べられるときにそのポイントを受け渡す
/// </summary>
public class EatBase : MonoBehaviour {

    protected int HP;
    protected Vector3 MyPosition;
    protected Vector3 TargetPosition;
    protected Vector3 MyDirection;     //自身の向いている方向    
    protected int eatPoint;
    protected Vector3 ForcePosition;
    protected float force = 0;

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

    public void Move()
    {
        if (transform.parent != null)
        {
            return;
        }
        MyPosition = transform.position;

        if (force > 0) force -=  Time.deltaTime*20.0f;
        else if (force < 0) force = 0;
        Vector3 moving = (ForcePosition*force)+TargetPosition-MyPosition;
        MyPosition += moving  * Time.deltaTime;

        //moving.Normalize();

        //MyPosition += moving;

        //MyPosition.y = transform.position.y;
        
        SetDirection(moving);//必要ない？
        if (Math.Length(moving) >= 1.0f)
        {
            Quaternion q = Quaternion.LookRotation(moving);
            transform.rotation = q;
        }
        transform.position = MyPosition;

        if(anim!=null)
        if (Math.Length(moving) <= 0.05f)
        {
            anim.SetFloat("walk", 0.0f);
            return;
        }
        else
        {
            anim.SetFloat("walk", 1.0f);
        }
    }
}
