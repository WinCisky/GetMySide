using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Cinemachine;

public class GameManager : MonoBehaviour
{

    //riferimento al GameManager in uso
    public static GameManager GM;
    //menu di gioco
    public Canvas
        mainMenu, // menu principale
        inGameUI, // interfaccia di gioco
        deathMenu, // menu sconfitta
        pauseMenu, // menu pausa
        endMenu; // menu fine gioco
    public TextMeshProUGUI
        inGameInfo; // informazione per l'utente
    //la posizione del giocatore
    public Transform player;
    //indicatore di avanzamento del livello
    public Slider progress_bar;
    //oggetto che fa' iniziare l'avanzamento del livello
    public GameObject level_progress_starter;
    //per abilitare il movimento
    public SwitchSide input_movement_manager;

    //momento in cui lo stage e' iniziato
    private float stageStartTime;
    //coroutine con l'avanzamento del livello
    private Coroutine levelAdvancment;
    //quando lo scudo si sara' rigenerato
    private float shieldCooldown;
    //lo scudo e' gia' attivo?
    private bool isShieldUp;
    //livello raggiunto
    public int level_to_start;
    //cinemachine front camera component
    public GameObject introCamera, firstCamera;
    //camera principale
    public Camera mainCamera;

    //oggetti per le pool
    //father e' per tenere gli oggetti ordinati
    public GameObject
        asteroid, //level 1
        asteroid_father, //level 1
        mine, //level 2
        mine_father, //level 2
        explosion_effect, //level 2 support
        explosione_effect_father,//level 2 support
        shooting_ship, //level 3
        shooting_ship_father, //level 3
        enemy_ship, //level 4
        enemy_ship_father; //level 4

    //pool di oggetti
    private List<GameObject>
        asteroids, //level 1
        mines, //level 2
        explosions, //level 2 support
        shooting_ships, //level 3
        enemy_ships; //level 4

    /*
    private struct GridStruct
    {
        public Vector3 pos; //posizione alla quale devo spawnare
        public List<GameObject> queue; //cosa devo spawnare
    }
    //posizioni per l'istanziazione
    private GridStruct[,] grid;
    */


    //list l1 : different objs
    //list l2 : different frame
    //list l3 : different pos <x,z>
    List<List<List<Vector2>>> obstacles;

    void AddNumbers(List<List<Vector2>> frames, Vector2[] numbers)
    {
        List<Vector2> tmp = new List<Vector2>();
        for (int i = 0; i < numbers.Length; i++)
        {
            tmp.Add(numbers[i]);
        }
        frames.Add(tmp);
    }

