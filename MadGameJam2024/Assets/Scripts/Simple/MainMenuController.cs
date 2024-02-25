using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject mainMenu , optionMenu;

    private void Awake()
    {
        slider.value = 1;
    }

    public void SetGameScene()
    {
        SceneManager.LoadScene(1); //<--- Game Scene
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeVolume(Slider slider)
    {
        audioSource.volume = slider.value;
    }

    public void CloseMenu(GameObject menu)
    {
        menu.SetActive(false);
    }

    public void ShowMenu(GameObject menu)
    {
        menu.SetActive(true);
    }
}
