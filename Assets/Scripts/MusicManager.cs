using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    AudioClip mainTheme;
    [SerializeField]
    AudioClip menuTheme;

    Scene scene;

    void Start()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    void OnLevelLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        var newSceneName = scene;

        if (newSceneName.name != this.scene.name)
        {
            this.scene = newSceneName;
            Invoke(nameof(PlayMusic), 0.2f);
        }
    }

    void PlayMusic()
    {
        AudioClip clipToPlay = null;
        if (scene.name == "Menu")
        {
            clipToPlay = menuTheme;
        }
        else if (scene.name == "Game")
        {
            clipToPlay = mainTheme;
        }

        if (clipToPlay != null)
        {
            AudioManager.Instance.PlayMusic(clipToPlay, 2);
        }
    }
}
