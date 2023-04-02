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
        
    }

    void ButtonClicked()
    {
        SceneManager.LoadScene("Stage 1");
    }
}
