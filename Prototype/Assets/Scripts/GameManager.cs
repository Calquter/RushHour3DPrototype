using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameFinished;
    public int gatheredGold;

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

    public void Succes()
    {
        if (!isGameFinished && playerOBJ.GetComponent<CarController>()?.carHealth > 0)
        {
            UIManager.instance.finishBG.gameObject.SetActive(true);
            isGameFinished = true;
            PlayerPrefs.SetInt("MyGold", PlayerPrefs.GetInt("MyGold") + 15 + gatheredGold);
            PlayerPrefs.SetInt("MyCurrentLevel", PlayerPrefs.GetInt("MyCurrentLevel") + 1);
        }
        
    }

    public void Failed()
    {
        UIManager.instance.LoseBehaviour();
    }
}
