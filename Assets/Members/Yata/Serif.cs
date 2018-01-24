using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Serif : MonoBehaviour {
    public string[] hairetsu;

    // Use this for initialization
    void Start()
    {
        TextAsset textasset = new TextAsset();
        textasset = Resources.Load("ObjectData", typeof(TextAsset)) as TextAsset;
        string textstring = textasset.text;
        string kugiru = "\n";
        hairetsu = textstring.Split(kugiru[0]);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
