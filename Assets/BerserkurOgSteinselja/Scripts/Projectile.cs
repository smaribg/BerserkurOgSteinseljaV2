using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float speed = 20f;
	
	// Update is called once per frame
	void Update () {
		//transform.position = (transform.position + transform.forward * speed * Time.deltaTime);
		this.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
	}
}
