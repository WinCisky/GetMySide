using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class MovementAssistantMine : MonoBehaviour
{
    public float margin_of_explosion;
    public Vector3 rotating_speed;
    public Rigidbody rb;
    /*
    public bool toDestroy;
    public bool destroyable;
    public float movement_speed;
    */
}

//Hybrid ECS
public class MovementSystemMine : ComponentSystem
{
    struct ComponentsComet
    {
        public MovementAssistantMine movement;
        public Transform transform;
        public ParticleSystem explosionParticles;
    }

    protected override void OnUpdate()
    {
        float fixedDeltaTime = Time.deltaTime;
        float point_of_explosion = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, 0)).y;
        //modifica parametri per le singole entita'
        foreach (var e in GetEntities<ComponentsComet>())
        {
            //rotazione
            e.transform.rotation *= Quaternion.Euler(
                e.movement.rotating_speed.x,
                e.movement.rotating_speed.y,
                e.movement.rotating_speed.z);

            if (e.transform.position.y < point_of_explosion + e.movement.margin_of_explosion)
            {
                //esplodo
                //muovo gli oggetti vicini
                Collider[] colliders = Physics.OverlapSphere(e.transform.position, Mathf.Infinity);
                //mostro il particle system
                //resetto la posizione
                //cancello le forze
                //resetto la rotaione
                //aggiungo le forze

            }
        }
    }
}