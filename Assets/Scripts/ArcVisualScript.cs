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
    public void Initialize(int colony, Transform[] points, float alpha){
        SetUpLine(points);
        DrawLine();
        ColorLine(colony, alpha);
    }

    /// <summary>
    /// Updates the line points
    /// </summary>
    private void SetUpLine(Transform[] points){
        arcRenderer.positionCount = points.Length;
        this.arcPoints = points;
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
    /// Sets the color and gradient of the line
    /// </summary>
    private void ColorLine(int colony, float alpha){
        Color colonyColor = SelectColonyColor();
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(colonyColor, 0.0f), new GradientColorKey(colonyColor, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        arcRenderer.colorGradient = gradient;
    }

    /// <summary>
    /// Selects the colony color
    /// </summary>
    private Color SelectColonyColor(){
        return Color.red;
    }
}
