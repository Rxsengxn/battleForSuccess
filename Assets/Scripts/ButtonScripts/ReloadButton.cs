using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadButton : MonoBehaviour
{
    // Start is called before the first frame update
    private bool gamePaused = false;
    public GameObject pauseMenu;
    private static string curPlayingSong;
    private static bool songSwitched = false;
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        AudioManager.Instance.Stop(AudioManager.Instance.GetCurPlayingSong());
        Time.timeScale = 0;
        GameManager.Instance.GameOn = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseGame()
    {
        gamePaused = !gamePaused;
        if (gamePaused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            curPlayingSong = AudioManager.Instance.GetCurPlayingSong();
            Debug.Log("lsafkmlkasmnfokasolkfasoklf" + curPlayingSong);
            AudioManager.Instance.Pause(curPlayingSong);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            
            if (songSwitched)
            {
                AudioManager.Instance.Play(curPlayingSong);
                songSwitched = false;
            }
            else
                AudioManager.Instance.Resume(curPlayingSong);
        }
    }

    public void SwitchTracks()
    {
        songSwitched = true;
        AudioManager.Instance.Stop(curPlayingSong);
        Debug.Log(curPlayingSong);
        curPlayingSong =  curPlayingSong.EndsWith('c') ? "GameMusic2" : "GameMusic";
    }
}