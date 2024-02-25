using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class LineController : MonoBehaviour
{
    [SerializeField] private AreaController controller;
    [SerializeField] private LineRenderer lineRenderer1, lineRenderer2, lineRenderer3, lineRenderer4;
    [SerializeField] private Transform[] points;
    [SerializeField] private Material[] tex;
    public Color frozenColor, defaultColor;
    public ColorGradientMode colorGradient;
    public bool isFrozenWall, isSpikeWall;


    bool hasEnter1, hasEnter2, hasEnter3, hasEnter4;

    void Update()
    {
        lineRenderer1.SetPosition(0, points[0].position);
        lineRenderer1.SetPosition(1, points[1].position);
        lineRenderer2.SetPosition(0, points[1].position);
        lineRenderer2.SetPosition(1, points[2].position);
        lineRenderer3.SetPosition(0, points[2].position);
        lineRenderer3.SetPosition(1, points[3].position);
        lineRenderer4.SetPosition(0, points[3].position);
        lineRenderer4.SetPosition(1, points[0].position);

        SetSpikesTexture();
        SetTextureColor();

        SetValues();
        
    }

    private void SetSpikesTexture()
    {
        if (controller.texWall1 && !hasEnter1)
        {
            lineRenderer1.material = tex[0];
        }
        if (controller.texWall2 && !hasEnter2)
        {
            hasEnter2 = true;
            lineRenderer2.material = tex[0];
        }
        if (controller.texWall3 && !hasEnter3)
        {
            hasEnter3 = true;
            lineRenderer3.material = tex[0];
        }
        if (controller.texWall4 && !hasEnter4)
        {
            hasEnter4 = true;
            lineRenderer4.material = tex[0];
        }
    }
    private void SetTextureColor()
    {
        if (controller.frozenWall1 && !hasEnter1)
        {
            //lineRenderer1.startColor = frozenColor;
            lineRenderer1.material.color = Color.blue;
        }
        if (controller.frozenWall2 && !hasEnter2)
        {
            hasEnter2 = true;
            lineRenderer2.material.color = Color.blue;
        }
        if (controller.frozenWall3 && !hasEnter3)
        {
            hasEnter3 = true;
            lineRenderer3.material.color = Color.blue;
        }
        if (controller.frozenWall4 && !hasEnter4)
        {
            hasEnter4 = true;
            lineRenderer4.material.color = Color.blue;
        }
    }

    void SetValues()
    {
        if (controller.isDeactive1) { lineRenderer1.material = tex[1]; hasEnter1 = false; controller.texWall1 = false; controller.frozenWall1 = false; lineRenderer1.material.color = Color.white; }
        if (controller.isDeactive2) { lineRenderer2.material = tex[1]; hasEnter2 = false; controller.texWall2 = false; controller.frozenWall2 = false; lineRenderer2.material.color = Color.white; }
        if (controller.isDeactive3) { lineRenderer3.material = tex[1]; hasEnter3 = false; controller.texWall3 = false; controller.frozenWall3 = false; lineRenderer3.material.color = Color.white; }
        if (controller.isDeactive4) { lineRenderer4.material = tex[1]; hasEnter4 = false; controller.texWall4 = false; controller.frozenWall4 = false; lineRenderer4.material.color = Color.white; }
    }

    public void SetUpLine(Transform[] points)
    {
        this.points = points;
    }

}