    // Use this for initialization
    void Awake()
    {

        //Testing
        obstacles = new List<List<List<Vector2>>>();
        List<List<Vector2>> frame_one = new List<List<Vector2>>();
        AddNumbers(frame_one, new[] { new Vector2(0, 0), new Vector2(20, 20) });
        AddNumbers(frame_one, new[] { new Vector2(1, 1), new Vector2(19, 19) });
        AddNumbers(frame_one, new[] { new Vector2(2, 2), new Vector2(18, 18) });
        AddNumbers(frame_one, new[] { new Vector2(3, 3), new Vector2(17, 17) });
        AddNumbers(frame_one, new[] { new Vector2(4, 4), new Vector2(16, 16) });
        AddNumbers(frame_one, new[] { new Vector2(5, 5), new Vector2(15, 15) });
        AddNumbers(frame_one, new[] { new Vector2(6, 6), new Vector2(14, 14) });
        AddNumbers(frame_one, new[] { new Vector2(7, 7), new Vector2(13, 13) });
        AddNumbers(frame_one, new[] { new Vector2(8, 8), new Vector2(12, 12) });
        AddNumbers(frame_one, new[] { new Vector2(9, 9), new Vector2(11, 11) });
        //one central
        AddNumbers(frame_one, new[] { new Vector2(9, 9), new Vector2(11, 11) });
        AddNumbers(frame_one, new[] { new Vector2(8, 8), new Vector2(12, 12) });
        AddNumbers(frame_one, new[] { new Vector2(7, 7), new Vector2(13, 13) });
        AddNumbers(frame_one, new[] { new Vector2(6, 6), new Vector2(14, 14),
            new Vector2(10, 10)});
        AddNumbers(frame_one, new[] { new Vector2(5, 5), new Vector2(15, 15),
            new Vector2(10, 10), new Vector2(11, 11), new Vector2(9, 9)});
        AddNumbers(frame_one, new[] { new Vector2(4, 4), new Vector2(16, 16),
            new Vector2(10, 10), new Vector2(11, 11), new Vector2(9, 9), new Vector2(12, 12), new Vector2(8, 8)});
        AddNumbers(frame_one, new[] { new Vector2(3, 3), new Vector2(17, 17),
            new Vector2(10, 10), new Vector2(11, 11), new Vector2(9, 9), new Vector2(12, 12), new Vector2(8, 8), new Vector2(13, 13), new Vector2(7, 7) });
        //two lateral
        //not full screen distance
        AddNumbers(frame_one, new[] { new Vector2(3, 3), new Vector2(17, 17),
            new Vector2(10, 10), new Vector2(11, 11), new Vector2(9, 9), new Vector2(12, 12), new Vector2(8, 8), new Vector2(13, 13), new Vector2(7, 7) });
        AddNumbers(frame_one, new[] { new Vector2(4, 4), new Vector2(16, 16),
            new Vector2(10, 10), new Vector2(11, 11), new Vector2(9, 9), new Vector2(12, 12), new Vector2(8, 8)});
        AddNumbers(frame_one, new[] { new Vector2(5, 5), new Vector2(15, 15),
            new Vector2(10, 10), new Vector2(11, 11), new Vector2(9, 9)});
        AddNumbers(frame_one, new[] { new Vector2(6, 6), new Vector2(14, 14),
            new Vector2(10, 10)});
        AddNumbers(frame_one, new[] { new Vector2(7, 7), new Vector2(13, 13) });
        AddNumbers(frame_one, new[] { new Vector2(8, 8), new Vector2(12, 12) });
        AddNumbers(frame_one, new[] { new Vector2(9, 9), new Vector2(11, 11) });
        //one central


        obstacles.Add(frame_one);

        Debug.Log(map(0, 0, 20, -30, 30));
        Debug.Log(map(18, 0, 20, -30, 30));
        Debug.Log(map(9, 0, 20, -30, 30));
        Debug.Log(map(10, 0, 20, -30, 30));


        //Inizializzo il riferimento
        GM = this;
        //disattivo lo scudo di default
        isShieldUp = false;
        shieldCooldown = Mathf.Infinity;

        /*
        //inizializzo la griglia
        grid = new GridStruct[20,20];
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                grid[i, j].pos = new Vector3(i - 10, 30, j - 10);
                grid[i, j].queue = new List<GameObject>();
            }
        }
        */

        //just for testing TO REMOVE
        PlayerPrefs.SetInt("level", 1);

        //imposto il livello in base a quello raggiunto
        level_to_start = PlayerPrefs.GetInt("level", 1);
    }

    // Update is called once per frame
    void Update()
    {

    }









    public void StartLevel()
    {
        mainMenu.gameObject.SetActive(false);
        inGameUI.gameObject.SetActive(true);
        asteroids = new List<GameObject>();
        mines = new List<GameObject>();
        shooting_ships = new List<GameObject>();
        enemy_ships = new List<GameObject>();
        firstCamera.SetActive(true);
        introCamera.SetActive(false);
        StartCoroutine(StartLvl());
        input_movement_manager.can_move = true;

        StartCoroutine(Spawner());
    }



    private IEnumerator StartLvl()
    {
        //mostra testo
        inGameInfo.SetText("Level " + level_to_start);
        //attendi 1s
        yield return new WaitForSeconds(2);
        //nascondi testo
        inGameInfo.SetText("");
        //controllo se il giocatore puo' sparare TODO
        if (false)
        {
            ShootLogic.SL.can_shoot = true;
            ShootLogic.SL.StartShooting();
        }
        //istanzio gli oggetti
        switch (level_to_start)
        {
            case 1:
                level_progress_starter.SetActive(true);
                //Aggiungo la sequenza di creazione degli ogetti
                //TODO
                break;
            case 2:
                //TODO
                break;
            case 3:
                //TODO
                break;
            case 4:
                //TODO
                break;
            case 5:
                //TODO
                break;
        }
    }

