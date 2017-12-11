using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gallary : MonoBehaviour {

    public Text targetText;
    
    public List<GameObject> GallaryObjects;
    public Player player;
    public Camera camera;

    // Use this for initialization
    void Start () {
        //obj = null;
        GallaryObjects = new List<GameObject>();
        
    }
	
	// Update is called once per frame
	void Update () {
        //if(obj!=null)obj.transform.Rotate(new Vector3(0, 1.0f, 0));
        //this.targetText.text = tag;
        //Destroy(obj, 4.0f);
        //Destroy(targetText, 4.0f);
        //obj = null;
        if (Input.GetKeyDown(KeyCode.Q)) GallaryInst(BaseCharacter.CreateCharacter("a"));
        DispGallary();
    }

    void GallaryInst(GameObject obj)
    {
        GameObject g=Instantiate(obj);
        GallaryObjects.Add(g);
        Destroy(g, 5);
        
    }

    void GallaryInst(List<GameObject> objs)
    {
        foreach (GameObject g in objs)
        {
            GameObject obj = Instantiate(g);
            GallaryObjects.Add(obj);
        }
    }

    void DispGallary()
    {
        float screenwidth=Screen.width/2;
        float count = 0;
        int length= GallaryObjects.Count;
        foreach (GameObject g in GallaryObjects)
        {
            if (g != null)
            {
                g.transform.Rotate(new Vector3(0, 1.0f, 0));
                g.transform.position = new Vector3((count) * 2 / length, 0, 2);
                count++;
            }
            else
            {
                GallaryObjects.Remove(g);
            }
        }

    }

}
