using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zapScript : MonoBehaviour
{
    public float zapSpeed;
    public float zapRange;
    private Vector3 initialPos;
    void zapMovement()
    {
        transform.Translate(Vector3.forward * zapSpeed * Time.deltaTime);
    }

    private void Awake()
    {
        initialPos = this.transform.position;
    }

    private void Update()
    {
        zapMovement();
        if (this.transform.position.z > initialPos.z + zapRange)
        {
            Destroy(this.gameObject);
        }
    }

}
