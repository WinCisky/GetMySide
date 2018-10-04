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
                //riposiziono l'oggetto
                if (e.movement.reset)
                {
                    e.movement.reset = false;
                    e.transform.position = player;
                }
                //muovo l'oggetto
                e.transform.position += fixedDeltaTime * Vector3.up * 300;
                //spengo l'oggetto
                if (e.transform.position.y > 40)
                    e.movement.usable = true;
            }
        }
    }
}