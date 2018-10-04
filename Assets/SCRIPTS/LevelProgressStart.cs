using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgressStart : MonoBehaviour {

    public Camera cam;

	// Use this for initialization
	void Start () {

        transform.position = new Vector3(0, cam.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y, 0);
		
	}

    private void OnTriggerEnter(Collider other)
    {
        switch (GameManager.GM.level_to_start)
        {
            case 1:
                if (other.tag == "asteroid")
                {
                    //quando un asteroide attraversa l'oggetto
                    GameManager.GM.timeStart();
                    gameObject.SetActive(false);
                }                    
                break;
            case 2:
                if (other.tag == "mine")
                {
                    //quando una mina attraversa l'oggetto
                    GameManager.GM.timeStart();
                    gameObject.SetActive(false);
                }
                break;
                //TO ADD MORE LEVELS
        }
    }
}
