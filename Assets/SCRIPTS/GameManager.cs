using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Cinemachine;

public class GameManager : MonoBehaviour {

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

    //mesh degli oggetti
    public Mesh[] asteroids_meshes;

    //pool di oggetti
    private List<GameObject> 
        asteroids, //level 1
        mines, //level 2
        explosions, //level 2 support
        shooting_ships, //level 3
        enemy_ships; //level 4

    // Use this for initialization
    void Awake () {
        //Inizializzo il riferimento
        GM = this;
        //disattivo lo scudo di default
        isShieldUp = false;
        shieldCooldown = Mathf.Infinity;

        //just for testing TO REMOVE
        PlayerPrefs.SetInt("level", 1);

        //imposto il livello in base a quello raggiunto
        level_to_start = PlayerPrefs.GetInt("level", 1);
	}
	
	// Update is called once per frame
	void Update () {
		
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
                SpawnStuff(0, 70);
                for (int i = 0; i < 8; i++) // 2 mins
                {
                    SpawnStuff(0, 10);
                    yield return new WaitForSeconds(15);
                }
                DeleteStuff(0, 30);
                break;
            case 2:
                if (asteroids.Count == 0)
                    SpawnStuff(0, 120);
                level_progress_starter.SetActive(true);
                SpawnStuff(1, 10);
                for (int i = 0; i < 8; i++) // 2 mins
                {
                    SpawnStuff(1, 3);
                    yield return new WaitForSeconds(15);
                }
                //DeleteStuff(0, 30);
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

    private void SpawnStuff(int index, int amount)
    {
        switch (index)
        {
            case 0:
                for (int i = 0; i < amount; i++)
                {
                    //creo l'oggetto
                    GameObject go = Instantiate(asteroid);
                    //imposto il padre
                    go.transform.parent = asteroid_father.transform;
                    float scale = Random.Range(3, 5);
                    go.GetComponentInChildren<MovementAssistantComet>().movement_speed = Mathf.Lerp(5, 3, scale);
                    go.GetComponentInChildren<MovementAssistantComet>().rotating_speed = Vector3.zero;
                    go.transform.position = new Vector3(0, -50, 0);
                    asteroids.Add(go);
                }
                break;
            case 1:
                for (int i = 0; i < amount; i++)
                {
                    //TODO
                }
                break;
            default:
                break;
        }
    }

    private void DeleteStuff(int index, int percent)
    {
        switch (index)
        {
            case 0:
                for (int i = 0; i < asteroids.Count; i++)
                {
                    if (Random.Range(0, 100) < percent) // cancello una percentuale degli oggetti
                        asteroids[i].GetComponent<MovementAssistantComet>().toDestroy = true;
                }
                StartCoroutine(GarbageDeleter(0, asteroids));
                break;
            default:
                break;
        }
    }

    //cancella gli oggetti usciti di scena
    IEnumerator GarbageDeleter(int index, List<GameObject> list)
    {
        yield return new WaitForSeconds(10);
        switch (index)
        {
            case 0:
                //1 min, cancello ogni 15 s
                for (int i = 0; i < 4; i++)
                {
                    foreach (var item in list)
                    {
                        if (item.GetComponent<MovementAssistantComet>().destroyable)
                        {
                            list.Remove(item);
                            Destroy(item);
                        }
                    }
                    yield return new WaitForSeconds(15);
                }
                break;
            default:
                break;
        }
        yield return null;
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
