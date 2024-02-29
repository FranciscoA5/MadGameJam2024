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
    public List<Animator> anim;
    public Image bigBoy;
    public Sprite[] smallBoy;

    private void Awake()
    {
        slider.value = 1;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        BigBoyStuff();
    }

    public void BigBoyStuff()
    {
        if(slider.value > 0 && slider.value < 0.1f) 
        {
            bigBoy.sprite = smallBoy[0];
        }
        else if (slider.value > 0.1 && slider.value < 0.25f)
        {
            bigBoy.sprite = smallBoy[1];
        }
        else if (slider.value > 0.25 && slider.value < 0.5f)
        {
            bigBoy.sprite = smallBoy[2];
        }
        else if (slider.value > 0.5 && slider.value < 0.75f)
        {
            bigBoy.sprite = smallBoy[3];
        }
        else if (slider.value > 0.75 && slider.value < 1f)
        {
            bigBoy.sprite = smallBoy[4];
        }
    }


    private void OnEnable()
    {
        foreach (var anim in anim)
        {
            anim.Play("isActive");
        }
    }
    public void SetGameScene(int id)
    {
        SceneManager.LoadScene(id); //<--- Game Scene
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
