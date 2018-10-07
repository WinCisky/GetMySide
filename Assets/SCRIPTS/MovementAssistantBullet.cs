using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class MovementAssistantBullet : MonoBehaviour {
    public bool usable, reset;
}

//Hybrid ECS
public class MovementSystemBullet : ComponentSystem
{
    struct ComponentsBullet
    {
        public MovementAssistantBullet movement;
        public Transform transform;
    }
    
    float fixedDeltaTime;
    Vector3 player;

    protected override void OnUpdate()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
        player = GameManager.GM.player.position;
        foreach (var e in GetEntities<ComponentsBullet>())
        {
            if (!e.movement.usable)
            {
                Vector3 movement = e.movement.transform.rotation * Vector3.up * 50;
                e.transform.position += fixedDeltaTime * movement;
                //spengo l'oggetto
                if (e.transform.position.y > 40 || e.movement.reset)
                {
                    e.transform.position = Vector3.up * -100;
                    e.movement.usable = true;
                }
            }
        }
    }
}