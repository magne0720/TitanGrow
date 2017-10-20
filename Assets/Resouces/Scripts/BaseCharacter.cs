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

    static BaseCharacter CreateCharacter(string path, Vector3 pos, float speed)
    {
        BaseCharacter baseCharacter = new BaseCharacter();

        baseCharacter.MyModel = Resources.Load(path) as GameObject;
        baseCharacter.MyPosition = pos;
        baseCharacter.MySpeed = speed;

        return baseCharacter;
    }

    // Use this for initialization
    void Start()
    {

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
        moving.Normalize();

        MyPosition += moving * MySpeed * Time.deltaTime;

        transform.position = MyPosition;
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
