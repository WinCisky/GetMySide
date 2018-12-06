using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    //player
    public GameObject main_cube;
    //player transform
    private Transform player_pos;
    //player rigidbody
    private Rigidbody player_rb;

    //instance of cube
    public GameObject cube_instance;
    //current level
    private int level;
    //bounds of the world
    private Vector3 world_bounds = new Vector3(10, 50, 10);

    //pool of world cubes
    private List<GameObject> pool_world_objs;
    //size of the pool
    private int pool_size = 50;

    // Start is called before the first frame update
    void Start()
    {
        player_pos = main_cube.transform;
        player_rb = main_cube.GetComponent<Rigidbody>();
        level = 0;
        pool_world_objs = new List<GameObject>();
        SpawnLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //set the cubes position for the level
    private void SpawnLevel()
    {
        //creation-deactivation of cubes
        CreateResetPool();
        //set the new pos for cubes
        switch (level)
        {
            case 0:
                GetCube().transform.position = new Vector3(0, 0, 0);
                GetCube().transform.position = new Vector3(1, 0, 1);
                GetCube().transform.position = new Vector3(2, 0, 2);
                GetCube().transform.position = new Vector3(3, 0, 3);
                GetCube().transform.position = new Vector3(4, 0, 4);
                GetCube().transform.position = new Vector3(5, 0, 5);
                GetCube().transform.position = new Vector3(6, 0, 6);
                GetCube().transform.position = new Vector3(7, 0, 7);
                GetCube().transform.position = new Vector3(8, 0, 8);
                GetCube().transform.position = new Vector3(9, 0, 9);
                GetCube().transform.position = new Vector3(-1, 0, -1);
                GetCube().transform.position = new Vector3(-2, 0, -2);
                GetCube().transform.position = new Vector3(-3, 0, -3);
                GetCube().transform.position = new Vector3(-4, 0, -4);
                GetCube().transform.position = new Vector3(-5, 0, -5);
                GetCube().transform.position = new Vector3(-6, 0, -6);
                GetCube().transform.position = new Vector3(-7, 0, -7);
                GetCube().transform.position = new Vector3(-8, 0, -8);
                GetCube().transform.position = new Vector3(-9, 0, -9);
                break;
            case 1:
                break;
            case 2:
                break;

            default:
                break;
        }
    }

    //returns a cube (inactive or new)
    private GameObject GetCube()
    {
        for (int i = 0; i < pool_world_objs.Count; i++)
        {
            if (!pool_world_objs[i].activeSelf)
            {
                pool_world_objs[i].SetActive(true);
                return pool_world_objs[i];
            }
        }
        GameObject go = Instantiate(cube_instance);
        go.SetActive(true);
        pool_world_objs.Add(cube_instance);
        return go;
    }

    //add obj to pool or deactivate them
    private void CreateResetPool()
    {
        foreach (var item in pool_world_objs)
        {
            item.SetActive(false);
        }
        while(pool_world_objs.Count < pool_size)
        {
            GameObject go = Instantiate(cube_instance);
            go.SetActive(false);
            pool_world_objs.Add(cube_instance);
        }
    }

    private void FixedUpdate()
    {
        //check if player is out of world bounds
        if(
            player_pos.position.z > world_bounds.z ||
            player_pos.position.z < -world_bounds.z ||
            player_pos.position.x > world_bounds.x ||
            player_pos.position.x < -world_bounds.x ||
            player_pos.position.y > world_bounds.y ||
            player_pos.position.y < -world_bounds.y)
        {
            Respawn();
        }
    }

    //reset player position to the latest spawn point
    private void Respawn()
    {
        //currently i have not set spawn points
        player_pos.position = new Vector3(0, 2, 0);
        //remove all forces applied to the cube
        player_rb.velocity = Vector3.zero;
    }
}
