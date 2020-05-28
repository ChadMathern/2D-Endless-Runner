using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //declaring variables for jump force and move speed that can be modified within the inspector
    public float moveSpeed;
    public float jumpForce;
    //floats used later for generating additional jumpforce while spacebar is held
    public float airTime;
    private float airTimeCounter;
    //
    private Rigidbody2D myRigidBody;
    //declaring grounded boolian to be defined later
    public bool Grounded;
    //calling LayerMask which specifies layers that can interact with eachother(physics.raycast)
    public LayerMask whatIsGround;
    //declaring your public ground check transform that is atatched to the bottom of our player used to determine grounded state later
    public Transform GroundCheck;
    //declaring the float for our OverlapCircle
    public float groundCheckRadius;
    //calling our sprites animator which we will implement later on
    private Animator myAnimator;
    //calling our game manager that is used for restarting player position upon failure
    public GameManager theGameManager;

	// Use this for initialization
	void Start () {
        //assigning rigidbody and animator components of our sprite
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        //airtimecounter is set to our public airtime float
        airTimeCounter = airTime;
	}
	
	// Update is called once per frame
	void Update () {
        
        //using OverlapCircle to reconcile our grounded state. Overlap circle parameters are our groundcheck transform point, our groundcheck transforms scale float, and our whatIsGround layermask
        //overlapCircle checks these parameters to check for colliders overlapping this circle. the addition of our layermask is optional and allows us to check for collisoon between specific layers
        Grounded = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
        //player movement. velocity of rigid body through a new vector. through the x axis defined as our public float movespeed and y as its current lineal velocity
        myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);
        //if spacebar or left mouse is pressed down
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {//only if player is grounded
            if (Grounded)
            { //then using vector2 to change linear velocity through our public jumpforce float
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
            }
        }
        //now using getkey (not getkeydown) to allow for holding buttons down to increase jump height
        if(Input.GetKey (KeyCode.Space) || Input.GetMouseButton(0))
        {//this is only possible if our airtime counter is greater than 0
            if(airTimeCounter > 0)
            {//then jumpforce will be constant based on length of button hold
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                //to limit this we have our airtime counter counting down to zero and it will result in jumpforce ending
                airTimeCounter -= Time.deltaTime;
            }
        }
        //if spacebar or mouse button is released ar anytime our airtimecounter will become 0
        if(Input.GetKeyUp (KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            airTimeCounter = 0;
        }
        //if player is ever grounded our airtimecounter will reset to our public float airtime which we define in the inspector.
        //remember that we set airTimeCounter = to airtime in initizlization
        if(Grounded)
        {
            airTimeCounter = airTime;
        }
        //these lines assign values to our animator to affect transitions 
        //specifying our current x velocity for our speed condition in the animator determining if our running animation or idle animation is playing
        myAnimator.SetFloat("Speed", myRigidBody.velocity.x);
        //this line will transition between our jumping and running animation based off wether or not the player is grounded
        myAnimator.SetBool("Grounded", Grounded);
	}
    //when our players collider is touchig our killbox game object
    void OnCollisionEnter2D (Collision2D other)
    {//somewhat similar to layers, tags are built into unity. basically saying that if our player collider meets another object's collider tagged as "killbox" to restart the game 
        if (other.gameObject.tag == "killbox")
        {//uses our game manager to restart
            theGameManager.RestartGame();
        }
    }
}
