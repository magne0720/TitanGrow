using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public GameObject player; //player
    public Vector3 playerPos;


    public float distance;
    public float speedX;
    public float speedY;

    // Use this for initialization
    void Start()
    {
        //初期化
        speedX = 30.0f;
        speedY = 30.0f;

        //タグ("Player")を検出
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        //playerの移動量分、カメラも移動
        transform.position -= player.transform.position - playerPos;
        playerPos = player.transform.position;
       // transform.position = new Vector3(0, 0, Math.Length(playerPos - transform.position) * player.transform.localScale.z);
        if (Input.GetMouseButton(0))
        {
            InputJoystick(Input.GetAxis("MouseX"), Input.GetAxis("MouseY"));
        }
        
        transform.LookAt(playerPos);
    }

    public void InputJoystick(float x,float y)
    {
        transform.RotateAround(playerPos, Vector3.up, x * speedX);
        transform.RotateAround(playerPos, Vector3.right, y * speedY);
    }

    void SetDistance(float s)
    {
        distance = s;
        if (s > 0)
        {
            distance *= -1;
        }
    }
}
