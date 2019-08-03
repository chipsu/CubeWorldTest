using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePhysics : MonoBehaviour
{
    public Vector3 InputForce;
    Rigidbody _rb;
    ConstantForce _constantForce;
    GameObject _world;

    void Start()
    {
        InputForce = Vector3.zero;
        _world = GameObject.Find("World");
        _rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        _rb.useGravity = false;
        _rb.freezeRotation = true;
        _rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        _constantForce = gameObject.AddComponent(typeof(ConstantForce)) as ConstantForce;
    }

    // if physics use cf, else position manually
    void Update()
    {
        _constantForce.force = GetForce();
        _constantForce.relativeForce = InputForce;
        Debug.DrawLine(transform.position, transform.position + transform.right, Color.red, Time.deltaTime, false);
        Debug.DrawLine(transform.position, transform.position + transform.up, Color.green, Time.deltaTime, false);
        Debug.DrawLine(transform.position, transform.position + transform.forward, Color.blue, Time.deltaTime, false);
        Debug.DrawLine(transform.position, transform.position + _constantForce.force, Color.yellow, Time.deltaTime, false);
        Debug.DrawLine(transform.position, transform.position + transform.TransformPoint(InputForce), Color.black, Time.deltaTime, false);
    }

    Vector3 GetForce()
    {
        RaycastHit hit;
        var force = Vector3.zero;
        var ray = new Ray(transform.position, (transform.position - _world.transform.position).normalized * -1.0f);
        transform.up = transform.position.normalized;
        if (Physics.Raycast(ray, out hit))
        {
            force += hit.normal * -9.81f;
            Debug.DrawLine(ray.origin, hit.point, Color.white, Time.deltaTime, false);
            Debug.DrawLine(hit.point, hit.point + hit.normal, Color.magenta, Time.deltaTime, false);
        }
        else
        {
            Debug.DrawLine(ray.origin, transform.position + ray.direction, Color.black, Time.deltaTime, false);
        }
        return force;
    }
}
