using UnityEngine;

public class NodeScript : MonoBehaviour
{   
    private int id = -1;
    private int demand = 1;

    
    /// <summary>
    /// Function to assign the id of the node
    /// </summary>
    public void SetId(int id){
        this.id = id;
    }
    
    /// <summary>
    /// This function sets the demand of the node
    /// </summary>
    public void SetDemand(int demand){
        this.demand = demand;
    }

    /// <summary>
    /// This function returns the position of the node
    /// </summary>
    public Vector2 GetPosition(){
        return this.transform.position;
    }  

    /// <summary>
    /// This function returns the id of the node
    /// </summary>
    public int GetId(){
        return this.id;
    }

    /// <summary>
    /// This function returns the demand of the node
    /// </summary>
    public int GetDemand(){
        return this.demand;
    }

    /// <summary>
    /// This functions lets the algorithm know that the ant has arrived to the node so that it can decide which one is the next
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other) {
        
    }
}
