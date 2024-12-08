using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoints : MonoBehaviour
{
    // Vari�veis de controle do player
    Transform goal;
    float speed = 2.0f; // Reduzida para um movimento mais controlado
    float accuracy = 0.2f; // Maior precis�o para alcan�ar o waypoint
    float rotSpeed = 2.0f;

    // Vari�veis de controle dos waypoints
    public WaypointManager waypointManager;
    GameObject[] waypoints;
    GameObject currentNode;
    int currentWaypoint = 0;
    Graph graph;

    void Start()
    {
        // Inicializa as vari�veis
        waypoints = waypointManager.waypoints;
        graph = waypointManager.graph;
        currentNode = waypoints[0];

        // Come�a indo para uma posi��o inicial
        // Invoke(nameof(GoToColeseum), 2.0f);
    }

    // M�todos para ir para waypoints espec�ficos
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
        CheckFall(); // Verifica se o player caiu do cen�rio

        // Verifica se o caminho � v�lido e se h� waypoints restantes
        if (graph == null || graph.GetPath() == null || graph.GetPath().Count == 0 || currentWaypoint >= graph.GetPath().Count)
        {
            return;
        }

        // Se o player chegou ao waypoint atual, atualiza para o pr�ximo
        if (Vector3.Distance(graph.GetPath()[currentWaypoint].GetId().transform.position, transform.position) < accuracy)
        {
            currentNode = graph.GetPath()[currentWaypoint].GetId();
            currentWaypoint++;
        }

        // Se ainda h� waypoints no caminho, move-se em dire��o ao pr�ximo
        if (currentWaypoint < graph.GetPath().Count)
        {
            goal = graph.GetPath()[currentWaypoint].GetId().transform;

            // Rotaciona para olhar em dire��o ao waypoint
            Vector3 lookAtGoal = new Vector3(goal.position.x, transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - transform.position;
            if (direction.magnitude > 0.1f) // Impede rota��es desnecess�rias
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
            }

            // Move em dire��o ao waypoint
            Vector3 moveDirection = direction.normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
        }
    }

    // M�todo para verificar se o player caiu do cen�rio
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
