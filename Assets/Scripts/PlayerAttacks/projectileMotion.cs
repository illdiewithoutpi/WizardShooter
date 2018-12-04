﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileMotion : MonoBehaviour {

    public float projectileSpeed = 10.0f;
    public float shotTimeMax = 5.0f;
    public bool shotByPlayer = true;
    private bool setMove = false;
    private Vector3 moveVec = new Vector3(0f,0f,1f);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (transform.rotation != Quaternion.identity && !setMove)
        {
            moveVec = transform.forward;
            setMove = true;
            transform.rotation = Quaternion.identity;
        }

        //Move straight forwards
        var _move = moveVec*projectileSpeed*Time.deltaTime;

        transform.position += _move;

        //Destroying self after a period of time
        shotTimeMax -= Time.deltaTime;
        if (shotTimeMax <= 0) {
            Destroy(gameObject);
        }

	}

    private void OnTriggerEnter(Collider other) {

        //Check if colliding with enemy
        if (shotByPlayer)
        {
            if (other.tag == "Enemy")
            {
                enemyHealth _mytarg = other.GetComponent<enemyHealth>();
                _mytarg.myHealth--;
                Destroy(gameObject);
            }
            if (other.tag == "Boss")
            {
                bossAI _mytarg = other.GetComponent<bossAI>();
                _mytarg.getHit();
                Destroy(gameObject);
            }
        } else if (other.tag == "Player") {
            playerHealth _mytarg = other.GetComponent<playerHealth>();
            _mytarg.myHealth -= 1;
            Destroy(gameObject);
        }

        if (other.tag == "Wall")
        {
            Destroy(gameObject);
        }

    }

}
