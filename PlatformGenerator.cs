using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {
    //the transform point that is a child to our camera
    public Transform GenerationPoint;
    //will be assigned a random range (DistanceBetweenMin-DistanceBetweenMax)
    public float DistanceBetween;

    
    //can be assigned values in the inspector, used in defining Distance Between
    public float DistanceBetweenMin;
    public float DistanceBetweenMax;

   //will be used to store a random value from our ObjectPools list
    public int PlatformPicker;
    //new array that will store widths of our platforms. used later so platforms do not spawn on top of eachother
    private float[] platformWidths;
    //new array of object pools
    public ObjectPooler[] ObjectPools;
    
    //will be defined by our object generators Y position
    private float minHeight;
    //our maxHeightPoint's position
    public Transform maxHeightPoint;
    //defined by our maxHeightPoint's Y value
    private float maxHeight;
    //maximum height change from platform to platform
    public float maxHeightChange;
    //used to determine how much a platforms height can change in a specific instance
    private float heightChange;

	// Use this for initialization
	void Start () {
        
        //fills our platformWidths float array to its desired ammount with specified values
        //sets the length of the platformwidths array equal to our objectPools array
        platformWidths = new float[ObjectPools.Length];
        //creates spaces in the array that can be filled with values
        for (int i = 0; i <ObjectPools.Length; i++)
        {//ensures the list is filled with values equal to the widths of our pooled objects in our array
            platformWidths[i] = ObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }
        //min height will be the the linear position of our generator in the scene
        minHeight = transform.position.y;
        //max height will be equal to the linear position of our MaxHeightPoint
        maxHeight = maxHeightPoint.position.y;

            }
	
	// Update is called once per frame
	void Update () {
        //if the generators position on the x axis is less than the generation points then it will run the following code
        if (transform.position.x < GenerationPoint.position.x)
        {//assigns a random value between min and max to DistanceBetween
            DistanceBetween = Random.Range(DistanceBetweenMin, DistanceBetweenMax);
            //randomly selects an object from the pool 
            PlatformPicker = Random.Range(0, ObjectPools.Length);
            //possible height change is equal to a random value between maxHeightChange and -maxheightChange. So it can either move up or down based on the assigned maxHeightChange value
            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);
            //if our height change will return a value higher than our MaxHeightPoint position it will simply set the platform to our maxHeightPosition
            if(heightChange > maxHeight)
            {
                heightChange = maxHeight;
                //if our heightchange will return a value lower than our minheight it sets it to our minHeight position
            } else if (heightChange < minHeight)
            {
                heightChange = minHeight;
                //this way platforms stay on the screen and cannot be generated outside the players view
            }
            //adds 1/2 of platform width and DistanceBetween to the x of the transform. This way our generator moves half of the selected platform width + distance between value. Then the Y change is = HeightChange
            //we do not want to move the complete length at once because it would leave our generator in the middle of the generated platform 
            //our DistanceBetween can return a value that would leave overlapping platforms so we add the other half later
            transform.position = new Vector3(transform.position.x + (platformWidths[PlatformPicker] / 2) + DistanceBetween, heightChange, transform.position.z);

            //gets a pooled object from the objectPools
            GameObject newPlatform = ObjectPools[PlatformPicker].GetPooledObject();
            //sets selected object from the pool to active and transforms it accordingly thus generating a new platform 
            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            //pretty similar to line 77 except for the absence of the distancebetween value. This is so after the platform has been created the point moves the other half of the platform lenght
            //this is so the generation point is always on the end of the activated platform at the end of the update loop
            transform.position = new Vector3(transform.position.x + (platformWidths[PlatformPicker] / 2), transform.position.y, transform.position.z);
        }
		
	}
}
