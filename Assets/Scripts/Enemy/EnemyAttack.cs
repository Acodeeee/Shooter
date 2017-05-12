using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	public float timeBetweenAttack = 0.5f;	//攻击间隔时间
	public int attackDamage = 10;			//攻击伤害值

	Animator anim;			//Enemy动画
	GameObject player;		//Player对象
	PlayerHealth playerHealth;	//Player的PlayerHealth组件
	EnemyHealth enemyHealth;	//Enemy的EnemyHealth组件
	bool playerInRange;			//判断Player是否在Enemy攻击的范围内
	float timer = 0f;				//计时器

	void Awake(){
		anim = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player");
		playerHealth = player.GetComponent<PlayerHealth>();
		enemyHealth = GetComponent<EnemyHealth>();
	}

	void Update(){
		timer += Time.deltaTime;
		/*
		Enemy攻击条件：
			1.Player在攻击范围内
			2.经过了时间间隔
			3.Enemy没有死亡
			4.Player没有死亡
		 */
		if(playerInRange && timer >= timeBetweenAttack && 
			playerHealth.currentHealth >0 && enemyHealth.currentHealth >0 ){
				
			Attack();

		}else if(playerHealth.currentHealth <= 0){
			//Player死亡，Enemy执行胜利动画
			anim.SetTrigger("PlayerDead");
		}
	}

	//Enemy触发器碰撞到Player
	void OnTriggerEnter(Collider other){
		if(other.gameObject == player){
			playerInRange = true;
		}
	}

	//Enemy触发器离开Player
	void OnTriggerExit(Collider other){
		if(other.gameObject == player){
			playerInRange = false;
		}
	}

	void Attack(){
		timer = 0f; //计时器归零
		if(playerHealth.currentHealth > 0){
			playerHealth.TakeDamage(attackDamage);
		}
	}

}
