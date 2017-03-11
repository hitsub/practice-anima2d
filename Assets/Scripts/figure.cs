using UnityEngine;

public class Figure : MonoBehaviour {

	public Animator animator;
	public Manager manager;
	public float jumpPower;
	public float stompPower;

	Rigidbody2D rigid;
	Vector2	v; //速度

	void Start () {
		rigid = this.gameObject.GetComponent<Rigidbody2D> ();
	}

	void Update () {
		if (animator.GetBool ("isEnd")) //ゲームオーバー時キャラを動かせないように
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

		/* ジャンプ */
		if (Input.GetKeyDown (KeyCode.Space) && (animator.GetBool ("isWalk") || animator.GetBool ("isRun")) && animator.GetBool("isGround")) {
			animator.SetTrigger ("Jump");
			v.y = jumpPower;
		}

		/* 速度設定 */
		v.x = (animator.GetBool ("isRun")) ? (xAxis * 6f) : (xAxis * 2f);
		rigid.velocity = v;

	}

	void OnTriggerEnter2D (Collider2D c){
		//接地判定
		if (c.gameObject.name == "Ground") 
			animator.SetBool ("isGround", true);

		/* 敵接触処理 */
		if (c.gameObject.tag == "Enemy") {
			//敵のトリガエリア削除
			Destroy (c.gameObject.GetComponent<PolygonCollider2D> ());

			//敵の消滅アニメーショントリガ
			c.gameObject.GetComponent<Animator> ().SetTrigger ("Crush");

			//連続ジャンプ
			v.y = jumpPower;
			rigid.velocity = v;

			//接触時敵を押し下げる
			Vector3 enemyPos = c.gameObject.transform.position;
			c.gameObject.transform.position = new Vector3 (enemyPos.x, enemyPos.y - stompPower, enemyPos.z);

			//残り時間の追加
			manager.AddTime ();

			//スコア加算
			manager.AddScore ();
		}
	}

	void OnTriggerStay2D (Collider2D c){
		//接地判定(念のため)
		if (c.gameObject.name == "Ground") 
			animator.SetBool ("isGround", true);
	}

	void OnTriggerExit2D (Collider2D c){
		//接地判定
		if (c.gameObject.name == "Ground")
			animator.SetBool ("isGround", false);
	}

	void End(){
		//ゲームオーバー時
		rigid.simulated = false;
	}
}
