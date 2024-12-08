using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoints : MonoBehaviour
{
    // Variáveis de controle do player
    Transform goal;
    float speed = 2.0f; // Reduzida para um movimento mais controlado
    float accuracy = 0.2f; // Maior precisão para alcançar o waypoint
    float rotSpeed = 2.0f;

    // Variáveis de controle dos waypoints
    public WaypointManager waypointManager;
    GameObject[] waypoints;
    GameObject currentNode;
    int currentWaypoint = 0;
    Graph graph;

    void Start()
    {
        // Inicializa as variáveis
        waypoints = waypointManager.waypoints;
        graph = waypointManager.graph;
        currentNode = waypoints[0];

        // Começa indo para uma posição inicial
        // Invoke(nameof(GoToColeseum), 2.0f);
    }

    // Métodos para ir para waypoints específicos
    public void GoToPyramid()
    {
        graph.AStar(currentNode, waypoints[4]);
        currentWaypoint = 0;
    }

    public void GoToFountain()
    {
        graph.AStar(currentNode, waypoints[8]);
        currentWaypoint = 0;
    }

    public void GoToSculpture()
    {
        graph.AStar(currentNode, waypoints[11]);
        currentWaypoint = 0;
    }

    public void GoToColeseum()
    {
        graph.AStar(currentNode, waypoints[19]);
        currentWaypoint = 0;
    }

    public void GoToCastle()
    {
        graph.AStar(currentNode, waypoints[6]);
        currentWaypoint = 0;
    }

    void Update()
    {
        CheckFall(); // Verifica se o player caiu do cenário

        // Verifica se o caminho é válido e se há waypoints restantes
        if (graph == null || graph.GetPath() == null || graph.GetPath().Count == 0 || currentWaypoint >= graph.GetPath().Count)
        {
            return;
        }

        // Se o player chegou ao waypoint atual, atualiza para o próximo
        if (Vector3.Distance(graph.GetPath()[currentWaypoint].GetId().transform.position, transform.position) < accuracy)
        {
            currentNode = graph.GetPath()[currentWaypoint].GetId();
            currentWaypoint++;
        }

        // Se ainda há waypoints no caminho, move-se em direção ao próximo
        if (currentWaypoint < graph.GetPath().Count)
        {
            goal = graph.GetPath()[currentWaypoint].GetId().transform;

            // Rotaciona para olhar em direção ao waypoint
            Vector3 lookAtGoal = new Vector3(goal.position.x, transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - transform.position;
            if (direction.magnitude > 0.1f) // Impede rotações desnecessárias
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
            }

            // Move em direção ao waypoint
            Vector3 moveDirection = direction.normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
        }
    }

    // Método para verificar se o player caiu do cenário
    void CheckFall()
    {
        if (transform.position.y < -5) // Define um limite para detectar quedas
        {
            Debug.Log("Player caiu! Reposicionando...");
            transform.position = waypoints[0].transform.position; // Reposiciona no primeiro waypoint
            currentWaypoint = 0;
        }
    }
}
