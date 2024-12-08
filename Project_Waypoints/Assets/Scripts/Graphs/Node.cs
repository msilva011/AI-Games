using System.Collections.Generic;
using UnityEngine;

public class Node
{
    // um nó pode ter vários edges
    public List<Edge> edges = new List<Edge>();
    // define o caminho que vai do nó atual até o nó de destino
    public Node path = null;
    // representa o objeto que está associado ao nó
    GameObject id;    
    // variáveis auxiliares para o algoritmo de busca A*
    public float fCost, gCost, hCost;
    // identifica o nó de onde veio para o nó atual
    public Node cameFrom;

    // método construtor
    public Node(GameObject i)
    {
        id = i;        
        path = null;
    }
    // retorna o objeto associado ao GameObject id
    public GameObject GetId()
    {
        return id;
    }
}
