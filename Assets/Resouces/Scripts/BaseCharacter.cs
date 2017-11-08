using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//キャラクターの基底クラス
//これをアタッチすると以下の行動ができるようになる
//「移動」「ジャンプ」
//AIにも対応させるので、ここにはインターフェースの処理を書かないこと
public class BaseCharacter : MonoBehaviour
{
    public int MyHitpoint;
    public float MySpeed;
    public GameObject MyModel=null;
    public Vector3 MyPosition;
    public Vector3 TargetPosition;
    public Vector3 MyDirection;		//自身の向いている方向
    public Physics velo;

    static BaseCharacter CreateCharacter(string path, Vector3 pos, float speed)
    {
        BaseCharacter baseCharacter = new BaseCharacter();

        //baseCharacter.MyModel = Resources.Load(path) as GameObject;
        baseCharacter.MyPosition = pos;
        baseCharacter.MySpeed = speed;

        return baseCharacter;
    }

    // Use this for initialization
    public void Start()
    {
        MySpeed = 5.0f;
    }
    // Update is called once per frame
    public void Update()
    {
        Move();
        LiveCheck();
        
    }
    public void Initialize()
    {

    }
    public void MoveRight()
    {
        TargetPosition.x += 1.0f;
    }
    public void MoveLeft()
    {
        TargetPosition.x -= 1.0f;
    }
    public void MoveFront()
    {
        TargetPosition.z += 1.0f;
    }
    public void MoveBack()
    {
        TargetPosition.z -= 1.0f;
    }
    public void Move()
    {
        Vector3 moving = TargetPosition - MyPosition;
        if (Math.Length(moving) <= MySpeed*Time.deltaTime) return;
       moving.Normalize();

        MyPosition += moving * MySpeed * Time.deltaTime;

        transform.position = new Vector3(MyPosition.x,MyPosition.y,MyPosition.z);

        //SetDirection(moving);
        Quaternion q = Quaternion.LookRotation(moving);
        transform.rotation = q;

    }

    void Damage(int p)
    {
        MyHitpoint -= p;
    }

    void Death()
    {
        MyHitpoint = 0;
        Destroy(gameObject);
    }

    void LiveCheck()
    {
        if (MyHitpoint <= 0)
            Death();
    }

    public void SetTarget(Vector3 target)
    {
        TargetPosition = target;
    }
    public Vector3 GetTarget()
    {
        return TargetPosition;
    }

    public void SetDirection(Vector3 v)
    {
        MyDirection = v;
    }

    //Y軸基点で回転(正数で左回転？)
    public void RotateY(float deg,float height=1)
    {
        Vector3 vector = (TargetPosition-MyPosition).normalized;
        //ラジアンに変換
        float rag = Math.DegToRag(deg);

        float ax = vector.x * Mathf.Cos(rag) - vector.z * Mathf.Sin(rag);
        float az = vector.x * Mathf.Sin(rag) + vector.z * Mathf.Cos(rag);

        vector.x = ax ;
        vector.z = az ;

       // SetDirection(vector+transform.position);
        SetTarget(vector*height+MyPosition);
    }

    void OnCollisionEnter(Collision c)
    {

    }
    void OnCollisionStay(Collision c)
    {

    }
    void OnCollisionExit(Collision c)
    {

    }
}
