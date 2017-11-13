﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    private float movement = 3.0f;
    private float rotateSpeed = 2f;
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

        float cameraX = Input.GetAxis("MouseX");
        float cameraY = Input.GetAxis("MouseY");
        camera.InputJoystick(cameraX, cameraY);
        Vector3 abc = new Vector3(cameraX, cameraY, 0);
        if (abc.magnitude > camerasensivity)
        {

        }
        Debug.Log(cameraX+","+cameraY);
        
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical") ;
        Vector3 direction = new Vector3(moveX, 0, moveZ);
        if (direction.magnitude > controlsensivity)
        {
            player.character.TargetPosition = direction+player.character.MyPosition;
            player.character.SetTarget(direction);
            control = direction;
        }
        if (Input.GetButtonDown("triangle"))
        {

            Debug.Log("ok");
            
        }
        if (Input.GetButtonDown("square"))
        {

            Debug.Log("卍");

        }

        if (Input.GetButtonDown("cross"))
        {

            Debug.Log("unch");

        }

        if (Input.GetButtonDown("circle"))
        {

            Debug.Log("ばななぁ");

        }

    }
}
