using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Vector3 point_of_explosion = new Vector3(0, transform.position.y, 0);
        if(SwitchSide.SS.i % 2 == 0)
        {
            point_of_explosion.x = other.transform.position.x;
            point_of_explosion.z = GameManager.GM.player.position.z;
        }
        else
        {
            point_of_explosion.x = GameManager.GM.player.position.x;
            point_of_explosion.z = other.transform.position.z;
        }
        other.GetComponent<Rigidbody>().AddExplosionForce(10, point_of_explosion, Mathf.Infinity);
        //lascio che sia il Component a disattivare l'oggetto alla prossima iterazione
        gameObject.GetComponent<MovementAssistantBullet>().reset = true;
    }
}
