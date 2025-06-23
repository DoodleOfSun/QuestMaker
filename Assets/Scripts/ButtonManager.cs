using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour
{
    private AudioManager am;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        am = FindAnyObjectByType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartButtonClicked()
    {
        GameManager.instance.FadeOut();
        am.PlayClickSound();
    }

    public void OnOptionButtonClicked()
    {
        GameManager.instance.Option();
        am.PlayClickSound();
    }

    public void OnExitButtonClicked()
    {
        am.PlayClickSound();
        // Exit the application
        Application.Quit();
    }
}
