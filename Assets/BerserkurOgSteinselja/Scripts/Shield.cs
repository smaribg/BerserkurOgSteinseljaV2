using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Shield : MonoBehaviour {

	// Use this for initialization
	public Player player;
	[HideInInspector]
	public bool _shieldButtonDown;
	private BoxCollider _collider;
	private Renderer _renderer;
	void Start () {
		player = ReInput.players.GetPlayer(0);
		_collider = GetComponent<BoxCollider>();
		_renderer = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
		_shieldButtonDown = player.GetButton("Shield");
		if(_shieldButtonDown){
			_collider.enabled = true;
			_renderer.enabled = true;
		}else{
			_collider.enabled = false;
			_renderer.enabled = false;
		}
	}
}
