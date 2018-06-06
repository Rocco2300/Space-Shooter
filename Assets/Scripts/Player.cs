using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Boundary
{
	public float xMin, xMax, yMin, yMax;
}

public class Player : MonoBehaviour
{
	public float speed;
	public Boundary boundary;
	public float fireRate;
	public GameObject laser;
	public Transform gun;

	private float nextFire;
	Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal"); 
		float moveVertical = Input.GetAxis("Vertical");

		Vector2 movement = new Vector2(moveHorizontal, moveVertical);

		rb.velocity = movement * speed;
		rb.position = new Vector2
		(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
			Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax)
		);
	}

	void Update()
	{
		if(Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(laser, gun.position, gun.rotation);
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Enemy" || collision.tag == "EnemyBullet")
		{
			Destroy(gameObject);
		}
	}
}
