using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//キャラクターの基底クラス
//これをアタッチすると以下の行動ができるようになる
//「移動」「ジャンプ」
//AIにも対応させるので、ここにはインターフェースの処理を書かないこと
public class BaseCharacter : MonoBehaviour
{
    public GameObject MyModel=null;
    public Vector3 MyPosition;
    public Vector3 TargetPosition;
    public float MySpeed;
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
    void Start()
    {
        MySpeed = 5.0f;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Initialize()
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
        if (Math.Length(moving) <= 0.1f) return;
       moving.Normalize();

        MyPosition += moving * MySpeed * Time.deltaTime;

        transform.position = new Vector3(MyPosition.x,0,MyPosition.z);

        SetDirection(moving);

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
    public void RotateY(float deg)
    {
        Vector3 vector = (TargetPosition-MyPosition).normalized;
        //ラジアンに変換
        float rag = Math.DegToRag(deg);

        float ax = vector.x * Mathf.Cos(rag) - vector.z * Mathf.Sin(rag);
        float az = vector.x * Mathf.Sin(rag) + vector.z * Mathf.Cos(rag);

        vector.x = ax ;
        vector.z = az ;

        SetDirection(vector);
        SetTarget(vector);
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
