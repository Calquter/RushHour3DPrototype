using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Referances")]
    public Slider healthSlider;
    public Image healthSliderImage;
    public Slider finishSlider;
    public TMP_Text goldText;
    public Image finishBG;
    public TMP_Text finishTxt;
    public Button finishButton;
    [SerializeField] private TMP_Text _levelText;

    private void Awake() => instance = this;

    private void Start()
    {
        _levelText.text = "Level " + (PlayerPrefs.GetInt("MyCurrentLevel") + 1);
    }

    public void LoseBehaviour()
    {
        finishBG.gameObject.SetActive(true);
        finishBG.color = new Color32(180, 77, 57, 205);
        finishTxt.text = "Failed !";
        finishButton.gameObject.SetActive(false);
    }

    public void NextButton()
    {
        SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("MyCurrentLevel"));
    }
    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public IEnumerator GoldTextTime()
    {
        goldText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        goldText.gameObject.SetActive(false);
    }
}
