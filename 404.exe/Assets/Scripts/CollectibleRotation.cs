using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleRotation : MonoBehaviour
{
    private float speed = 4f;
    [SerializeField]
    GameObject[] candy;
    [SerializeField]
    GameObject[] sanitizer;
    // Start is called before the first frame update
    void Start()
    {
        candy = GameObject.FindGameObjectsWithTag("Candy");
        sanitizer = GameObject.FindGameObjectsWithTag("Sanitizer");
    }

    private void FixedUpdate()
    {
        foreach (GameObject candy in candy)
        {
            candy.transform.Rotate(0, speed, 0);
        }

        foreach (GameObject sanitizer in sanitizer)
        {
            sanitizer.transform.Rotate(0, 0, speed);
        }
    }
}