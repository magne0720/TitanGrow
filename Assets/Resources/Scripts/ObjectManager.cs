﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    
    public static List<GameObject> GameObjects;//ゲームに使用するオブジェクト
    public List<GameObject> GameObjects_copy;
	// Use this for initialization
	void Start () {
        GameObjects = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        GameObjects_copy = GameObjects;
	}

    public static void AddObject(GameObject g)
    {
        g.name +=" "+ GameObjects.Count.ToString();
        GameObjects.Add(g);
    }
    public static void removeObject(GameObject g)
    {
        Destroy(g);
        //GameObjects.Remove(g);
    }
    public void AllClear()
    {
        foreach(GameObject g in GameObjects)
        {
            Debug.Log("objectsize=" + GameObjects.IndexOf(g));
            removeObject(g);
        }
        GameObjects.Clear();
    }
}
