using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Link é uma estrutura que representa uma aresta
[System.Serializable]
public struct Link
{
    // Uni representa que o NPC só pode ir em uma direção
    // Bi representa que o NPC pode ir e voltar
    public enum direction { UNI, BI }
    public GameObject node1;
    public GameObject node2;
    public direction dir;
}

public class WaypointManager : MonoBehaviour
{
    // waypoints que o NPC vai seguir
    public GameObject[] waypoints;
    // links que ligam os waypoints
    public Link[] links;
    // grafo que representa os waypoints
    public Graph graph = new Graph();
    
    void Start()
    {
        // adiciona os nós e arestas ao grafo
        if(waypoints.Length > 0)
        {
            foreach(GameObject wp in waypoints)
            {
                graph.AddNode(wp);
            }
            foreach(Link l in links)
            {
                graph.AddEdge(l.node1, l.node2);
                // se a direção for bidirecional, adiciona a aresta inversa
                if(l.dir == Link.direction.BI)
                {
                    graph.AddEdge(l.node2, l.node1);
                }
            }
        }
    }
}
