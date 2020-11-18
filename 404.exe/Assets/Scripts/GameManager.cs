using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerMovement _PlayerMovement;
    public GameObject GameOver;
    // Start is called before the first frame update
    void Start()
    {
        _PlayerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_PlayerMovement.isDead == true)
        {
            GameOver.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
