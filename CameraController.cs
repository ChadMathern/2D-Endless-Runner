using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    //calling our PlayerController
    public PlayerController thePlayer;
    //variable that will be set to our players position in game
    private Vector3 lastPlayerPosition;
    //will be used to determin how far the camera has to move per update
    private float distanceToMove;

	// Use this for initialization
	void Start () {
        //defining thePlayer with FindObjectOfType. This returns our PlayerController script
        thePlayer = FindObjectOfType<PlayerController>();
        //lastPlayerPosition is defined as the current position of thePlayer upon startup
        lastPlayerPosition = thePlayer.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        //distanceToMove is defined as the difference between the lastPlayerPosition and what it is currently
        //we are also only concerned wiith movement along the x axis hence the .x
        distanceToMove = thePlayer.transform.position.x - lastPlayerPosition.x;
        //this will transfom the position of the actual camera. Its new vector will only increase along the x axis based off the difference we calculated and stored in distanceToMove.
        //the rest of the vector tree does not have to change so we allow it to keep its current y and z position the same
        transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
        //placed at the end of the update to return the current position of the character before looping again
        lastPlayerPosition = thePlayer.transform.position;
	}
}
