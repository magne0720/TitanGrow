using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour {


    public static List<GameObject> Robots;//自身から見て敵となるもの
    public static List<GameObject> Humans;//自身から見て敵となるもの

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void AddRobot(GameObject g)
    {
        Robots.Add(g);
    }
    public void AddHuman(GameObject g)
    {
        Humans.Add(g);
    }

    public void RemoveRobot(GameObject g)
    {
        Robots.Remove(g);

    }
    public void RemoveHuman(GameObject g)
    {
        Humans.Remove(g);
    }
}
