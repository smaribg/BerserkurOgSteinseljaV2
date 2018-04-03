using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using System;

public class PlayerController : MonoBehaviour
{

    public int playerId;

	public float movementSpeed = 5.0f;
	private Player player;
	private Vector3 moveVector;
	public int hp = 3;
	public Slider healthSlider;
	public TMPro.TextMeshProUGUI healthText;
	public GameObject GameOverPanel;
	private Vector3 _aimVector;
	private int currentHealth = 0;
    private Animator animator;
	void Start () {
		player = ReInput.players.GetPlayer(playerId);
		currentHealth = hp;
		healthText.text = currentHealth + "/" + hp;
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
		ProcessInput();
	}
	void checkIfDead(){
		if(currentHealth == 0){
			Die();
		}
	}

    private void Die()
    {
		GameOverPanel.SetActive(true);
    }

    private void GetInput()
    {
        moveVector.x = player.GetAxis("Move Horizontal");
        moveVector.z = player.GetAxis("Move Vertical");


        _aimVector.x = player.GetAxis("Aim Horizontal");
        _aimVector.z = player.GetAxis("Aim Vertical");

    }

    private void ProcessInput()
    {
        if (moveVector.x != 0.0f || moveVector.z != 0.0f)
        {
            transform.position = (transform.position + moveVector * movementSpeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(moveVector);
            animator.SetBool("RunningForward", true);
        }
        else
        {
			if(animator == null){
				Debug.Log("NULL BISS");
			}
            animator.SetBool("RunningForward", false);
        }
        if (_aimVector.x != 0.0f || _aimVector.z != 0.0f)
        {
            transform.rotation = Quaternion.LookRotation(_aimVector);
        }

    }
	public void TakeDamage(){
		currentHealth -=1;
		healthSlider.value = currentHealth;
		healthText.text = currentHealth + "/" + hp;
		
		checkIfDead();

	}
}
