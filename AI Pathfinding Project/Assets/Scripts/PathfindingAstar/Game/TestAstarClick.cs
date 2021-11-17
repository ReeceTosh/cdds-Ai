using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class TestAstarClick : MonoBehaviour
{
    [SerializeField] private Sprite Wall;
    private AStar pathfinding;

    void Start()
    {
        pathfinding = new AStar(10, 10);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Get world position within the grid and find the path from 0,0 to where ever the mouse was clicked
            Vector3 mouseWorldPosition = UtilityClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            List<PathNodeAI> path = pathfinding.FindPath(0, 0, x, y);
            Debug.Log("Path length:" + path.Count);
            if (path != null)
            {
                for(int i = 0; i < path.Count - 1; i++)
                {
                    
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i+1].x, path[i+1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPosition = UtilityClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);

            if (pathfinding.GetNode(x, y) == null)
            {
                return;
            }
            else
            {
                pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
                if (pathfinding.GetNode(x, y).isWalkable == false)
                {
                    Debug.Log("image has been set");
                    //ImageConversion.EncodeToPNG()
                }
            }
        }
    }
}
