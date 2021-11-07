using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float _backSpace;
    [SerializeField] private float _upSpace;
    [SerializeField] private float _lerpSpeed;
    [SerializeField] private float _fovSpeed;

    private Camera _myCam;

    private Vector3 _newPos;
    private void Start()
    {
        _myCam = this.gameObject.GetComponent<Camera>();
    }
    private void FixedUpdate()
    {
        if (!GameManager.instance.isGameFinished)
            FollowPlayer();
        else
            FinishedBehaviour();
    }
    private void FollowPlayer()
    {

        Transform playerTransform = GameManager.instance.playerOBJ.transform;

        _newPos = playerTransform.position + playerTransform.forward * _backSpace + playerTransform.up * _upSpace;
        transform.position = Vector3.Lerp(transform.position, _newPos, _lerpSpeed * GameManager.instance.playerOBJ.GetComponent<CarController>().carSpeed * Time.deltaTime);
    }

    public void CameraEffect(bool isIncreasing)
    {
        if (isIncreasing)
        {
            _myCam.fieldOfView = Mathf.Lerp(_myCam.fieldOfView, 110, _fovSpeed * Time.deltaTime);
        }
        else
        {
            _myCam.fieldOfView = Mathf.Lerp(_myCam.fieldOfView, 60, Mathf.Pow(_fovSpeed, 2) * Time.deltaTime);
        }
    }

    private void FinishedBehaviour()
    {
        transform.LookAt(GameManager.instance.playerOBJ.transform);

        transform.position = Vector3.Slerp(transform.position, GameManager.instance.finishLine.transform.position + GameManager.instance.finishLine.transform.forward * 5f + GameManager.instance.finishLine.transform.up * 5, Time.deltaTime * .35f);
        CameraEffect(false);
    }
}
