using UnityEngine;
using System.Collections;

public class EnemyCollider : MonoBehaviour {

	private Animator anim;


	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") 
		{
			this.anim.SetInteger("State", 2);
			Destroy (gameObject, 2f);

		}
		
	}
}
