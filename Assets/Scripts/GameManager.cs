using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    
    [Header("Settings")]
    public float globalVolume=1f;
    public float musicVolume=1f;
    public float soundEffectsVolume=1f;

    public static bool gamePaused = false;
    [Space(16)]
    public bool filmVolumeEnebled=false;
    public bool bearTheKillerEnebled=false;
    public bool treeDVolumeEnebled=false;
    [Space(24)]
    [Header("World")]
    public int score;
    public int Highscore;
    [HideInInspector]
    public float time;

    public Text timeText;
    public Text scoreText;
    public Text highScoreText;

    [Header("Manager Settings")]
    [SerializeField] bool world=true;


    float seconds;
    [Header("Ui")]
    public GameObject[] panels;
    public GameObject PausePanel;
    public Slider[] slidersVolume;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private AudioSource clickAudio;

    [SerializeField] private GameObject[] volumes;
    private int currentVolumeIndex;
    private void Start()
    {
        SetVolume(0, true);
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.buildIndex == 0) PlayerPrefs.SetInt("score", 0);
        globalVolume = PlayerPrefs.GetFloat("globalVolume");
        musicVolume = PlayerPrefs.GetFloat("musicVolume");
        soundEffectsVolume = PlayerPrefs.GetFloat("soundVolume");
        slidersVolume[0].value = globalVolume;
        slidersVolume[1].value = musicVolume;
        slidersVolume[2].value = soundEffectsVolume;
        Highscore =PlayerPrefs.GetInt("highscore");
        if(!world)highScoreText.text = "highscore:"+Highscore.ToString(); LockLevel(0);
        score = PlayerPrefs.GetInt("score");
    }
    private void Update()
    {
        EnableVolume();
        seconds = Mathf.FloorToInt(time); // old: time % 60
        if (world)
        {
            time -= Time.deltaTime;
            timeText.text = seconds.ToString();
            scoreText.text = score.ToString();
            if(score > Highscore) Highscore = score;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gamePaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
            PlayerPrefs.SetInt("score", score);
            if (time <= 0) GameOver();
        }
        else
        {

        }
        PlayerPrefs.SetInt("highscore", Highscore);
        PlayerPrefs.SetFloat("globalVolume", globalVolume);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("soundVolume", soundEffectsVolume);


        if(time < 0 && world) GameOver();
    }
    
    public void Resume()
    {
        clickAudio.Play();
        gamePaused = false;
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        clickAudio.Play();
        gamePaused =true;
        PausePanel.SetActive(true);
        Time.timeScale=0f;
    }
    public void GameOver()
    {
        deathScreen.SetActive(true);
        score = 0;
        PlayerPrefs.SetInt("score", 0);
        Destroy(FindObjectOfType<Player>());
    }

    public void LoadScene(int scene)
    {
        clickAudio.Play();
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }
    public void LoadPanel(int panel)
    {
        clickAudio.Play();
        for (int i = 0; i < panels.Length; i++)
        {
            if(i == panel)
            {
                panels[i].gameObject.SetActive(true);
            }
            else
            {
                panels[i].gameObject.SetActive(false);
            }
        }
    }
    public void ExitGame()
    {
        clickAudio.Play();
        Application.Quit();
    }
    public void OpenURL(string url)
    {
        clickAudio.Play();
        Application.OpenURL(url);
    }
    IEnumerator LoadScene(string scene)
    {

        SceneManager.LoadScene(scene);
        yield return new WaitForSeconds(1);    
    }
    public void SetGlobalVolume(Slider slider)
    {
        globalVolume = slider.value;
    }
    public void SetMusicVolume(Slider slider)
    {
        musicVolume = slider.value;
    }
    public void SetSoundVolume(Slider slider)
    {
        soundEffectsVolume = slider.value;
    }
    
    private void SetVolume(int index,bool unEnable = false)
    {
        for (int i = 0; i < volumes.Length; i++)
        {
            if (!unEnable)
            {
                if (i == index)
                {
                    volumes[i].SetActive(true);
                }
                else
                {
                    volumes[i].SetActive(false);
                }
            }
            else
            {
                volumes[i].SetActive(false);
            }
        }
    }
    private void EnableVolume()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetVolume(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetVolume(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetVolume(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetVolume(0, true);
        }
    }

    public void SetLevel(int level)
    {
        PlayerPrefs.SetInt("currentLevelIndex", level);
        // 0 Forest
        // 1 Winter
        // 2 Desert
    }
    public void LockLevel(int locked)
    {
        PlayerPrefs.SetInt("LevelLocked", locked);
        PlayerPrefs.Save();
    }
}
