using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 巨人が食べることの可能なものの基底クラス
/// 食べられるときにそのポイントを受け渡す
/// </summary>
public class EatBase : MonoBehaviour {

    public int HP;
    protected Vector3 MyPosition;
    protected Vector3 TargetPosition;
    protected Vector3 MyDirection;     //自身の向いている方向    
    protected int eatPoint;
    protected Vector3 ForcePosition;
    protected float force = 0, gravity = 0;
    protected bool isLand;

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
        if (Math.Length(TargetPosition) < 0.1f)
        {
            speed = 0;
        }
        //飛ばされの減衰
        if (force > 0) force -= Time.deltaTime * 3.0f;
        else if (force < 0) force = 0;
        //飛ばされている場合
        gameObject.GetComponent<Rigidbody>().AddForce(ForcePosition * force);
        //通常移動
        transform.position += (TargetPosition * speed * Time.deltaTime);

        SetDirection(new Vector3(TargetPosition.x,0,TargetPosition.z));//必要ない？

        if (Math.Length(TargetPosition) >= 1.0f)
        {
            Quaternion q = Quaternion.LookRotation(new Vector3(TargetPosition.x,0,TargetPosition.z));
            transform.rotation = q;
        }
    }
    //吹き飛ばされている
    public void ForceMove()
    {
        //飛ばされの減衰
        if (force > 0) force -= Time.deltaTime * 9.8f;
        else if (force < 0) force = 0;
        //飛ばされている場合
        gameObject.GetComponent<Rigidbody>().AddForce(ForcePosition * force, ForceMode.Force);
    }
    public void Gravity()
    {
        if (MyPosition.y < 0)
        {
            MyPosition.y = 0;
            isLand = true;
        }

        if (!isLand)
        {
            gravity += Time.deltaTime;

            MyPosition.y -= gravity;
        }
    }
}
