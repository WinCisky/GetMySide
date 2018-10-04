using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class MovementAssistantMine : MonoBehaviour
{
    public float margin_of_explosion;
    public Vector3 rotating_speed;
    public Rigidbody rb;
    public bool toDestroy;
    public bool destroyable;
    public float movement_speed;
    public GameObject explosion;
}

//Hybrid ECS
public class MovementSystemMine : ComponentSystem
{
    struct ComponentsComet
    {
        public MovementAssistantMine movement;
        public Transform transform;
    }

    protected override void OnUpdate()
    {
        float fixedDeltaTime = Time.deltaTime;
        //float point_of_explosion = GameManager.GM.mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, 0)).y;
        float point_of_explosion = GameManager.GM.player.transform.position.y;
        //modifica parametri per le singole entita'
        foreach (var e in GetEntities<ComponentsComet>())
        {
            //rotazione
            e.transform.rotation *= Quaternion.Euler(
                e.movement.rotating_speed.x,
                e.movement.rotating_speed.y,
                e.movement.rotating_speed.z);
            if (e.transform.position.y < (point_of_explosion + e.movement.margin_of_explosion))
            {
                //esplodo
                //muovo gli oggetti vicini
                //Collider[] colliders = Physics.OverlapSphere(e.transform.position, Mathf.Infinity);
                //mostro il particle system
                //resetto la posizione
                //cancello le forze
                //resetto la rotaione
                //aggiungo le forze

                //esplosione
                //Debug.Log(e.movement.explosion.GetComponent<Renderer>().material.GetFloat("Vector1_17CCCFF2"));
                //e.movement.explosion.GetComponent<Renderer>().material.SetFloat("Vector1_17CCCFF2", Time.time);

                //attivo l'oggetto
                e.movement.explosion.SetActive(true);
                //attivo l'esplosione
                e.movement.explosion.GetComponent<ShowExplosion>().StartExplosion();
                //reimposto la posizione dell'esplosione
                e.movement.explosion.transform.position = e.transform.position;
                //tolgo la mina
                e.transform.position = new Vector3(Random.Range(-40, 40), Random.Range(40, 90), Random.Range(-40, 40));
            }
            else
            {
                //muovo la mina
                e.transform.position += new Vector3(0, -fixedDeltaTime * e.movement.movement_speed, 0);
            }
        }
    }
}