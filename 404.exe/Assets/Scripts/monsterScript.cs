using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterScript : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement _PlayerMovement;
    private bool disabled = false;
    private void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        _PlayerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Crash");
            disabled = true;
            _PlayerMovement.isDead = true;
            animator.SetBool("dead", true);
        }
        else if (other.CompareTag("Zap"))
        {
            Debug.Log("Zapped");
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            disabled = true;
        }            
    }
}
