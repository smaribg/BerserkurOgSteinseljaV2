using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.UI;
public class Boss : MonoBehaviour
{

    public Transform[] targets;
    [HideInInspector]
    public Transform target;
    public int MoveSpeed = 2;
    public int MaxDist = 500;
    public int MinDist = 1;
    [HideInInspector]
    public bool killable = false;
    [HideInInspector]
    public float distanceFromTarget;
    private float timeNextJump;
    public float jumpInterval;
    [HideInInspector]
    public float health;
    public float maxHealth;

    void Start(){
        health = maxHealth;
    }

    void Update()
    {
        target = targets[0];
        foreach (Transform t in targets)
        {
            if (Vector3.Distance(transform.position, t.position) < Vector3.Distance(transform.position, target.position))
            {
                target = t;
            }
        }
        
        transform.LookAt(target);
        distanceFromTarget = Vector3.Distance(transform.position, target.position);
        Vector3 thing =  target.position - transform.position;
        thing.Normalize();

        if (distanceFromTarget <= MaxDist &&
            distanceFromTarget >= MinDist)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            if(Time.time >= timeNextJump){
                Debug.Log("hallo");
                GetComponent<Rigidbody>().AddForce(new Vector3(thing.x*40, 2, thing.z*40),ForceMode.Impulse);
                timeNextJump = Time.time + jumpInterval;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player" || other.collider.tag == "Player2")
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(transform.forward.x * 200, 100, transform.forward.z * 200), ForceMode.Impulse);
            other.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
    }
}
