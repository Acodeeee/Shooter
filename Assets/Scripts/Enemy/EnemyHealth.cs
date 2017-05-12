using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;
	public float sinkSpeed = 2.5f;
	public int scoreValue = 10;
	public AudioClip deathClip;

	Animator anim;
	AudioSource enemyAudio;
	ParticleSystem hitParticle;	//Enemy被攻击时的粒子特效
	CapsuleCollider capsuleCollider;	//Enemy的碰撞体组件
	bool isDead;
	bool isSinking;

	void Awake(){
		currentHealth = startingHealth;
		anim = GetComponent<Animator>();
		enemyAudio = GetComponent<AudioSource>();
		hitParticle = GetComponentInChildren<ParticleSystem>();
		capsuleCollider = GetComponent<CapsuleCollider>();	//物理碰撞器
	}
	void Update(){
		if(isSinking){
			transform.Translate(Vector3.down * sinkSpeed * Time.deltaTime);
		}
	}
	/*Player伤害Enemy时调用此方法,此方法给Player调用
		amount: 伤害值
		hitPoint: Enemy被攻击的位置
	*/
	public void TakeDamage(int amount, Vector3 hitPoint){
		//是否已经死亡
		if(isDead){
			return;
		}

		//播放音乐
		enemyAudio.Play();

		//减少生命值
		currentHealth -= amount;

		//播放粒子特效
		hitParticle.transform.position = hitPoint;
		hitParticle.Play();

		//判断是Enemy是否死亡
		if(currentHealth <= 0){
			Death();
		}

	}
	void Death(){
		//设置Enemy死亡标识
		isDead = true;

		//关闭物理碰撞器, 使尸体不会阻碍Player运动
		capsuleCollider.isTrigger = true;

		//播放Enemy死亡动画
		anim.SetTrigger("Dead");

		//播放Enemy死亡音乐
		enemyAudio.clip = deathClip;
		enemyAudio.Play();
	}

	//此函数在Enemy Death动画里被自动调用
	public void StartSinking ()
    {
        // Find and disable the Nav Mesh Agent.
        GetComponent <NavMeshAgent> ().enabled = false;

        // 如果isKinematic启用，力、碰撞或关节将不会影响这个刚体。刚体将通过改变transform.postion根据动画或脚本完全控制。
        GetComponent <Rigidbody> ().isKinematic = true;

        // The enemy should no sink.
        isSinking = true;

        // Increase the score by the enemy's score value.
         ScoreManager.score += scoreValue;

        // After 2 seconds destory the enemy.
        Destroy (gameObject, 2f);
    }


}
