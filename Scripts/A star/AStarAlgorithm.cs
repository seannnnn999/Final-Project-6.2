using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using UnityEngine;

public class AStarAlgorithm : MonoBehaviour
{
    public static AStarAlgorithm instance;


    private void Awake()
    {
        instance = this;
    }

    
    public List<Nodes> GeneratePath(Nodes start, Nodes end)
    {
        List<Nodes> openSet = new List<Nodes>();

        foreach(Nodes n in FindObjectsOfType<Nodes>())
        {
            n.gScore = 5;
        }

        start.gScore = 0;
        start.hScore = Vector2.Distance(start.transform.position, end.transform.position);
        openSet.Add(start);

        while (openSet.Count > 0)
        {
            int lowestF = default;
            for(int i = 0; i<openSet.Count; i++)
            {
                if (openSet[i].FScore() < openSet[lowestF].FScore())
                {
                    lowestF = i; 
                }
            }
            
            Nodes currentNode = openSet[lowestF];
            openSet.Remove(currentNode);

            if(currentNode == end)
            {
                List<Nodes> path = new List<Nodes>();
               
                path.Insert(0, end);
                return path;
            }

            foreach(Nodes connectedNode in currentNode.connections)
            {
                float heldGScore = connectedNode.gScore + Vector2.Distance(currentNode.transform.position, connectedNode.transform.position);

                if (heldGScore < connectedNode.gScore)
                {
                    connectedNode.start = currentNode;
                    connectedNode.gScore = heldGScore;
                    connectedNode.hScore = Vector2.Distance(connectedNode.transform.position, end.transform.position);
                    
                  
                }
            }
        }
        return null;
    }
}
