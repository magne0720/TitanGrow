﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    private float movement = 3.0f;
    private float rotateSpeed = 2.0f;
    float moveX = 0f, moveZ = 0f;
    public float controlsensivity = 0.01f;
    public float camerasensivity = 0.01f;

    CharacterController controller;
    public CameraControl camera;

    public Player player;
    public Vector3 control;
    

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float cameraX = Input.GetAxis("MouseX")/25;
        float cameraY = Input.GetAxis("MouseY")/25;
        camera.InputJoystick(cameraX, cameraY);
        Vector3 abc = new Vector3(cameraX, cameraY, 0);
        if (abc.magnitude > camerasensivity)
        {

        }
        //Debug.Log(cameraX+","+cameraY);
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(moveX, 0, moveZ);
        direction = Math.RotateY(direction,camera.direction.x);
       if (direction.magnitude > controlsensivity)
        {
            player.TargetPosition = direction+player.MyPosition;
            player.SetTarget(direction);
            control = direction;
        }
        if (Input.GetButtonDown("triangle")) player.GetComponent<Player>().Eat();

        if (Input.GetButtonDown("square")) player.GetComponent<Player>().Catch(player.catchObjects[0]);

        if (Input.GetButtonDown("cross")) player.GetComponent<Player>().Release();

        //if (Input.GetButtonDown("circle")) decision();

    }
}
