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

    private void Start() {
        for (int i=0; i<nNodes ; i++){
            Vector2 position = new Vector2(Random.Range(-limit, limit), Random.Range(-limit, limit));
            GameObject newNode = Instantiate(nodeObject, position, Quaternion.identity);
            newNode.GetComponent<NodeScript>().SetId(i);
            newNode.GetComponent<NodeScript>().SetDemand(1);
        }
    }
}
