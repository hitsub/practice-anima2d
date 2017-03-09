using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

	Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		rigid.velocity = new Vector2 (-2f, rigid.velocity.y);
	}

	void Crushed(){
		Destroy (this.gameObject);
	}

	void OnTriggerExit2D(Collider2D c){
		if (c.gameObject.name == "EnemyArea") {
			Destroy (this.gameObject);
		}
	}
}
