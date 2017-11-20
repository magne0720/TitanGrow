using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour {

    public List<GameObject> GameObjects;//ゲームに使用するオブジェクト

	// Use this for initialization
	void Start () {
        GameObjects = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddObject(GameObject g)
    {
        GameObjects.Add(g);
    }
    public void removeObject(GameObject g)
    {
        Destroy(g);
    }
    public void AllClear()
    {
        foreach(GameObject g in GameObjects)
        {
            removeObject(g);
        }
        GameObjects.Clear();
    }
}
