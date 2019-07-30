using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePhysics : MonoBehaviour
{
    Rigidbody _rb;
    ConstantForce _constantForce;
    GameObject _world;

    void Start()
    {
        _world = GameObject.Find("World");
        _rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        _rb.useGravity = false;
        _constantForce = gameObject.AddComponent(typeof(ConstantForce)) as ConstantForce;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        var ray = new Ray(transform.position, (transform.position - _world.transform.position).normalized * -1.0f);
        transform.up = transform.position.normalized;
        if (Physics.Raycast(ray, out hit))
        {
            var force = hit.normal * -9.81f;
            _constantForce.force = force;
            Debug.DrawLine(ray.origin, hit.point, Color.white, Time.deltaTime, false);
            Debug.DrawLine(hit.point, hit.point + hit.normal * 2.0f, Color.magenta, Time.deltaTime, false);
        }
        else
        {
            Debug.DrawLine(ray.origin, transform.position + ray.direction, Color.black, Time.deltaTime, false);
        }
        Debug.DrawLine(transform.position, transform.position + transform.right, Color.red, Time.deltaTime, false);
        Debug.DrawLine(transform.position, transform.position + transform.up, Color.green, Time.deltaTime, false);
        Debug.DrawLine(transform.position, transform.position + transform.forward, Color.blue, Time.deltaTime, false);
        Debug.DrawLine(transform.position, transform.position + _constantForce.force * 2.0f, Color.yellow, Time.deltaTime, false);
    }
}
