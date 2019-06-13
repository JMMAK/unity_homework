using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour {

	//移动的方向
	public Vector3 Direction = Vector3.up;
	//移动的速度
	public int Speed = 50;
	//lifetime in seconds
	public float Lifetime = 5.0f;

	// Use this for initialization
	void Start () {
	
		//destroys ammo in lifetime
		Invoke ("DestroyMe", Lifetime);

	}
	
	// Update is called once per frame
	void Update () {
	
		//以一定的速度改变子弹的移动位置
		transform.position += Direction * Speed * Time.deltaTime;
	}

	void DestroyMe()
	{
		//销毁场景里的子弹对象
		Destroy (gameObject);
	}
}
