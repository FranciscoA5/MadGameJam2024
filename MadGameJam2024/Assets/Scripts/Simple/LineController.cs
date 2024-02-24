using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] private AreaController controller;
    [SerializeField] private LineRenderer lineRenderer1, lineRenderer2, lineRenderer3, lineRenderer4;
    [SerializeField] private Transform[] points;
    [SerializeField] private Material[] tex;

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

        SetValues();

        //lineRenderer.material.SetTexture("SpikeWalls", tex[0]);
    }

    async void SetValues()
    {
        await Task.Delay(controller.delaySpike);

        if (hasEnter1) { lineRenderer1.material = tex[1]; hasEnter1 = false; }
        if (hasEnter2) { lineRenderer2.material = tex[1]; hasEnter2 = false; }
        if (hasEnter3) { lineRenderer3.material = tex[1]; hasEnter3 = false; }
        if (hasEnter4) { lineRenderer4.material = tex[1]; hasEnter4 = false; }
    }

    public void SetUpLine(Transform[] points)
    {
        this.points = points;
    }

}
