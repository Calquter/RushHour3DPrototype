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

    [Header("Base Class Referances")]
    [SerializeField] protected ParticleSystem _smokeEffect;

    public abstract void CarCrash();
    public abstract void TakeDamage(int multiplier = 1);
    public abstract void AvoidObstacles();

}
