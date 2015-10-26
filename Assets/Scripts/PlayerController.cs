using UnityEngine;
using System.Collections;

[System.Serializable]
public class VelocityRange
{
    // PUBLIC INSTANCE VARIABLES
    public float vMin, vMax;

    // CONSTRUCTOR ++++++++++++++++++++++++++++++++
    public VelocityRange(float vMin, float vMax)
    {
        this.vMin = vMin;
        this.vMax = vMax;
    }
}
public class PlayerController : MonoBehaviour {

    //public variable
    public float speed = 50f;
    public float jump = 500f;
    public VelocityRange velocityRange = new VelocityRange(300f, 1000f);
    public int gemValue;

    //private variables
    private Rigidbody2D rb2d;
    private Transform _transform;
    private Animator anim;
    private float _movingValue = 0;
    private bool _isFacingRight = true;
    private bool _isGrounded = true;

    private AudioSource[] soundCLips;
    private AudioSource jumping, crystal;

    private GameController gameController;
   

    // Use this for initialization
    void Start ()
    {
        // Finding GameController game object to access methods in GameController script 
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }

        this.rb2d = gameObject.GetComponent<Rigidbody2D>();
        this._transform = gameObject.GetComponent<Transform>();
        this.anim = gameObject.GetComponent<Animator>();

        this.soundCLips = gameObject.GetComponents<AudioSource>();
        this.jumping = soundCLips[0];
        this.crystal = soundCLips[1];
	
	}

    // MOVEMENT ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ 
    void FixedUpdate()
    {
        float forceX = 0f;
        float forceY = 0f;

        float absVelX = Mathf.Abs(this.rb2d.velocity.x);
        float absVelY = Mathf.Abs(this.rb2d.velocity.y);

        this._movingValue = Input.GetAxis("Horizontal"); // gives moving variable a value of -1 to 1
                                                         // Update is called once per frame
        if (this._movingValue != 0)
        { // player is moving
          //check if player is grounded
            if (this._isGrounded)
            {
                this.anim.SetInteger("State", 1);
                if (this._movingValue > 0)
                {
                    // move right
                    if (absVelX < this.velocityRange.vMax)
                    {
                        forceX = this.speed;
                        this._isFacingRight = true;
                        this._flip();
                    }
                }
                else if (this._movingValue < 0)
                {
                    // move left
                    if (absVelX < this.velocityRange.vMax)
                    {
                        forceX = -this.speed;
                        this._isFacingRight = false;
                        this._flip();
                    }
                }
            }


        }
        else
        {
            // set our idle animation here
            this.anim.SetInteger("State", 0);
        }

        // check if player is jumping
        if ((Input.GetKey("up") || Input.GetKey(KeyCode.W)))
        {
            // chec if player is grounded
            if (this._isGrounded)
            {
                this.anim.SetInteger("State", 2);
                if (absVelY < this.velocityRange.vMax)
                {
                    forceY = this.jump;
                    this.jumping.Play();
                    this._isGrounded = false;
                }
            }
        }

        // add force to the player to push him
        this.rb2d.AddForce(new Vector2(forceX, forceY));
    }

    void OnCollisionStay2D(Collision2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Ground"))
        {
            this._isGrounded = true;
        }
    }


    private void _flip()
    {
        if (this._isFacingRight)
        {
            this._transform.localScale = new Vector3(20f, 20f, 1f); // reset to normal scale
        }
        else
        {
            this._transform.localScale = new Vector3(-20f, 20f, 1f);
        }
    }

    // HIT DETECTION ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Crystal")
        {
            this.crystal.Play();
            gameController.GainScore(gemValue);
            Destroy(other.gameObject);
        }
    }
}
