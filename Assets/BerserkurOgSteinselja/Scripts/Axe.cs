using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Axe : MonoBehaviour
{
    public Transform Pivot;
    public Player player;
    private bool _axeButtonDown;
    private bool _swinging = false;
    private bool _shielding;
    private float _swingSpeed;
    private float _swingAngle;
    private Animator animator;
    private BoxCollider _collider;
    private Renderer _renderer;
    // Use this for initialization
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        _collider = this.GetComponent<BoxCollider>();
        _renderer = this.GetComponent<Renderer>();
        player = ReInput.players.GetPlayer(0);
        _swingSpeed = 500;
        Hide();
    }

    void Update()
    {
        GetInput();
        ProcessInput();
    }

    void Hide()
    {
        _collider.enabled = false;
        _renderer.enabled = false;
    }

    void Show()
    {
        _collider.enabled = true;
        //_renderer.enabled = true;
    }
    // Update is called once per frame
    void Rotate()
    {
        transform.RotateAround(Pivot.position, Vector3.up, _swingSpeed * Time.deltaTime);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }
    private void GetInput()
    {
        _axeButtonDown = player.GetButtonDown("Fire");
        _shielding = transform.parent.GetComponentInChildren<Shield>()._shieldButtonDown;
    }

    private void ProcessInput()
    {
        if (_axeButtonDown && !_swinging && !_shielding)
        {
            _swinging = true;
            transform.RotateAround(Pivot.position, Vector3.up, -50);
            Show();
        }
        //Swinging motion
        if (_swinging)
        {
            if (transform.localEulerAngles.y < 50 || transform.localEulerAngles.y > 300)
            {
                Rotate();
            }
            else
            {
                _swinging = false;
                transform.RotateAround(Pivot.position, Vector3.up, -50);
                Hide();
            }
        }

		animator.SetBool("Attacking", _swinging);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(transform.forward.x * 200, 100, transform.forward.z * 200), ForceMode.Impulse);
            if (other.gameObject.GetComponent<Enemy>().killable)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
