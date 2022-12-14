using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GraphScript : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI displaytext;
    [SerializeField] public TextMeshProUGUI antText;
    private Dictionary<int, NodeScript> nodes = new Dictionary<int, NodeScript>();

    [Header("Arc Prefab")]
    [SerializeField] GameObject arcVisualObject;

    [Header("Algorithm parameters")]
    private bool multipleColonies = false;
    private int maxIterations = 100;
    private int antsNumber = 25;
    private int nAntColonies = 1;
    private int centerNode = 0;
    private float vehicleCapacity = 50f;
    private float q0 = 0.2f;
    private float initialPheromone = 0.001f;
    private float beta = 2.3f;
    private float pheromoneDropFactor = 1f;
    private float pheromoneEvaporation = 0.1f;
    private int candidateListSize = 50;

    [Header("Solution parameters")]
    private float minDistance = float.MaxValue;
    private bool showAll = false;

    [Header("Arcs of the graph")]
    private Dictionary<int, Dictionary<string, Arc>> colonies = new Dictionary<int, Dictionary<string, Arc>>();


    public void Initialize(int iterations, int numberAnts, float vehicleCapacity, float q0, float beta, float pheromoneDropFactor, float pheromoneEvaporation, int candidateListSize, bool multipleColonies, bool showAll){
        this.maxIterations = iterations;
        this.antsNumber = numberAnts;
        this.vehicleCapacity = vehicleCapacity;
        this.q0 = q0;
        this.beta = beta;
        this.pheromoneDropFactor = pheromoneDropFactor;
        this.pheromoneEvaporation = pheromoneEvaporation;
        this.candidateListSize = candidateListSize;
        this.multipleColonies = multipleColonies;
        this.showAll = showAll;
    }


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

        public void PheromoneEvaporation(float evaporationRate){
            this.pheromoneLevel = this.pheromoneLevel * (1 - evaporationRate);
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

        CreateArcs(0);
        StartProgram();
    }


    /// <summary>
    /// Creates the arcs between the nodes
    /// </summary>
    private void CreateArcs(int colony){
        Dictionary<string, Arc> arcs = new Dictionary<string, Arc>();
        colonies.Add(colony, arcs);
        foreach (NodeScript n in nodes.Values){
            int currentNodeId = n.GetId();
            foreach (NodeScript next in nodes.Values){
                int nextNodeId = next.GetId();
                if (currentNodeId != nextNodeId){
                    CreateArc(colony, currentNodeId, nextNodeId);
                }
            }
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


    /// <summary>
    /// Displays graphically the best solution
    /// </summary>
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
    public void UpdateVisualArcs(int maxColony){
        ArcVisualScript[] allObjects = FindObjectsOfType<ArcVisualScript>();
        foreach(ArcVisualScript arc in allObjects) {
            Destroy(arc.gameObject);
        }
        for (int i = 0; i < maxColony+1; i++){
            Dictionary<string, Arc> arcs = colonies[i];
            float maxPheromone = GetMaxPheromoneInColony(arcs);
            foreach (Arc arc in arcs.Values){
                GameObject newArcVisual = Instantiate(arcVisualObject, this.transform.position, Quaternion.identity);
                Transform[] nodesTransforms = {nodes[arc.GetNodeA()].GetTransform(), nodes[arc.GetNodeB()].GetTransform()};
                float alpha = arc.GetPheromoneLevel()/maxPheromone;
                newArcVisual.GetComponent<ArcVisualScript>().Initialize(i, nodesTransforms, alpha);
            }
        }
    }


    public void DestroyAllArcs(){
        ArcVisualScript[] allObjects = FindObjectsOfType<ArcVisualScript>();
        foreach(ArcVisualScript arc in allObjects) {
            Destroy(arc.gameObject);
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

    private void StartProgram(){
        StartCoroutine(ACOVRP());
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
    private IEnumerator ACOVRP(){
        
        // Initialize some parameters for the algorithm
        List<int> bestSolution = new List<int>();

        float bestDistance = float.MaxValue;
        int currentIteration = 0;
        
        int maxColony = 0;
        while (currentIteration < maxIterations){
            List<List<int>> solutions = new List<List<int>>();
            List<float> solutionDistances = new List<float>();
            for (int i=0; i<antsNumber; i++){
                if (showAll){
                    string antTe = "Ant " + (i+1).ToString();
                    antText.text = antTe;
                }
                // Create new ant
                Ant currentAnt = new Ant(this.vehicleCapacity);
                bool[] visitedNodes = new bool[nodes.Count];
                // Add the depot node as the start of solution
                currentAnt.AddSolutionNode(centerNode, 0f);
            
                // Set depot node as visited
                visitedNodes[centerNode] = true;

                // The pheromones of the colony to follow
                int currentColony = 0;
                int currentNode = centerNode;
                float currentDistance = 0;

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
                    currentDistance += CalculateDistanceBetweenNodes(currentColony, currentNode, nextNode);

                    visitedNodes[nextNode] = true;

                    currentNode = nextNode;
                    if (currentAnt.HasMaxCapacity()){
                            currentAnt.AddSolutionNode(centerNode, 0f);
                            currentDistance += CalculateDistanceBetweenNodes(currentColony, currentNode, centerNode);

                            currentAnt.ResetCapacity();
                            currentNode = centerNode;

                            if (multipleColonies){
                                currentColony += 1;
                                if (!colonies.ContainsKey(currentColony)){
                                    CreateArcs(currentColony);
                                }
                            }
                    }
                }

                if (currentNode != centerNode){
                    currentAnt.AddSolutionNode(centerNode, 0f);
                    currentDistance += CalculateDistanceBetweenNodes(currentColony, currentNode, centerNode);
                }

                solutionDistances.Add(currentDistance);
                solutions.Add(currentAnt.GetSolution());
                if (currentDistance < bestDistance){
                    bestSolution = currentAnt.GetSolution();
                    bestDistance = currentDistance;
                }

                
                maxColony = currentColony;
                if (showAll){
                    DestroyAllArcs();
                    UpdateVisualArcs(maxColony);
                    ShowBestSolution(currentAnt.GetSolution());
                    yield return new WaitForSeconds(0.1f);
                }


            }
            
            UpdateEvaporationInTrails();
            for (int i=0; i<solutions.Count; i++){
                UpdatePheromoneTrails(solutions[i], solutionDistances[i]);
            }
            UpdatePheromoneTrails(bestSolution, bestDistance);

            string textMsg = "Iteration " + currentIteration + "\nBest distance: " + bestDistance;
            Debug.Log(textMsg);
            displaytext.text = textMsg;
            currentIteration += 1;
            
            DestroyAllArcs();
            UpdateVisualArcs(maxColony);
            yield return new WaitForSeconds(1f);
            if (showAll){
                antText.text = "Best solution";
            }
            ShowBestSolution(bestSolution);
            yield return new WaitForSeconds(1f);
        }

        
        UpdateVisualArcs(maxColony);
        ShowBestSolution(bestSolution);
        string msg = "Best distance: " + bestDistance + "      Resetting...";
        displaytext.text = msg;
        yield return new WaitForSeconds(3f);
        Reset();
    }

    public void Reset(){
        SceneManager.LoadScene(0);
    }

    
    /// <summary>
    /// Updates the arcs based on the best solution found
    /// </summary>
    private void UpdateBestCourse(){}


    /// <summary>
    /// Updates the pheromones in the trails
    /// <summary>
    private void UpdatePheromoneTrails(List<int> solution, float solutionDistance){
        int nodeA = solution[0];
        int nodeB = 0;
        int currentColony = 0;
        for (int i=1; i<solution.Count; i++){
            nodeB = solution[i];
            string arcId = GetArcId(currentColony, nodeA, nodeB);
            colonies[currentColony][arcId].PheromoneVariation(pheromoneDropFactor/solutionDistance);
            nodeA = nodeB;
            if (nodeA == centerNode && multipleColonies){
                currentColony += 1;
            }
        }
    }

    
    /// <summary>
    /// Evaporate trails
    /// </summary>
    private void UpdateEvaporationInTrails(){
        foreach (Dictionary<string, Arc> colony in colonies.Values){
            foreach(Arc arc in colony.Values){
                arc.PheromoneEvaporation(pheromoneEvaporation);
            }
        }
    }


    /// <summary>
    /// Tells if all the nodes have been visited
    /// </summary>
    private bool AllVisited(bool[] nodesVisited){
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
        Dictionary<string, Arc> colonyArcs = colonies[currentColony];
        List<string> nodeArcs = GetNodeArcs(currentNode, colonyArcs);
        List<string> arcIdsToConsider = FilterByVisitedNodes(currentNode, visitedNodes, nodeArcs);
        List<string> arcCandidates = OrderCandidates(arcIdsToConsider, currentColony);
        int selectedNode = GetMaxPheromone(currentNode, arcCandidates, colonyArcs);
        return selectedNode;
    }


    /// <summary>
    /// Follows a random paht
    /// </summary>
    private int FollowRandomPath(int currentColony, int currentNode, bool[] visitedNodes){
        Dictionary<string, Arc> colonyArcs = colonies[currentColony];
        List<string> nodeArcs = GetNodeArcs(currentNode, colonyArcs);
        List<string> arcIdsToConsider = FilterByVisitedNodes(currentNode, visitedNodes, nodeArcs);
        List<string> arcCandidates = OrderCandidates(arcIdsToConsider, currentColony);
        int selectedNode = GetNodeFromProbabilityDistribution(currentNode, arcCandidates, colonyArcs);
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
            int otherId = GetOtherNodeId(arcId, currentNode);
            if (!visitedNodes[otherId]){
                filteredArcs.Add(arcId);
            }
        }
        return filteredArcs;
    }

    
    /// <summary>
    /// Filter the nodes by the candidate list size, order them by weight
    /// </summary>
    private List<string> OrderCandidates(List<string> nodeArcs, int currentColony){
        List<string> orderedArcs = new List<string>();
        int max = Mathf.Min(candidateListSize, nodeArcs.Count);
        for (int i=0; i<max; i++){
            float localMin = float.MaxValue;
            string minArc = "";
            foreach(string arcId in nodeArcs){
                if (!orderedArcs.Contains(arcId)){
                    float weight = colonies[currentColony][arcId].GetWeight();
                    if(weight <= localMin){
                        minArc = arcId;
                        localMin = weight;
                    }
                }
            }
            orderedArcs.Add(minArc);
        }
        return orderedArcs;
    }


    /// <summary>
    /// Gets the max pheromone node
    /// </summary>
    private int GetMaxPheromone(int currentNode, List<string> arcIds, Dictionary<string, Arc> colonyArcs){
        float maxValue = -1f;
        string bestArc = "";
        foreach (string arcId in arcIds){
            float arcFitness = CalculateArcFitness(arcId, colonyArcs);
            if (arcFitness > maxValue){
                maxValue = arcFitness;
                bestArc = arcId;
            }
        }
        
        int bestNode = GetOtherNodeId(bestArc, currentNode);
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

        while (i < arcIds.Count && !found){
            float arcFitness = CalculateArcFitness(arcIds[i], colonyArcs);
            float probabilityToPickArc = arcFitness/totalProbability;
            cummulativeProb += probabilityToPickArc;
            if (cummulativeProb > r){
                found = true;
            }else{
                i++;
            }
        }
        if(i >= arcIds.Count){
            i--;
        }
        int selectedNode = GetOtherNodeId(arcIds[i], currentNode);
        return selectedNode;
    }


    /// <summary>
    /// Calculates the fitness of the arc based on the formula of the paper
    /// </summary>
    private float CalculateArcFitness(string arcId, Dictionary<string, Arc> colonyArcs){
        float pheromone = colonyArcs[arcId].GetPheromoneLevel();
        float distance = 1/colonyArcs[arcId].GetWeight();
        return pheromone * Mathf.Pow(distance, beta);
    }


    /// <summary>
    /// Get other node id of an arc given one
    /// </summary>
    private int GetOtherNodeId(string arcId, int node){
        string[] parsedId = arcId.Split(",");
        int otherId = -1;
        if (int.Parse(parsedId[0]) != node){
            otherId = int.Parse(parsedId[0]);
        }else{
            otherId = int.Parse(parsedId[1]);
        }
        return otherId;
    }


    /// <summary>
    /// Build the arc id from two nodes
    /// </summary>
    private string GetArcId(int colony, int nodeA, int nodeB){
        string strNodeAId = nodeA.ToString();
        string strNodeBId = nodeB.ToString();
        string id1 = strNodeAId + "," + strNodeBId;
        string id2 = strNodeBId + "," + strNodeAId;
        if (colonies[colony].ContainsKey(id1)){
            return id1;
        }else{
            return id2;
        }
    }
}
