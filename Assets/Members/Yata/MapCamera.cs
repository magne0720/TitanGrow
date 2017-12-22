using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour {
    public Transform target;
    public  float _height = 0f;

    void Start()
    {
    }

    void Update()
    {
        StartCoroutine(UpdatePosition());

    }
    IEnumerator UpdatePosition()
    {
        while (true)
        {

            yield return new WaitForFixedUpdate();
            transform.position = new Vector3(target.position.x, _height, target.position.z);
            transform.eulerAngles = new Vector3(90f, 0f, -target.eulerAngles.y);
        }
    }
}
