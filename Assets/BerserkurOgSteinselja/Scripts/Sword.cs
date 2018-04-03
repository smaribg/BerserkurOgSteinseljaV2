using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody>().AddTorque(transform.up, ForceMode.Impulse);
	}
}
