using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : Car
{
    [SerializeField] private float _damageThreshold;
    [SerializeField] private float _topSpeed;

    [Header("Referances")]
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private Transform _raycastPos;
    [SerializeField] private ParticleSystem _speedParticle;
    [SerializeField] private CameraFollow _myCamera;

    private bool isDead;
    public bool isPlayerDead { get { return isDead; } private set { isDead = value; } }

    private void Start()
    {
        _rBody = GetComponent<Rigidbody>();
        isPlayerDead = false;
    }
    void Update()
    {
        if (!isPlayerDead)
            MoveActions();
    }
    private void MoveActions()
    {
        _rBody.velocity = transform.forward * carSpeed;

        if (carSpeed > 25 && !_speedParticle.gameObject.activeSelf)
            _speedParticle.gameObject.SetActive(true);
        else if (carSpeed < 25 && _speedParticle.gameObject.activeSelf)
            _speedParticle.gameObject.SetActive(false);

        if (Input.GetMouseButton(0))
        {

            carSpeed = Mathf.Lerp(carSpeed, _topSpeed, Time.deltaTime / 6);

            if (transform.position.x > -1.8f)
                RotatePlayerModel(transform.position + -Vector3.right * 2 + Vector3.forward * 5.5f);
            else if (transform.position.x < -2f)
                RotatePlayerModel(transform.position + Vector3.right * 2 + Vector3.forward * 5.5f);
            else
                RotatePlayerModel(transform.position + Vector3.forward * 10.5f);

            if (!GameManager.instance.isGameFinished)
                _myCamera.CameraEffect(true);


        }
        else
        {
            AvoidObstacles();

            if (transform.position.x < 1.8f)
                RotatePlayerModel(transform.position + Vector3.right * 2 + Vector3.forward * 5.5f);
            else if (transform.position.x > 2f)
                RotatePlayerModel(transform.position + -Vector3.right * 2 + Vector3.forward * 5.5f);
            else
            {
                RotatePlayerModel(transform.position + Vector3.forward * 10.5f);
            }

            if (carSpeed < _damageThreshold)
                _myCamera.CameraEffect(false);

        }
    }
    private void RotatePlayerModel(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 2.5f);
       
    }
    public override void AvoidObstacles()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(_raycastPos.transform.position, _raycastPos.transform.forward, out hitInfo, _avoidDistance, _obstacleMask))
            carSpeed = Mathf.Lerp(carSpeed, hitInfo.collider.GetComponent<Car>().carSpeed, Time.deltaTime * 3.5f);
    }
    public override void TakeDamage(int multiplier = 1)
    {
        float damage = carSpeed > _damageThreshold ? carSpeed - _damageThreshold + 1 : 1;

        carHealth -= damage * 2.5f * multiplier;
        UIManager.instance.healthSlider.value = carHealth;

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
        isPlayerDead = true;
        carSpeed = 0;
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Obstacle")
        {
            if (carSpeed >= _damageThreshold)
            {
                Vector3 obsttacleToPlayer = transform.position - col.collider.transform.position;

                if (Vector3.Dot(col.collider.transform.forward.normalized, obsttacleToPlayer.normalized) > 0.5f)
                    TakeDamage(3);
                else
                    TakeDamage();

                print(Vector3.Dot(col.collider.transform.forward.normalized, obsttacleToPlayer.normalized));

                carSpeed = _damageThreshold - 5;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FinishLine")
        {
            GameManager.instance.isGameFinished = true;
            _speedParticle.gameObject.SetActive(false);
        }
    }
}
