using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapicon : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
        pos.y = 490;
        transform.position = pos;

	}
}
