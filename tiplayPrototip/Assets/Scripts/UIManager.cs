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
    [SerializeField] private TMP_Text _levelText;

    private void Awake() => instance = this;

    private void Start()
    {
        _levelText.text = "Level " + (PlayerPrefs.GetInt("MyCurrentLevel") + 1);
    }
    public IEnumerator GoldTextTime()
    {
        goldText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        goldText.gameObject.SetActive(false);
    }
}
