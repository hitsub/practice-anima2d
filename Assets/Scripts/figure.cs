using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class figure : MonoBehaviour {

	public Animator animator;
	float vel, vy;
	Rigidbody2D rigid;
	float isRight = 1f;
	bool jump = false;

	// Use this for initialization
	void Start () {
		rigid = this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2	v = rigid.velocity;

		/* 方向転換 */
		float xAxis = Input.GetAxis("Horizontal");
		if (xAxis > 0)
			transform.eulerAngles = new Vector3 (0, 0, 0);
		else if (xAxis < 0)
			transform.eulerAngles = new Vector3 (0, 180, 0);


		/* 歩く・走るのトリガ管理 */
		if (Mathf.Abs(xAxis) > 0.1f) {
			animator.SetBool ("isWalk", true);
		}else{
			animator.SetBool ("isWalk", false);
			animator.SetBool ("isRun", false);
		}
		if (Input.GetKeyDown (KeyCode.LeftShift))
			animator.SetBool ("isRun", true);

		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			animator.SetBool ("isRun", false);
		}

		//ジャンプ
		if (Input.GetKeyDown (KeyCode.Space) && (animator.GetBool ("isWalk") || animator.GetBool ("isRun")) && animator.GetBool("isGround")) {
			animator.SetTrigger ("Jump");
			v.y = 7f;
		}

		/* 速度設定 */
		v.x = (animator.GetBool ("isRun")) ? (xAxis * 6f) : (xAxis * 2f);
		rigid.velocity = v;

	}

	/*
	void OnCollisionEnter2D (Collision2D c){
		if (c.gameObject.name == "ground") 
			animator.SetBool ("isGround", true);
	}
	void OnCollisionExit2D (Collision2D c){
		if (c.gameObject.name == "ground")
			animator.SetBool ("isGround", false);
	}
	*/
	void OnTriggerEnter2D (Collider2D c){
		if (c.gameObject.name == "ground") 
			animator.SetBool ("isGround", true);
	}
	void OnTriggerStay2D (Collider2D c){
		if (c.gameObject.name == "ground") 
			animator.SetBool ("isGround", true);
	}
	void OnTriggerExit2D (Collider2D c){
		if (c.gameObject.name == "ground")
			animator.SetBool ("isGround", false);
	}
}
