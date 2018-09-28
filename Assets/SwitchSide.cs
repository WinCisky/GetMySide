using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SwitchSide : MonoBehaviour
{

    //main camera (useless so far)
    public Camera cam;
    //collider to be resized during rotation
    public BoxCollider coll;
    //velocita' di movimento
    public float speed = 3;
    //effect used when there's a dimension switch
    public GameObject explosionEffect;
    //normal ship shader
    public Shader normal_shader;
    //invincible ship shader
    public Shader invincible_shader;
    //ship material
    public Material ship_material;
    //list of cam for all directions
    public GameObject[] cameras;
    //size of camera movement delimiter
    public BoxCollider camera_delimiter;
    //can the player move?
    public bool can_move;
    //child used for rotation
    public Transform player_to_rotate_z, player_to_rotate_y;
    //post processing profile
    public PostProcessProfile post_process_volume;
    //central point
    public Transform central_point;

    //point to rotate around
    private Vector3 center_of_rotation;
    //only 1 rotation at a time
    private bool is_rotating = false;
    //last time the user touched the screen
    private float last_tap_time;
    //position of the last tap
    private Vector2 last_tap_pos;
    //cd of dimension swap
    private float dim_swap_cd;
    //limit the player movement
    private bool can_go_left, can_go_right;

    //precedent position used for deltaMovement
    private Vector3 last_pos;
    //possible rotations
    private float[] rotations;
    //possible movement directions
    private Vector3[] direction;
    //current rotation
    private int i = 3;
    //chromatica aberr modifier
    ChromaticAberration chromatic_aberr;

    //modifiche imminenti
    private void Awake()
    {
        //reimposto lo shader dell'astronave
        ship_material.shader = normal_shader;
        can_move = false;
        last_pos = Vector3.zero;

        chromatic_aberr = null;
        post_process_volume.TryGetSettings(out chromatic_aberr);
        chromatic_aberr.intensity.value = 0.2f;
    }

    // Use this for initialization
    void Start()
    {
        //inizializzazione delle possibili rotaioni
        rotations = new float[4];
        rotations[0] = -90;
        rotations[1] = -180;
        rotations[2] = -270;
        rotations[3] = -360; // 0
        direction = new Vector3[4];
        direction[0] = -transform.right;
        direction[1] = transform.right;
        direction[2] = -transform.right;
        direction[3] = transform.right;
        center_of_rotation = transform.position + transform.forward * 3;
        //StartCoroutine(Rotate());
        last_tap_time = Time.time;
        last_tap_pos = Vector2.zero;
        dim_swap_cd = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        can_go_left = false;
        can_go_right = false;
        //determino se il giocatore si puo' muovere a destra e/o sinistra
        switch (i)
        {
            case 0:
                if (transform.position.z > -29)
                    can_go_left = true;
                if (transform.position.z < 29)
                    can_go_right = true;
                break;
            case 1:
                if (transform.position.x < 29)
                    can_go_left = true;
                if (transform.position.x > -29)
                    can_go_right = true;
                break;
            case 2:
                if (transform.position.z < 29)
                    can_go_left = true;
                if (transform.position.z > -29)
                    can_go_right = true;
                break;
            case 3:
                if (transform.position.x > -29)
                    can_go_left = true;
                if (transform.position.x < 29)
                    can_go_right = true;
                break;
        }

        //Debug.Log(i % direction.Length + " " + (i + 1) % direction.Length);
       

        if (can_move)
        {
            //INPUT PC
            if (Input.GetKeyDown(KeyCode.Space))
                if (Time.time - dim_swap_cd > 10)
                {
                    StartCoroutine(Rotate());
                    dim_swap_cd = Time.time;
                }
            if (Input.GetKey(KeyCode.A))
                if (can_go_left)
                    transform.Translate(direction[(i * 2) % direction.Length] * speed * Time.deltaTime);
            if (Input.GetKey(KeyCode.S))
                if (can_go_right)
                    transform.Translate(direction[(i * 2 + 1) % direction.Length] * speed * Time.deltaTime);
            //INPUT PHONE
            //interazione dell'utente
            if (Input.touchCount > 0)
            {
                Vector2 start_pos = Vector3.zero;
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    /*
                    //due tocchi in poco tempo e vicini
                    if (Input.GetTouch(0).tapCount == 2 && Vector2.Distance(last_tap_pos, Input.GetTouch(0).position) < 20)
                        if (Time.time - dim_swap_cd > 10)
                        {
                            StartCoroutine(Rotate());
                            dim_swap_cd = Time.time;
                        }
                    last_tap_pos = Input.GetTouch(0).position;
                    */
                    //punto in cui incomincio a toccare lo schermo
                    start_pos = Input.GetTouch(0).position;
                }
                else if(Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    if((Input.GetTouch(0).position-start_pos).magnitude > 10)
                    {
                        //c'e' stato uno swipe
                        if (Time.time - dim_swap_cd > 10) // posso ruotare
                        {
                            StartCoroutine(Rotate());
                            dim_swap_cd = Time.time;
                        }
                    }
                }
                //movimento dell'utente
                if (Input.GetTouch(0).position.x > (Screen.width / 2))
                {
                    if (can_go_left)
                        transform.Translate(-direction[(i * 2) % direction.Length] * speed * Time.deltaTime);
                }
                else
                {
                    if (can_go_right)
                        transform.Translate(-direction[(i * 2 + 1) % direction.Length] * speed * Time.deltaTime);
                }
            }

            //Ship rotation (it's easier than you think)
            //goal: tilt the ship (x & z axes) in order to follow the moving direction
            //solution : I divided the ship in the renderer and the collider,
            //the collider only rotates z axe, the renderer rotates z and y axes

            float multiplier_z = 8;
            float multiplier_y = 6;
            Vector3 offset = Vector3.zero;
            Vector3 delta_movement = last_pos - transform.position;
            float deltaT = Time.deltaTime;
            if (deltaT != 0)
            {
                switch ((i + 1) % 4)
                {
                    case 0: // front

                        player_to_rotate_z.transform.rotation = Quaternion.LerpUnclamped(
                        player_to_rotate_z.transform.rotation,
                        Quaternion.Euler(
                            0,
                            0,
                            (delta_movement.x * multiplier_z) / deltaT)
                        , deltaT * 20);
                        player_to_rotate_y.transform.rotation = Quaternion.LerpUnclamped(
                        player_to_rotate_y.transform.rotation,
                        Quaternion.Euler(
                            0,
                            -(delta_movement.x * multiplier_y) / deltaT,
                            (delta_movement.x * multiplier_z) / deltaT)
                        , deltaT * 20);
                        break;
                    case 1: //right
                        player_to_rotate_z.transform.rotation = Quaternion.LerpUnclamped(
                        player_to_rotate_z.transform.rotation,
                        Quaternion.Euler(
                            0,
                            -90,
                            (delta_movement.z * multiplier_z) / deltaT)
                        , deltaT * 20);
                        player_to_rotate_y.transform.rotation = Quaternion.LerpUnclamped(
                        player_to_rotate_y.transform.rotation,
                        Quaternion.Euler(
                            0,
                            -90 - (delta_movement.z * multiplier_y) / deltaT,
                            (delta_movement.z * multiplier_z) / deltaT)
                        , deltaT * 20);
                        break;
                    case 2: //behind
                        player_to_rotate_z.transform.rotation = Quaternion.LerpUnclamped(
                        player_to_rotate_z.transform.rotation,
                        Quaternion.Euler(
                            0,
                            -180,
                            -(delta_movement.x * multiplier_z) / deltaT)
                        , deltaT * 20);
                        player_to_rotate_y.transform.rotation = Quaternion.LerpUnclamped(
                        player_to_rotate_y.transform.rotation,
                        Quaternion.Euler(
                            0,
                            -(delta_movement.x * multiplier_y) / deltaT,
                            (delta_movement.x * multiplier_z) / deltaT)
                        , deltaT * 20);
                        break;
                    case 3: //left
                        player_to_rotate_z.transform.rotation = Quaternion.LerpUnclamped(
                        player_to_rotate_z.transform.rotation,
                        Quaternion.Euler(
                            0,
                            -270,
                            -(delta_movement.z * multiplier_z) / deltaT)
                        , deltaT * 20);
                        player_to_rotate_y.transform.rotation = Quaternion.LerpUnclamped(
                        player_to_rotate_y.transform.rotation,
                        Quaternion.Euler(
                            0,
                            -90 - (delta_movement.z * multiplier_y) / deltaT,
                            (delta_movement.z * multiplier_z) / deltaT)
                        , deltaT * 20);
                        break;
                }
                last_pos = transform.position;
            }
        }
    }
    /*
    //modifico la posizione delle videocamere virtuali in modo da avere una miglio transizione quando cambio dimensione
    private void FixedUpdate()
    {
        cameras[0].transform.position = new Vector3(transform.position.x, 6, -45 + transform.position.z);
        cameras[1].transform.position = new Vector3(45 + transform.position.x, 6, transform.position.z);
        cameras[2].transform.position = new Vector3(transform.position.x, 6, 45 + transform.position.z);
        cameras[3].transform.position = new Vector3(-45 + transform.position.x, 6, transform.position.z);
    }
    */

    IEnumerator Rotate()
    {
        //se stava gia' ruotando attendo che la rotazione finisca
        while (is_rotating)
            yield return new WaitForFixedUpdate();
        is_rotating = true;
        can_move = false;
        chromatic_aberr.intensity.value = 1;
        //mostro l'effetto
        explosionEffect.transform.position = transform.position;
        explosionEffect.GetComponent<ParticleSystem>().Play();
        //allontano gli oggetti vicini
        //esplosione ad 1 dimensione (quella attiva)
        Vector3 explosion_pos = transform.position;

        Collider[] colliders = Physics.OverlapSphere(transform.position, Mathf.Infinity);
        foreach (Collider item in colliders)
        {
            Rigidbody rb = item.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if ((i % 2) == 0)
                    explosion_pos.z = rb.transform.position.z;
                else
                    explosion_pos.x = rb.transform.position.x;
                rb.angularVelocity = Vector3.zero;
                rb.velocity = Vector3.zero;
                rb.AddExplosionForce(800, explosion_pos, Mathf.Infinity);
            }
        }

        //slow-motion
        Time.timeScale = 0f;

        //riduco lo spazio di collisione
        coll.size = Vector3.one;

        i = (i + 1) % rotations.Length;

        cameras[i].SetActive(false); switch (i)
        {
            case 3:
                cameras[0].transform.position = Vector3.zero;
                break;
            case 0:
                cameras[1].transform.position = Vector3.zero;
                break;
            case 1:
                cameras[2].transform.position = Vector3.zero;
                break;
            case 2:
                cameras[3].transform.position = Vector3.zero;
                break;
        }
        cameras[(i + 1) % cameras.Length].SetActive(true);        

        central_point.position = new Vector3(central_point.position.x, cameras[0].transform.position.y, central_point.transform.position.z);

        //cameras[(i + 1) % cameras.Length].GetComponent<Cinemachine.CinemachineVirtualCamera>().LookAt = central_point;

        //cambio le dimensioni del delimitatore della camera (nell'asse in cui sto guardando)
        //in modo da mostrare tutti gli oggetti
        if ((i % 2) == 0)
            camera_delimiter.size = new Vector3(60, 60, 91);
        else
            camera_delimiter.size = new Vector3(91, 60, 60);
        //TEST
        bool finished = false;
        while (!finished)
        {
            float old_value = transform.rotation.y;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, rotations[i], 0), Time.unscaledDeltaTime * 60);
            //cameras[(i+1)%cameras.Length].transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, rotations[i], 0), Time.unscaledDeltaTime * 60);
            //stessa rotaione per 2 frame -> ho finito di girare
            if (transform.rotation.y == old_value)
                finished = true;
            yield return new WaitForEndOfFrame();
        }
        if (i == 3)
            transform.rotation = Quaternion.identity;
        coll.size = new Vector3(1, 1, 100);

        cameras[(i + 1) % cameras.Length].GetComponent<Cinemachine.CinemachineVirtualCamera>().LookAt = null;
        Debug.Log(i);
        switch (i)
        {
            case 0:
                cameras[1].transform.position = new Vector3(45, 6, transform.position.z);
                Debug.Log(cameras[1].transform.position + cameras[1].name);
                break;
            case 1:
                cameras[2].transform.position = new Vector3(transform.position.x, 6, 45);
                Debug.Log(cameras[2].transform.position + cameras[2].name);
                break;
            case 2:
                cameras[3].transform.position = new Vector3(-45, 6, transform.position.z);
                Debug.Log(cameras[3].transform.position + cameras[3].name);
                break;
            case 3:
                cameras[0].transform.position = new Vector3(transform.position.x, 6, -45);
                Debug.Log(cameras[0].transform.position + cameras[0].name);
                break;
        }


        //rendo invincibile l'astronave
        StartCoroutine(invincibility(3));

        //normal speed
        Time.timeScale = 1;

        //end
        chromatic_aberr.intensity.value = 0.2f;
        is_rotating = false;
        can_move = true;
        yield return null;
    }

    private IEnumerator invincibility(float seconds)
    {
        coll.enabled = false;
        //attivo animazione per fare capire al giocatore che l'oggetto e' invulnerabile
        ship_material.shader = invincible_shader;
        yield return new WaitForSeconds(seconds);
        coll.enabled = true;
        //distattivo l'animazione
        ship_material.shader = normal_shader;
    }
}
