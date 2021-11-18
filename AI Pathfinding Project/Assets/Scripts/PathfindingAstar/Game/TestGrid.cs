using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class TestGrid : MonoBehaviour
{
    private AStar pathfinding;
    void Start()
    {
        pathfinding = new AStar(5, 5);

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Vector3 mousWorldPosition = UtilityClass.GetMouseWorldPosition();
            
            pathfinding.GetGrid().GetXY(mousWorldPosition, out int x, out int y);
            List<PathNodeAI> path = pathfinding.FindPath(0, 0, x, y);
            
            if (path == null)
            {
                Debug.Log("it is active");
                for (int i = 0; i<path.Count - 1; i++)
                {
                    
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                }
 
            }
        }
    }
}
