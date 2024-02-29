using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int lifes = 3;
    private int coins = 0;
    [SerializeField] private List<GameObject> heartsList = new List<GameObject>();
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject uiMenu;
    [SerializeField] private TextMeshProUGUI coinDisplay;
    void Start()
    {
        gameOverText.GetComponent<TextMeshProUGUI>().text = "Game Over!";
        coinDisplay.text = coins.ToString() + "X";
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(lifes == 0)
        {
            gameOverText.SetActive(true);
            Time.timeScale = 0;
            uiMenu.SetActive(true);
        }

        if(coins == 5)
        {
            gameOverText.SetActive(true);
            gameOverText.GetComponent<TextMeshProUGUI>().text = "WIN";
            Time.timeScale = 0;
            uiMenu.SetActive(true);
        }
    }

    public void TakeDamage()
    {
        lifes--;
    }

    public void GetCoins()
    {
        coins++;
        coinDisplay.text = coins.ToString() + "X";
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Menos UMA VIDA");
            lifes--;
            heartsList[lifes].SetActive(false);
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            GetCoins();
            Destroy(collision.gameObject);
        }
    }

    public GameObject ReturnPos()
    {
        return this.gameObject;
    }
}
