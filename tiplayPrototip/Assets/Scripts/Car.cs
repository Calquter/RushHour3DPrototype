using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Car : MonoBehaviour
{

    public float carHealth;
    [SerializeField] protected float _carSpeed;
    [SerializeField] protected float _avoidDistance;


    public abstract void CarCrash();
    public abstract void AvoidObstacles();

}
