﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float mSpeed = 28f;
    private float initialSpeed;
    private Animator animator;
    public bool isDead;

    void PlayerRunning()
    {
        transform.Translate(Vector3.back * mSpeed * Time.deltaTime);
        //transform.Translate(Vector3.down * 3 * Time.deltaTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        initialSpeed = mSpeed;
        isDead = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            PlayerRunning();
        }


        mSpeed = initialSpeed + (transform.position.z * 0.001f);

    }
}