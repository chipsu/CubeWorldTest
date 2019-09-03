using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePhysics : MonoBehaviour
{
    public Vector3 InputForce;
    public bool StickToGround;
    Rigidbody _rb;
    ConstantForce _constantForce;
    GameObject _world;
    Collider _worldCollider;

    void Start()
    {
        InputForce = Vector3.zero;
        StickToGround = true;
        _world = GameObject.Find("World");
        _rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        _rb.useGravity = false;
        _rb.freezeRotation = true;
        _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        //_rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        _constantForce = gameObject.AddComponent(typeof(ConstantForce)) as ConstantForce;
        _worldCollider = _world.GetComponentInChildren<Collider>();
    }

    // if physics use cf, else position manually
    void Update()
    {
        RaycastHit hit;
        var ray = new Ray(transform.position, (transform.position - _world.transform.position).normalized * -1.0f);
        //if(!Physics.Raycast(ray, out hit))
        if(!_worldCollider.Raycast(ray, out hit, 1000.0f))
        {
            Debug.Log("Ground object not found!");
            Debug.DrawLine(ray.origin, transform.position + ray.direction, Color.black, Time.deltaTime, false);
            return;
        }
        Debug.DrawLine(ray.origin, hit.point, Color.white, Time.deltaTime, false);
        Debug.DrawLine(hit.point, hit.point + hit.normal, Color.magenta, Time.deltaTime, false);
        UpdatePhysics(hit);
    }

    void UpdatePhysics(RaycastHit hit)
    {
        if (StickToGround)
        {
            _rb.position = hit.point;
            _constantForce.force = Vector3.zero;
        }
        else
        {
            _constantForce.force = GetGravity(hit);
        }
        _constantForce.relativeForce = GetInputForce(hit);
        transform.up = transform.position.normalized;
        transform.forward = _world.transform.up;
        transform.rotation = Quaternion.LookRotation(Vector3.Exclude(hit.normal, transform.forward), hit.normal);
        Debug.DrawLine(transform.position, transform.position + transform.right, Color.red, Time.deltaTime, false);
        Debug.DrawLine(transform.position, transform.position + transform.up, Color.green, Time.deltaTime, false);
        Debug.DrawLine(transform.position, transform.position + transform.forward, Color.blue, Time.deltaTime, false);
        Debug.DrawLine(transform.position, transform.position + _constantForce.force, Color.yellow, Time.deltaTime, false);
        Debug.DrawLine(transform.position, transform.position + transform.TransformPoint(_constantForce.relativeForce), Color.black, Time.deltaTime, false);
    }

    Vector3 GetGravity(RaycastHit hit)
    {
        return hit.normal * -9.81f;
    }

    Vector3 GetInputForce(RaycastHit hit)
    {
        return new Vector3(1.0f, 0, 0);
        return InputForce;
    }
}
