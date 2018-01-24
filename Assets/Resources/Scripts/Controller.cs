using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーが操作するためのインターフェースクラス
/// </summary>
public class Controller : MonoBehaviour {
    
    const float controlsensivity = 0.01f;
    const float camerasensivity = 0.01f;
    private float rotateSpeed = 2.0f;

    CharacterController cCon;
    CameraControl cCam;

    public Player player;
    public Vector3 target;
    private bool isControll;

    void Start()
    {
        cCon = GetComponent<CharacterController>();
    }

    void Update()
    {  
        //カメラ操作
        float cameraX = Input.GetAxis("MouseX");
        float cameraY = Input.GetAxis("MouseY");
        //プレイヤー移動
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(moveX, 0, moveZ);
        target = Math.RotateY(direction, cCam.direction.x);

        //プレイヤー情報があるとき
        if (isControll)
        {
            if (direction.magnitude > controlsensivity)
            {
            }
                player.SetTargetTo(target);
            //食べる
            if (Input.GetButtonDown("triangle") || Input.GetMouseButtonDown(1))
                player.Eat();
            //つかむ、投げる
            if (Input.GetButtonDown("square") || Input.GetMouseButtonDown(0))
                player.CatchAction();
            //離す
            if (Input.GetButtonDown("cross") || Input.GetMouseButtonDown(3))
                player.Release();
        }
           
            //ズームインアウト
            if (Input.GetButton("R1")) cCam.distance += 0.04f;
            if (Input.GetButton("R2")) cCam.distance -= 0.04f;


        cCam.InputJoystick(target, cameraX, cameraY);

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
        ResetCamera();
    }
    public void ResetCamera()
    {
        transform.position = new Vector3();
        transform.rotation = new Quaternion();
    }
}
