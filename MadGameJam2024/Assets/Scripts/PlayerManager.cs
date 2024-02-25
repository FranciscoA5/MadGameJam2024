using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int lifes = 3;
    [SerializeField] private List<GameObject> heartsList = new List<GameObject>();
    [SerializeField] private GameObject gameOverText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lifes == 0)
        {
            gameOverText.SetActive(true);
            Time.timeScale = 0;//TODO: butoes para retry ou main menu
        }
    }

    public void TakeDamage()
    {
        lifes--;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Menos UMA VIDA");
            lifes--;
            heartsList[lifes].SetActive(false);
        }
    }
}
