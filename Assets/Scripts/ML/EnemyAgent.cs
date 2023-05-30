using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class EnemyAgent : Agent
{

    const int moveZ = 0;
    [SerializeField]
    private Transform targetTransform;
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;
    public float moveSpeed = 2f;

    public override void OnEpisodeBegin()
    {
        transform.GetComponentInParent<EnvController>().ResetEnv();
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];

        var tempTarget = transform.position + new Vector3(moveX, moveY, moveZ)*moveSpeed*Time.deltaTime ;

        var location = tilemap.WorldToCell(tempTarget);
        var tile = tilemap.GetTile(location);
        if (!allowedTiles.Contain(tile))
        {
            SetReward(-100);
            EndEpisode();
        }
        else
        {
            transform.position =  tempTarget;
            AddReward(20 - CalculateDistance());
            
        }
    }

    //public override void Heuristic(in ActionBuffers actionsOut)
    //{
    //ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
    // continuousActions[0] = Input.GetAxisRaw("Horizontal");
    //continuousActions[1] = Input.GetAxisRaw("Vertical");
    //}

    private float CalculateDistance()
    {
        var dx = transform.position.x - targetTransform.position.x;
        var dy = transform.position.y - targetTransform.position.y;
        return Mathf.Sqrt(dx*dx + dy*dy);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(targetTransform.position);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            SetReward(100);
            EndEpisode();
        }
    }
}
