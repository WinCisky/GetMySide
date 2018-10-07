using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLogic : MonoBehaviour {

    public static ShootLogic SL;
    public bool can_shoot = false;
    public GameObject ship_orientation;
    public GameObject laser1, laser2;
    public AnimationClip shoot_anim, idle_shoot_anim;
    public GameObject beam;
    private List<GameObject> beanpool;

    private void Awake()
    {
        SL = this;
    }

    // Use this for initialization
    void Start () {
        beanpool = new List<GameObject>();
        for (int i = 0; i < 20; i++)
        {
            GameObject obj = Instantiate(beam);
            obj.GetComponent<MovementAssistantBullet>().reset = true;
            obj.GetComponent<MovementAssistantBullet>().usable = false;
            beanpool.Add(obj);
        }
        /*
        StartCoroutine(shootl1());
        StartCoroutine(shootl2());
        */
    }

    public void StartShooting()
    {
        StartCoroutine(shootl1());
        StartCoroutine(shootl2());
    }


    //riposiziono il raggio gli do la rotazione del giocatore e lo lascio andare
    private void ShootBeam(Transform origin)
    {
        bool found = false;
        for (int i = 0; i < beanpool.Count && !found; i++)
        {
            if (beanpool[i].GetComponent<MovementAssistantBullet>().usable)
            {
                beanpool[i].GetComponent<MovementAssistantBullet>().usable = false;
                beanpool[i].GetComponent<MovementAssistantBullet>().reset = false;
                beanpool[i].transform.position = origin.position;
                //inverto la z nella seconda e terza dim
                int rev = 1;
                if (SwitchSide.SS.i == 0 || SwitchSide.SS.i == 1)
                    rev = -1;
                beanpool[i].transform.rotation = Quaternion.Euler(
                    0,
                    ((SwitchSide.SS.i + 1) % 4) * 90, //ok
                    ship_orientation.transform.eulerAngles.z * rev); //ok
                found = true;
            }
        }
    }

    public IEnumerator shootl1()
    {
        laser1.GetComponent<Animation>().PlayQueued("shoot_animation");
        laser1.GetComponent<Animation>().PlayQueued("idle_shoot");
        yield return new WaitUntil(() => laser1.GetComponent<Animation>().IsPlaying("idle_shoot"));
        //Debug.Log("oky oky " + Time.time);
        ShootBeam(laser1.transform);
        //i can shoot angain
        yield return new WaitForSeconds(1);
        if (can_shoot)
        {
            StartCoroutine(shootl1());
        }
    }

    public IEnumerator shootl2()
    {
        laser2.GetComponent<Animation>().PlayQueued("shoot_animation");
        laser2.GetComponent<Animation>().PlayQueued("idle_shoot");
        yield return new WaitUntil(() => laser2.GetComponent<Animation>().IsPlaying("idle_shoot"));
        //Debug.Log("oky oky " + Time.time);
        ShootBeam(laser2.transform);
        //i can shoot angain
        yield return new WaitForSeconds(1);
        if (can_shoot)
        {
            StartCoroutine(shootl2());
        }
    }
}
