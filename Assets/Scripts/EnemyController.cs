using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    //public instances
    public float speed = 1f;
    public Transform visionStart;
    public Transform visionEnd;
	public int lifeValue;

    //private instances
    private Rigidbody2D rb2d;
    private Transform _transform;
	private Animator anim;

    private bool grounded = false;
    private bool NOPE = false;

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
		this.anim = gameObject.GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
	    if(this.grounded)
        {
            this.anim.SetInteger("State", 1); //waling state
            this.rb2d.velocity = new Vector2(this._transform.localScale.x, 0) * this.speed;

            this.NOPE = Physics2D.Linecast(this.visionStart.position, this.visionEnd.position, 1 << LayerMask.NameToLayer("Solid"));
            Debug.DrawLine(this.visionStart.position, this.visionEnd.position);

            if (NOPE == false)
            {
                this._flip();
            }
        }

        else
        {
            this.anim.SetInteger("State", 0); // idle state
        }
    }

    void OnCollisionStay2D(Collision2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Ground"))
        {
            this.grounded = true;
        }
    }

	void OnCollisionExit2D(Collision2D otherCollider)
	{
		if (otherCollider.gameObject.CompareTag("Ground"))
		{
			this.grounded = true;
		}
	}

    private void _flip()
    {
        if (this._transform.localScale.x == -20)
        {
            this._transform.localScale = new Vector3(20f, 20f, 1f);
        }
        else
        {
            this._transform.localScale = new Vector3(-20f, 20f, 1f); 
        }
    }

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") 
		{
			this.anim.SetInteger("State", 2); //death state
			gameObject.GetComponent<Collider2D> ().enabled = false;
			gameController.LoseLife(lifeValue);
			speed = 0; //stops moving after death
			Destroy (gameObject, 2f); //destroy gameObject 2 second delay so that animation can play
			
		}
		
	}
	

}
