using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    CubePhysics _cubePhysics;

    void Start()
    {
        _cubePhysics = gameObject.GetComponent<CubePhysics>();
    }

    void Update()
    {
        var force = new Vector3(1.0f, 0.0f, 0.0f);
        if (force.sqrMagnitude > 0.0f)
        {
            _cubePhysics.InputForce = force * 10.0f;
        }
    }
}
