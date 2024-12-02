using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform Player;
    Vector3 offset;
    void Start()
    {
        offset.z =  transform.position.z - Player.position.z ;
    }
    void Update()
    {
        var newPos = transform.position;
        newPos.z = Player.position.z +  offset.z;
        transform.position = newPos;
    }
}