using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GraphScript : MonoBehaviour
{
    private Dictionary<int, NodeScript> nodes = new Dictionary<int, NodeScript>();

    [Header("Arc Prefab")]
    [SerializeField] GameObject arcVisualObject;

    [Header("Algorithm parameters")]
    private float currentPheromoneWasted;
    private int nAntColonies = 1;
    private int centerNode = 0;

    [Header("Arcs of the graph")]
    private Dictionary<int, Dictionary<string, Arc>> colonies = new Dictionary<int, Dictionary<string, Arc>>();


    /// <summary>
    /// Class containing info of an arc
    /// </summary>
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

    /// <summary>
    /// Gets the created nodes and adds them to the graph
    /// </summary>
    public void GetNodes()
    {
        NodeScript[] existingNodes = FindObjectsOfType<NodeScript>();
        foreach (NodeScript n in existingNodes){
            if (n.IsCenter()){
                this.centerNode = n.GetId();
            }
            nodes.Add(n.GetId(), n);
        }

        CreateArcs();
        UpdateVisualArcs();
        StartDecrease();
    }


    /// <summary>
    /// Creates the arcs between the nodes
    /// </summary>
    private void CreateArcs(){
        for (int i = 0; i < nAntColonies; i++){
            Dictionary<string, Arc> arcs = new Dictionary<string, Arc>();
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
            Debug.Log(colonies[i].Count);
        }
    }


    /// <summary>
    /// Creates an arc of a colony between to nodes
    /// </summary>
    public void CreateArc(int colony, int nodeA, int nodeB){
        string arcId = CreateNodeKey(nodeB, nodeA);
        if (!colonies[colony].ContainsKey(arcId)){
            Debug.Log($"Not contains {arcId[0]}, {arcId[1]}");
            float distance = CalculateDistanceBetweenNodes(colony, nodeA, nodeB);
            float pheromone = nodes[nodeA].GetDemand();
            Arc newArc = new Arc(nodeA, nodeB, distance, pheromone);
            arcId = CreateNodeKey(nodeA, nodeB);
            colonies[colony].Add(arcId, newArc);
        } 
    }

    
    /// <summary>
    /// Calculate distance between nodes
    /// </summary>
    private float CalculateDistanceBetweenNodes(int colony, int nodeA, int nodeB){
        Vector2 nodeAPosition = nodes[nodeA].GetComponent<Transform>().position;
        Vector2 nodeBPosition = nodes[nodeB].GetComponent<Transform>().position;
        float distance = Vector2.Distance(nodeAPosition, nodeBPosition);
        return distance;
    }


    /// <summary>
    /// Updates the graph visually with the arcs
    /// <summary>
    public void UpdateVisualArcs(){
        ArcVisualScript[] allObjects = FindObjectsOfType<ArcVisualScript>();
        foreach(ArcVisualScript arc in allObjects) {
            Destroy(arc.gameObject);
        }
        for (int i = 0; i < nAntColonies; i++){

            Dictionary<string, Arc> arcs = colonies[i];
            float maxPheromone = GetMaxPheromoneInColony(arcs);
            if (maxPheromone == 0){
                //SceneManager.LoadScene(0);
            }
            foreach (Arc arc in arcs.Values){
                GameObject newArcVisual = Instantiate(arcVisualObject, this.transform.position, Quaternion.identity);
                Transform[] nodesTransforms = {nodes[arc.GetNodeA()].GetTransform(), nodes[arc.GetNodeB()].GetTransform()};
                float alpha = arc.GetPheromoneLevel()/maxPheromone;
                newArcVisual.GetComponent<ArcVisualScript>().Initialize(i, nodesTransforms, alpha);
            }
        }
    }


    /// <summary>
    /// Gets the maximum pheromone of the colony
    /// </summary>
    private float GetMaxPheromoneInColony(Dictionary<string, Arc> arcs){
        float maxPheromone = 0f;
        foreach (Arc arc in arcs.Values){
            float currentPheromoneLevel = arc.GetPheromoneLevel();
            if (currentPheromoneLevel > maxPheromone){
                maxPheromone = currentPheromoneLevel;
            }
        }
        return maxPheromone;
    }

    
    /// <summary>
    /// Just to test
    /// </summary>
    IEnumerator WaitForNextStep(){
        DecreasePheromones();
        yield return new WaitForSeconds(0.5f);
        UpdateVisualArcs();
        StartDecrease();
    }


    /// <summary>
    /// Just to test
    /// </summary>
    private void StartDecrease(){
        StartCoroutine(WaitForNextStep());
    }


    /// <summary>
    /// Just to test
    /// </summary>
    private void DecreasePheromones(){
        for (int i = 0; i < nAntColonies; i++){
            Dictionary<string, Arc> arcs = colonies[i];
            foreach (Arc arc in arcs.Values){
                arc.PheromoneVariation(-1);
            }
        }
    }

    /// <summary>
    /// Creates the arc id for the dictionary
    /// <summary>
    private string CreateNodeKey(int nodeA, int nodeB){
        string key = nodeA.ToString() + ',' + nodeB.ToString();
        return key;
    }


    /// <summary>
    /// Algorithm
    /// </summary>
    private void ACOVRP(){
        int currentIteration = 0;
        while (currentIteration < 50){

        }
    }

}
