using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShader : MonoBehaviour
{

    public Material material;
    public Vector2 pos;
    public float timer;
    public bool isActive;
    public bool checkPoint;
    int activeMode;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, material);
    }

    // Use this for initialization
    void Start()
    {
        material = Resources.Load("Shaders/Changer") as Material;
    }

    // Update is called once per frame
    void Update()
    {

        if (timer > 1.0f)
        {
            StopShader();
        }
        if (timer < 0)
        {
            StopShader();
        }

        if (isActive)
            timer += Time.deltaTime * activeMode;
        //(x2+y2-1)3=x2y3

        float x = Mathf.Cos(timer * Mathf.PI * 2.0f);
        float y = Mathf.Sin(timer * Mathf.PI * 2.0f);

        material.SetFloat("u_offset_x", x);
        material.SetFloat("u_offset_y", y);

        //Vector3 mouse = Input.mousePosition;
        //mouse.x /= Screen.width;
        //mouse.y /= Screen.height;

        //material.SetVector("u_offsetXY", mouse);
        //Debug.Log("X," + mouse.x + "Y," + mouse.y);

        //material.SetFloat("u_timer", Mathf.Sin(timer * Mathf.PI * 2.0f * 2.0f));
        material.SetFloat("u_timer", timer * 1.5f);
    }
    public void StartShader(int mode)
    {
        if (isActive) return;

        isActive = true;
        if (mode > 0)
        {
            activeMode = 1;
            timer = 0;
        }
        else if (mode < 0)
        {
            activeMode = -1;
            timer = 1.0f;
        }
        else mode = 0;
    }
    public void StopShader()
    {
        isActive = false;
        if (timer < 0)
        {
            timer = 0;
            checkPoint = true;
        }
        if (timer > 1.0f)
        {
            timer = 1.0f;
        }
    }
}
