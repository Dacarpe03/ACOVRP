using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcVisualScript : MonoBehaviour
{
    private LineRenderer arcRenderer;
    private Transform[] arcPoints;


    /// <summary>
    /// Gets a reference to the line renderer of the arc
    /// </summary>
    private void Awake(){
        arcRenderer = GetComponent<LineRenderer>();
    }


    /// <summary>
    /// Call the different steps of creating the line
    /// </summary>
    public void Initialize(Transform[] points, float alpha){
        SetUpLine(points);
        DrawLine();
        SetAlpha(alpha);
    }

    /// <summary>
    /// Updates the line points
    /// </summary>
    private void SetUpLine(Transform[] points){
        arcRenderer.positionCount = points.Length;
        this.arcPoints = points;
        Debug.Log(arcPoints);
        DrawLine();
    }

    /// <summary>
    /// Draws the line 
    /// </summary>
    private void DrawLine(){
        for(int i=0; i<arcPoints.Length; i++){
            arcRenderer.SetPosition(i, arcPoints[i].position);
        }
    }


    /// <summary>
    /// Sets the gradient of the line
    /// </summary>
    private void SetAlpha(float alpha){
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.red, 0.0f), new GradientColorKey(Color.red, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        arcRenderer.colorGradient = gradient;
    }
}
