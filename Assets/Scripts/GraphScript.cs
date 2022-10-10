using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphScript : MonoBehaviour
{
    private Dictionary<int, NodeScript> nodes = new Dictionary<int, NodeScript>();

    [Header("Arc Prefab")]
    [SerializeField] GameObject arcVisualObject;


    private Dictionary<int[], Arc> arcs = new Dictionary<int[], Arc>();
    private class Arc {

        private float weight;
        private float pheromoneLevel;

        private ArcVisualScript arcVisualObject;

        public Arc(float weight, float pheromoneLevel, ArcVisualScript arcVisual){
            this.weight = weight;
            this.pheromoneLevel = pheromoneLevel;
            this.arcVisualObject = arcVisual;
        }
    }

    public void GetNodes()
    {
        NodeScript[] existingNodes = FindObjectsOfType<NodeScript>();
        foreach (NodeScript n in existingNodes){
            nodes.Add(n.GetId(), n);
        }


        foreach (KeyValuePair<int, NodeScript> kvp in nodes){
            Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value.GetId()}");
        }

        CreateArc(1, 2);
        CreateArc(3, 4);
        CreateArc(0, 6);
    }

    public void CreateArc(int nodeA, int nodeB){
        GameObject newArcVisual = Instantiate(arcVisualObject, this.transform.position, Quaternion.identity);
        Transform[] nodesTransforms = {nodes[nodeA].GetTransform(), nodes[nodeB].GetTransform()};
        newArcVisual.GetComponent<ArcVisualScript>().SetUpLine(nodesTransforms);
        Arc newArc = new Arc(1f, 1f, newArcVisual.GetComponent<ArcVisualScript>());

        int[] arcId = {nodeA, nodeB};
        arcs.Add(arcId, newArc);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
