using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    //public instances
    public float speed = 1f;
    public Transform visionStart;
    public Transform visionEnd;

    //private instances
    private Rigidbody2D rb2d;
    private Transform _transform;
    private Animator anim;
    private CircleCollider2D death;

    private bool grounded = false;
    private bool NOPE = false;

	// Use this for initialization
	void Start ()
    {
        this.rb2d = gameObject.GetComponent<Rigidbody2D>();
        this._transform = gameObject.GetComponent<Transform>();
        this.anim = gameObject.GetComponent<Animator>();
        this.death = gameObject.GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
	    if(this.grounded)
        {
            this.anim.SetInteger("State", 1);
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
            this.anim.SetInteger("State", 0);
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
            this._transform.localScale = new Vector3(-20f, 20f, 1f); // reset to normal scale
        }
    }
}
