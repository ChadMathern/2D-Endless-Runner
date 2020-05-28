using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour {
   
    public GameObject PlatformDestructionPoint;
	// Use this for initialization
	void Start () {
        //defining PlatformDestructionPoint as our game object PlatformDestructionPoint that is a child of our camera 
        PlatformDestructionPoint = GameObject.Find("PlatformDestructionPoint");
	}
	
	// Update is called once per frame
	void Update () {
        //stetes that if the object this script is attatched to ever has a lesser x value than our PlatformDestructionPoint the object becomes inactive
        if (transform.position.x < PlatformDestructionPoint.transform.position.x)
        {


            gameObject.SetActive(false);
        }
	}
}
