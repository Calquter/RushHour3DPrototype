using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Car : MonoBehaviour
{
    [Header("Values")]
    public float carHealth;
    public float carSpeed;
    protected Rigidbody _rBody;
    [SerializeField] protected float _avoidDistance;


    public abstract void CarCrash();
    public abstract void TakeDamage();
    public abstract void AvoidObstacles();

}
