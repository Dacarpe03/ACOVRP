using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSpawnerScript : MonoBehaviour
{
    [Header("Number of nodes to spawn")]
    [SerializeField] private int nNodes = 5;
    [SerializeField] private float limit = 50f;
    private float capacityOne = 160f;

    [Header("Node Prefab")]
    [SerializeField] GameObject nodeObject;


    /// <summary>
    /// Spawns a series of nodes with random positions
    /// </summary>
    private void SpawnRandomly() {
        bool center = false;
        for (int i=0; i<nNodes ; i++){
            Vector2 position = new Vector2(Random.Range(-limit, limit), Random.Range(-limit, limit));
            GameObject newNode = Instantiate(nodeObject, position, Quaternion.identity);
            newNode.GetComponent<NodeScript>().SetId(i+1);
            newNode.GetComponent<NodeScript>().SetDemand(1);
            if (!center){
                newNode.GetComponent<NodeScript>().SetCenter();
                newNode.GetComponent<NodeScript>().SetDemand(0);
                center = true;
            }
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

        FindObjectOfType<GraphScript>().SetCapacity(this.capacityOne);
        FindObjectOfType<GraphScript>().GetNodes();
    }

}
