using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    //our platformgGenerator transform point
    public Transform platformGenerator;
    //private variable for where
    private Vector3 platformStartPoint;
    //our player script
    public PlayerController thePlayer;
    //will serve as the restart point
    private Vector3 playerStartpoint;

    private PlatformDestroyer[] platformList;

	// Use this for initialization
	void Start () {
        //defines our platformStartPoint as the position of our PlatformGenerator on game start
        platformStartPoint = platformGenerator.position;
        //defines playerStartPoint as the players starting position
        playerStartpoint = thePlayer.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //new method that can be called from our PlayerController script
    public void RestartGame()
    {//starts our Coroutine below (IEnumerator restartGameCo()
        StartCoroutine ("RestartGameCo");
    }


    public IEnumerator RestartGameCo()
    {
        thePlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        platformList = FindObjectsOfType<PlatformDestroyer>();
        for (int i =0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }
        thePlayer.transform.position = playerStartpoint;
        platformGenerator.position = platformStartPoint;
        thePlayer.gameObject.SetActive(true);
    }
    

}
