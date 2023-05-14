using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button retryButton;

    // Start is called before the first frame update
    void Start()
    {
        retryButton.onClick.AddListener(ButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            foreach (GameObject musicplayer in GameObject.FindGameObjectsWithTag("Music"))
            {
                Destroy(musicplayer);
            }
            SceneManager.LoadScene(0);
        }
    }

    void ButtonClicked()
    {
        if(SceneManager.GetActiveScene().buildIndex + 1 == 6)
        {
            foreach(GameObject musicplayer in GameObject.FindGameObjectsWithTag("Music"))
            {
                Destroy(musicplayer);
            }
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
