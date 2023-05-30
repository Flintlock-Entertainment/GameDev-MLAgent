using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingManager : MonoBehaviour
{
    public GameObject toSpawn;
    public int gridHeight = 5;
    public int gridWidth = 5;
    void Start()
    {
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                Instantiate(toSpawn, new Vector3((i - Mathf.FloorToInt(gridWidth / 2)) * 10, 0, (j - Mathf.FloorToInt(gridHeight / 2)) * 10), Quaternion.identity);
            }
        }
    }

}