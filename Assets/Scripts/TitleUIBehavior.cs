using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUIBehavior : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button tutButton;
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(PlayButtonClicked);
        tutButton.onClick.AddListener(TutButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayButtonClicked()
    {
        SceneManager.LoadScene("Stage Level 2");
    }
    void TutButtonClicked()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
