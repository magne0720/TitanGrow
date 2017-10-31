using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    private float movement = 3.0f;
    private float rotateSpeed = 2f;
    float moveX = 0f, moveZ = 0f;
    public float controlsensivity = 0.01f;

    CharacterController controller;

    public BaseCharacter player;
    public Vector3 control;
    

    void Start()
    {
        controller = GetComponent<CharacterController>();
        
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical") ;
        Vector3 direction = new Vector3(moveX, 0, moveZ);
        if (direction.magnitude > controlsensivity)
        {
            player.TargetPosition += direction;
            control = direction;
        }

        
        if (Input.GetButtonDown("triangle"))
        {

            Debug.Log("ok");
            
        }
        //jump.y += Physics.gravity.y * Time.deltaTime;
    }
}
