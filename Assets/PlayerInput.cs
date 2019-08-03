using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Rigidbody _rb;
    GameObject _camera;
    CubePhysics _cubePhysics;

    void Start()
    {
        _camera = GameObject.Find("Camera");
        _cubePhysics = gameObject.GetComponent<CubePhysics>();
    }

    Rigidbody Rb
    {
        get
        {
            if (_rb == null)
            {
                _rb = gameObject.GetComponent<Rigidbody>();
            }
            return _rb;
        }
    }

    void Update()
    {
        var force = new Vector3(Input.GetAxis("Vertical"), 0.0f, Input.GetAxis("Horizontal") * -1.0f);
        if (force.sqrMagnitude > 0.0f)
        {
            // Add world space force based on camera up vector
            //Rb.AddForce(force);
            _cubePhysics.InputForce = force * 10.0f;
            //Rb.AddRelativeForce(force);
        }

    }
}
