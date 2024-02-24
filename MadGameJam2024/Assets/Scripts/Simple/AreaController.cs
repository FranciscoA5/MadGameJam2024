using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform[] points;
    [SerializeField] private LineController line;
    [SerializeField] private Transform firstPos;
    [Space]
    [Header("Box Limits")]
    [SerializeField] private float minDistX;
    [SerializeField] private float minDistY;
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
    [SerializeField] bool Method2;

    private float velY, velX;


    void Start()
    {
        line.SetUpLine(points);
        firstPos = transform;
    }

    private void Update()
    {
        if(Method2) ShrinkAreaMethod2();
        else ShrinkArea();

        if(!Wall1 || !Wall2 || !Wall3 || !Wall4)
            UpdateLineNewPos();

        UpdateCenter(speed);
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
        
        if (Wall1)
        {
            points[0].position = points[0].position - points[0].right * 0.3f;
            points[1].position = points[1].position - points[1].right * 0.3f;
            Wall1 = true;
        }
        if (Wall2)
        {
            points[1].position = points[1].position - points[1].up * 0.3f;
            points[2].position = points[2].position - points[2].up * 0.3f;
            Wall2 = true;
        }
        if (Wall3)
        {
            points[2].position = points[2].position + points[2].right * 0.3f;
            points[3].position = points[3].position + points[3].right * 0.3f;
            Wall3 = true;
        }
        if (Wall4)
        {
            points[3].position = points[3].position + points[3].up * 0.3f;
            points[4].position = points[4].position + points[4].up * 0.3f;
            Wall4 = true;
        }

    }

    private void UpdateCenter(float vel)
    {
        float posY = (points[0].position.y + points[1].position.y + points[2].position.y + points[4].position.y) / 4;
        float posX = (points[0].position.x + points[1].position.x + points[2].position.x + points[4].position.x) / 4;



        Vector3 newPos = new Vector3(posX, posY, Camera.main.transform.position.z);
        cam.transform.position = newPos;

    }

}
