using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Referances")]
    public Slider healthSlider;
    public Image healthSliderImage;
    public Slider finishSlider;

    private void Awake() => instance = this;

}
