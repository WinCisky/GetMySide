using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class MovementAssistantComet : MonoBehaviour
{
    public bool toDestroy;
    public bool destroyable;
    public Vector3 rotating_speed;
    public Rigidbody rb;
    public float movement_speed;
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
        //parametri identici per tutte le entita'
        //Vector3 target = GameManager.GM.player.transform.position;
        float fixedDeltaTime = Time.deltaTime;
        //modifica parametri per le singole entita'
        foreach (var e in GetEntities<ComponentsComet>())
        {
            //rotazione
            e.transform.rotation *= Quaternion.Euler(
                e.movement.rotating_speed.x,
                e.movement.rotating_speed.y,
                e.movement.rotating_speed.z);
            bool out_of_delimiter = false;
            if (e.transform.position.x > 40)
                out_of_delimiter = true;
            if (e.transform.position.x < -40)
                out_of_delimiter = true;
            if (e.transform.position.z > 40)
                out_of_delimiter = true;
            if (e.transform.position.z < -40)
                out_of_delimiter = true;
            //riposizionamento
            if (e.transform.position.y < -10 || out_of_delimiter)
            {
                //lóggetto va distrutto?
                if (!e.movement.toDestroy)
                {
                    e.transform.position = new Vector3(
                        Random.Range(-30, +30),
                        Random.Range(40, 90),
                        Random.Range(-30, +30));
                    //cancello le forze precedenti e reimposto la forza iniziale
                    e.movement.rb.velocity = Vector3.zero;
                    e.movement.rb.angularVelocity = Vector3.zero;
                    e.movement.transform.rotation = Quaternion.identity;
                    e.movement.rb.AddForce(-e.transform.up * e.movement.movement_speed, ForceMode.Impulse);
                }
                else
                {
                    //l'oggetto puo' essere dstrutto
                    e.movement.destroyable = true;
                }
            }
        }
    }
}