    private GameObject GetPoolElement(List<GameObject> list, int index_for_instantiation)
    {
        //cerco un oggetto inattivo
        foreach (var item in list)
        {
            switch (index_for_instantiation)
            {
                case 0:
                    if (item.GetComponentInChildren<MovementAssistantComet>().can_be_used)
                    {
                        item.GetComponentInChildren<MovementAssistantComet>().can_be_used = false;
                        return item;
                    }
                    break;
                default:
                    return null;
                    break;
            }
        }
        //non ho trovato nessun oggetto inattivo -> ne creo uno
        switch (index_for_instantiation)
        {
            case 0:
                //creo l'oggetto
                GameObject go = Instantiate(asteroid);
                //imposto il padre
                go.transform.parent = asteroid_father.transform;
                //imposto la posizione
                go.transform.position = new Vector3(0, -50, 0);
                //imposto come in uso
                go.GetComponentInChildren<MovementAssistantComet>().can_be_used = false;
                //aggiungo alla pool
                list.Add(go);
                //ritorno l'oggetto
                return go;
            default:
                return null;
                break;
        }
    }

    /*
    private void AddNull(int n)
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                if (grid[i, j].queue.Count < n)
                    grid[i, j].queue.Add(null);
            }
        }
    }

    //Creazione della coda
    private void AddSection()
    {
        
    }
    */

    private float map(float value, int low1, int high1, int low2, int high2)
    {
        return (low2 + (value - low1) * (high2 - low2) / (high1 - low1));
    }

    private IEnumerator Spawner()
    {
        foreach (var obj in obstacles)
        {
            foreach (var frame in obj)
            {
                foreach (var item in frame)
                {
                    GameObject go = GetPoolElement(asteroids, 0);
                    go.transform.position = new Vector3(map(item.x, 0, 20, -30, 30), 10, map(item.y, 0, 20, -30, 30));
                }
                yield return new WaitForSeconds(1);
            }
        }
    }


    public void timeStart()
    {
        levelAdvancment = StartCoroutine(updateProgressBar());
    }

    private IEnumerator updateProgressBar()
    {
        //avanzamento = 0
        stageStartTime = Time.time;
        progress_bar.value = 0;
        float progress = Time.time - stageStartTime;
        while (progress < 150) //2 min 30 s
        {
            progress_bar.value = progress;
            progress = Time.time - stageStartTime;
            yield return new WaitForFixedUpdate();
        }
        EndLevel();
    }

    public void EndLevel()
    {
        StartCoroutine(EndLvl());
    }

    private IEnumerator EndLvl()
    {
        yield return null;
        //avanzamento = 0
        //mostra testo
        inGameInfo.SetText("Level Clear");
        //attendi 1s
        yield return new WaitForSeconds(3);
        //nascondi testo
        inGameInfo.SetText("");
        //incremento il livello e lo salvo
        level_to_start += 1;
        if (PlayerPrefs.GetInt("level", 1) < level_to_start)
            PlayerPrefs.SetInt("level", level_to_start);
        switch (level_to_start)
        {
            case 2:

                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            default:
                break;
        }
        //avvio il livello successivo
        StartCoroutine(StartLvl());
    }

    public void GameOver()
    {
        input_movement_manager.can_move = false;
        //fine del gioco
        Time.timeScale = 0;
        //mostro il menu
        inGameUI.gameObject.SetActive(false);
        deathMenu.gameObject.SetActive(true);
    }

    //TO FIX
    public void PlayAgain()
    {
        //elimino gli oggetti
        //(potrebbero essere riutilizzati (FOR FUTURE UPDATE)
        foreach (var item in asteroids)
        {
            Destroy(item);
        }
        foreach (var item in mines)
        {
            Destroy(item);
        }
        foreach (var item in shooting_ships)
        {
            Destroy(item);
        }
        foreach (var item in enemy_ships)
        {
            Destroy(item);
        }
        //reimposta la vita del giocatore
        //TODO
        //attivo il menu di gioco
        deathMenu.gameObject.SetActive(false);
        inGameUI.gameObject.SetActive(true);
        //avvio il livello
        StopCoroutine(levelAdvancment);
        progress_bar.value = 0;
        level_progress_starter.SetActive(true);
        Time.timeScale = 1;
        input_movement_manager.can_move = true;
        StartCoroutine(StartLvl());
    }

    public void Pause()
    {
        Time.timeScale = 0;
        inGameUI.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
        inGameUI.gameObject.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
