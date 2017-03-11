using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour {

	public Animator animator;
	public Manager manager;

	float vel, vy;
	Rigidbody2D rigid;
	float isRight = 1f;
	bool jump = false;
	Vector2	v;

	// Use this for initialization
	void Start () {
		rigid = this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (animator.GetBool ("isEnd"))
			return;

		v = rigid.velocity;

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
			v.y = 10f;
		}

		/* 速度設定 */
		v.x = (animator.GetBool ("isRun")) ? (xAxis * 6f) : (xAxis * 2f);
		rigid.velocity = v;

	}

	void OnTriggerEnter2D (Collider2D c){
		//接地判定
		if (c.gameObject.name == "Ground") 
			animator.SetBool ("isGround", true);

		//敵接触処理
		if (c.gameObject.tag == "Enemy") {
			//敵のトリガエリア削除
			Destroy (c.gameObject.GetComponent<PolygonCollider2D> ());

			//敵の消滅アニメーショントリガ
			c.gameObject.GetComponent<Animator> ().SetTrigger ("Crush");

			//連続ジャンプ
			v.y = 10f;
			rigid.velocity = v;

			//接触時敵を押し下げる
			Vector3 enemyPos = c.gameObject.transform.position;
			c.gameObject.transform.position = new Vector3 (enemyPos.x, enemyPos.y - 0.35f, enemyPos.z);

			//残り時間の追加
			manager.AddTime ();

			//スコア加算
			manager.AddScore ();
		}
	}

	void OnTriggerStay2D (Collider2D c){
		//接地判定
		if (c.gameObject.name == "ground") 
			animator.SetBool ("isGround", true);
	}

	void OnTriggerExit2D (Collider2D c){
		//接地判定
		if (c.gameObject.name == "ground")
			animator.SetBool ("isGround", false);
	}

	void End(){
		rigid.simulated = false;
	}
}
