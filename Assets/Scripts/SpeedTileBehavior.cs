using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTileBehavior : MonoBehaviour
{
    public bool isBuff;

    private Animator anim;
    private bool isActive = true;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("buff", isBuff);
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isActivated", isActive);
        
    }

    public bool giveEffect()
    {
        StartCoroutine(Deactivate());
        return isBuff;
    }

    public bool checkActivation()
    {
        return isActive;
    }

    IEnumerator Deactivate()
    {
        isActive = false;
        yield return new WaitForSeconds(5f);
        isActive = true;
    }

    /*private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Light")
        {
            anim.SetBool("isLightened", true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Light")
        {
            anim.SetBool("isLightened", false);
        }
    }*/
}
