using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Rigidbody _rb;
    void Start()
    {
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
            Rb.AddRelativeForce(force);
        }

    }
}
