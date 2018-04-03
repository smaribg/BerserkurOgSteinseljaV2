using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {

	public GameObject arrow;
    private float timeNextShot;
    public float shotInterval;
	private Enemy enemy;
    public float maxShootDistance;

    void Start() {
		enemy = this.GetComponent<Enemy>();
	}	

    // Update is called once per frame
    void Update () {
		if (Time.time >= timeNextShot &&
			enemy.distanceFromTarget <= maxShootDistance) {
			Instantiate(arrow, transform.position, transform.rotation * Quaternion.Euler(90, 0 ,0));
			timeNextShot = Time.time + shotInterval;
		}
	}
}
