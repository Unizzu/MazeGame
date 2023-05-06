using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTileBehavior : MonoBehaviour
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
        anim.SetBool("isActive", isActive);

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
}
