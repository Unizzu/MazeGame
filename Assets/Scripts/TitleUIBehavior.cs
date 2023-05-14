using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUIBehavior : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button tutButton;
    [SerializeField] private GameObject MusicPlayer;
    void Awake()
    {
        DontDestroyOnLoad(MusicPlayer);
    }

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(PlayButtonClicked);
        //tutButton.onClick.AddListener(TutButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayButtonClicked()
    {
        SceneManager.LoadScene("Stage Select");
    }
    /*void TutButtonClicked()
    {
        SceneManager.LoadScene("Tutorial");
    }*/
}
