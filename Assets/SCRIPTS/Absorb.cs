using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(HandleAbsorption(other.gameObject));
    }

    IEnumerator HandleAbsorption(GameObject go)
    {
        switch (go.tag)
        {
            case "comet":
                break;
            case "mine":
                break;
        }
        float t1 = Time.time + 1;
        while(Time.time < t1)
        {
            yield return new WaitForFixedUpdate();
        }
    }
}
