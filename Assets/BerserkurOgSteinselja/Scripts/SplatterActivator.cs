using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatterActivator : MonoBehaviour {
	
    public enum ForceType
    {
        Slash,
        Pierce
    }

    public int numberOfSplats = 10;
	public float minSplatRange = 7f;
	public float maxSplatRange = 12f;
    public float velocity = 10f;
    public ForceType forceType = ForceType.Pierce;
    private bool collided = false;

    void OnCollisionEnter(Collision other)
    {
        var target = other.collider.GetComponent<SplatterTarget>();
        if (target != null) {
            collided = true;
            target.Bleed(this, other);
        }
    }
}
