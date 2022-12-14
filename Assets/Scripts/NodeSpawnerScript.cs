using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeSpawnerScript : MonoBehaviour
{
    [Header("Number of nodes to spawn")]
    [SerializeField] private int nNodes = 20;
    [SerializeField] private float limit = 50f;
    private float capacityOne = 160f;
    private float capacityThree = 200f;
    private float capacityFour = 200f;

    [Header("Node Prefab")]
    [SerializeField] GameObject nodeObject;

    private int problem;

    [Header("Algorithm parameters")]
    private int iterations = 100;
    private int numberAnts = 25;
    private float vehicleCapacity = 1f;
    private float q0 = 0.9f;
    private float beta = 2.3f;
    private float pheromoneDropFactor = 1f;
    private float pheromoneEvaporation = 0.1f;
    private int candidateListSize = 50;
    private bool multipleColonies = false;
    private bool showAll = false;

    /// <summary>
    /// Spawns a series of nodes with random positions
    /// </summary>
    private void SpawnRandomly() {
        bool center = false;
        for (int i=0; i<nNodes ; i++){
            Vector2 position = new Vector2(Random.Range(0, limit), Random.Range(0, limit));
            GameObject newNode = Instantiate(nodeObject, position, Quaternion.identity);
            newNode.GetComponent<NodeScript>().SetId(i+1);
            newNode.GetComponent<NodeScript>().SetDemand(10);
            if (!center){
                newNode.GetComponent<NodeScript>().SetCenter();
                newNode.GetComponent<NodeScript>().SetDemand(0);
                center = true;
            }
        }

        FindObjectOfType<GraphScript>().Initialize(iterations, numberAnts, vehicleCapacity, q0, beta, pheromoneDropFactor, pheromoneEvaporation, candidateListSize, multipleColonies, showAll);
        FindObjectOfType<GraphScript>().GetNodes();
    }


    /// <summary>
    /// Initialize graph
    /// </summary>
    private void InitializeGraph(int problem){
        if (problem == 1 && vehicleCapacity == 1f){
            vehicleCapacity = capacityOne;
        }
        else if (problem == 3 && vehicleCapacity == 1f){
            vehicleCapacity = capacityThree;
        }
        else if (problem == 4 && vehicleCapacity == 1f){
            vehicleCapacity = capacityFour;
        }

        FindObjectOfType<GraphScript>().Initialize(iterations, numberAnts, vehicleCapacity, q0, beta, pheromoneDropFactor, pheromoneEvaporation, candidateListSize, multipleColonies, showAll);
        FindObjectOfType<GraphScript>().GetNodes();
    }

    /// <summary>
    /// Updates the number of nodes
    /// </summary>
    public void ReadProblem(int p){

    }

    /// <summary>
    /// Gets a the configuration parameters of the algorithm
    /// </summary>
    public void GetData(int problem){

        Destroy(FindObjectOfType<CanvasScript>().gameObject);

        if (problem == 1){
            SpawnC1();
        } else if (problem == 3){
            SpawnC3();
        } else if (problem == 4){
            SpawnC4();
        } else{
            SpawnRandomly();
        }
    }

    /// <summary>
    /// Updates the number of iterations
    /// </summary>
    public void ReadNumberIterations(string niterations){
        if (niterations != ""){
            iterations = int.Parse(niterations);
        }
    }

    /// <summary>
    /// Updates the number of ants
    /// </summary>
    public void ReadNumberAnts(string nAnts){
        if (nAnts != ""){
            numberAnts = int.Parse(nAnts);
        }
    }

    /// <summary>
    /// Updates the size of candidate list
    /// </summary>
    public void ReadNCandidates(string nCandidates){
        if (nCandidates != ""){
            candidateListSize = int.Parse(nCandidates);
        }
    }

    /// <summary>
    /// Updates the size of candidate list
    /// </summary>
    public void Readq0(string strQ0){
        if (strQ0 != ""){
            q0 = float.Parse(strQ0);
        }
    }

    /// <summary>
    /// Updates the size of candidate list
    /// </summary>
    public void ReadCapacity(string strCapacity){
        if (strCapacity != ""){
            vehicleCapacity = float.Parse(strCapacity);
        }
    }

    /// <summary>
    /// Updates the size of candidate list
    /// </summary>
    public void ReadBeta(string strBeta){
        if (strBeta != ""){
            beta = float.Parse(strBeta);
        }
    }

    /// <summary>
    /// Updates the size of candidate list
    /// </summary>
    public void ReadPheromoneDrop(string strPD){
        if (strPD != ""){
            pheromoneDropFactor = float.Parse(strPD);
        }
    }

    /// <summary>
    /// Updates the size of candidate list
    /// </summary>
    public void ReadEvaporation(string strEvaporation){
        if (strEvaporation != ""){
            pheromoneEvaporation = float.Parse(strEvaporation);
        }
    }

    public void ToggleValueChanged(bool change)
    {
        multipleColonies = !multipleColonies;
    }

    public void ToggleShowSolutions(bool change){
        showAll = !showAll;
    }



    public void SpawnC1(){
        Vector2 position = new Vector2(0f, 0f);
        GameObject newNode;

        position = new Vector2(37.0f, 52.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(1); 
        newNode.GetComponent<NodeScript>().SetDemand(7.0f);

        position = new Vector2(49.0f, 49.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(2); 
        newNode.GetComponent<NodeScript>().SetDemand(30.0f);

        position = new Vector2(52.0f, 64.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(3); 
        newNode.GetComponent<NodeScript>().SetDemand(16.0f);

        position = new Vector2(20.0f, 26.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(4); 
        newNode.GetComponent<NodeScript>().SetDemand(9.0f);

        position = new Vector2(40.0f, 30.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(5); 
        newNode.GetComponent<NodeScript>().SetDemand(21.0f);

        position = new Vector2(21.0f, 47.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(6); 
        newNode.GetComponent<NodeScript>().SetDemand(15.0f);

        position = new Vector2(17.0f, 63.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(7); 
        newNode.GetComponent<NodeScript>().SetDemand(19.0f);

        position = new Vector2(31.0f, 62.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(8); 
        newNode.GetComponent<NodeScript>().SetDemand(23.0f);

        position = new Vector2(52.0f, 33.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(9); 
        newNode.GetComponent<NodeScript>().SetDemand(11.0f);

        position = new Vector2(51.0f, 21.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(10); 
        newNode.GetComponent<NodeScript>().SetDemand(5.0f);

        position = new Vector2(42.0f, 41.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(11); 
        newNode.GetComponent<NodeScript>().SetDemand(19.0f);

        position = new Vector2(31.0f, 32.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(12); 
        newNode.GetComponent<NodeScript>().SetDemand(29.0f);

        position = new Vector2(5.0f, 25.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(13); 
        newNode.GetComponent<NodeScript>().SetDemand(23.0f);

        position = new Vector2(12.0f, 42.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(14); 
        newNode.GetComponent<NodeScript>().SetDemand(21.0f);

        position = new Vector2(36.0f, 16.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(15); 
        newNode.GetComponent<NodeScript>().SetDemand(10.0f);

        position = new Vector2(52.0f, 41.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(16); 
        newNode.GetComponent<NodeScript>().SetDemand(15.0f);

        position = new Vector2(27.0f, 23.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(17); 
        newNode.GetComponent<NodeScript>().SetDemand(3.0f);

        position = new Vector2(17.0f, 33.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(18); 
        newNode.GetComponent<NodeScript>().SetDemand(41.0f);

        position = new Vector2(13.0f, 13.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(19); 
        newNode.GetComponent<NodeScript>().SetDemand(9.0f);

        position = new Vector2(57.0f, 58.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(20); 
        newNode.GetComponent<NodeScript>().SetDemand(28.0f);

        position = new Vector2(62.0f, 42.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(21); 
        newNode.GetComponent<NodeScript>().SetDemand(8.0f);

        position = new Vector2(42.0f, 57.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(22); 
        newNode.GetComponent<NodeScript>().SetDemand(8.0f);

        position = new Vector2(16.0f, 57.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(23); 
        newNode.GetComponent<NodeScript>().SetDemand(16.0f);

        position = new Vector2(8.0f, 52.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(24); 
        newNode.GetComponent<NodeScript>().SetDemand(10.0f);

        position = new Vector2(7.0f, 38.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(25); 
        newNode.GetComponent<NodeScript>().SetDemand(28.0f);

        position = new Vector2(27.0f, 68.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(26); 
        newNode.GetComponent<NodeScript>().SetDemand(7.0f);

        position = new Vector2(30.0f, 48.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(27); 
        newNode.GetComponent<NodeScript>().SetDemand(15.0f);

        position = new Vector2(43.0f, 67.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(28); 
        newNode.GetComponent<NodeScript>().SetDemand(14.0f);

        position = new Vector2(58.0f, 48.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(29); 
        newNode.GetComponent<NodeScript>().SetDemand(6.0f);

        position = new Vector2(58.0f, 27.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(30); 
        newNode.GetComponent<NodeScript>().SetDemand(19.0f);

        position = new Vector2(37.0f, 69.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(31); 
        newNode.GetComponent<NodeScript>().SetDemand(11.0f);

        position = new Vector2(38.0f, 46.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(32); 
        newNode.GetComponent<NodeScript>().SetDemand(12.0f);

        position = new Vector2(46.0f, 10.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(33); 
        newNode.GetComponent<NodeScript>().SetDemand(23.0f);

        position = new Vector2(61.0f, 33.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(34); 
        newNode.GetComponent<NodeScript>().SetDemand(26.0f);

        position = new Vector2(62.0f, 63.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(35); 
        newNode.GetComponent<NodeScript>().SetDemand(17.0f);

        position = new Vector2(63.0f, 69.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(36); 
        newNode.GetComponent<NodeScript>().SetDemand(6.0f);

        position = new Vector2(32.0f, 22.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(37); 
        newNode.GetComponent<NodeScript>().SetDemand(9.0f);

        position = new Vector2(45.0f, 35.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(38); 
        newNode.GetComponent<NodeScript>().SetDemand(15.0f);

        position = new Vector2(59.0f, 15.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(39); 
        newNode.GetComponent<NodeScript>().SetDemand(14.0f);

        position = new Vector2(5.0f, 6.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(40); 
        newNode.GetComponent<NodeScript>().SetDemand(7.0f);

        position = new Vector2(10.0f, 17.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(41); 
        newNode.GetComponent<NodeScript>().SetDemand(27.0f);

        position = new Vector2(21.0f, 10.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(42); 
        newNode.GetComponent<NodeScript>().SetDemand(13.0f);

        position = new Vector2(5.0f, 64.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(43); 
        newNode.GetComponent<NodeScript>().SetDemand(11.0f);

        position = new Vector2(30.0f, 15.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(44); 
        newNode.GetComponent<NodeScript>().SetDemand(16.0f);

        position = new Vector2(39.0f, 10.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(45); 
        newNode.GetComponent<NodeScript>().SetDemand(10.0f);

        position = new Vector2(32.0f, 39.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(46); 
        newNode.GetComponent<NodeScript>().SetDemand(5.0f);

        position = new Vector2(25.0f, 32.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(47); 
        newNode.GetComponent<NodeScript>().SetDemand(25.0f);

        position = new Vector2(25.0f, 55.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(48); 
        newNode.GetComponent<NodeScript>().SetDemand(17.0f);

        position = new Vector2(48.0f, 28.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(49); 
        newNode.GetComponent<NodeScript>().SetDemand(18.0f);

        position = new Vector2(56.0f, 37.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(50); 
        newNode.GetComponent<NodeScript>().SetDemand(10.0f);

        position = new Vector2(30.0f, 40.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(51); 
        newNode.GetComponent<NodeScript>().SetDemand(0f);
        newNode.GetComponent<NodeScript>().SetCenter();

        this.InitializeGraph(1);
    }

    public void SpawnC3(){
        Vector2 position = new Vector2(0f, 0f);
        GameObject newNode;

        position = new Vector2(41.0f, 49.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(1); 
        newNode.GetComponent<NodeScript>().SetDemand(10.0f);

        position = new Vector2(35.0f, 17.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(2); 
        newNode.GetComponent<NodeScript>().SetDemand(7.0f);

        position = new Vector2(55.0f, 45.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(3); 
        newNode.GetComponent<NodeScript>().SetDemand(13.0f);

        position = new Vector2(55.0f, 20.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(4); 
        newNode.GetComponent<NodeScript>().SetDemand(19.0f);

        position = new Vector2(15.0f, 30.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(5); 
        newNode.GetComponent<NodeScript>().SetDemand(26.0f);

        position = new Vector2(25.0f, 30.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(6); 
        newNode.GetComponent<NodeScript>().SetDemand(3.0f);

        position = new Vector2(20.0f, 50.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(7); 
        newNode.GetComponent<NodeScript>().SetDemand(5.0f);

        position = new Vector2(10.0f, 43.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(8); 
        newNode.GetComponent<NodeScript>().SetDemand(9.0f);

        position = new Vector2(55.0f, 60.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(9); 
        newNode.GetComponent<NodeScript>().SetDemand(16.0f);

        position = new Vector2(30.0f, 60.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(10); 
        newNode.GetComponent<NodeScript>().SetDemand(16.0f);

        position = new Vector2(20.0f, 65.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(11); 
        newNode.GetComponent<NodeScript>().SetDemand(12.0f);

        position = new Vector2(50.0f, 35.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(12); 
        newNode.GetComponent<NodeScript>().SetDemand(19.0f);

        position = new Vector2(30.0f, 25.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(13); 
        newNode.GetComponent<NodeScript>().SetDemand(23.0f);

        position = new Vector2(15.0f, 10.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(14); 
        newNode.GetComponent<NodeScript>().SetDemand(20.0f);

        position = new Vector2(30.0f, 5.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(15); 
        newNode.GetComponent<NodeScript>().SetDemand(8.0f);

        position = new Vector2(10.0f, 20.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(16); 
        newNode.GetComponent<NodeScript>().SetDemand(19.0f);

        position = new Vector2(5.0f, 30.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(17); 
        newNode.GetComponent<NodeScript>().SetDemand(2.0f);

        position = new Vector2(20.0f, 40.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(18); 
        newNode.GetComponent<NodeScript>().SetDemand(12.0f);

        position = new Vector2(15.0f, 60.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(19); 
        newNode.GetComponent<NodeScript>().SetDemand(17.0f);

        position = new Vector2(45.0f, 65.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(20); 
        newNode.GetComponent<NodeScript>().SetDemand(9.0f);

        position = new Vector2(45.0f, 20.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(21); 
        newNode.GetComponent<NodeScript>().SetDemand(11.0f);

        position = new Vector2(45.0f, 10.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(22); 
        newNode.GetComponent<NodeScript>().SetDemand(18.0f);

        position = new Vector2(55.0f, 5.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(23); 
        newNode.GetComponent<NodeScript>().SetDemand(29.0f);

        position = new Vector2(65.0f, 35.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(24); 
        newNode.GetComponent<NodeScript>().SetDemand(3.0f);

        position = new Vector2(65.0f, 20.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(25); 
        newNode.GetComponent<NodeScript>().SetDemand(6.0f);

        position = new Vector2(45.0f, 30.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(26); 
        newNode.GetComponent<NodeScript>().SetDemand(17.0f);

        position = new Vector2(35.0f, 40.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(27); 
        newNode.GetComponent<NodeScript>().SetDemand(16.0f);

        position = new Vector2(41.0f, 37.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(28); 
        newNode.GetComponent<NodeScript>().SetDemand(16.0f);

        position = new Vector2(64.0f, 42.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(29); 
        newNode.GetComponent<NodeScript>().SetDemand(9.0f);

        position = new Vector2(40.0f, 60.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(30); 
        newNode.GetComponent<NodeScript>().SetDemand(21.0f);

        position = new Vector2(31.0f, 52.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(31); 
        newNode.GetComponent<NodeScript>().SetDemand(27.0f);

        position = new Vector2(35.0f, 69.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(32); 
        newNode.GetComponent<NodeScript>().SetDemand(23.0f);

        position = new Vector2(53.0f, 52.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(33); 
        newNode.GetComponent<NodeScript>().SetDemand(11.0f);

        position = new Vector2(65.0f, 55.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(34); 
        newNode.GetComponent<NodeScript>().SetDemand(14.0f);

        position = new Vector2(63.0f, 65.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(35); 
        newNode.GetComponent<NodeScript>().SetDemand(8.0f);

        position = new Vector2(2.0f, 60.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(36); 
        newNode.GetComponent<NodeScript>().SetDemand(5.0f);

        position = new Vector2(20.0f, 20.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(37); 
        newNode.GetComponent<NodeScript>().SetDemand(8.0f);

        position = new Vector2(5.0f, 5.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(38); 
        newNode.GetComponent<NodeScript>().SetDemand(16.0f);

        position = new Vector2(60.0f, 12.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(39); 
        newNode.GetComponent<NodeScript>().SetDemand(31.0f);

        position = new Vector2(40.0f, 25.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(40); 
        newNode.GetComponent<NodeScript>().SetDemand(9.0f);

        position = new Vector2(42.0f, 7.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(41); 
        newNode.GetComponent<NodeScript>().SetDemand(5.0f);

        position = new Vector2(24.0f, 12.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(42); 
        newNode.GetComponent<NodeScript>().SetDemand(5.0f);

        position = new Vector2(23.0f, 3.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(43); 
        newNode.GetComponent<NodeScript>().SetDemand(7.0f);

        position = new Vector2(11.0f, 14.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(44); 
        newNode.GetComponent<NodeScript>().SetDemand(18.0f);

        position = new Vector2(6.0f, 38.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(45); 
        newNode.GetComponent<NodeScript>().SetDemand(16.0f);

        position = new Vector2(2.0f, 48.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(46); 
        newNode.GetComponent<NodeScript>().SetDemand(1.0f);

        position = new Vector2(8.0f, 56.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(47); 
        newNode.GetComponent<NodeScript>().SetDemand(27.0f);

        position = new Vector2(13.0f, 52.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(48); 
        newNode.GetComponent<NodeScript>().SetDemand(36.0f);

        position = new Vector2(6.0f, 68.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(49); 
        newNode.GetComponent<NodeScript>().SetDemand(30.0f);

        position = new Vector2(47.0f, 47.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(50); 
        newNode.GetComponent<NodeScript>().SetDemand(13.0f);

        position = new Vector2(49.0f, 58.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(51); 
        newNode.GetComponent<NodeScript>().SetDemand(10.0f);

        position = new Vector2(27.0f, 43.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(52); 
        newNode.GetComponent<NodeScript>().SetDemand(9.0f);

        position = new Vector2(37.0f, 31.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(53); 
        newNode.GetComponent<NodeScript>().SetDemand(14.0f);

        position = new Vector2(57.0f, 29.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(54); 
        newNode.GetComponent<NodeScript>().SetDemand(18.0f);

        position = new Vector2(63.0f, 23.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(55); 
        newNode.GetComponent<NodeScript>().SetDemand(2.0f);

        position = new Vector2(53.0f, 12.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(56); 
        newNode.GetComponent<NodeScript>().SetDemand(6.0f);

        position = new Vector2(32.0f, 12.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(57); 
        newNode.GetComponent<NodeScript>().SetDemand(7.0f);

        position = new Vector2(36.0f, 26.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(58); 
        newNode.GetComponent<NodeScript>().SetDemand(18.0f);

        position = new Vector2(21.0f, 24.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(59); 
        newNode.GetComponent<NodeScript>().SetDemand(28.0f);

        position = new Vector2(17.0f, 34.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(60); 
        newNode.GetComponent<NodeScript>().SetDemand(3.0f);

        position = new Vector2(12.0f, 24.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(61); 
        newNode.GetComponent<NodeScript>().SetDemand(13.0f);

        position = new Vector2(24.0f, 58.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(62); 
        newNode.GetComponent<NodeScript>().SetDemand(19.0f);

        position = new Vector2(27.0f, 69.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(63); 
        newNode.GetComponent<NodeScript>().SetDemand(10.0f);

        position = new Vector2(15.0f, 77.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(64); 
        newNode.GetComponent<NodeScript>().SetDemand(9.0f);

        position = new Vector2(62.0f, 77.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(65); 
        newNode.GetComponent<NodeScript>().SetDemand(20.0f);

        position = new Vector2(49.0f, 73.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(66); 
        newNode.GetComponent<NodeScript>().SetDemand(25.0f);

        position = new Vector2(67.0f, 5.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(67); 
        newNode.GetComponent<NodeScript>().SetDemand(25.0f);

        position = new Vector2(56.0f, 39.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(68); 
        newNode.GetComponent<NodeScript>().SetDemand(36.0f);

        position = new Vector2(37.0f, 47.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(69); 
        newNode.GetComponent<NodeScript>().SetDemand(6.0f);

        position = new Vector2(37.0f, 56.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(70); 
        newNode.GetComponent<NodeScript>().SetDemand(5.0f);

        position = new Vector2(57.0f, 68.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(71); 
        newNode.GetComponent<NodeScript>().SetDemand(15.0f);

        position = new Vector2(47.0f, 16.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(72); 
        newNode.GetComponent<NodeScript>().SetDemand(25.0f);

        position = new Vector2(44.0f, 17.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(73); 
        newNode.GetComponent<NodeScript>().SetDemand(9.0f);

        position = new Vector2(46.0f, 13.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(74); 
        newNode.GetComponent<NodeScript>().SetDemand(8.0f);

        position = new Vector2(49.0f, 11.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(75); 
        newNode.GetComponent<NodeScript>().SetDemand(18.0f);

        position = new Vector2(49.0f, 42.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(76); 
        newNode.GetComponent<NodeScript>().SetDemand(13.0f);

        position = new Vector2(53.0f, 43.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(77); 
        newNode.GetComponent<NodeScript>().SetDemand(14.0f);

        position = new Vector2(61.0f, 52.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(78); 
        newNode.GetComponent<NodeScript>().SetDemand(3.0f);

        position = new Vector2(57.0f, 48.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(79); 
        newNode.GetComponent<NodeScript>().SetDemand(23.0f);

        position = new Vector2(56.0f, 37.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(80); 
        newNode.GetComponent<NodeScript>().SetDemand(6.0f);

        position = new Vector2(55.0f, 54.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(81); 
        newNode.GetComponent<NodeScript>().SetDemand(26.0f);

        position = new Vector2(15.0f, 47.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(82); 
        newNode.GetComponent<NodeScript>().SetDemand(16.0f);

        position = new Vector2(14.0f, 37.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(83); 
        newNode.GetComponent<NodeScript>().SetDemand(11.0f);

        position = new Vector2(11.0f, 31.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(84); 
        newNode.GetComponent<NodeScript>().SetDemand(7.0f);

        position = new Vector2(16.0f, 22.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(85); 
        newNode.GetComponent<NodeScript>().SetDemand(41.0f);

        position = new Vector2(4.0f, 18.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(86); 
        newNode.GetComponent<NodeScript>().SetDemand(35.0f);

        position = new Vector2(28.0f, 18.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(87); 
        newNode.GetComponent<NodeScript>().SetDemand(26.0f);

        position = new Vector2(26.0f, 52.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(88); 
        newNode.GetComponent<NodeScript>().SetDemand(9.0f);

        position = new Vector2(26.0f, 35.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(89); 
        newNode.GetComponent<NodeScript>().SetDemand(15.0f);

        position = new Vector2(31.0f, 67.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(90); 
        newNode.GetComponent<NodeScript>().SetDemand(3.0f);

        position = new Vector2(15.0f, 19.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(91); 
        newNode.GetComponent<NodeScript>().SetDemand(1.0f);

        position = new Vector2(22.0f, 22.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(92); 
        newNode.GetComponent<NodeScript>().SetDemand(2.0f);

        position = new Vector2(18.0f, 24.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(93); 
        newNode.GetComponent<NodeScript>().SetDemand(22.0f);

        position = new Vector2(26.0f, 27.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(94); 
        newNode.GetComponent<NodeScript>().SetDemand(27.0f);

        position = new Vector2(25.0f, 24.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(95); 
        newNode.GetComponent<NodeScript>().SetDemand(20.0f);

        position = new Vector2(22.0f, 27.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(96); 
        newNode.GetComponent<NodeScript>().SetDemand(11.0f);

        position = new Vector2(25.0f, 21.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(97); 
        newNode.GetComponent<NodeScript>().SetDemand(12.0f);

        position = new Vector2(19.0f, 21.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(98); 
        newNode.GetComponent<NodeScript>().SetDemand(10.0f);

        position = new Vector2(20.0f, 26.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(99); 
        newNode.GetComponent<NodeScript>().SetDemand(9.0f);

        position = new Vector2(18.0f, 18.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(100); 
        newNode.GetComponent<NodeScript>().SetDemand(17.0f);

        position = new Vector2(35.0f, 35.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(101); 
        newNode.GetComponent<NodeScript>().SetDemand(0f);
        newNode.GetComponent<NodeScript>().SetCenter();

        this.InitializeGraph(3);
    }

    public void SpawnC4(){
        Vector2 position = new Vector2(0f, 0f);
        GameObject newNode;
        

        position = new Vector2(37.0f, 52.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(1); 
        newNode.GetComponent<NodeScript>().SetDemand(7.0f);

        position = new Vector2(49.0f, 49.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(2); 
        newNode.GetComponent<NodeScript>().SetDemand(30.0f);

        position = new Vector2(52.0f, 64.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(3); 
        newNode.GetComponent<NodeScript>().SetDemand(16.0f);

        position = new Vector2(20.0f, 26.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(4); 
        newNode.GetComponent<NodeScript>().SetDemand(9.0f);

        position = new Vector2(40.0f, 30.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(5); 
        newNode.GetComponent<NodeScript>().SetDemand(21.0f);

        position = new Vector2(21.0f, 47.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(6); 
        newNode.GetComponent<NodeScript>().SetDemand(15.0f);

        position = new Vector2(17.0f, 63.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(7); 
        newNode.GetComponent<NodeScript>().SetDemand(19.0f);

        position = new Vector2(31.0f, 62.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(8); 
        newNode.GetComponent<NodeScript>().SetDemand(23.0f);

        position = new Vector2(52.0f, 33.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(9); 
        newNode.GetComponent<NodeScript>().SetDemand(11.0f);

        position = new Vector2(51.0f, 21.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(10); 
        newNode.GetComponent<NodeScript>().SetDemand(5.0f);

        position = new Vector2(42.0f, 41.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(11); 
        newNode.GetComponent<NodeScript>().SetDemand(19.0f);

        position = new Vector2(31.0f, 32.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(12); 
        newNode.GetComponent<NodeScript>().SetDemand(29.0f);

        position = new Vector2(5.0f, 25.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(13); 
        newNode.GetComponent<NodeScript>().SetDemand(23.0f);

        position = new Vector2(12.0f, 42.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(14); 
        newNode.GetComponent<NodeScript>().SetDemand(21.0f);

        position = new Vector2(36.0f, 16.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(15); 
        newNode.GetComponent<NodeScript>().SetDemand(10.0f);

        position = new Vector2(52.0f, 41.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(16); 
        newNode.GetComponent<NodeScript>().SetDemand(15.0f);

        position = new Vector2(27.0f, 23.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(17); 
        newNode.GetComponent<NodeScript>().SetDemand(3.0f);

        position = new Vector2(17.0f, 33.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(18); 
        newNode.GetComponent<NodeScript>().SetDemand(41.0f);

        position = new Vector2(13.0f, 13.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(19); 
        newNode.GetComponent<NodeScript>().SetDemand(9.0f);

        position = new Vector2(57.0f, 58.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(20); 
        newNode.GetComponent<NodeScript>().SetDemand(28.0f);

        position = new Vector2(62.0f, 42.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(21); 
        newNode.GetComponent<NodeScript>().SetDemand(8.0f);

        position = new Vector2(42.0f, 57.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(22); 
        newNode.GetComponent<NodeScript>().SetDemand(8.0f);

        position = new Vector2(16.0f, 57.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(23); 
        newNode.GetComponent<NodeScript>().SetDemand(16.0f);

        position = new Vector2(8.0f, 52.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(24); 
        newNode.GetComponent<NodeScript>().SetDemand(10.0f);

        position = new Vector2(7.0f, 38.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(25); 
        newNode.GetComponent<NodeScript>().SetDemand(28.0f);

        position = new Vector2(27.0f, 68.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(26); 
        newNode.GetComponent<NodeScript>().SetDemand(7.0f);

        position = new Vector2(30.0f, 48.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(27); 
        newNode.GetComponent<NodeScript>().SetDemand(15.0f);

        position = new Vector2(43.0f, 67.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(28); 
        newNode.GetComponent<NodeScript>().SetDemand(14.0f);

        position = new Vector2(58.0f, 48.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(29); 
        newNode.GetComponent<NodeScript>().SetDemand(6.0f);

        position = new Vector2(58.0f, 27.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(30); 
        newNode.GetComponent<NodeScript>().SetDemand(19.0f);

        position = new Vector2(37.0f, 69.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(31); 
        newNode.GetComponent<NodeScript>().SetDemand(11.0f);

        position = new Vector2(38.0f, 46.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(32); 
        newNode.GetComponent<NodeScript>().SetDemand(12.0f);

        position = new Vector2(46.0f, 10.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(33); 
        newNode.GetComponent<NodeScript>().SetDemand(23.0f);

        position = new Vector2(61.0f, 33.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(34); 
        newNode.GetComponent<NodeScript>().SetDemand(26.0f);

        position = new Vector2(62.0f, 63.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(35); 
        newNode.GetComponent<NodeScript>().SetDemand(17.0f);

        position = new Vector2(63.0f, 69.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(36); 
        newNode.GetComponent<NodeScript>().SetDemand(6.0f);

        position = new Vector2(32.0f, 22.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(37); 
        newNode.GetComponent<NodeScript>().SetDemand(9.0f);

        position = new Vector2(45.0f, 35.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(38); 
        newNode.GetComponent<NodeScript>().SetDemand(15.0f);

        position = new Vector2(59.0f, 15.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(39); 
        newNode.GetComponent<NodeScript>().SetDemand(14.0f);

        position = new Vector2(5.0f, 6.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(40); 
        newNode.GetComponent<NodeScript>().SetDemand(7.0f);

        position = new Vector2(10.0f, 17.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(41); 
        newNode.GetComponent<NodeScript>().SetDemand(27.0f);

        position = new Vector2(21.0f, 10.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(42); 
        newNode.GetComponent<NodeScript>().SetDemand(13.0f);

        position = new Vector2(5.0f, 64.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(43); 
        newNode.GetComponent<NodeScript>().SetDemand(11.0f);

        position = new Vector2(30.0f, 15.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(44); 
        newNode.GetComponent<NodeScript>().SetDemand(16.0f);

        position = new Vector2(39.0f, 10.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(45); 
        newNode.GetComponent<NodeScript>().SetDemand(10.0f);

        position = new Vector2(32.0f, 39.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(46); 
        newNode.GetComponent<NodeScript>().SetDemand(5.0f);

        position = new Vector2(25.0f, 32.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(47); 
        newNode.GetComponent<NodeScript>().SetDemand(25.0f);

        position = new Vector2(25.0f, 55.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(48); 
        newNode.GetComponent<NodeScript>().SetDemand(17.0f);

        position = new Vector2(48.0f, 28.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(49); 
        newNode.GetComponent<NodeScript>().SetDemand(18.0f);

        position = new Vector2(56.0f, 37.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(50); 
        newNode.GetComponent<NodeScript>().SetDemand(10.0f);

        position = new Vector2(41.0f, 49.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(51); 
        newNode.GetComponent<NodeScript>().SetDemand(10.0f);

        position = new Vector2(35.0f, 17.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(52); 
        newNode.GetComponent<NodeScript>().SetDemand(7.0f);

        position = new Vector2(55.0f, 45.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(53); 
        newNode.GetComponent<NodeScript>().SetDemand(13.0f);

        position = new Vector2(55.0f, 20.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(54); 
        newNode.GetComponent<NodeScript>().SetDemand(19.0f);

        position = new Vector2(15.0f, 30.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(55); 
        newNode.GetComponent<NodeScript>().SetDemand(26.0f);

        position = new Vector2(25.0f, 30.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(56); 
        newNode.GetComponent<NodeScript>().SetDemand(3.0f);

        position = new Vector2(20.0f, 50.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(57); 
        newNode.GetComponent<NodeScript>().SetDemand(5.0f);

        position = new Vector2(10.0f, 43.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(58); 
        newNode.GetComponent<NodeScript>().SetDemand(9.0f);

        position = new Vector2(55.0f, 60.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(59); 
        newNode.GetComponent<NodeScript>().SetDemand(16.0f);

        position = new Vector2(30.0f, 60.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(60); 
        newNode.GetComponent<NodeScript>().SetDemand(16.0f);

        position = new Vector2(20.0f, 65.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(61); 
        newNode.GetComponent<NodeScript>().SetDemand(12.0f);

        position = new Vector2(50.0f, 35.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(62); 
        newNode.GetComponent<NodeScript>().SetDemand(19.0f);

        position = new Vector2(30.0f, 25.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(63); 
        newNode.GetComponent<NodeScript>().SetDemand(23.0f);

        position = new Vector2(15.0f, 10.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(64); 
        newNode.GetComponent<NodeScript>().SetDemand(20.0f);

        position = new Vector2(30.0f, 5.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(65); 
        newNode.GetComponent<NodeScript>().SetDemand(8.0f);

        position = new Vector2(10.0f, 20.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(66); 
        newNode.GetComponent<NodeScript>().SetDemand(19.0f);

        position = new Vector2(5.0f, 30.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(67); 
        newNode.GetComponent<NodeScript>().SetDemand(2.0f);

        position = new Vector2(20.0f, 40.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(68); 
        newNode.GetComponent<NodeScript>().SetDemand(12.0f);

        position = new Vector2(15.0f, 60.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(69); 
        newNode.GetComponent<NodeScript>().SetDemand(17.0f);

        position = new Vector2(45.0f, 65.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(70); 
        newNode.GetComponent<NodeScript>().SetDemand(9.0f);

        position = new Vector2(45.0f, 20.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(71); 
        newNode.GetComponent<NodeScript>().SetDemand(11.0f);

        position = new Vector2(45.0f, 10.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(72); 
        newNode.GetComponent<NodeScript>().SetDemand(18.0f);

        position = new Vector2(55.0f, 5.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(73); 
        newNode.GetComponent<NodeScript>().SetDemand(29.0f);

        position = new Vector2(65.0f, 35.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(74); 
        newNode.GetComponent<NodeScript>().SetDemand(3.0f);

        position = new Vector2(65.0f, 20.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(75); 
        newNode.GetComponent<NodeScript>().SetDemand(6.0f);

        position = new Vector2(45.0f, 30.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(76); 
        newNode.GetComponent<NodeScript>().SetDemand(17.0f);

        position = new Vector2(35.0f, 40.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(77); 
        newNode.GetComponent<NodeScript>().SetDemand(16.0f);

        position = new Vector2(41.0f, 37.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(78); 
        newNode.GetComponent<NodeScript>().SetDemand(16.0f);

        position = new Vector2(64.0f, 42.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(79); 
        newNode.GetComponent<NodeScript>().SetDemand(9.0f);

        position = new Vector2(40.0f, 60.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(80); 
        newNode.GetComponent<NodeScript>().SetDemand(21.0f);

        position = new Vector2(31.0f, 52.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(81); 
        newNode.GetComponent<NodeScript>().SetDemand(27.0f);

        position = new Vector2(35.0f, 69.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(82); 
        newNode.GetComponent<NodeScript>().SetDemand(23.0f);

        position = new Vector2(53.0f, 52.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(83); 
        newNode.GetComponent<NodeScript>().SetDemand(11.0f);

        position = new Vector2(65.0f, 55.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(84); 
        newNode.GetComponent<NodeScript>().SetDemand(14.0f);

        position = new Vector2(63.0f, 65.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(85); 
        newNode.GetComponent<NodeScript>().SetDemand(8.0f);

        position = new Vector2(2.0f, 60.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(86); 
        newNode.GetComponent<NodeScript>().SetDemand(5.0f);

        position = new Vector2(20.0f, 20.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(87); 
        newNode.GetComponent<NodeScript>().SetDemand(8.0f);

        position = new Vector2(5.0f, 5.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(88); 
        newNode.GetComponent<NodeScript>().SetDemand(16.0f);

        position = new Vector2(60.0f, 12.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(89); 
        newNode.GetComponent<NodeScript>().SetDemand(31.0f);

        position = new Vector2(40.0f, 25.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(90); 
        newNode.GetComponent<NodeScript>().SetDemand(9.0f);

        position = new Vector2(42.0f, 7.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(91); 
        newNode.GetComponent<NodeScript>().SetDemand(5.0f);

        position = new Vector2(24.0f, 12.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(92); 
        newNode.GetComponent<NodeScript>().SetDemand(5.0f);

        position = new Vector2(23.0f, 3.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(93); 
        newNode.GetComponent<NodeScript>().SetDemand(7.0f);

        position = new Vector2(11.0f, 14.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(94); 
        newNode.GetComponent<NodeScript>().SetDemand(18.0f);

        position = new Vector2(6.0f, 38.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(95); 
        newNode.GetComponent<NodeScript>().SetDemand(16.0f);

        position = new Vector2(2.0f, 48.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(96); 
        newNode.GetComponent<NodeScript>().SetDemand(1.0f);

        position = new Vector2(8.0f, 56.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(97); 
        newNode.GetComponent<NodeScript>().SetDemand(27.0f);

        position = new Vector2(13.0f, 52.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(98); 
        newNode.GetComponent<NodeScript>().SetDemand(36.0f);

        position = new Vector2(6.0f, 68.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(99); 
        newNode.GetComponent<NodeScript>().SetDemand(30.0f);

        position = new Vector2(47.0f, 47.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(100); 
        newNode.GetComponent<NodeScript>().SetDemand(13.0f);

        position = new Vector2(49.0f, 58.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(101); 
        newNode.GetComponent<NodeScript>().SetDemand(10.0f);

        position = new Vector2(27.0f, 43.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(102); 
        newNode.GetComponent<NodeScript>().SetDemand(9.0f);

        position = new Vector2(37.0f, 31.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(103); 
        newNode.GetComponent<NodeScript>().SetDemand(14.0f);

        position = new Vector2(57.0f, 29.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(104); 
        newNode.GetComponent<NodeScript>().SetDemand(18.0f);

        position = new Vector2(63.0f, 23.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(105); 
        newNode.GetComponent<NodeScript>().SetDemand(2.0f);

        position = new Vector2(53.0f, 12.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(106); 
        newNode.GetComponent<NodeScript>().SetDemand(6.0f);

        position = new Vector2(32.0f, 12.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(107); 
        newNode.GetComponent<NodeScript>().SetDemand(7.0f);

        position = new Vector2(36.0f, 26.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(108); 
        newNode.GetComponent<NodeScript>().SetDemand(18.0f);

        position = new Vector2(21.0f, 24.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(109); 
        newNode.GetComponent<NodeScript>().SetDemand(28.0f);

        position = new Vector2(17.0f, 34.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(110); 
        newNode.GetComponent<NodeScript>().SetDemand(3.0f);

        position = new Vector2(12.0f, 24.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(111); 
        newNode.GetComponent<NodeScript>().SetDemand(13.0f);

        position = new Vector2(24.0f, 58.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(112); 
        newNode.GetComponent<NodeScript>().SetDemand(19.0f);

        position = new Vector2(27.0f, 69.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(113); 
        newNode.GetComponent<NodeScript>().SetDemand(10.0f);

        position = new Vector2(15.0f, 77.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(114); 
        newNode.GetComponent<NodeScript>().SetDemand(9.0f);

        position = new Vector2(62.0f, 77.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(115); 
        newNode.GetComponent<NodeScript>().SetDemand(20.0f);

        position = new Vector2(49.0f, 73.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(116); 
        newNode.GetComponent<NodeScript>().SetDemand(25.0f);

        position = new Vector2(67.0f, 5.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(117); 
        newNode.GetComponent<NodeScript>().SetDemand(25.0f);

        position = new Vector2(56.0f, 39.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(118); 
        newNode.GetComponent<NodeScript>().SetDemand(36.0f);

        position = new Vector2(37.0f, 47.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(119); 
        newNode.GetComponent<NodeScript>().SetDemand(6.0f);

        position = new Vector2(37.0f, 56.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(120); 
        newNode.GetComponent<NodeScript>().SetDemand(5.0f);

        position = new Vector2(57.0f, 68.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(121); 
        newNode.GetComponent<NodeScript>().SetDemand(15.0f);

        position = new Vector2(47.0f, 16.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(122); 
        newNode.GetComponent<NodeScript>().SetDemand(25.0f);

        position = new Vector2(44.0f, 17.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(123); 
        newNode.GetComponent<NodeScript>().SetDemand(9.0f);

        position = new Vector2(46.0f, 13.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(124); 
        newNode.GetComponent<NodeScript>().SetDemand(8.0f);

        position = new Vector2(49.0f, 11.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(125); 
        newNode.GetComponent<NodeScript>().SetDemand(18.0f);

        position = new Vector2(49.0f, 42.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(126); 
        newNode.GetComponent<NodeScript>().SetDemand(13.0f);

        position = new Vector2(53.0f, 43.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(127); 
        newNode.GetComponent<NodeScript>().SetDemand(14.0f);

        position = new Vector2(61.0f, 52.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(128); 
        newNode.GetComponent<NodeScript>().SetDemand(3.0f);

        position = new Vector2(57.0f, 48.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(129); 
        newNode.GetComponent<NodeScript>().SetDemand(23.0f);

        position = new Vector2(56.0f, 37.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(130); 
        newNode.GetComponent<NodeScript>().SetDemand(6.0f);

        position = new Vector2(55.0f, 54.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(131); 
        newNode.GetComponent<NodeScript>().SetDemand(26.0f);

        position = new Vector2(15.0f, 47.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(132); 
        newNode.GetComponent<NodeScript>().SetDemand(16.0f);

        position = new Vector2(14.0f, 37.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(133); 
        newNode.GetComponent<NodeScript>().SetDemand(11.0f);

        position = new Vector2(11.0f, 31.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(134); 
        newNode.GetComponent<NodeScript>().SetDemand(7.0f);

        position = new Vector2(16.0f, 22.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(135); 
        newNode.GetComponent<NodeScript>().SetDemand(41.0f);

        position = new Vector2(4.0f, 18.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(136); 
        newNode.GetComponent<NodeScript>().SetDemand(35.0f);

        position = new Vector2(28.0f, 18.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(137); 
        newNode.GetComponent<NodeScript>().SetDemand(26.0f);

        position = new Vector2(26.0f, 52.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(138); 
        newNode.GetComponent<NodeScript>().SetDemand(9.0f);

        position = new Vector2(26.0f, 35.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(139); 
        newNode.GetComponent<NodeScript>().SetDemand(15.0f);

        position = new Vector2(31.0f, 67.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(140); 
        newNode.GetComponent<NodeScript>().SetDemand(3.0f);

        position = new Vector2(15.0f, 19.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(141); 
        newNode.GetComponent<NodeScript>().SetDemand(1.0f);

        position = new Vector2(22.0f, 22.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(142); 
        newNode.GetComponent<NodeScript>().SetDemand(2.0f);

        position = new Vector2(18.0f, 24.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(143); 
        newNode.GetComponent<NodeScript>().SetDemand(22.0f);

        position = new Vector2(26.0f, 27.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(144); 
        newNode.GetComponent<NodeScript>().SetDemand(27.0f);

        position = new Vector2(25.0f, 24.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(145); 
        newNode.GetComponent<NodeScript>().SetDemand(20.0f);

        position = new Vector2(22.0f, 27.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(146); 
        newNode.GetComponent<NodeScript>().SetDemand(11.0f);

        position = new Vector2(25.0f, 21.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(147); 
        newNode.GetComponent<NodeScript>().SetDemand(12.0f);

        position = new Vector2(19.0f, 21.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(148); 
        newNode.GetComponent<NodeScript>().SetDemand(10.0f);

        position = new Vector2(20.0f, 26.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(149); 
        newNode.GetComponent<NodeScript>().SetDemand(9.0f);

        position = new Vector2(18.0f, 18.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(150); 
        newNode.GetComponent<NodeScript>().SetDemand(17.0f);

        position = new Vector2(35.0f, 35.0f); 
        newNode = Instantiate(nodeObject, position, Quaternion.identity); 
        newNode.GetComponent<NodeScript>().SetId(151); 
        newNode.GetComponent<NodeScript>().SetDemand(0f);
        newNode.GetComponent<NodeScript>().SetCenter();

        this.InitializeGraph(4);
    }
}   
