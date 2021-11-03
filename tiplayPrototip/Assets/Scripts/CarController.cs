using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : Car
{
    [SerializeField] private float _damageThreshold;
    [SerializeField] private float _topSpeed;

    [Header("Referances")]
    [SerializeField] private GameObject _carModel;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private Transform _raycastPos;
    [SerializeField] private ParticleSystem _smokeEffect;
    [SerializeField] private CameraFollow _myCamera;
    

    private bool _canTakeDamage;
    private void Start()
    {
        _rBody = GetComponent<Rigidbody>();
        _canTakeDamage = true;
    }
    void Update()
    {
        MoveActions();
    }


    private void MoveActions()
    {
        _rBody.velocity = Vector3.forward * carSpeed;


        if (Input.GetMouseButton(0))
        {

            carSpeed = Mathf.Lerp(carSpeed, _topSpeed, Time.deltaTime / 6);

            if (transform.position.x > -2)
                _rBody.velocity += -Vector3.right * (carSpeed * 0.5f);

            if (transform.position.x > -1.5f)
                RotatePlayerModel(transform.position + transform.right * -2 + transform.forward * 3.5f);
            else
                RotatePlayerModel(transform.position + transform.forward * 2);

            _myCamera.CameraEffect(true);

        }
        else
        {
            AvoidObstacles();

            if (transform.position.x < 2)
                _rBody.velocity += Vector3.right * (carSpeed * 0.5f);

            if (transform.position.x < 1.5f)
                RotatePlayerModel(transform.position + transform.right * 2 + transform.forward * 3.5f);
            else
                RotatePlayerModel(transform.position + transform.forward * 5);

            if (carSpeed < _damageThreshold)
                _myCamera.CameraEffect(false);

        }
    }
    private void RotatePlayerModel(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir);
        _carModel.transform.rotation = Quaternion.Slerp(_carModel.transform.rotation, rot, Time.deltaTime * 3.5f);
       
    }
    public override void AvoidObstacles()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(_raycastPos.transform.position, _raycastPos.transform.forward, out hitInfo, _avoidDistance, _obstacleMask))
            carSpeed = Mathf.Lerp(carSpeed, hitInfo.collider.GetComponent<Car>().carSpeed, Time.deltaTime * 3.5f);
    }
    public override void TakeDamage()
    {
        float damage = carSpeed > _damageThreshold ? carSpeed - _damageThreshold + 1 : 1;

        carHealth -= damage * 2.5f;
        UIManager.instance.healthSlider.value = carHealth;
        _canTakeDamage = false;

        if (carHealth <= 50 && !_smokeEffect.gameObject.activeSelf)
        {
            _smokeEffect.gameObject.SetActive(true);

            if (UIManager.instance.healthSliderImage.color != Color.red)
                UIManager.instance.healthSliderImage.color = Color.red;
        }

        if (carHealth <= 0)
            CarCrash();
    }
    public override void CarCrash()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Obstacle")
        {
            if (carSpeed >= _damageThreshold)
            {
                TakeDamage();
                carSpeed = _damageThreshold - 5;
            }
        }
    }
}
