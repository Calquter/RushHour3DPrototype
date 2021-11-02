using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Car : MonoBehaviour
{

    public float carHealth;
    public float carSpeed;
    protected Rigidbody _rBody;
    [SerializeField] protected float _avoidDistance;

    protected void Start()
    {
        _rBody = GetComponent<Rigidbody>();
    }

    public abstract void CarCrash();
    public abstract void AvoidObstacles();

}
