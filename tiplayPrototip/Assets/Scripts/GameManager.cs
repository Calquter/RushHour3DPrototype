using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool _isGameFinished;

    [Header("Referances")]
    public GameObject playerOBJ;
    [SerializeField] private GameObject _finishLine;

    private void Awake() => instance = this;

    private void Start()
    {
        UIManager.instance.finishSlider.maxValue = Vector3.Distance(playerOBJ.transform.position, _finishLine.transform.position);
    }
    private void Update()
    {
        UIManager.instance.finishSlider.value = CalculateFinishProgress();
    }
    private float CalculateFinishProgress()
    {
        if (!_isGameFinished)
        {
            float distance = Vector3.Distance(playerOBJ.transform.position, _finishLine.transform.position);

            float progress = UIManager.instance.finishSlider.maxValue - distance;

            return progress;
        }
        return UIManager.instance.finishSlider.maxValue;
    }
}
