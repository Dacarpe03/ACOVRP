using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphScript : MonoBehaviour
{
    private Dictionary<int, NodeScript> nodes = new Dictionary<int, NodeScript>();

    [Header("Arc Prefab")]
    [SerializeField] GameObject arcVisualObject;

    private float currentPheromoneWasted;
    private int nAntColonies = 1;

    private Dictionary<int, Dictionary<int[], Arc>> colonies = new Dictionary<int, Dictionary<int[], Arc>>();
    private List<ArcVisualScript> arcVisuals = new List<ArcVisualScript>();

    private class Arc {
        private int nodeA;
        private int nodeB;
        private float weight;
        private float pheromoneLevel {get; set;}

        public Arc(int nodeA, int nodeB, float weight, float pheromoneLevel){
            this.nodeA = nodeA;
            this.nodeB = nodeB;
            this.weight = weight;
            this.pheromoneLevel = pheromoneLevel;
        }

        public float GetPheromoneLevel(){
            return this.pheromoneLevel;
        }

        public int GetNodeA(){
            return this.nodeA;
        }

        public int GetNodeB(){
            return this.nodeB;
        }

        public void PheromoneVariation(float variation){
            this.pheromoneLevel += variation;
            if (this.pheromoneLevel < 0){
                pheromoneLevel = 0f;
            }
        }
    }

    public void GetNodes()
    {
        NodeScript[] existingNodes = FindObjectsOfType<NodeScript>();
        foreach (NodeScript n in existingNodes){
            nodes.Add(n.GetId(), n);
        }

        CreateArcs();
        UpdateVisualArcs();
        StartDecrease();
    }


    private void CreateArcs(){
        for (int i = 0; i < nAntColonies; i++){
            Dictionary<int[], Arc> arcs = new Dictionary<int[], Arc>();
            colonies.Add(i, arcs);

            foreach (NodeScript n in nodes.Values){
                int currentNodeId = n.GetId();
                foreach (NodeScript next in nodes.Values){
                    int nextNodeId = next.GetId();
                    if (currentNodeId != nextNodeId){
                        CreateArc(i, currentNodeId, nextNodeId);
                    }
                }
            }
        }
    }

    public void CreateArc(int colony, int nodeA, int nodeB){
        Arc newArc = new Arc(nodeA, nodeB, 1f, (nodeA * nodeB)+1);
        int[] arcId = {nodeA, nodeB};
        colonies[colony].Add(arcId, newArc);
        Debug.Log($"{colonies[colony][arcId]}");
    }

    public void UpdateVisualArcs(){
        ArcVisualScript[] allObjects = FindObjectsOfType<ArcVisualScript>();
        foreach(ArcVisualScript arc in allObjects) {
            Destroy(arc.gameObject);
        }
        for (int i = 0; i < nAntColonies; i++){

            Dictionary<int[], Arc> arcs = colonies[i];
            float maxPheromone = GetMaxPheromoneInColony(arcs);
            foreach (Arc arc in arcs.Values){
                GameObject newArcVisual = Instantiate(arcVisualObject, this.transform.position, Quaternion.identity);
                Transform[] nodesTransforms = {nodes[arc.GetNodeA()].GetTransform(), nodes[arc.GetNodeB()].GetTransform()};
                float alpha = arc.GetPheromoneLevel()/maxPheromone;
                newArcVisual.GetComponent<ArcVisualScript>().Initialize(nodesTransforms, alpha);
            }
        }
    }


    private float GetMaxPheromoneInColony(Dictionary<int[], Arc> arcs){
        float maxPheromone = 0f;
        foreach (Arc arc in arcs.Values){
            float currentPheromoneLevel = arc.GetPheromoneLevel();
            if (currentPheromoneLevel > maxPheromone){
                maxPheromone = currentPheromoneLevel;
            }
        }
        return maxPheromone;
    }

    
    IEnumerator WaitForNextStep(){
        DecreasePheromones();
        yield return new WaitForSeconds(1);
        UpdateVisualArcs();
        StartDecrease();
    }

    private void StartDecrease(){
        StartCoroutine(WaitForNextStep());
    }

    private void DecreasePheromones(){
        for (int i = 0; i < nAntColonies; i++){
            Dictionary<int[], Arc> arcs = colonies[i];
            foreach (Arc arc in arcs.Values){
                arc.PheromoneVariation(-1);
            }
        }
    }

}
