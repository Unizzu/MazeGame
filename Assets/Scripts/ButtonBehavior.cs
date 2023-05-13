using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    [SerializeField] private string targetColor;
    private Animator anim;

    public string ButtonColor;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pressed()
    {
        anim.SetBool("IsPressed", true);
        foreach (GameObject blocks in GameObject.FindGameObjectsWithTag(targetColor))
        {
            ButtonBlockBehavior blockScript = blocks.GetComponent<ButtonBlockBehavior>();
            blockScript.ButtonSwitch();
        }
        foreach (GameObject buttons in GameObject.FindGameObjectsWithTag("Button"))
        {
            ButtonBehavior bb = buttons.GetComponent<ButtonBehavior>();
            if(ButtonColor == bb.ButtonColor)
            {
                bb.Switch();
            }
        }
    }

    public void Released()
    {
        anim.SetBool("IsPressed", false);
    }

    public void Switch()
    {
        if (anim.GetBool("IsOn"))
            anim.SetBool("IsOn", false);
        else
            anim.SetBool("IsOn", true);
    }
}
