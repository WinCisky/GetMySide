using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class MovementAssistantComet : MonoBehaviour
{    
    //NEW UNIT TESTING
    public Vector3 direction;
    public Quaternion rotation;
    public float speed;
    public bool can_be_used;
    public bool can_rotate;
}

//Hybrid ECS
public class MovementSystemComet : ComponentSystem
{
    struct ComponentsComet
    {
        public MovementAssistantComet movement;
        public Transform transform;
    }

    protected override void OnUpdate()
    {
        float fixedDeltaTime = Time.deltaTime;
        //modifica parametri per le singole entita'
        foreach (var e in GetEntities<ComponentsComet>())
        {
            //l'oggetto e' usabile?
            if (e.transform.position.y < -10)
            {
                e.movement.can_be_used = true;
            }
            else
            {
                //posizione
                e.transform.position += e.movement.direction * fixedDeltaTime * e.movement.speed;
                //rotazione
                if (e.movement.can_rotate)
                    e.transform.rotation *= e.movement.rotation;
            }            
        }
    }
}