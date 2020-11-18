using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach on candy

public class CoinMove : MonoBehaviour
{
    Coin coin;

    void Start()
    {
        coin = gameObject.GetComponent<Coin>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, coin.player.position, coin.moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
