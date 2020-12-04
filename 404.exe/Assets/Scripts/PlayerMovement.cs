using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float mSpeed = 28f;
    private Animator animator;
    public bool isDead;

    void PlayerRunning()
    {
        transform.Translate(Vector3.back * mSpeed * Time.deltaTime);
        transform.Translate(Vector3.down * 3 * Time.deltaTime);
    }

    void Awake()
    {
        isDead = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        PlayerRunning();
    }
}