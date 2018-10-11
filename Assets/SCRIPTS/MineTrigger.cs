using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineTrigger : MonoBehaviour {

    //public GameObject explosion_alert;

    private void Explode()
    {
        gameObject.GetComponent<MovementAssistantMine>().toExplode = true;
        //gameObject.GetComponent<MovementAssistantMine>().explosion.GetComponent<ShowExplosion>().StartExplosion();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bullet")
            Explode();
    }
}
