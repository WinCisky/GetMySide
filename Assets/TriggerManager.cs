using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour {

    //simple logic : if something collide -> game over
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("killed by: " + other.name);
        GameManager.GM.GameOver();
    }
}
