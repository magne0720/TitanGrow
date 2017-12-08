using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    
    const float controlsensivity = 0.01f;
    const float camerasensivity = 0.01f;
    private float rotateSpeed = 2.0f;

    CharacterController cCon;
    CameraControl cCam;

    public Player player;
    private bool isControll;

    void Start()
    {
        cCon = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (isControll)
        {
            //カメラ操作
            float cameraX = Input.GetAxis("MouseX");
            float cameraY = Input.GetAxis("MouseY");
            cCam.InputJoystick(cameraX, cameraY);
            //プレイヤー移動
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(moveX, 0, moveZ);
            direction = Math.RotateY(direction, cCam.direction.x);
            if (direction.magnitude > controlsensivity)
            {
                player.SetTarget(direction);
            }
            //食べる
            if (Input.GetButtonDown("triangle")) player.GetComponent<Player>().Eat();
            //つかむ、投げる
            if (Input.GetButtonDown("square")) player.GetComponent<Player>().CatchAction();
            //離す
            if (Input.GetButtonDown("cross")) player.GetComponent<Player>().Release();
            //ズームインアウト
            if (Input.GetButton("R1")) cCam.distance += 0.02f;
            if (Input.GetButton("R2")) cCam.distance -= 0.02f;
            //デバッグ用(成長抑制)
            if (Input.GetButtonDown("L1")) player.FoodPoint++;
        }
    }
    public void SetCamera(CameraControl c)
    {
        cCam = c;
    }
    public void SetPlayer(Player p)
    {
        player = p;
        cCam.player = p.gameObject;
    }
    public void SetControll(bool  isOK=true)
    {
        isControll = isOK;
    }
}
