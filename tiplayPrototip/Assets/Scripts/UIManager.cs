using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Referances")]
    public Slider healthSlider;
    public Image healthSliderImage;
    public Slider finishSlider;
    public TMP_Text goldText;

    private void Awake() => instance = this;


    public IEnumerator GoldTextTime()
    {
        goldText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        goldText.gameObject.SetActive(false);
    }
}
