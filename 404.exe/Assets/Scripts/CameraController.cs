using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;
    public float smoothSpeed = 0.2f;

    void Start()
    {
        offset = transform.position - player.position;
    }

    void Update()
    {
        //Vector3 newPosition = new Vector3(transform.position.x, offset.y + player.position.y, offset.z + player.position.z);
        Vector3 newPosition = new Vector3(offset.x + player.position.x, offset.y + player.position.y, offset.z + player.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed);
    }
}
