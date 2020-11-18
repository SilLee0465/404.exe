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
    private float LastTappedTime;

    private Vector3 InitialPos;
    private Vector3 EndPos;
    public float Deadzone = 20f;
    public float SwipeDelta = 1f;
    private float LastSwipedTime;

    private Animator animator;

    public float PosTranslateDist = 5f;

    void Start()
    {
        if (DisableTouchMouseLink == true) // to unlink mouse with touch and have separate functions for those inputs
        {
            Input.simulateMouseWithTouches = false;
        }
        animator = this.GetComponent<Animator>();
    }
    public void CustomButtonTrigger()
    {
        if (Tap == true)
        {
            

        }

        if (DoubleTap == true)
        {
            if (zapCooldown == 0)
            {
                zapCooldown += 1;
                Instantiate(zapPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
            }
            else
            {

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
                        transform.Translate(Vector3.left * distanceBetweenLanes);
                    }
                    else
                    {
                        if (!tripped)
                        {
                            tripped = true;
                        }
                        else
                        {
                            animator.SetBool("dead", true);
                        }
                    }

                }
                else
                {
                    Debug.Log("LEFT");
                    if (curLane != 1)
                    {
                        transform.Translate(Vector3.right * distanceBetweenLanes);
                        curLane -= 1;
                    }
                    else
                    {
                        if (!tripped)
                        {
                            tripped = true;
                        }
                        else
                        {
                            animator.SetBool("dead", true);
                        }
                    }
                    //  gameObject.transform.Translate(DefaultPOS_Player.position.x - PosTranslateDist, 0, 0);
                }
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
                        gameObject.transform.Translate(Vector3.up * jumpForce);
                    }

                }
                else
                {
                    Debug.Log("DOWN");
                    animator.SetBool("sliding", true);
                    transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    sliding = true;
                    StartCoroutine(slideCancel());
                }
            }
        }
    }
    
    IEnumerator slideCancel()
    {
        yield return new WaitForSeconds(1);
        sliding = false;
        transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        animator.SetBool("sliding", false);
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
        if(tripped)
        {
            StartCoroutine(tripRecover(tripRecoveryTime)) ;
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
