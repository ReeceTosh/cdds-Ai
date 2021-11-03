using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrid : MonoBehaviour
{
    [SerializeField] private HeatVisual heatMapVisual;
    protected GridSquare grid;
    void Start()
    {
        //need to update the values of the parameters to create a map.
        grid = new GridSquare(8, 8, 5f, Vector3.zero);

        if (heatMapVisual)
        {
            heatMapVisual.SetGrid(grid);
        }

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = UtilsClass.GetMouseWorldPosition();
            int value = grid.GetValue(position);
            if (value < 100)
            {
                grid.SetValue(position, value + 5);
            }
            else
            {
                grid.SetValue(position, 0);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
    }
}
