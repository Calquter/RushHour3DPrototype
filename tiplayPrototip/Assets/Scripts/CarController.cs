using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : Car
{
    [Header("Referances")]
    [SerializeField] private GameObject _playerModel;

    private Rigidbody _rBody;

    void Start()
    {
        _rBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MoveActions();
    }


    private void MoveActions()
    {
        _rBody.velocity = Vector3.forward * _carSpeed;


        if (Input.GetMouseButton(0))
        {

            _carSpeed = Mathf.Lerp(_carSpeed, 35, Time.deltaTime);

            if (transform.position.x > -2)
                _rBody.velocity += -Vector3.right * (_carSpeed * 0.4f);

            if (transform.position.x > -1.5f)
                RotatePlayerModel(transform.position + transform.right * -2 + transform.forward * 3.5f);
            else
                RotatePlayerModel(transform.position + transform.forward * 2);

        }
        else
        {
            if (transform.position.x < 2)
                _rBody.velocity += Vector3.right * (_carSpeed * 0.4f);


            if (transform.position.x < 1.5f)
                RotatePlayerModel(transform.position + transform.right * 2 + transform.forward * 3.5f);
            else
                RotatePlayerModel(transform.position + transform.forward * 5);

        }
    }

    private void RotatePlayerModel(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir);
        _playerModel.transform.rotation = Quaternion.Slerp(_playerModel.transform.rotation, rot, Time.deltaTime * 3.5f);
       
        
    }

    public override void AvoidObstacles()
    {
       
    }

    public override void CarCrash()
    {
        
    }
}
