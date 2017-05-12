using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public int startingHealth = 100;						//开始Player的生命值
	public int currentHealth;								//当前Player的生命值
	public Slider healthSlider;								//Slider滑块
	public Image damageImage;								//Player被伤害时闪烁的图片
	public AudioClip deathClip;								//Player死亡时发出的声音
	public float flashSpeed = 5f;							//Player被伤害时闪烁的速度
	public Color flashColor = new Color(1f, 0f, 0f, 0.2f);	//Player被伤害时闪烁的颜色

	Animator anim;		//Player动画
	AudioSource playerAudio;	//player发出的声音
	PlayerShooting playerShooting;
	PlayerMovement playerMovement;	

	bool isDead;	//player是否死亡
	bool damaged;	//player是否被伤害

	void Awake(){
		anim = GetComponent<Animator>();
		playerAudio = GetComponent<AudioSource>();
		playerMovement = GetComponent<PlayerMovement>();
		playerShooting = GetComponentInChildren<PlayerShooting>();
		currentHealth = startingHealth;
	}
	void Update(){
		//player被伤害执行界面图片闪烁
		if(damaged){
			damageImage.color = flashColor;
		}else{
			damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		damaged = false;
	}

	//player被攻击会调用这个函数，amount是被伤害量
	public void TakeDamage(int amount){
		damaged =true;

		//减少生命
		currentHealth -= amount;
		//滑块移动
		healthSlider.value = currentHealth;
		//播放被攻击的音乐
		playerAudio.Play();
		//判断player是否死亡
		if(currentHealth <= 0 && !isDead){
			Death();
		}
	}

	void Death(){
		isDead = true;

		//死亡声音
		playerAudio.clip = deathClip;

		//死亡动画
		anim.SetTrigger("Die");

		//关闭移动
		playerMovement.enabled = false;

		//关闭射击
		playerShooting.DisableEffects();
		playerShooting.enabled = false;

	}
	
}
