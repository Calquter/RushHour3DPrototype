using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameFinished;

    [Header("Referances")]
    public GameObject playerOBJ;
    public GameObject finishLine;

    private void Awake() => instance = this;

    private void Start()
    {
        UIManager.instance.finishSlider.maxValue = Vector3.Distance(playerOBJ.transform.position, finishLine.transform.position);
    }
    private void Update()
    {
        UIManager.instance.finishSlider.value = CalculateFinishProgress();
    }
    private float CalculateFinishProgress()
    {
        if (!isGameFinished)
        {
            float distance = Vector3.Distance(playerOBJ.transform.position, finishLine.transform.position);

            float progress = UIManager.instance.finishSlider.maxValue - distance;

            return progress;
        }
        return UIManager.instance.finishSlider.maxValue;
    }
}
