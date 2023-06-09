using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnvController : MonoBehaviour
{
    GameObject Target, Agent, Floor;
    [SerializeField] Tilemap tileMap;
    // Start is called before the first frame update
    void Awake()
    {
        Target = gameObject.transform.Find("Player").gameObject;
        Agent = gameObject.transform.Find("Enemy").gameObject;
        Floor = gameObject.transform.Find("Grid").gameObject;

        ResetEnv();
    }

    public void ResetEnv()
    {
        Floor.transform.localPosition = gameObject.transform.localPosition;
        Agent.transform.position = tileMap.GetCellCenterWorld(new Vector3Int(-3, -3, 0));   //Floor.transform.localPosition + Vector3.up * 0.5f
        Target.transform.position = tileMap.GetCellCenterWorld(new Vector3Int(8, 1, 0));  //Agent.transform.localPosition + Vector3.left * 2
        Target.GetComponent<RandomMover>().ChangeTarget();
    }

    public Vector3 GetFloorPosition()
    {
        return Floor.transform.position;
    }

    public Vector3 GetAgentPosition()
    {
        return Agent.transform.position;
    }
}