using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSpawnerScript : MonoBehaviour
{
    [Header("Number of nodes to spawn")]
    [SerializeField] private int nNodes = 5;
    [SerializeField] private float limit = 50f;

    [Header("Node Prefab")]
    [SerializeField] GameObject nodeObject;


    /// <summary>
    /// Spawns a series of nodes with random positions
    /// </summary>
    private void SpawnRandomly() {
        for (int i=0; i<nNodes ; i++){
            Vector2 position = new Vector2(Random.Range(-limit, limit), Random.Range(-limit, limit));
            GameObject newNode = Instantiate(nodeObject, position, Quaternion.identity);
            newNode.GetComponent<NodeScript>().SetId(i);
            newNode.GetComponent<NodeScript>().SetDemand(1);
        }
        FindObjectOfType<GraphScript>().GetNodes();
    }


    /// <summary>
    /// Gets a the configuration parameters of the algorithm
    /// </summary>
    public void GetData(){

        Destroy(FindObjectOfType<CanvasScript>().gameObject);

        SpawnRandomly();
    }


    /// <summary>
    /// Updates the number of nodes
    /// </summary>
    public void ReadNumberNodes(string nNodesStr){
        if (nNodesStr != ""){
            nNodes = int.Parse(nNodesStr);
        }
    }
}
