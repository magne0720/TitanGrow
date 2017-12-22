using System.Collections;
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

    public static void StartUpData()
    {
        GameObjects = new List<GameObject>();
    }

    public static void AddObject(GameObject g)
    {
        //g.name +=" "+ GameObjects.Count.ToString();
        GameObjects.Add(g);
    }
    public static void removeObject(GameObject g)
    {
        GameObjects.Remove(g);
    }
    public static void AllClear()
    {
        foreach(GameObject g in GameObjects)
        {
            Destroy(g);
        }
        GameObjects.Clear();
    }
}
