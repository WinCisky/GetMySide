using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowExplosion : MonoBehaviour {

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
            yield return new WaitForFixedUpdate();
        }
        //disattivo il gameObject
        gameObject.SetActive(false);
    }
}
