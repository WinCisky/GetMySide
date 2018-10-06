using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLogic : MonoBehaviour {

    public bool can_shoot = true;
    public GameObject ship_orientation;
    public GameObject laser1, laser2;
    public AnimationClip shoot_anim, idle_shoot_anim;
    public GameObject beam;
    private List<GameObject> beanpool;

	// Use this for initialization
	void Start () {
        beanpool = new List<GameObject>();
        for (int i = 0; i < 20; i++)
        {
            GameObject obj = Instantiate(beam);
            obj.SetActive(false);
            beanpool.Add(obj);
        }
        StartCoroutine(shootl1());
        //StartCoroutine(shootl2());
    }

    private void ShootBeam(Transform origin)
    {
        bool found = false;
        for (int i = 0; i < beanpool.Count || found; i++)
        {
            if (!beanpool[i].activeInHierarchy)
            {
                beanpool[i].SetActive(true);
                beanpool[i].transform.position = origin.position;
                //TO FIX
                beanpool[i].transform.rotation = Quaternion.Euler(ship_orientation.transform.eulerAngles.x, GameManager.GM.player.transform.rotation.eulerAngles.y, 0);
                found = true;
            }
        }
    }

    public IEnumerator shootl1()
    {
        laser1.GetComponent<Animation>().PlayQueued("shoot_animation");
        laser1.GetComponent<Animation>().PlayQueued("idle_shoot");
        yield return new WaitUntil(() => laser1.GetComponent<Animation>().IsPlaying("idle_shoot"));
        Debug.Log("oky oky" + Time.time);
        //ShootBeam(laser1.transform);
        //i can shoot angain
        yield return new WaitForSeconds(10);
        if (can_shoot)
        {
            StartCoroutine(shootl1());
        }
    }

    public IEnumerator shootl2()
    {
        //yield return new WaitForSeconds(.5f);
        while (true)
        {
            if (can_shoot)
            {
                laser2.GetComponent<Animation>().PlayQueued("shoot_animation");
                laser2.GetComponent<Animation>().PlayQueued("idle_shoot");
                yield return new WaitUntil(() => laser2.GetComponent<Animation>().IsPlaying("shoot_animation"));
                Debug.Log("oky oky");
                ShootBeam(laser1.transform);
                //i can shoot angain
            }
            else //aspetto un po'
                yield return new WaitForSeconds(1);
        }
    }
}
