using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AreaController : MonoBehaviour
{

    public static AreaController Instance;

    [SerializeField] private Camera cam;
    [SerializeField] private Transform[] points; //<<<------ Objects with colliders
    //[SerializeField] private LineController[] line;
    [SerializeField] private LineRenderer[] lines;
    [SerializeField] private Texture[] tex;
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
    [SerializeField] private float speed, speed2;
    [Space]
    [Header("Individual Walls")]
    public bool Wall1;
    public bool Wall2, Wall3, Wall4;
    [Space]
    [Header("Frozen Walls")]
    public bool frozenWall1;
    public bool frozenWall2, frozenWall3, frozenWall4;
    [Space]
    [Header("Texture Walls")]
    public bool texWall1;
    public bool texWall2, texWall3, texWall4;
    [Space]
    [Header("Deactivate Texture Walls")]
    public bool isDeactive1;
    public bool isDeactive2, isDeactive3, isDeactive4;
    [Space]
    [Header("Shrink All wall at same time!")]
    [SerializeField] bool allActive;
    [Space]
    [Header("Shrink Method:\nFalse: Distance Base Speed\nTrue: Identical Shrink Speed")]
    [SerializeField] bool ShrinkMode1;

    private float velY, velX;
    [SerializeField] private bool ativeCollider;
    float offset = 0.25f;
    public int delaySpike = 1;

    void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        if(ShrinkMode1) ShrinkArea();
        else ShrinkAreaMethod2();

        UpdateLineNewPos();
        //Colocar no player
        //UpdateCenter(speed);

        UpdateColliders();

        SetTextureSpikes();
    }


    public void ShrinkAreaMethod2()
    {
        //Limiting shrinking on X
        if (MinDistX() && allActive)
        {
            if (!frozenWall1)
            {
                points[0].position += points[0].right * Time.deltaTime * speed;
                points[1].position += points[1].right * Time.deltaTime * speed;
            }
            if (!frozenWall3)
            {
                points[2].position -= points[2].right * Time.deltaTime * speed;
                points[3].position -= points[3].right * Time.deltaTime * speed;
            }
        }
        //Limiting shrinking on Y
        if (MinDistY() && allActive)
        {
            if (!frozenWall4)
            {
                points[3].position -= points[3].up * Time.deltaTime * speed;
                points[4].position -= points[4].up * Time.deltaTime * speed;
            }
            if (!frozenWall2)
            {
                points[1].position += points[1].up * Time.deltaTime * speed;
                points[2].position += points[2].up * Time.deltaTime * speed;
            }
        }
        //update center of area
        //UpdateCenter(speed);
    }

    public void ShrinkArea()
    {
        //Limiting shrinking on X
        if (MinDistX())
        {
            //Left Wall
            if (allActive)
            {
                if (!frozenWall1)
                {
                    points[0].position += points[0].right * Time.deltaTime * velX;
                    points[1].position += points[1].right * Time.deltaTime * velX;
                }
                if (!frozenWall3)
                {
                    points[2].position -= points[2].right * Time.deltaTime * velX;
                    points[3].position -= points[3].right * Time.deltaTime * velX;
                }
                //update center of area
                //UpdateCenter(velX);
            }
        }
        //Limiting shrinking on Y
        if (MinDistY())
        {
            //Up Wall
            if (allActive)
            {
                if (!frozenWall4)
                {
                    points[3].position -= points[3].up * Time.deltaTime * velY;
                    points[0].position -= points[4].up * Time.deltaTime * velY;
                }
                if (!frozenWall2)
                {
                    points[1].position += points[1].up * Time.deltaTime * velY;
                    points[2].position += points[2].up * Time.deltaTime * velY;
                }
                //update center of area
                //UpdateCenter(velY);
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
            points[0].position = Vector3.Lerp(points[0].position, pos1 , speed2);
            points[1].position = Vector3.Lerp(points[1].position, pos2, speed2);
            ResetValues();
            Wall1 = false;
        }
        //Down
        if (Wall2 && !hasEnter2)
        {
            hasEnter2 = true;
            Vector3 pos1 = new Vector3(points[1].position.x, points[1].position.y - growDistY, 0);
            Vector3 pos2 = new Vector3(points[2].position.x, points[2].position.y - growDistY, 0);
            points[1].position = Vector3.Lerp(points[1].position, pos1, speed2);
            points[2].position = Vector3.Lerp(points[2].position, pos2, speed2);
            ResetValues();
            Wall2 = false;
        }
        //Right
        if (Wall3 && !hasEnter3)
        {
            hasEnter3 = true;
            Vector3 pos1 = new Vector3(points[2].position.x + growDistX, points[2].position.y, 0);
            Vector3 pos2 = new Vector3(points[3].position.x + growDistX, points[3].position.y, 0);
            points[2].position = Vector3.Lerp(points[2].position, pos1, speed2);
            points[3].position = Vector3.Lerp(points[3].position, pos2, speed2);
            ResetValues();
            Wall3 = false;
        }
        //Up
        if (Wall4 && !hasEnter4)
        {
            hasEnter4 = true;
            Vector3 pos1 = new Vector3(points[3].position.x, points[3].position.y + growDistY, 0);
            Vector3 pos2 = new Vector3(points[0].position.x, points[0].position.y + growDistY, 0);
            points[3].position = Vector3.Lerp(points[3].position, pos1, speed2);
            points[0].position = Vector3.Lerp(points[0].position, pos2, speed2);
            ResetValues();
            Wall4 = false;
        }

    }

    public Vector2 CenterPos()
    {
        float posY = (points[0].position.y + points[1].position.y + points[2].position.y + points[3].position.y) / 4;
        float posX = (points[0].position.x + points[1].position.x + points[2].position.x + points[3].position.x) / 4;

        Vector2 Pos = new Vector2(posX, posY);
        return Pos;
    }

    private void UpdateCenter(float vel)
    {
        float posY = (points[0].position.y + points[1].position.y + points[2].position.y + points[3].position.y) / 4;
        float posX = (points[0].position.x + points[1].position.x + points[2].position.x + points[3].position.x) / 4;

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
        float centerY = Vector2.Distance(points[0].position, points[1].position) / 2;
        float centerX = Vector2.Distance(points[0].position, points[3].position) / 2;

        float lenghtY = Vector2.Distance(points[0].position, points[1].position);
        float lenghtX = Vector2.Distance(points[0].position, points[3].position);

        float width = lines[0].GetComponent<LineRenderer>().startWidth / 2;
        //wall1

        points[0].GetComponent<BoxCollider2D>().offset = new Vector2(0, -centerY);
        points[1].GetComponent<BoxCollider2D>().offset = new Vector2(centerX, 0);
        points[2].GetComponent<BoxCollider2D>().offset = new Vector2(0, centerY);
        points[3].GetComponent<BoxCollider2D>().offset = new Vector2(-centerX, 0);

        //Update of collider scales
        points[0].GetComponent<BoxCollider2D>().size = new Vector2(width, lenghtY - offset);
        points[1].GetComponent<BoxCollider2D>().size = new Vector2(lenghtX - offset, width);
        points[2].GetComponent<BoxCollider2D>().size = new Vector2(width, lenghtY - offset);
        points[3].GetComponent<BoxCollider2D>().size = new Vector2(lenghtX - offset, width);

    }

    private void SetTextureSpikes()
    {
        if (texWall1)
        {
            lines[0].material.SetTexture("SpikeWall", tex[0]);
        }
        else if (texWall2)
        {
            lines[1].material.SetTexture("SpikeWall", tex[0]);
        }
        else if (texWall3)
        {
            lines[2].material.SetTexture("SpikeWall", tex[0]);
        }
        else if (texWall4)
        {
            lines[3].material.SetTexture("SpikeWall", tex[0]);
        }
        else
        {
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i].material.SetTexture("SpikeWall", tex[1]);
            }
        }
    }
}
