using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNPC : Car
{
    [SerializeField] private Transform _rayTransform;
    [SerializeField] private LayerMask _playerLayer;
    private bool _isTriggered;
    [SerializeField] private float _startY;
    private void Start()
    {
        _startY = transform.rotation.eulerAngles.y;
        _rBody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        
        _rBody.velocity = transform.forward * carSpeed;
        transform.rotation = Quaternion.Euler(0, _startY, 0);
        AvoidObstacles();
    }

    public override void AvoidObstacles()
    {

        if (Physics.Raycast(_rayTransform.position, _rayTransform.forward, _avoidDistance, _playerLayer))
            _isTriggered = true;

        if (_isTriggered)
        {
            Vector3 dir = transform.position + transform.forward * 2f + transform.right * 5f;
            dir.y = 0;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime / 3.5f);
        }
    }
    public override void CarCrash()
    {
        _smokeEffect.gameObject.SetActive(true);
        carSpeed = 0;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Player" && col.collider.GetComponent<CarController>().carSpeed > 15f)
        {
            CarCrash();
        }
    }
}
