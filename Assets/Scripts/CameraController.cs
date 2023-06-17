using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public RoomsOnTriggerEnter currRoom;
    public float moveSpeed;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }


    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        Vector3 targetPos = GetCameraTargetPos();

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);
    }

    
    Vector3 GetCameraTargetPos()
    {
        if (currRoom == null)
        {
            return new Vector3(0, 0, -10);
        }
        
        Vector3 targetPos = new Vector3(currRoom.xPosition, currRoom.yPosition, -10);
        return targetPos;
    }

    public bool IsSwitchingScene()
    {
        return transform.position.Equals(GetCameraTargetPos()) == false;
    }

    public void OnPlayerEnterRoom(RoomsOnTriggerEnter room)
    {
        CameraController.instance.currRoom = room;
    }
}
