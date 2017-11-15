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

    const int TS_MAX = 3;
    const int TS_MIN = 0;


    public enum MODE
    {
        TITLE=0,GAME,OPTION,NUM
    };
    MODE mode;
    int titleselect;

    public GameObject cam;
    public CollisionManager CM;
    public Player player;
    public Controller controller;
    public CanvasControl canvas;

    const float playerspeed = 4.5f;
    // Use this for initialization
    void Start ()
    {
        mode = MODE.TITLE;
        titleselect = 0;
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
                if (Input.GetKeyDown(KeyCode.Space))
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
            canvas = CanvasControl.CreateCanvas().GetComponent<CanvasControl>();
        }
        DispCanvas(true);
    }
    void TitleSelectInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //左を選ぶ
            if (titleselect < TS_MIN) titleselect = TS_MIN;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //右を選ぶ
            if (titleselect > TS_MAX) titleselect = TS_MAX;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (mode == MODE.TITLE)
            {
                canvas.FadeIn();
                StartUp();
            }
        }
    }

    void StartUp()
    {
        mode = MODE.GAME;
        //ゲームの立ち上げ(ゲームプレイ時)
        //カメラの設定
        if (cam == null)
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera");
        }

        //CollisionManagerの生成
        if (CM == null)
        {
            //CM = this.gameObject.AddComponent<CollisionManager>();
        }
        //３．ゲーム設定
        //playerの生成
        // if (player == null)
        if(player==null)
        {
            player = Player.Create("a").AddComponent<Player>();
            player.tag = "Player";
         }
        if (controller == null)
        {
            controller= this.gameObject.AddComponent<Controller>();
            controller.player = player;
            controller.camera = cam.GetComponent<CameraControl>();
            controller.camera.player = player.gameObject;
        }

        //４．オブジェクト情報

        //５．オブジェクト追加

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
    }

    void GameStop()
    {

    }

    public static void debugPoint(GameObject g)
    {
        for (int x = -2; x <= 2; x++)
            for (int y = -2; y <= 2; y++)
                for (int z = -2; z <= 2; z++)
                {
                    GameObject go = Instantiate(g);
                    go.transform.position = new Vector3(x * 10, z * 10, y * 10);
                }
    }
}
