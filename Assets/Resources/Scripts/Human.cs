using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : BaseCharacter
{
    private float speed = 4.0f;
    private Vector3 targetPosition;
    private float moveTime;
    private void Start()
    {
        targetPosition = GetRandomPosition();
        moveTime = Random.Range(1,5);
    }

    // Update is called once per frame
    void Update()
    {
        moveTime -= Time.deltaTime;
        //moveTimeが0以上なら行動する
        if (moveTime > 0)
        {
            //正面に進む
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - this.transform.position);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime / 2);
        }
        //moveTimeが0以下なら次のポイントを設定する
        if (moveTime < 0)
        {
            moveTime = MoveTime();
            targetPosition = GetRandomPosition();
            Debug.Log(targetPosition);
        }


    }

    public Vector3 GetRandomPosition()
    {
        float levelSize = 8.0f;
        return new Vector3(Random.Range(-levelSize+1, levelSize), 0, Random.Range(-levelSize, levelSize));
    }

    public float MoveTime()
    {
        return Random.Range(1, 5);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "wall")
        {
            moveTime = 0;
            moveTime = MoveTime();
            targetPosition = GetRandomPosition();
            Debug.Log(targetPosition);
        }
    }


}
