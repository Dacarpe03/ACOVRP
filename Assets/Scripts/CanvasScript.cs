using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    public void StartSimulation()
    {
        FindObjectOfType<NodeSpawnerScript>().GetData();
    }
}
