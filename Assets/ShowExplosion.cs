using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowExplosion : MonoBehaviour
{

    public void StartExplosion()
    {
        StartCoroutine(AdvanceExplosionTime());
    }

    private IEnumerator AdvanceExplosionTime()
    {
        //imposto l'avanzamento
        float advancment = 0;
        while (advancment < 3)
        {
            advancment += Time.fixedDeltaTime;
            gameObject.GetComponent<Renderer>().material.SetFloat("Vector1_17CCCFF2", advancment);
            float distance;
            yield return new WaitForFixedUpdate();
            if (advancment > 0.2f && advancment < 1)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, Mathf.Infinity);
                if (((SwitchSide.SS.i + 1) % 2) == 0)
                {
                    distance = new Vector2(
                        GameManager.GM.player.transform.position.x - transform.position.x,
                        GameManager.GM.player.transform.position.y - transform.position.y).magnitude;
                    //Debug.Log(distance);
                    if (distance < 4)
                    {
                        //the player has been hit by the bomb explosion
                        Debug.Log("hit");
                        GameManager.GM.GameOver();
                    }
                    foreach (var item in colliders)
                    {
                        Rigidbody rb = item.GetComponent<Rigidbody>();
                        if(rb)
                            item.GetComponent<Rigidbody>().AddExplosionForce(1, new Vector3(
                            transform.position.x, transform.position.y, item.transform.position.z), 25);
                    }
                    /*
                    else if (distance < 10)
                    {
                        GameManager.GM.playerRb.AddExplosionForce(3, new Vector3(transform.position.x, 0, GameManager.GM.player.transform.position.z), 10);
                        StartCoroutine(RemoveExplosionForces());
                    }
                    */
                }
                else
                {
                    distance = new Vector2(
                        GameManager.GM.player.transform.position.y - transform.position.y,
                        GameManager.GM.player.transform.position.z - transform.position.z).magnitude;
                    //Debug.Log(distance);
                    if (distance < 4)
                    {
                        //the player has been hit by the bomb explosion
                        Debug.Log("hit");
                        GameManager.GM.GameOver();
                    }
                    foreach (var item in colliders)
                    {
                        Rigidbody rb = item.GetComponent<Rigidbody>();
                        if (rb)
                            item.GetComponent<Rigidbody>().AddExplosionForce(1, new Vector3(
                            item.transform.position.x, transform.position.y, transform.position.z), 25);
                    }
                    /*
                    else if (distance < 10)
                    {
                        GameManager.GM.playerRb.AddExplosionForce(3, new Vector3(GameManager.GM.player.transform.position.x, 0, transform.position.z), 10);
                        StartCoroutine(RemoveExplosionForces());
                    }
                    */
                }
            }
        }
        //disattivo il gameObject
        gameObject.SetActive(false);
    }

    private IEnumerator RemoveExplosionForces()
    {
        yield return new WaitForSeconds(2);
        //GameManager.GM.playerRb.velocity = Vector3.zero;
        //GameManager.GM.playerRb.angularVelocity = Vector3.zero;
    }
}
