using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Bow : MonoBehaviour
{

    public Transform Arrow;
    public Player player;
    private Animator animator;
    private bool _bowButton;
    private bool _bowButtonUp;
    private Vector3 _aimVector;
    private bool readyForShooting;
    private float force;


    // Use this for initialization
    void Start()
    {
        player = ReInput.players.GetPlayer(1);
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        ProcessInput();
        force += 0.5f;
    }

    private void GetInput()
    {
        _bowButton = player.GetButton("Fire");
        _bowButtonUp = player.GetButtonUp("Fire");
    }

    private void ProcessInput()
    {
        animator.SetBool("DrawBow", _bowButton);

        if (_bowButtonUp)
        {
			if (force > 5f) {
				var arrow = Instantiate(Arrow, transform.position, transform.rotation);
				arrow.GetComponent<Arrow>().force = force;
			}
        }

        if (!_bowButton)
        {
            force = 0f;
        }
    }
}
