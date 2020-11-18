using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCollection : MonoBehaviour
{
    [SerializeField]
    int candy;
    [SerializeField]
    int sanitizer;
    // Start is called before the first frame update
    void Start()
    {
        candy = 0;
        sanitizer = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Candy")
        {
            other.gameObject.SetActive(false);
            candy++;
        }
        if(other.tag == "Sanitizer")
        {
            other.gameObject.SetActive(false);
            sanitizer++;
        }
    }
}
