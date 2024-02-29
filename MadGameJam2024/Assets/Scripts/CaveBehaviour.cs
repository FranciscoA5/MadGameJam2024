using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveBehaviour : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyList;
     private int enemyCount ;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyCount < 15)
        {
            SpawnEnemies();
           

        }
    }

    private void SpawnEnemies()
    {
        int randomIndex = Random.Range(0, enemyList.Count);
       
        Instantiate(enemyList[randomIndex], transform.position, Quaternion.identity);
      
        enemyCount++;


    }
 
}
