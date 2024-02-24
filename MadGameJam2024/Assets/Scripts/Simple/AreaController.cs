using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform[] points; //<<<------ Objects with colliders
    [SerializeField] private LineController line;
    bool hasEnter1, hasEnter2, hasEnter3, hasEnter4;
    [Space]
    [Header("Box Limits")]
    [SerializeField] private float minDistX;
    [SerializeField] private float minDistY;
    [SerializeField] private float growDistY;
    [SerializeField] private float growDistX;
    [Space]
    [Header("Time from Shrinking")]
    [SerializeField] private float timeSpan;
    [SerializeField] private float speed;
    [Space]
    [Header("Individual Walls")]
    [SerializeField] bool Wall1;
    [SerializeField] bool Wall2, Wall3, Wall4;
    [Space]
    [Header("Shrink All wall at same time!")]
    [SerializeField] bool allActive;
    [Space]
    [Header("Shrink Method:\nFalse: Distance Base Speed\nTrue: Identical Shrink Speed")]
    [SerializeField] bool ShrinkMode1;

    private float velY, velX;
    [SerializeField] private bool ativeCollider;

    void Start()
    {
        line.SetUpLine(points);
    }

    private void Update()
    {
        if(ShrinkMode1) ShrinkArea();
        else ShrinkAreaMethod2();

        UpdateLineNewPos();

        UpdateCenter(speed);

        UpdateColliders();
    }

    public void ShrinkAreaMethod2()
    {
        //Limiting shrinking on X
        if (MinDistX() && allActive)
        {
            points[0].position += points[0].right * Time.deltaTime * speed;
            points[1].position += points[1].right * Time.deltaTime * speed;

            points[2].position -= points[2].right * Time.deltaTime * speed;
            points[3].position -= points[3].right * Time.deltaTime * speed;
        }
        //Limiting shrinking on Y
        if (MinDistY() && allActive)
        {
            points[3].position -= points[3].up * Time.deltaTime * speed;
            points[4].position -= points[4].up * Time.deltaTime * speed;


            points[1].position += points[1].up * Time.deltaTime * speed;
            points[2].position += points[2].up * Time.deltaTime * speed;
        }
        //update center of area
        UpdateCenter(speed);
    }

    public void ShrinkArea()
    {
        //Limiting shrinking on X
        if (MinDistX())
        {
            //Left Wall
            if (allActive)
            {
                points[0].position += points[0].right * Time.deltaTime * velX;
                points[1].position += points[1].right * Time.deltaTime * velX;
            
                points[2].position -= points[2].right * Time.deltaTime * velX;
                points[3].position -= points[3].right * Time.deltaTime * velX;
                //update center of area
                UpdateCenter(velX);
            }
        }
        //Limiting shrinking on Y
        if (MinDistY())
        {
            //Up Wall
            if (allActive)
            {
                points[3].position -= points[3].up * Time.deltaTime * velY;
                points[4].position -= points[4].up * Time.deltaTime * velY;
            
                points[1].position += points[1].up * Time.deltaTime * velY;
                points[2].position += points[2].up * Time.deltaTime * velY;
                //update center of area
                UpdateCenter(velY);
            }
        }
        //Update moving velocity on Y or X
        updateVelocity();
    }

    public void updateVelocity()
    {
        velY = Mathf.Abs(points[0].position.y - points[1].position.y / Time.deltaTime * timeSpan) / 4;
        velX = Mathf.Abs(points[1].position.x - points[2].position.x / Time.deltaTime * timeSpan) / 4;
    }

    private bool MinDistY()
    {
        velY = Mathf.Abs(points[0].position.y - points[1].position.y);

        if (velY > minDistY) return true;
        else return false; 
    }

    private bool MinDistX()
    {
        velX = Mathf.Abs(points[1].position.x - points[2].position.x);

        if (velX > minDistX) return true;
        else return false;
    }

    public void UpdateLineNewPos()
    {
        //Left
        if (Wall1 && !hasEnter1)
        {
            hasEnter1 = true;
            Vector3 pos1 = new Vector3(points[0].position.x - growDistX , points[0].position.y, 0);
            Vector3 pos2 = new Vector3(points[1].position.x - growDistX, points[1].position.y, 0);
            points[0].position = pos1;
            points[1].position = pos2;
            ResetValues();
            Wall1 = false;
        }
        //Down
        if (Wall2 && !hasEnter2)
        {
            hasEnter2 = true;
            Vector3 pos1 = new Vector3(points[1].position.x, points[1].position.y - growDistY, 0);
            Vector3 pos2 = new Vector3(points[2].position.x, points[2].position.y - growDistY, 0);
            points[1].position = pos1;
            points[2].position = pos2;
            ResetValues();
            Wall2 = false;
        }
        //Right
        if (Wall3 && !hasEnter3)
        {
            hasEnter3 = true;
            Vector3 pos1 = new Vector3(points[2].position.x + growDistX, points[2].position.y, 0);
            Vector3 pos2 = new Vector3(points[3].position.x + growDistX, points[3].position.y, 0);
            points[2].position = pos1;
            points[3].position = pos2;
            ResetValues();
            Wall3 = false;
        }
        //Up
        if (Wall4 && !hasEnter4)
        {
            hasEnter4 = true;
            Vector3 pos1 = new Vector3(points[3].position.x, points[3].position.y + growDistY, 0);
            Vector3 pos2 = new Vector3(points[0].position.x, points[0].position.y + growDistY, 0);
            points[3].position = pos1;
            points[0].position = pos2;
            ResetValues();
            Wall4 = false;
        }

    }

    private void UpdateCenter(float vel)
    {
        float posY = (points[0].position.y + points[1].position.y + points[2].position.y + points[4].position.y) / 4;
        float posX = (points[0].position.x + points[1].position.x + points[2].position.x + points[4].position.x) / 4;

        Vector3 newPos = new Vector3(posX, posY, Camera.main.transform.position.z);
        cam.transform.position = newPos;

    }

    private async void ResetValues()
    {
        await Task.Delay(1000);
        hasEnter1 = false;
        hasEnter2 = false;
        hasEnter3 = false;
        hasEnter4 = false;
    }

    

    private void UpdateColliders()
    {
        float offset = 1;

        float centerY = Vector2.Distance(points[0].position, points[1].position) / 2;
        float centerX = Vector2.Distance(points[0].position, points[3].position) / 2;

        float lenghtY = Vector2.Distance(points[0].position, points[1].position);
        float lenghtX = Vector2.Distance(points[0].position, points[3].position);
        //wall1

        points[0].GetComponent<BoxCollider2D>().offset = new Vector2(0, -centerY);
        points[1].GetComponent<BoxCollider2D>().offset = new Vector2(centerX, 0);
        points[2].GetComponent<BoxCollider2D>().offset = new Vector2(0, centerY);
        points[3].GetComponent<BoxCollider2D>().offset = new Vector2(-centerX, 0);

        //Update of collider scales
        points[0].GetComponent<BoxCollider2D>().size = new Vector2(1, lenghtY - offset);
        points[1].GetComponent<BoxCollider2D>().size = new Vector2(lenghtX - offset, 1);
        points[2].GetComponent<BoxCollider2D>().size = new Vector2(1, lenghtY - offset);
        points[3].GetComponent<BoxCollider2D>().size = new Vector2(lenghtX - offset, 1);

    }
}
