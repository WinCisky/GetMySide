using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourSetup : MonoBehaviour {

    public Color[] colors;

    public Camera mainCam;
    public Material
        asteroids,
        asteroids_anim,
        beam,
        ship,
        explosion,
        mine,
        trail;

    private void Awake()
    {
        int rand = Random.Range(0, 19);
        mainCam.backgroundColor = colors[0 + (5 * rand)];
        asteroids.SetColor("_BaseColor", colors[1 + (5 * rand)]);
        beam.SetColor("_BaseColor", colors[2 + (5 * rand)]);
        ship.SetColor("_BaseColor", colors[2 + (5 * rand)]);
        mine.SetColor("_BaseColor", colors[3 + (5 * rand)]);
        asteroids_anim.SetColor("_BaseColor", colors[3 + (5 * rand)]);
        //leave as FFF
        //explosion.SetColor("_BaseColor1", colors[1 + (5 * rand)]);
        //explosion.SetColor("_BaseColor2", colors[1 + (5 * rand)]);
        trail.SetColor("_BaseColor", colors[4 + (5 * rand)]);
    }
}
