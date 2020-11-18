using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach on candy

public class Coin : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 10f;

    CoinMove coinMove;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        coinMove = gameObject.GetComponent<CoinMove>();
        coinMove.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Candy Detector")
        {
            coinMove.enabled = true;
        }
    }
}
