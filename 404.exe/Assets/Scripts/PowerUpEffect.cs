using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEffect : MonoBehaviour
{
    // Attach on Player with tag "Player"
    public GameObject spiderWeb;
    public float score;
    static float plusingScore = 1;
    public bool web;
    public float candy;
    float candyGet = 1;
    public float mSpeed = 10f;
    public bool zapBuff;

    // Start is called before the first frame update
    void Start()
    {
        spiderWeb = GameObject.FindGameObjectWithTag("Candy Detector");
        spiderWeb.SetActive(false);
        web = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * mSpeed * Time.deltaTime);
        score += plusingScore;
        if (web == true)
        {
            StartCoroutine(ActivateWeb());
        }
        if(zapBuff == true)
        {
            StartCoroutine(ActivateZap());
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Candy")
        {
            candy += candyGet;
            Destroy(collision.gameObject);
        }
    }

    void OnTriggerEnter(Collider powUp)
    {
        if (powUp.gameObject.tag == "SpiderWeb")
        {
            web = true;
            Destroy(powUp.gameObject);
        }
        if(powUp.gameObject.tag == "FullMoon")
        {
            plusingScore = plusingScore * 2; 
            Destroy(powUp.gameObject);
        }
        if(powUp.gameObject.tag == "JackOLantern")
        {
            candyGet = 2; //candyCount
            Destroy(powUp.gameObject);
        }
        if(powUp.gameObject.tag == "Broomstick")
        {
            mSpeed = 20f; //player speed
            Destroy(powUp.gameObject);
        }
        if(powUp.gameObject.tag == "MagicWand")
        {
            zapBuff = true;
            Destroy(powUp.gameObject);
            //Might need changing something in "TapSwipe.cs" which when zapBuff == true, instantiate another 2 zap at both side line
        }
    }

    IEnumerator ActivateWeb()
    {
        spiderWeb.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        spiderWeb.SetActive(false);
        web = false;
    }

    IEnumerator ActivateZap()
    {
        yield return new WaitForSecondsRealtime(1);
        zapBuff = false;
    }
}
