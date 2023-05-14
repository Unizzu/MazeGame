using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectUIBehavior : MonoBehaviour
{
    [SerializeField] private Button[] Buttons = new Button[4];
    
    // Start is called before the first frame update
    void Start()
    {
        Buttons[0].onClick.AddListener(TutPlay);
        Buttons[1].onClick.AddListener(S1Play);
        Buttons[2].onClick.AddListener(S2Play);
        Buttons[3].onClick.AddListener(S3Play);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TutPlay()
    {
        SceneManager.LoadScene("Tutorial");
    }

    void S1Play()
    {
        SceneManager.LoadScene("Stage Level 1");
    }
    void S2Play()
    {
        SceneManager.LoadScene("Stage Level 2");
    }
    void S3Play()
    {
        SceneManager.LoadScene("Stage Level 3");
    }
}
