using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

	public int attackDamage = 20;	//攻击伤害值
	public float timeBetweenBullets = 0.15f;			//最小攻击间隔时间
	public float range = 100f; 	//攻击距离

	float timer = 0f;	//计时器
	Ray shootRay;
	RaycastHit shootHit;	//	Ray攻击到的物体信息
	int shootableMask;		//	有效攻击层
	ParticleSystem gunParticles;
	LineRenderer gunLine;
	Light gunLight;
	AudioSource gunAudio;
	float effectsDisplayTime = 0.025f;	//激光消失时间

	void Awake(){
		shootableMask = LayerMask.GetMask("Shootable");
		gunParticles = GetComponent<ParticleSystem>();
		gunLine = GetComponent<LineRenderer>();
		gunLight = GetComponent<Light>();
		gunAudio = GetComponent<AudioSource>();
	}
	void Update(){
		timer += Time.deltaTime;
		
		if(timer >= timeBetweenBullets && Input.GetButton("Fire1")){
			Shoot();
		}
		if(timer >= effectsDisplayTime){
			DisableEffects();
		}
	}
	//激光消失
	public void DisableEffects(){
		gunLight.enabled = false;
		gunLine.enabled = false;
	}

	//射击
	void Shoot(){
		//计时归零
		timer = 0f;

		//播放声音
		gunAudio.Play();

		//播放粒子特效
		gunParticles.Stop();
		gunParticles.Play();

		//发射激光
		gunLight.enabled = true;
		gunLine.enabled = true;
		gunLine.SetPosition(0, transform.position);
		shootRay.origin = transform.position;	//设置射线起点
		shootRay.direction = transform.forward;	//设置射线方向

		//判断是否射击到Enemy
		if(Physics.Raycast(shootRay, out shootHit, range, shootableMask)){
			//发送Line（激光）
			gunLine.SetPosition(1, shootHit.point);

			//enemy掉生命值
			EnemyHealth enemyHealth = shootHit.transform.GetComponent<EnemyHealth>();
			if(enemyHealth != null){
				enemyHealth.TakeDamage(attackDamage, shootHit.point);
			}
		}else{
			//笔直发射
			gunLine.SetPosition(2, shootRay.origin + shootRay.direction * range);
		}
	}

}
