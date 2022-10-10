using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphScript : MonoBehaviour
{
    Dictionary<int, NodeScript> nodes = new Dictionary<int, NodeScript>();

    // Start is called before the first frame update
    public void GetNodes()
    {
        NodeScript[] existingNodes = FindObjectsOfType<NodeScript>();
        foreach (NodeScript n in existingNodes){
            nodes.Add(n.GetId(), n);
        }

        Debug.Log("HOla");
        foreach (KeyValuePair<int, NodeScript> kvp in nodes){
            Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value.GetId()}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
