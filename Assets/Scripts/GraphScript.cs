using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphScript : MonoBehaviour
{
    private Dictionary<int, NodeScript> nodes = new Dictionary<int, NodeScript>();

    [Header("Arc Prefab")]
    [SerializeField] GameObject arcVisualObject;


    private Dictionary<int[], Arc> arcs = new Dictionary<int[], Arc>();
    private List<ArcVisualScript> arcVisuals = new List<ArcVisualScript>();

    private class Arc {
        private float weight;
        private float pheromoneLevel;

        public Arc(float weight, float pheromoneLevel){
            this.weight = weight;
            this.pheromoneLevel = pheromoneLevel;
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

        CreateArc(1, 1, 2, false);
        CreateArc(2, 3, 4, false);
        CreateArc(3, 0, 6, true);
    }

    public void CreateArc(int colony, int nodeA, int nodeB, bool col){
        GameObject newArcVisual = Instantiate(arcVisualObject, this.transform.position, Quaternion.identity);
        Transform[] nodesTransforms = {nodes[nodeA].GetTransform(), nodes[nodeB].GetTransform()};
        newArcVisual.GetComponent<ArcVisualScript>().SetUpLine(nodesTransforms);
        if (col){
            float alpha = 0.2f;
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(Color.red, 0.0f), new GradientColorKey(Color.red, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );
            newArcVisual.GetComponent<LineRenderer>().colorGradient = gradient;
        }
        arcVisuals.Add(newArcVisual.GetComponent<ArcVisualScript>());

        Arc newArc = new Arc(1f, 1f);

        int[] arcId = {colony, nodeA, nodeB};
        arcs.Add(arcId, newArc);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
