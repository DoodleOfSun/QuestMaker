using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject optionPanel;
    private Image fadeoutImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        optionPanel = GameObject.Find("OptionPanel");
        GameObject fadeObj = GameObject.Find("Image_FadeOut");
        if (fadeObj != null)
        {
            fadeoutImage = fadeObj.GetComponent<Image>();
        }

        fadeoutImage.enabled = false;

        optionPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInLogic());
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutLogicAndGoGameScene());
    }

    private IEnumerator FadeInLogic()
    {
        Debug.Log("페이드인");
        fadeoutImage.enabled = true;
        Color color = fadeoutImage.color;
        while (color.a >= 0.05f)
        {
            color.a -= 0.01f;
            fadeoutImage.color = color;
            yield return new WaitForFixedUpdate();
        }


        color.a = 0;
        fadeoutImage.color = color;
    }

    private IEnumerator FadeOutLogicAndGoGameScene()
    {
        fadeoutImage.enabled = true;
        Color color = fadeoutImage.color;
        while (color.a <= 0.95f)
        {
            color.a += 0.01f;
            fadeoutImage.color = color;
            yield return new WaitForFixedUpdate();
        }

        MoveToGameScene();
        color.a = 1;
        fadeoutImage.color = color;
    }

    public void Option()
    {
        Debug.Log("옵션버튼 눌림");
        if (optionPanel.activeSelf)
        {
            optionPanel.SetActive(false);
        }
        else
        {
            optionPanel.SetActive(true);
        }
    }

    public void MoveToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void MoveToReesultScene()
    {
        SceneManager.LoadScene("ResultScene");
    }
}
