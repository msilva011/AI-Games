using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    List<Edge> edges = new List<Edge>(); // lista de arestas
    List<Node> nodes = new List<Node>(); // lista de n�s
    List<Node> path = new List<Node>();  // lista de caminho

    public Graph() { } // construtor

    // adiciona um n� ao grafo
    public void AddNode(GameObject id)
    {
        Node node = new Node(id);
        nodes.Add(node);
    }
    // adiciona uma aresta ao grafo
    public void AddEdge(GameObject fromNode, GameObject toNode)
    {
        // procura os n�s de origem e destino
        Node from = FindNode(fromNode);
        Node to = FindNode(toNode);
        // se encontrou os n�s, cria uma aresta
        if (from != null && to != null)
        {
            Edge edge = new Edge(from, to);
            edges.Add(edge);
            from.edges.Add(edge);
        }
    }
    // procura um n� no grafo
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
    // calcula a dist�ncia entre dois n�s
    float GetDistance(Node a, Node b)
    {
        Vector3 direction = a.GetId().transform.position - b.GetId().transform.position;
        return direction.sqrMagnitude;
    }

    // retorna o menor valor de fCost na lista de n�s
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
        // se o n� de origem � igual ao n� de destino, n�o faz nada
        if(startId == endId)
        {
            path.Clear();
            return false;
        }
        // procura os n�s de origem e destino
        Node start = FindNode(startId);
        Node end = FindNode(endId);
        // se n�o encontrou os n�s, n�o faz nada
        if (start == null || end == null) return false;
        // inicializa as listas de n�s abertos e fechados
        List<Node> open = new List<Node>();
        List<Node> closed = new List<Node>();
        // vari�veis auxiliares para o algoritmo
        float tentativeGScore = 0;      // serve para auxiliar a selecionar o pr�ximo n� no grafo
        bool tentativeIsBetter = false; // se o tentativeGScore for melhor, utiliza o melhor
        // inicializa os valores do n� de origem
        start.gCost = 0;
        start.hCost = GetDistance(start, end);
        start.fCost = start.hCost;
        // adiciona o n� de origem na lista de n�s abertos
        open.Add(start);
        // enquanto houver n�s abertos
        while(open.Count > 0)
        {
            // seleciona o n� com menor fCost
            int i = LowestFCost(open);
            Node current = open[i];
            // se o n� selecionado � o n� de destino, reconstr�i o caminho
            if(current.GetId() == end.GetId())
            {
                ReconstructPath(start, end);
                return true;
            }
            // remove o n� selecionado da lista de n�s abertos e adiciona na lista de n�s fechados
            open.RemoveAt(i);
            closed.Add(current);

            Node neighbour;
            // para cada n� vizinho do n� atual
            foreach(Edge e in current.edges)
            {
                // associa um n� vizinho ao n� atual
                neighbour = e.endNode;
                // se o n� vizinho j� foi visitado, n�o faz nada
                if (closed.IndexOf(neighbour) > -1) continue;
                // calcula o custo do n� vizinho
                tentativeGScore = current.gCost + GetDistance(current, neighbour);
                // se o n� vizinho n�o est� na lista de n�s abertos, adiciona
                if (open.IndexOf(neighbour) == -1)
                {
                    open.Add(neighbour);
                    tentativeIsBetter = true;
                }
                // se o n� vizinho est� na lista de n�s abertos, verifica se o custo � melhor
                else if (tentativeGScore < neighbour.gCost) tentativeIsBetter = true;
                // se o custo n�o � melhor, n�o faz nada
                else tentativeIsBetter = false;
                // se o custo � melhor, atualiza os valores do n� vizinho
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
    // reconstr�i o caminho
    void ReconstructPath(Node start, Node end)
    {
        // limpa a lista de caminho
        path.Clear();
        // adiciona o n� de destino na lista de caminho
        path.Add(end);
        // associa o n� atual ao n� de onde veio
        Node currentNode = end.cameFrom;
        // enquanto o n� atual � diferente do n� de origem
        while(currentNode != start && currentNode != null)
        {   
            // adiciona o n� atual na lista de caminho no in�cio da lista
            path.Insert(0, currentNode);
            // associa o n� atual ao n� de onde veio
            currentNode = currentNode.cameFrom;
        }
        // adiciona o n� de origem na lista de caminho no in�cio da lista
        path.Insert(0, start);
    }
    // retorna o caminho
    public List<Node> GetPath()
    {
        return path;
    }
}
