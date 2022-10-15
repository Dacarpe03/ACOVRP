using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GraphScript : MonoBehaviour
{
    private Dictionary<int, NodeScript> nodes = new Dictionary<int, NodeScript>();

    [Header("Arc Prefab")]
    [SerializeField] GameObject arcVisualObject;

    [Header("Algorithm parameters")]
    private int maxIterations = 1;
    private int iterationBlock = 25;
    private float currentPheromoneWasted;
    private int nAntColonies = 1;
    private int centerNode = 0;
    private float vehicleCapacity = 101f;
    private float q0 = 0f;
    private float initialPheromone = 0.1f;
    private float beta = 2.3f;

    [Header("Solution parameters")]
    private float minDistance = float.MaxValue;

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

        public float GetWeight(){
            return this.weight;
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
    /// Ant class that builds a solution
    /// </summary>
    private class Ant {
        private List<int> solution;
        private float maxCapacity;
        private float currentCapacity;
        
        public Ant(float maxCapacity){
            this.solution = new List<int>();
            this.maxCapacity = maxCapacity;
            this.currentCapacity = 0f;
        }

        public bool HasMaxCapacity(){
            return this.currentCapacity >= maxCapacity;
        }

        public List<int> GetSolution(){
            return this.solution;
        }

        public void AddSolutionNode(int newNode, float nodeDemand){
            solution.Add(newNode);
            currentCapacity += nodeDemand;
        }

        public void ResetCapacity(){
            this.currentCapacity = 0f;
        }

    }


    /// <summary>
    /// Sets the capacity of the vehicles
    /// </summary>
    public void SetCapacity(float capacity){
        this.vehicleCapacity = capacity;
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
            float distance = CalculateDistanceBetweenNodes(colony, nodeA, nodeB);
            float pheromone = nodes[nodeA].GetDemand();
            Arc newArc = new Arc(nodeA, nodeB, distance, initialPheromone);
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


    private void ShowBestSolution(List<int> solution){
        Transform[] nodesTransforms = new Transform[solution.Count];
        int i=0;
        foreach (int nodeId in solution){
            nodesTransforms[i] = nodes[nodeId].GetTransform();
            i++;
        }
        GameObject newArcVisual = Instantiate(arcVisualObject, this.transform.position, Quaternion.identity);
        newArcVisual.GetComponent<ArcVisualScript>().Solution(i, nodesTransforms, 1f);
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
        ACOVRP();
        //StartCoroutine(WaitForNextStep());
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
        // Initialize some parameters for the algorithm
        List<int> bestSolution = new List<int>();
        bool[] visitedNodes = new bool[nodes.Count];
        int currentIteration = 0;
        int subIterations = 0;

        while (currentIteration < maxIterations){
            Debug.Log("Nueva iteracion");
            // Check if need for global pheromone update
            if (subIterations >= iterationBlock){
                UpdateBestCourse();
                subIterations = 0;
            }

            // Create new ant
            Ant currentAnt = new Ant(vehicleCapacity);

            // Add the depot node as the start of solution
            currentAnt.AddSolutionNode(centerNode, 0f);

            // Set depot node as visited
            visitedNodes[centerNode] = true;

            // The pheromones of the colony to follow
            int currentColony = 0;
            int currentNode = centerNode;

            // Loop to build a solution
            while(!AllVisited(visitedNodes)){
                float random = Random.Range(0f, 1f);
                int nextNode = 0;
                if (random <= q0){
                    nextNode = FollowPheromones(currentColony, currentNode, visitedNodes);
                }else{
                    nextNode = FollowRandomPath(currentColony, currentNode, visitedNodes);
                }

                float demand = nodes[nextNode].GetDemand();
                currentAnt.AddSolutionNode(nextNode, demand);

                visitedNodes[nextNode] = true;
                currentNode = nextNode;

                if (currentAnt.HasMaxCapacity()){
                        currentAnt.AddSolutionNode(centerNode, 0f);
                        currentAnt.ResetCapacity();
                        currentNode = centerNode;
                }
            }
            currentAnt.AddSolutionNode(centerNode, 0f);
            bestSolution = currentAnt.GetSolution();
            currentIteration += 1;
        }

        ShowBestSolution(bestSolution);
    }


    /// <summary>
    /// Updates the arcs based on the best solution found
    /// </summary>
    private void UpdateBestCourse(){}


    /// <summary>
    /// Tells if all the nodes have been visited
    /// </summary>
    private bool AllVisited(bool[] nodesVisited){
        Debug.Log("All visited");
        for (int i=0; i<nodesVisited.Length; i++){
            if (!nodesVisited[i]){
                return false;
            }
        }
        return true;
    }


    /// <summary>
    /// Follows the pheromones and returns the node where the ant will move
    /// </summary>
    private int FollowPheromones(int currentColony, int currentNode, bool[] visitedNodes){
        Debug.Log("Follow Phermones");
        Dictionary<string, Arc> colonyArcs = colonies[currentColony];
        List<string> nodeArcs = GetNodeArcs(currentNode, colonyArcs);
        List<string> arcIdsToConsider = FilterByVisitedNodes(currentNode, visitedNodes, nodeArcs);
        int selectedNode = GetMaxPheromone(currentNode, arcIdsToConsider, colonyArcs);
        return selectedNode;
    }


    /// <summary>
    /// Follows a random paht
    /// </summary>
    private int FollowRandomPath(int currentColony, int currentNode, bool[] visitedNodes){
        Dictionary<string, Arc> colonyArcs = colonies[currentColony];
        List<string> nodeArcs = GetNodeArcs(currentNode, colonyArcs);
        List<string> arcIdsToConsider = FilterByVisitedNodes(currentNode, visitedNodes, nodeArcs);
        int selectedNode = GetNodeFromProbabilityDistribution(currentNode, arcIdsToConsider, colonyArcs);
        return selectedNode;
    }


    /// <summary>
    /// Returns the keys of the arcs that are connected to a node
    /// </summary>
    private List<string> GetNodeArcs(int node, Dictionary<string, Arc> colonyArcs){
        string strNodeId = node.ToString();
        string id1 = ","+strNodeId;
        string id2 = strNodeId + ",";
        List<string> nodeArcs = colonyArcs.Keys.Where(arcId => arcId.Contains(id1) || arcId.Contains(id2)).ToList<string>();
        return nodeArcs;
    }


    /// <summary>
    /// Filters the visited nodes
    /// </summary>
    private List<string> FilterByVisitedNodes(int currentNode, bool[] visitedNodes, List<string>nodeArcs){
        List<string> filteredArcs = new List<string>();
        foreach(string arcId in nodeArcs){
            int otherId = GetOtherArcId(arcId, currentNode);
            if (!visitedNodes[otherId]){
                filteredArcs.Add(arcId);
            }
        }
        return filteredArcs;
    }

    
    /// <summary>
    /// Gets the max pheromone node
    /// </summary>
    private int GetMaxPheromone(int currentNode, List<string> arcIds, Dictionary<string, Arc> colonyArcs){
        float maxValue = 0f;
        string bestArc = "";
        foreach (string arcId in arcIds){
            float arcFitness = CalculateArcFitness(arcId, colonyArcs);
            if (arcFitness > maxValue){
                maxValue = arcFitness;
                bestArc = arcId;
            }
        }
        
        int bestNode = GetOtherArcId(bestArc, currentNode);
        return bestNode;
    }
    

    /// <summary>
    /// Gets a node from a random probability distribution
    /// </summary>
    private int GetNodeFromProbabilityDistribution(int currentNode, List<string> arcIds, Dictionary<string, Arc> colonyArcs){
        float totalProbability = 0f;
        foreach(string arcId in arcIds){
            float arcFitness = CalculateArcFitness(arcId, colonyArcs);
            totalProbability += arcFitness; 
        }

        
        int i = 0;
        float r = Random.Range(0f, 1f);
        bool found = false;
        float cummulativeProb = 0f;
        while (i < arcIds.Count-1 && !found){
            Debug.Log(i);
            float arcFitness = CalculateArcFitness(arcIds[i], colonyArcs);
            cummulativeProb += arcFitness;
            float probabilityToPickArc = arcFitness/totalProbability;
            Debug.Log(probabilityToPickArc);
            if (probabilityToPickArc < r){
                found = true;
            }
            i++;
        }
        int selectedNode = GetOtherArcId(arcIds[i], currentNode);
        return selectedNode;
    }


    /// <summary>
    /// Calculates the fitness of the arc based on the formula of the paper
    /// </summary>
    private float CalculateArcFitness(string arcId, Dictionary<string, Arc> colonyArcs){
        float pheromone = colonyArcs[arcId].GetPheromoneLevel();
        float distance = colonyArcs[arcId].GetWeight();
        return pheromone * Mathf.Pow(distance, beta);
    }


    /// <summary>
    /// Get other node id of an arc given one
    /// </summary>
    private int GetOtherArcId(string arcId, int node){
        Debug.Log("I get here");
        string[] parsedId = arcId.Split(",");
        Debug.Log(arcId);
        int otherId = -1;
        if (int.Parse(parsedId[0]) != node){
            otherId = int.Parse(parsedId[0]);
        }else{
            otherId = int.Parse(parsedId[1]);
        }
        return otherId;
    }
}
