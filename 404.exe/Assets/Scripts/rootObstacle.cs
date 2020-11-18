using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rootObstacle : MonoBehaviour
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
        Debug.Log("NO");
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Yes");
            _PlayerMovement.isDead = true;
        }
    }

}
