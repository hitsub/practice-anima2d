using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

	float timer = 15f;
	const float timerMax = 15f;

	public Image timerGuage;
	public Text scoreText;
	public Animator animatorCharacter;
	public Text endMessage;
	int score = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;	

		//ゲージ量の変更
		timerGuage.fillAmount = timer / timerMax;

		/* ゲージ色 */
		Color color = new Color (1f, 1f, 1f, 1f);
		if (timer <= timerMax / 3) {
			color = new Color (1f, timer / (timerMax / 3), timer / timerMax, 1f);
		}
		timerGuage.color = color;

		/* ゲームオーバー後 */
		if (timer <=  0) {
			animatorCharacter.SetBool ("isEnd", true);
			endMessage.enabled = true;
			//ESCでもう一度遊ぶ
			if (Input.GetKey (KeyCode.Escape)) {
				SceneManager.LoadScene ("samplegame");
			}
		}
	}

	public void AddTime(){
		//呼び出し元:Figure
		if (animatorCharacter.GetBool ("isEnd"))
			return;
		timer += 1f;
		timer = Mathf.Clamp (timer, 0, timerMax); //タイマー値の上限・下限設定
	}

	public void AddScore(){
		//呼び出し元:Figure
		if (animatorCharacter.GetBool ("isEnd"))
			return;
		score += 10;
		scoreText.text = score.ToString ();
	}
}
