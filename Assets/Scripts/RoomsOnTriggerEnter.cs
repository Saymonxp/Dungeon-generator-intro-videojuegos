using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsOnTriggerEnter : MonoBehaviour
{
    public float xPosition;
    public float yPosition;
    // Start is called before the first frame update
    void Start()
    {
        xPosition = transform.position.x;
        yPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CameraController.instance.OnPlayerEnterRoom(this);
        }
    }
}
