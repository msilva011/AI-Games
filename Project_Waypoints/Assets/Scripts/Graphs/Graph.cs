using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    List<Edge> edges = new List<Edge>(); // lista de arestas
    List<Node> nodes = new List<Node>(); // lista de nós
    List<Node> path = new List<Node>();  // lista de caminho

    public Graph() { } // construtor

    // adiciona um nó ao grafo
    public void AddNode(GameObject id)
    {
        Node node = new Node(id);
        nodes.Add(node);
    }
    // adiciona uma aresta ao grafo
    public void AddEdge(GameObject fromNode, GameObject toNode)
    {
        // procura os nós de origem e destino
        Node from = FindNode(fromNode);
        Node to = FindNode(toNode);
        // se encontrou os nós, cria uma aresta
        if (from != null && to != null)
        {
            Edge edge = new Edge(from, to);
            edges.Add(edge);
            from.edges.Add(edge);
        }
    }
    // procura um nó no grafo
    Node FindNode(GameObject id)
    {
        foreach (Node n in nodes)
        {
            if (n.GetId() == id)
            {
                return n;
            }
        }
        return null;
    }
    // calcula a distância entre dois nós
    float GetDistance(Node a, Node b)
    {
        Vector3 direction = a.GetId().transform.position - b.GetId().transform.position;
        return direction.sqrMagnitude;
    }

    // retorna o menor valor de fCost na lista de nós
    int LowestFCost(List<Node> listNode)
    {        
        float lowestFCost = listNode[0].fCost;
        int count = 0;
        int iteratorCount = 0;

        for(int i = 1; i < listNode.Count; i++)
        {
            if (listNode[i].fCost < lowestFCost)
            {
                lowestFCost = listNode[i].fCost;
                iteratorCount = count;
            }
            count++;
        }
        return iteratorCount;
    }
    // algoritmo de busca A*
    public bool AStar(GameObject startId, GameObject endId)
    {
        // se o nó de origem é igual ao nó de destino, não faz nada
        if(startId == endId)
        {
            path.Clear();
            return false;
        }
        // procura os nós de origem e destino
        Node start = FindNode(startId);
        Node end = FindNode(endId);
        // se não encontrou os nós, não faz nada
        if (start == null || end == null) return false;
        // inicializa as listas de nós abertos e fechados
        List<Node> open = new List<Node>();
        List<Node> closed = new List<Node>();
        // variáveis auxiliares para o algoritmo
        float tentativeGScore = 0;      // serve para auxiliar a selecionar o próximo nó no grafo
        bool tentativeIsBetter = false; // se o tentativeGScore for melhor, utiliza o melhor
        // inicializa os valores do nó de origem
        start.gCost = 0;
        start.hCost = GetDistance(start, end);
        start.fCost = start.hCost;
        // adiciona o nó de origem na lista de nós abertos
        open.Add(start);
        // enquanto houver nós abertos
        while(open.Count > 0)
        {
            // seleciona o nó com menor fCost
            int i = LowestFCost(open);
            Node current = open[i];
            // se o nó selecionado é o nó de destino, reconstrói o caminho
            if(current.GetId() == end.GetId())
            {
                ReconstructPath(start, end);
                return true;
            }
            // remove o nó selecionado da lista de nós abertos e adiciona na lista de nós fechados
            open.RemoveAt(i);
            closed.Add(current);

            Node neighbour;
            // para cada nó vizinho do nó atual
            foreach(Edge e in current.edges)
            {
                // associa um nó vizinho ao nó atual
                neighbour = e.endNode;
                // se o nó vizinho já foi visitado, não faz nada
                if (closed.IndexOf(neighbour) > -1) continue;
                // calcula o custo do nó vizinho
                tentativeGScore = current.gCost + GetDistance(current, neighbour);
                // se o nó vizinho não está na lista de nós abertos, adiciona
                if (open.IndexOf(neighbour) == -1)
                {
                    open.Add(neighbour);
                    tentativeIsBetter = true;
                }
                // se o nó vizinho está na lista de nós abertos, verifica se o custo é melhor
                else if (tentativeGScore < neighbour.gCost) tentativeIsBetter = true;
                // se o custo não é melhor, não faz nada
                else tentativeIsBetter = false;
                // se o custo é melhor, atualiza os valores do nó vizinho
                if (tentativeIsBetter)
                {
                    neighbour.cameFrom = current;
                    neighbour.gCost = tentativeGScore;
                    neighbour.hCost = GetDistance(neighbour, end);
                    neighbour.fCost = neighbour.gCost + neighbour.hCost;
                }
            }
        }
        return false;
    }
    // reconstrói o caminho
    void ReconstructPath(Node start, Node end)
    {
        // limpa a lista de caminho
        path.Clear();
        // adiciona o nó de destino na lista de caminho
        path.Add(end);
        // associa o nó atual ao nó de onde veio
        Node currentNode = end.cameFrom;
        // enquanto o nó atual é diferente do nó de origem
        while(currentNode != start && currentNode != null)
        {   
            // adiciona o nó atual na lista de caminho no início da lista
            path.Insert(0, currentNode);
            // associa o nó atual ao nó de onde veio
            currentNode = currentNode.cameFrom;
        }
        // adiciona o nó de origem na lista de caminho no início da lista
        path.Insert(0, start);
    }
    // retorna o caminho
    public List<Node> GetPath()
    {
        return path;
    }
}
