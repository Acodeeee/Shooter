using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour {

	public PlayerHealth playerHealth;
	public float restartDelay = 5f;	//重新加载游戏时间

	Animator anim;
	float restartTimer = 0f;

	void Awake(){
		anim = GetComponent<Animator>();
	}

	void Update(){
		if(playerHealth.currentHealth <= 0){
			//播放动画
			anim.SetTrigger("GameOver");

			//等待计时
			restartTimer += Time.deltaTime;

			//判断是非到达等待时间
			if(restartTimer >= restartDelay){
				//重新加载游戏
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

}
