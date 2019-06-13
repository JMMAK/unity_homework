using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	//外星飞船的生命值
	public int Health = 1;
	//外星飞船每秒移动的单元个数
	public float Speed = 1.0f;
	public float enemy_level = 10f;
	//外星飞船的移动范围
	public Vector2 MinMaxX = Vector2.zero;
	
	//炮塔
	public float ReloadDelay = 1.0f;
	public float min_ReloadDelay = 0.2f;
	private float Reload_current = 0;
	public GameObject PrefabAmmo = null;
	public GameObject GunPosition = null;

	//分数板
	public GUIText health_text = null;

	private bool WeaponsActivated = true;
	
	// Use this for initialization
	void Start () {
		Reload_current = ReloadDelay;
		set_score (Health);
	}
	
	// Update is called once per frame
	void Update()
	{
		float level;
		transform.position = new Vector3 (MinMaxX.x + Mathf.PingPong (Time.time * Speed, 1.0f) * (MinMaxX.y - MinMaxX.x),
		                                 transform.position.y, transform.position.z);
		if(Reload_current > min_ReloadDelay)
		{
			// - (ln(x+e) * enemy_level)
			level = Mathf.Log(Time.fixedTime+Mathf.Exp(1), Mathf.Exp(1))* 0.01f * enemy_level;
			Reload_current = ReloadDelay - level;
			print(Reload_current);
		}
	}
	
	//fire back
	void LateUpdate()
	{
		if(WeaponsActivated && Time.timeScale != 0)
		{
			float delay;
			Instantiate (PrefabAmmo,GunPosition.transform.position,PrefabAmmo.transform.rotation);
			WeaponsActivated = false;
			// 随机发射子弹
			delay = Reload_current * Random.Range(0.0f,2.0f);
			
			Invoke ("ActivateWeapons",delay);
		}
	}
	// ActivateWeapons
	void ActivateWeapons(){
		WeaponsActivated = true;
	}
	
	
	// if been hit
	void OnTriggerEnter(Collider other)
	{
		Health -= 1;
		set_score (Health);
		if(Health < 0)
		{
			//Vector3 posit = new Vector3(30, 44, 35);
			//Instantiate(gameObject, posit, gameObject.transform.rotation);
			Destroy (gameObject);
			
		}
		Destroy (other.gameObject);
	}

	void set_score(int health)
	{
		health_text.text = "Health: " + health;
	}
}
