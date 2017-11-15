using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{
    static Image canImage;
    static Text canText;
    public Image image;
    public Text text;
    public float timer;
    bool isFade;
    int select;
    int fader;

    public static GameObject CreateCanvas()
    {
        GameObject canvas = new GameObject();
        canvas.name = "canvas";
        canvas.AddComponent<Canvas>();
        canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

        //タイトルロゴ
        GameObject cImage = new GameObject();
        cImage.name = "Image";
        cImage.transform.parent = canvas.transform;
        cImage.AddComponent<Image>().sprite = Resources.Load("Textures/TitleLogo_W_B", typeof(Sprite)) as Sprite;
        RectTransform rectImage = cImage.GetComponent<RectTransform>();
        rectImage.position = canvas.transform.position;
        rectImage.sizeDelta = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        //選択のテキスト
        GameObject cText = new GameObject();
        cText.name = "Text";
        cText.transform.parent = canvas.transform;
        cText.AddComponent<Text>().text = "      Start       Gallery      Option";
        cText.GetComponent<Text>().font = Resources.Load("Fonts/arial", typeof(Font)) as Font;
        RectTransform rectText = cText.GetComponent<RectTransform>();
        rectText.position = canvas.transform.position + new Vector3(0, Screen.height * 0.3f, 0); ;
        rectText.sizeDelta = new Vector2(Screen.width * 0.5f, Screen.height * 0.3f);

        //メンバーに追加
        canImage = cImage.GetComponent<Image>();
        canText = cText.GetComponent<Text>();

        canvas.AddComponent<CanvasControl>();

        return canvas;
    }

	// Use this for initialization
	void Start ()
    {
        if (image == null)
        {
            image = canImage;
        }
        if (text == null)
        {
            text = canText;
        }
        isFade = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (isFade) Fader();
    }

    void Fader()
    {
        timer += Time.deltaTime*fader;

        if (timer > 1.0f||timer<0)
        {
            isFade = false;
            return;
        }
        text.color = new Color(1, 1, 1, timer);
        image.color = new Color(1, 1, 1, timer);
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
}
