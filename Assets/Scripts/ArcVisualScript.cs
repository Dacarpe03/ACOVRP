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
    /// Updates the line points
    /// </summary>
    public void SetUpLine(Transform[] points){
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
}
