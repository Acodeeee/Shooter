using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public PlayerHealth playerHealth;
	public GameObject enemy;
	public float spawnTime = 3f;	//Enemy产生间隔时间
	public Transform[] spawnPoints;	//Enemy产生位置

	void Start(){
		InvokeRepeating("Spawn", spawnTime, spawnTime);
	}

	//产生敌人
	void Spawn(){
		if(playerHealth.currentHealth <= 0){
			return;
		}
		int spawnPointIndex = Random.Range(0, spawnPoints.Length);

		Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	}
	
}
