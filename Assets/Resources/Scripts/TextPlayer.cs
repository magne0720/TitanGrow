using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPlayer : MonoBehaviour {
    public string[] scenarios;
    public GameMode game;

    [SerializeField]
    Text uiText;

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharacterDisplay = 0.05f;  // 1文字の表示にかかる時間

    private int currentLine = 0;
    private string currentText = string.Empty;  // 現在の文字列
    private float timeUntilDisplay = 0;     // 表示にかかる時間
    private float timeElapsed = 1;          // 文字列の表示を開始した時間
    private int lastUpdateCharacter = -1;       // 表示中の文字数

    private float autoChangeTimer = 0;//自動変更
    public float changeTime = 8.0f;

    public bool IsCompleteDisplayText
    {
        get { return Time.time > timeElapsed + timeUntilDisplay; }
    }

    void Start()
    {
        TextAsset textasset = new TextAsset();
        textasset = Resources.Load("Others/Serif", typeof(TextAsset)) as TextAsset;
        string textstring = textasset.text;
        string kugiru = "\n";
        scenarios = textstring.Split(kugiru[0]);
    }

    void Update()
    {
        uiText.enabled = false;
        if (game.mode == GameMode.MODE.GAME)
        {
            autoChangeTimer += Time.deltaTime;
            uiText.enabled = true;
            // 文字の表示が完了してるならクリック時に次の行を表示する
            if (IsCompleteDisplayText)
            {
                if (currentLine < scenarios.Length)
                    if (Input.GetButtonDown("circle") || autoChangeTimer >= changeTime)
                    {
                        SetNextLine();
                        autoChangeTimer = 0;
                    }
            }
            else
            {
                // 完了してないなら文字をすべて表示する
                if (Input.GetButtonDown("circle"))
                {
                    timeUntilDisplay = 0;
                }
            }
            int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
            if (displayCharacterCount != lastUpdateCharacter)
            {
                uiText.text = currentText.Substring(0, displayCharacterCount);
                lastUpdateCharacter = displayCharacterCount;
            }
        }
        else
        {
            SetResetLine();
        }
    }
   public void SetResetLine()
    {
        currentLine = 0;
        currentText = scenarios[currentLine];
        timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
        lastUpdateCharacter = -1;
    }

    void SetNextLine()
    {
        currentText = scenarios[currentLine];
        timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
        timeElapsed = Time.time;
        currentLine++;
        lastUpdateCharacter = -1;
        if (currentLine >= scenarios.Length) 
        {
            SetResetLine();
        }
    }
}

