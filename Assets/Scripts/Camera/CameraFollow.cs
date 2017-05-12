using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;		//Camera所要跟随移动的对象（Player）
	public float smoothing = 5f;	//平滑移动量

	Vector3 offset;					//Player到Camera的向量值

	void Start () {
		offset = transform.position - target.position;
	}
	
	void FixedUpdate () {
		Vector3 newCamPos = target.position + offset;	//Camera的目标位置
		transform.position = Vector3.Lerp(transform.position, newCamPos, smoothing * Time
		.deltaTime);
	}
}
