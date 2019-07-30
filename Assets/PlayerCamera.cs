using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    float Speed = 10.0f;
    GameObject _world;
    GameObject _player;

    void Start()
    {
        _world = GameObject.Find("World");
        _player = GameObject.Find("Player");
    }

    void Update()
    {
        var position = _player.transform.position.normalized * _world.transform.localScale.magnitude * 2.0f;
        transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * Speed);
        transform.LookAt(_world.transform.position);
    }
}
