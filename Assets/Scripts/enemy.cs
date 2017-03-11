using UnityEngine;

public class Enemy : MonoBehaviour {

	Rigidbody2D rigid;
	public float speed;

	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		rigid.velocity = new Vector2 (-1 * speed, rigid.velocity.y);
	}

	void Crushed(){
		//Crushアニメーション内で呼ばれる
		Destroy (this.gameObject);
	}

	void OnTriggerExit2D(Collider2D c){
		//場外に出たら消滅
		if (c.gameObject.name == "EnemyArea") {
			Destroy (this.gameObject);
		}
	}
}
