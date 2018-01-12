using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ミニマップの表示をする
public class MiniMap : MonoBehaviour {

    public Camera mapCam;
    public Player player;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.rotation = player.transform.rotation;

        foreach(GameObject g in ObjectManager.GameObjects)
        {
            if (Math.OnSricleAndPoint(player.transform.position, 200.0f, g.transform.position))
            {
                //g.  q

            }
            
        }

    }
}
