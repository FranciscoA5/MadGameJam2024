using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnCollectibles : MonoBehaviour
{
    [SerializeField] private GameObject collectiblePrefab;
    [SerializeField] private GameObject map;
    private int collectiblesCount;
    [SerializeField] private int distanceCollectibles;
    private List<Vector3> collectiblesPos = new List<Vector3>();
    

    private void Update()
    {
        if(collectiblesCount < 5 && SpawnObjectOutsideCamera())
        {
            collectiblesCount++;
       
        }

        if(collectiblesCount == 5)
        {
            map.GetComponent<BoxCollider2D>().enabled = false;
            
        }
    }

    
    private bool SpawnObjectOutsideCamera()
    {
       
        Camera mainCamera = Camera.main;
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Calculate bounds of the map GameObject
        Bounds mapBounds = CalculateMapBounds();

        // Calculate random spawn position within the spawning area

        float spawnX = Random.Range(mapBounds.min.x , mapBounds.max.x );
        float spawnY = Random.Range(mapBounds.min.y, mapBounds.max.y );
       

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

        for (int i = 0; i < collectiblesPos.Count; i++)
        {
            if (Vector3.Distance(spawnPosition, collectiblesPos[i]) < distanceCollectibles)
            {
                return false;
            }
        }

        if (spawnPosition.x > mainCamera.transform.position.x - cameraWidth/2 && spawnPosition.x < mainCamera.transform.position.x + cameraWidth/2)
        {
            return false;
        }
        else if (spawnPosition.y > mainCamera.transform.position.y - cameraHeight/2 && spawnPosition.y < mainCamera.transform.position.y + cameraHeight/2)
        {
            return false;
        }
        else
        {
            Instantiate(collectiblePrefab, spawnPosition, Quaternion.identity);
            collectiblesPos.Add(spawnPosition);
            return true;
        }

    }

    private Bounds CalculateMapBounds()
    {
        // Assumes that your map GameObject has a Collider2D component
        Collider2D mapCollider = map.GetComponent<Collider2D>();

        if (mapCollider != null)
        {
            return mapCollider.bounds;
        }
        else
        {
            Debug.LogError("Map GameObject is missing Collider2D component.");
            return new Bounds(Vector3.zero, Vector3.zero);
        }
    }

    
}
