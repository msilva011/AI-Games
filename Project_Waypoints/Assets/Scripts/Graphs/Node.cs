using System.Collections.Generic;
using UnityEngine;

public class Node
{
    // um n� pode ter v�rios edges
    public List<Edge> edges = new List<Edge>();
    // define o caminho que vai do n� atual at� o n� de destino
    public Node path = null;
    // representa o objeto que est� associado ao n�
    GameObject id;    
    // vari�veis auxiliares para o algoritmo de busca A*
    public float fCost, gCost, hCost;
    // identifica o n� de onde veio para o n� atual
    public Node cameFrom;

    // m�todo construtor
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
