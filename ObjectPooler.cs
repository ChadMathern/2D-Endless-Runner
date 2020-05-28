using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    //object we will use. To be assigned later in the inspector
    public GameObject pooledObject;
    //ammount of objects we will pool. Will define length of our list
    public int pooledAmount;
    //creating a list of game objects known as pooledObjects
    List<GameObject> pooledObjects;

    // Use this for initialization
    void Start() {
        //pooledObjects is equal to a new empty list of game objects
        pooledObjects = new List<GameObject>();
        //define new int as 0,check if i is less than pooledAmmount, and then add 1 to the value of i
        for (int i = 0; i < pooledAmount; i++)
        {//defining obj and GameObject casting it to ensure that whatever is returned is able to go into our list of GameObjects 
            GameObject obj = (GameObject)Instantiate(pooledObject);
            //now that we have instantiated it we do not want it active right away so we will mkae setActive to false
            obj.SetActive(false);
            //this adds our object to our pooledObjects list
            pooledObjects.Add(obj);
            //the above code will continute to add objects to our list until we reach the limit of our pooled ammount that can be defined in our inspector
        }

    }
    //creating a function to utilize our pooled objects and can be called from another script
    public GameObject GetPooledObject()
    {//runs through the list of pooled objects 
        for(int i = 0; i < pooledObjects.Count; i++)
        {//will determine if an object in the list is not active in the hierarchy
            if (!pooledObjects[i].activeInHierarchy)
            {//the script finds an object in the list that is not currently active in the hierarchy and returns that particular object
                //if an object is active then the statement will not return an object
                return pooledObjects[i];
            }
       }
        //these lines are used if every object in the list is found to be active
        //it will add an object to the list and return that 
        //ensuring that there is an inactive object in the list that can be returned
        GameObject obj = (GameObject)Instantiate(pooledObject);
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
        //this script ensures that pooled objects will not be called incorrectly at any given time. i.e removing active platforms out from under our player to put it in front of them
        //(because that does not make for a good player experiance)

    }
}