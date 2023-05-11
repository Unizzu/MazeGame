using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBlockBehavior : MonoBehaviour
{
    [SerializeField] private bool isActivated;

    private Animator anim;
    private Collider2D collid;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        collid = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("IsActive", isActivated);
        if(anim.GetBool("IsLightened"))
        {
            if (isActivated)
            {
                collid.isTrigger = false;
            }
            else
            {
                collid.isTrigger = true;
            }
        }
        else
        {
            collid.isTrigger = true;
        }
    }

    public void ButtonSwitch() 
    {
        if(isActivated)
        {
            isActivated = false;
        }
        else
        {
            isActivated = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Light")
        {
            anim.SetBool("IsLightened", true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Light")
        {
            anim.SetBool("IsLightened", false);
        }
    }
}
