using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class Manager_NavMesh : MonoBehaviour
{
    private NavMeshSurface _surface;

    private void Awake()
    {
        _surface = GetComponent<NavMeshSurface>();
    }
    
    public void Rebake()
    {
        _surface.BuildNavMesh();
    }
}
