using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲームUIを表示するクラス
/// 現状はタイトル画面のみ
/// </summary>
public class CanvasControl : MonoBehaviour
{
    static Image canImage;
    static Text canText;
    public Canvas canvas;
    public Image image;
    public Text text;
    public Camera cam;
    public List<GameObject> destItems;
    public float timer;
    bool isFade;
    int select;
    int fader;
    public enum MODE
    {
        NONE,TITLE,GALLERY
    }
    MODE mode;

    public static GameObject CreateCanvas(Camera mCam)
    {
        GameObject canvas = new GameObject();
        canvas.name = "canvas";
        //キャンバス
        canvas.AddComponent<Canvas>();
        Canvas cCanavas = canvas.GetComponent<Canvas>();
        cCanavas.renderMode = RenderMode.ScreenSpaceOverlay;
        cCanavas.worldCamera = mCam;

        //タイトルロゴ
        GameObject cImage = new GameObject();
        cImage.name = "Image";
        cImage.transform.parent = canvas.transform;
        cImage.AddComponent<Image>().sprite = Resources.Load("Textures/TitleLogo_W_C_Screen", typeof(Sprite)) as Sprite;
        RectTransform rectImage = cImage.GetComponent<RectTransform>();
        rectImage.anchoredPosition = new Vector2();

        //選択のテキスト
        GameObject cText = new GameObject();
        cText.name = "Text";
        cText.transform.parent = canvas.transform;
        cText.AddComponent<Text>().font = Resources.Load("Fonts/arial", typeof(Font)) as Font;
        RectTransform rectText = cText.GetComponent<RectTransform>();
        rectText.anchoredPosition = new Vector2(0.2f, 0.2f);
        rectText.sizeDelta = new Vector2(Screen.width * 0.2f, Screen.height * 0.3f);

        //メンバーに追加
        canImage = cImage.GetComponent<Image>();
        canText = cText.GetComponent<Text>();

        canvas.AddComponent<CanvasControl>().cam = mCam;

        return canvas;
    }

    // Use this for initialization
    void Start()
    {
        if (image == null)
        {
            image = canImage;
            image.material.shader.name = "UI/Default";
        }
        if (text == null)
        {
            text = canText;
            text.color = new Color(255.0f/255.0f,0.0f/255.0f,128.0f/255.0f,1.0f);
            //text.material = Resources.Load("Shaders/X-Ray") as Material;
//            text.material.shader = new Shader();
        }
        if (destItems == null)
        {
            destItems = new List<GameObject>();
        }
        if (canvas == null)
        {
            canvas = gameObject.GetComponent<Canvas>();
        }
        isFade = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFade) Fader();
        
        DispDestItem();

        switch (mode)
        {
            case MODE.NONE:
                break;
            case MODE.TITLE:
                break;
            case MODE.GALLERY:
                break;

        }

        image.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        text.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        text.fontSize = (int)(Screen.width * 0.05f);
    }

    void Fader()
    {
        timer += Time.deltaTime * fader;
        text.color = new Color(1, 1, 1, timer);
        image.color = new Color(1, 1, 1, timer);

        if (timer > 1.0f || timer < 0)
        {
            isFade = false;
            return;
        }
    }

    public void FadeIn()
    {
        isFade = true;
        fader = -1;
        timer = 1.0f;
    }
    public void FadeOut()
    {
        isFade = true;
        fader = 1;
        timer = 0;
    }

    public void ChangeText(int i)
    {
        switch (i)
        {
            case 0:
                text.text = "→Start  　Gallery  　Option";
                break;
            case 1:
                text.text = "　Start  →Gallery  　Option";
                break;
            case 2:
                text.text = "　Start  　Gallery  →Option";
                break;
            case 3:
                text.text = "　Start  　Gallery  　Option";
                break;
            default:
                text.text = "　Start  　Gallery  　Option";
                break;
        }
    }

    public void ChangeGallery(int i)
    {
        DataBaseManager.OBJECT obj = DataBaseManager.GetEnemyNum(i);
        text.text = obj.name+"\n";
        text.text += obj.guid+"\n";
        text.text += obj.eat;
    }

    public void AddDestItem(GameObject g)
    {
        destItems.Add(g);
    }

    private void DispDestItem()
    {
        //float size = 1.0f;
        int count = 0;
        foreach (GameObject g in destItems)
        {
            g.transform.parent = this.transform;
            g.transform.position = new Vector3(count, 0, 0);
            g.transform.Rotate(new Vector3(0, 1.0f, 0));
            count++;
        }
    }public void HideImage()
    {
        if (image.enabled)
        {
            image.enabled = false;
        }
        else
        {
            image.enabled = true;
        }
    }
}
