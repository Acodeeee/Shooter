using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

	Transform player;					//Player的Transform组件
	NavMeshAgent nav;					//自动导航组件
	PlayerHealth playerHealth;
	EnemyHealth enemyHealth;
	
	void Awake () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		nav = GetComponent<NavMeshAgent>();
		playerHealth = player.GetComponent<PlayerHealth>();
		enemyHealth = GetComponent<EnemyHealth>();
	}
	
	void Update () {
		if(playerHealth.currentHealth > 0 && enemyHealth.currentHealth >0){
			nav.SetDestination(player.position);

		}else{
			nav.enabled = false;
		}
	}
}
