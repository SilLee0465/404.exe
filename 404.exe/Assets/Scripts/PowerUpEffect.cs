using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEffect : MonoBehaviour
{
    // Attach on Player with tag "Player"

    public GameObject spiderWeb;
    public float score;
    static float plusingScore = 1;
    public float mSpeed = 10f;
    float candyGet = 1;

    public bool web;
    public bool zapBuff;
    public bool broom;
    public bool lantern;
    public bool fullMoon;
    Purchase store;
    
    PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        spiderWeb = GameObject.FindGameObjectWithTag("Candy Detector");
        spiderWeb.SetActive(false);
        web = false;
        player = gameObject.GetComponent<PlayerMovement>();
        store = GetComponent<Purchase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (web == true)
        {
            StartCoroutine(ActivateWeb());
        }
        if (zapBuff == true)
        {
            StartCoroutine(ActivateZap());
        }
        if (broom == true)
        {
            StartCoroutine(ActivateBroomstick());
        }
        if (lantern == true)
        {
            StartCoroutine(ActivateJackOLantern());
        }
        if (fullMoon == true)
        {
            StartCoroutine(ActivateFullMoon());
        }
    }

    void OnTriggerEnter(Collider powUp)
    {
        if (powUp.gameObject.tag == "SpiderWeb")
        {
            web = true;
            Destroy(powUp.gameObject);
        }
        if (powUp.gameObject.tag == "FullMoon")
        {
            fullMoon = true;
            Destroy(powUp.gameObject);
        }
        if (powUp.gameObject.tag == "JackOLantern")
        {
            lantern = true;
            Destroy(powUp.gameObject);
        }
        if (powUp.gameObject.tag == "Broomstick")
        {
            broom = true;
            Destroy(powUp.gameObject);
        }
        if (powUp.gameObject.tag == "MagicWand")
        {
            zapBuff = true;
            Destroy(powUp.gameObject);
        }
    }

    IEnumerator ActivateBroomstick()
    {
        player.mSpeed = 40f; //player speed
        yield return new WaitForSecondsRealtime(Purchase.broomDuration);
        player.mSpeed = 28f;
        broom = false;
    }

    IEnumerator ActivateWeb()
    {
        spiderWeb.SetActive(true);
        yield return new WaitForSecondsRealtime(Purchase.webDuration);
        spiderWeb.SetActive(false);
        web = false;
    }

    IEnumerator ActivateZap()
    {
        yield return new WaitForSecondsRealtime(Purchase.zapDuration);
        zapBuff = false;
    }


    IEnumerator ActivateJackOLantern()
    {
        candyGet = 2;
        yield return new WaitForSecondsRealtime(Purchase.lanternDuration);
        lantern = false;
        candyGet = 1;
    }

    IEnumerator ActivateFullMoon()
    {
        plusingScore = plusingScore * 2;
        yield return new WaitForSecondsRealtime(Purchase.moonDuration);
        fullMoon = false;
    }
}
