using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLogic : MonoBehaviour {

    public GameObject laser1, laser2;
    public Animator l1anim;

	// Use this for initialization
	void Start () {
        StartCoroutine(shoot());	
	}

    public IEnumerator shoot()
    {
        //i shoot
        l1anim.SetBool("shoot", true);
        yield return new WaitUntil(() => l1anim.GetCurrentAnimatorStateInfo(0).IsName("idle"));
        //i can shoot angain
    }
}
