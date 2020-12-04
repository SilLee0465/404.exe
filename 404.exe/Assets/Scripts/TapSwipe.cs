using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Serialization;

public class TapSwipe : MonoBehaviour
{
    public bool DisableTouchMouseLink = false;
    bool Tap = false;
    bool DoubleTap = false;

    [HideInInspector]
    public int curLane = 2;
    [HideInInspector]
    public bool jumping = false;
    [HideInInspector]
    public bool sliding = false;
    [HideInInspector]
    public bool tripped = false;
    public float tripRecoveryTime = 1.0f;

    public float jumpForce;
    public float jumpTime;
    public GameObject zapPrefab;
    public float zapCooldown = 1.0f;

    public float distanceBetweenLanes;

    public float DoubleTapDelta = 0.5f;
    public float shrinkTime;
    private float LastTappedTime;

    private Vector3 InitialPos;
    private Vector3 EndPos;
    public float Deadzone = 20f;
    public float SwipeDelta = 1f;
    private float LastSwipedTime;

    private Animator animator;

    private PlayerMovement _PlayerMovement;

    public float PosTranslateDist = 5f;
    //HaiJen added
    PowerUpEffect pue;
    //----------------

    void Start()
    {
        if (DisableTouchMouseLink == true) // to unlink mouse with touch and have separate functions for those inputs
        {
            Input.simulateMouseWithTouches = false;
        }
        _PlayerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        animator = this.GetComponent<Animator>();
        //HaiJen added
        pue = GetComponent<PowerUpEffect>();
        //----------------------------------
        Time.timeScale = 1;
    }
    public void CustomButtonTrigger()
    {
        if (DoubleTap == true)
        {
            if (zapCooldown == 0)
            {
                zapCooldown += 1;
                Instantiate(zapPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                //Haijen added
                if (pue.zapBuff)
                {
                    Instantiate(zapPrefab, new Vector3(-0.19f, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                    Instantiate(zapPrefab, new Vector3(-3.19f, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                    Instantiate(zapPrefab, new Vector3(2.81f, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                }
                //-----------------------------------------------------------------------------------------------------------------------
                FindObjectOfType<AudioManager>().Play("ZapEffect");
            }
        }

    }

    void SwipeDirection(Vector3 endPos)
    {
        float xAbs = Math.Abs(endPos.x);
        float yAbs = Math.Abs(endPos.y);
        if (!animator.GetBool("dead"))
        {
            if (xAbs > yAbs)
            {
                //left right
                if (endPos.x > 0)
                {
                    Debug.Log("RIGHT");
                    if (curLane != 3)
                    {
                        curLane += 1;
                        animator.SetBool("rightTurn", true);
                        animator.SetBool("leftTurn", false);
                        transform.Translate(Vector3.left * distanceBetweenLanes);
                        StartCoroutine(rightCancel());
                    }
                    else
                    {
                        if (!tripped)
                        {
                            tripped = true;
                        }
                        else
                        {
                            _PlayerMovement.isDead = true;
                        }
                    }

                }
                else
                {
                    Debug.Log("LEFT");
                    if (curLane != 1)
                    {
                        transform.Translate(Vector3.right * distanceBetweenLanes);
                        animator.SetBool("leftTurn", true);
                        animator.SetBool("rightTurn", false);
                        curLane -= 1;
                        StartCoroutine(leftCancel());
                    }
                    else
                    {
                        if (!tripped)
                        {
                            tripped = true;
                        }
                        else
                        {
                            _PlayerMovement.isDead = true;
                        }
                    }
                    //  gameObject.transform.Translate(DefaultPOS_Player.position.x - PosTranslateDist, 0, 0);
                }
                FindObjectOfType<AudioManager>().Play("Swipe");
            }
            else
            {
                //up down
                if (endPos.y > 0)
                {
                    Debug.Log("UP");
                    Debug.Log(jumpTime);
                    if (!jumping)
                    {
                        jumping = true;

                        StartCoroutine(jumpDown(jumpTime - 0.1f));
                        animator.SetBool("jumping", true);
                        //gameObject.transform.Translate(Vector3.up * jumpForce);
                    }

                }
                else
                {
                    Debug.Log("DOWN");
                    animator.SetBool("sliding", true);
                    if (jumping)
                    {
                        jumping = false;
                    }
                    transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    sliding = true;
                    StartCoroutine(slideCancel());
                }
            }
        }
    }

    IEnumerator slideCancel()
    {
        yield return new WaitForSeconds(shrinkTime);
        sliding = false;
        transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        animator.SetBool("sliding", false);
    }

    IEnumerator rightCancel()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("rightTurn", false);
    }

    IEnumerator leftCancel()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("leftTurn", false);
    }

    IEnumerator jumpDown(float delay)
    { 
        yield return new WaitForSeconds(delay);
        jumping = false;
        animator.SetBool("jumping", false);
        //transform.Translate(Vector3.down * jumpForce);
    }

    IEnumerator tripRecover(float delay)
    {
        yield return new WaitForSeconds(delay);
        tripped = false;
        Debug.Log("Trip Recover");
        StopCoroutine("tripRecover");
    }

    // Update is called once per frame
    void Update()
    {
        if (tripped)
        {
            StartCoroutine(tripRecover(tripRecoveryTime));
        }
        if (jumping)
        {
            transform.Translate(Vector3.up * jumpForce);
        }

        if (sliding)
        {
            transform.Translate(Vector3.down * 2 *  Time.deltaTime);
        }

        if (zapCooldown > 0)
        {
            zapCooldown -= Time.deltaTime;
        }
        else if (zapCooldown < 0)
        {
            zapCooldown = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            InitialPos = Input.mousePosition;
            Tap = true;

            if (Time.time - LastTappedTime < DoubleTapDelta)
            {
                DoubleTap = true;
            }
            LastTappedTime = Time.time;
            LastSwipedTime = Time.time;
            CustomButtonTrigger();
        }

        if (Input.GetMouseButton(0))
        {

            EndPos = Input.mousePosition - InitialPos;
            float currRange = Vector3.SqrMagnitude(EndPos);
            if (EndPos != Vector3.zero && currRange > Deadzone)
            {


            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            float currRange = Vector3.SqrMagnitude(EndPos);
            if (EndPos != Vector3.zero && currRange > Deadzone)
            {
                if (Time.time - LastSwipedTime < SwipeDelta)
                {

                    SwipeDirection(EndPos);
                }

            }
            Tap = false;
            DoubleTap = false;
            InitialPos = Vector3.zero;
            EndPos = Vector3.zero;
        }
    }
}
