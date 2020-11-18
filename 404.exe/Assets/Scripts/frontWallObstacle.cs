using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frontWallObstacle : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement _PlayerMovement;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        _PlayerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            animator.SetBool("dead", true);
            _PlayerMovement.isDead = true;
        }
    }
}
