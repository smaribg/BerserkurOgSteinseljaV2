using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    private CapsuleCollider _collider;
    private bool moving = true;
    private Vector3 scale;
    // Use this for initialization
    void Start()
    {
        scale = transform.localScale;
        _collider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            transform.position = (transform.position + transform.up * 20 * Time.deltaTime);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            return;
        }
        else if (other.tag == "Player" || other.tag == "Player2")
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(transform.forward.x * 200, 100, transform.forward.z * 200), ForceMode.Impulse);
            other.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
