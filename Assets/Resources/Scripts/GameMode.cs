using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲームの進行
///１．ゲームの立ち上げ
///２．マネージャー
///３．ゲーム設定
///４．オブジェクト情報
///５．オブジェクト追加
///６．ゲーム開始
/// </summary>
public class GameMode : MonoBehaviour {

    const int TS_MAX = 2;
    const int TS_MIN = 0;


    public enum MODE
    {
        TITLE=0,GAME,OPTION,NUM
    };
    MODE mode;
    int titleselect;
    bool isArrowed;

    public CameraControl cam;
    public ObjectManager Objects;
    public Player player;
    public Controller controller;
    public CanvasControl canvas;

    const float playerspeed = 4.5f;
    // Use this for initialization
    void Start ()
    {
        isArrowed = false;

        GameObject OM = new GameObject();
        OM.name = "ObjectManager";
        Objects = OM.gameObject.AddComponent<ObjectManager>();

        mode = MODE.TITLE;
        titleselect = 0;

        if (cam == null)
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>();
        }

        if (controller == null)
        {
            GameObject CO = new GameObject();
            CO.name = "Controller";
            controller = CO.gameObject.AddComponent<Controller>();
            controller.camera = cam.GetComponent<CameraControl>();
        }
    }

    // Update is called once per frame
    void Update () {
        Game();
	}

    void Game()
    {
        DispTitle();
        switch (mode)
        {
            case MODE.TITLE:
                TitleSelectInput();
                break;
            case MODE.GAME:
                if (Input.GetKeyDown(KeyCode.Space)||Input.GetButtonDown("circle"))
                {
                        GameEnd();
                        canvas.FadeOut();
                }
                break;
            case MODE.OPTION:
                break;
            case MODE.NUM:
                break;
            default:
                break;
        }
    }

    void DispCanvas(bool visible)
    {
        if (canvas != null)
        {
        }
    }

    //タイトルの表示
    void DispTitle()
    {
        if (canvas == null)
        {
            canvas = CanvasControl.CreateCanvas(cam.controlCamera).GetComponent<CanvasControl>();
        }
        else
        {
            canvas.ChangeText(titleselect);
        }
        //DispCanvas(true);
    }
    void TitleSelectInput()
    {
        int axis =(int)Input.GetAxis("LRArrow")+(int)Input.GetAxis("Horizontal");
        //左右移動
        if (axis != 0)
        {
            if (!isArrowed)
            {
                //-1で左、1で右
              titleselect+=(int)axis;
                if (titleselect < TS_MIN) titleselect = TS_MIN;
                if (titleselect > TS_MAX) titleselect = TS_MAX;
                isArrowed = true;
            }
        }
        else
        {
            isArrowed = false;
        }
        //選択
        if (Input.GetButtonDown("circle") || Input.GetKeyDown(KeyCode.Space))
        {
            if (mode == MODE.TITLE)
            {
                if (titleselect == 0)
                {
                    canvas.FadeIn();
                    StartUp();
                }
            }
        }
    }

    void StartUp()
    {
        mode = MODE.GAME;
        //ゲームの立ち上げ(ゲームプレイ時)
        //カメラの設定

        //CollisionManagerの生成
        if (Objects == null)
        {
            Objects = this.gameObject.AddComponent<ObjectManager>();
        }
        //３．ゲーム設定
        //playerの生成
        player = Player.CreatePlayer("a").GetComponent<Player>();
        player.tag = "Player";
        ObjectManager.AddObject(player.transform.gameObject);


        controller.player = player;
        controller.camera.player = player.gameObject;

        cam.distance = 1.0f;

        //４．オブジェクト情報

        //５．オブジェクト追加
        ObjectInstance();

        //６．ゲーム開始
        GameStart();
    }

    void GameStart()
    {
        mode = MODE.GAME;
    }

    void GameEnd()
    {
        mode = MODE.TITLE;
        Objects.AllClear();
        controller.player = null;
        player = null;
    }

    void GameStop()
    {

    }
    void ObjectInstance()
    {
        DataBaseManager.SpawnEnemyWave("EnemyData");
    }
}
