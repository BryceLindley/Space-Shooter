﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    float gameTimer = 0;
    float[] endLevelTimer = { 10, 30, 45 };
    int currentSceneNumber = 0;
    bool gameEnding = false;

    Scenes scenes;
    public enum Scenes
    {
        bootUp,
        title,
        shop,
        level1,
        level2,
        level3,
        gameOver
    }

    public MusicMode musicMode;
    public enum MusicMode
    {
        noSound, fadeDown, musicOn
    }

    void Start()
    {
        StartCoroutine(MusicVolume(MusicMode.musicOn));
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    void Update()
    {
        if (currentSceneNumber != SceneManager.GetActiveScene().buildIndex)
        {
            currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
            GetScene();
        }
        GameTimer();
    }

    IEnumerator MusicVolume(MusicMode musicMode)
    {
        switch (musicMode)
        {
            case MusicMode.noSound:
                {
                    GetComponent<AudioSource>().Stop();
                    break;
                }
            case MusicMode.fadeDown:
                {
                    GetComponentInChildren<AudioSource>().volume -= Time.deltaTime / 3;
                    break;
                }
            case MusicMode.musicOn:
                {
                    if (GetComponentInChildren<AudioSource>().clip != null)
                    {
                        GetComponentInChildren<AudioSource>().Play();
                        GetComponentInChildren<AudioSource>().volume = 1;
                    }
                    break;
                }
        }
        yield return new WaitForSeconds(0.1F);
    }

    void GetScene()
    {
        scenes = (Scenes)currentSceneNumber;
    }

    public void GameOver()
    {
        Debug.Log("ENDSCORE:" + GameManager.Instance.GetComponent<ScoreManager>().PlayersScore);
        SceneManager.LoadScene("gameOver");
    }

    void GameTimer()
    {
        switch (scenes)
        {
            case Scenes.level1:
            case Scenes.level2:
            case Scenes.level3:
                {
                    if (GetComponentInChildren<AudioSource>().clip == null)
                    {
                        AudioClip lvlMusic = Resources.Load<AudioClip>("Sound/lvlMusic") as AudioClip;
                        GetComponentInChildren<AudioSource>().clip = lvlMusic;
                        GetComponentInChildren<AudioSource>().Play();
                    }
                    if (gameTimer < endLevelTimer[currentSceneNumber - 3])
                    {
                        //if level has not completed
                        gameTimer += Time.deltaTime;
                    }
                    else
                    {
                        //if level is completed
                        StartCoroutine(MusicVolume(MusicMode.fadeDown));
                        if (!gameEnding)
                        {
                            gameEnding = true;
                            if (SceneManager.GetActiveScene().name != "level3")
                            {
                                GameObject.Find("Player").GetComponent<PlayerTransition>().LevelEnds = true;
                            }
                            else
                            {
                                GameObject.Find("Player").GetComponent<PlayerTransition>().GameCompleted = true;
                            }
                            Invoke("NextLevel", 4);
                        }
                    }
                        break;
                    }
            default:
                {
                    GetComponentInChildren<AudioSource>().clip = null;
                    break;
                }
        }
    }

    void NextLevel()
    {
        gameEnding = false;
        gameTimer = 0;
        SceneManager.LoadScene(GameManager.currentScene + 1);
        StartCoroutine(MusicVolume(MusicMode.musicOn));
    }

    public void ResetScene()
    {
        StartCoroutine(MusicVolume(MusicMode.noSound));
        gameTimer = 0;
        SceneManager.LoadScene(GameManager.currentScene);
    }

    public void BeginGame(int gameLevel)
    {
        SceneManager.LoadScene(gameLevel);
    }

    public void OnSceneLoaded(Scene aScene, LoadSceneMode aMode)
    {
        StartCoroutine(MusicVolume(MusicMode.musicOn));
        GetComponent<GameManager>().SetLivesDisplay(GameManager.playerLives);

        if (GameObject.Find("score"))
        {
            GameObject.Find("score").GetComponent<Text>().text = ScoreManager.playerScore.ToString();
        }
    }
}


