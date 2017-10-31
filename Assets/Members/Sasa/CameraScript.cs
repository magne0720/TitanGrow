using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    GameObject player; //player
    Vector3 playerPos;


    // Use this for initialization
    void Start()
    {
        //タグ("Player")を検出
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
      
        //playerの移動量分、カメラも移動
        transform.position += player.transform.position - playerPos;
        playerPos = player.transform.position;

        if (Input.GetMouseButton(0))
        {
            float mouseInputX = Input.GetAxis("Mouse X");
            float mouseInputY = Input.GetAxis("Mouse Y");
            transform.RotateAround(playerPos, Vector3.up, mouseInputX * Time.deltaTime * 200f);
            transform.RotateAround(playerPos, Vector3.right, mouseInputY * Time.deltaTime * 200f);
        }
    }
}
