using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNPC : Car
{

    private void Start()
    {
        _rBody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        _rBody.velocity = transform.forward * carSpeed;
    }

    public override void AvoidObstacles()
    {
        
    }

    public override void TakeDamage(int multiplier = 1)
    {
        
    }
    public override void CarCrash()
    {
        
    }
}
