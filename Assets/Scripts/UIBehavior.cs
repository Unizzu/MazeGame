using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBehavior : MonoBehaviour
{
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        text.SetActive(false);
        button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateGoalUI()
    {
        text.SetActive(true);
        button.SetActive(true);
    }
}
