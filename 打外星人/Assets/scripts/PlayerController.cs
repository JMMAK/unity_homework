using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//飞船的生命值
	public int Health = 1;
	public float Speed;
	//子弹两发的间隔
	public float ReloadDelay = 0.2f;
	public Vector2 MinMaxX = Vector2.zero;
	public GameObject PrefabAmmo = null;
	public GameObject GunPosition = null;
	public GUIText health_text = null;
	private bool WeaponsActivated = true;

	// Use this for initialization
	void Start () {
		set_score (Health);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = 
			new Vector3 (
				Mathf.Clamp (
					transform.position.x + Input.GetAxis ("Horizontal") * Speed * Time.deltaTime,
					MinMaxX.x,
					MinMaxX.y
				),
				transform.position.y,
				transform.position.z
				);

	}
	void LateUpdate(){
		if((Input.GetButton ("Fire1") | Input.GetButton ("Jump")) && WeaponsActivated) 
		{
			Instantiate (PrefabAmmo,GunPosition.transform.position,PrefabAmmo.transform.rotation);
			WeaponsActivated = false;
			Invoke ("ActivateWeapons",ReloadDelay);
		}
	}
	void ActivateWeapons(){
		WeaponsActivated = true;
	}
	
	
	
	// if been hit
	void OnTriggerEnter(Collider other)
	{
		Health -= 1;
		set_score (Health);
		if(Health < 0)
			Time.timeScale = 0;
		else
			Destroy (other.gameObject);
	}
	
	void set_score(int health)
	{
		health_text.text = "" + health;
	}
}
