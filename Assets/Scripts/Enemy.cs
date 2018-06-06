using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float speed;
	public Vector2 dir;
	public float lifetime;
	public float fireRate;
	public float delay;
	public GameObject laser;
	public Transform gun;

	Rigidbody2D rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = dir * speed;
		InvokeRepeating("Fire", delay, fireRate);
		Destroy(gameObject, lifetime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("Bullet"))
		{
			Destroy(gameObject);
			Destroy(collision.gameObject);
		}
	}

	void Fire()
	{
		Instantiate(laser, gun.position, gun.rotation);
	}
}