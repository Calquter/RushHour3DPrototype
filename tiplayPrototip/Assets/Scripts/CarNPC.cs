using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNPC : Car
{
    void Update()
    {
        _rBody.velocity = transform.forward * carSpeed;
    }

    public override void AvoidObstacles()
    {
        throw new System.NotImplementedException();
    }

    public override void CarCrash()
    {
        throw new System.NotImplementedException();
    }
}
