using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
	private Animator anim;
	private GameObject candy;

	private bool isEating = false;

	private void Start()
	{
		anim = GetComponent<Animator>();
		candy = GameObject.FindGameObjectWithTag("Candy");
	}

	private void Update()
	{
		anim.SetBool("isEating", isEating);

		if (transform.position.x > candy.transform.position.x)
		{
			transform.eulerAngles = new Vector3(0f, 180f, 0f);
		}
		else
		{
			transform.eulerAngles = Vector3.zero;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Candy")
		{
			isEating = true;
			Destroy(collision.gameObject);
		}
	}

	private void OnFinishEating()
	{
		isEating = false;
	}
}
