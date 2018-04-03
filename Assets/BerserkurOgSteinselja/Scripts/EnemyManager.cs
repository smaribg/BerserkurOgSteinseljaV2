using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {

	public GameObject EnemyPrefab;
	public GameObject BossPrefab;
	public GameObject Target;
	public Transform[] spawnPoints;
	public int startTime = 0;
	private int totalEnemies = 0;
	private int numberOfEnemies = 1;
	public Text EnimiesLeftText;
	// Use this for initialization
	void Start () {
		InvokeRepeating("SpawnEnemy", startTime, 15);
	}
	
	// Update is called once per frame
	void Update () {
		if(totalEnemies > 50){
			CancelInvoke();
		}
		EnimiesLeftText.text = "Number of enemies: " + (totalEnemies);
	}

	private void SpawnEnemy(){
		for(int i = 0; i < numberOfEnemies; i++){
			var enemy = Instantiate(EnemyPrefab, spawnPoints[Random.Range(0,spawnPoints.Length)].position + new Vector3(i,0,0), transform.rotation);
			enemy.GetComponent<Enemy>().target = Target.transform;
			totalEnemies++;
		}
		numberOfEnemies += 3;
	}
}
