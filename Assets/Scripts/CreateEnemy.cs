using UnityEngine;

public class CreateEnemy : MonoBehaviour {

	public GameObject enemyPrefab;
	float time = 0f;

	void Update () {
		time += Time.deltaTime;
		if (time >= 0.5f) {
			Instantiate (enemyPrefab, transform.position, Quaternion.identity);
			time = 0f;
		}
	}
}
