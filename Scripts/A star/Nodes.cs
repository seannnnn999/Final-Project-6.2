using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour
{
    public Nodes start;
    
    public List<Nodes> connections;

    public float gScore;
    public float hScore;

    public float FScore()
    {
        return gScore + hScore;
    }
}
