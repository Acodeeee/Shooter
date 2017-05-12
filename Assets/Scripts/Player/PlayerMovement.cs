using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float speed = 6f;	//Player的移动速度

	Vector3 movement;			//Player的移动方向
	Animator anim;				//Animator组件
	Rigidbody playerRigidbody;	//Rigidbody组件
	int floorMask;				//floor所在的层
	float camRayLength = 100f;	//摄像机发射射线的长度

	void Awake(){
		//绑定组件
		anim = GetComponent<Animator>();
		playerRigidbody = GetComponent<Rigidbody>();

		floorMask = LayerMask.GetMask("Floor");		//获取Floor的层级
	}
	//物理逻辑处理
	void FixedUpdate(){
		//根据名字获取轴值,只会返回-1，0，1
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		Move(h, v);
		Ture();
		Animating(h, v);
	}

	//移动
	void Move(float h, float v){
		movement.Set(h, 0f, v);			//指定移动方向

		movement = movement.normalized * speed * Time.deltaTime;	//指定移动速度
		playerRigidbody.MovePosition(transform.position + movement);
	}

	//旋转
	void Ture(){
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition); //摄像机向鼠标所在位置发射一条射线

		RaycastHit floorHit;	//储存射线碰撞的对象信息
		if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)){
			Vector3 playerToMouse = floorHit.point - transform.position;	//Player到碰撞点的向量
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);	//旋转四元数变量
			playerRigidbody.MoveRotation(newRotation);
		}
	}

	//动画
	void Animating(float h, float v){
		bool walking = h!=0f || v!=0f;	
		anim.SetBool("IsWalking", walking);
	}
}
