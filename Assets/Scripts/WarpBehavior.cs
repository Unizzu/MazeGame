using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpBehavior : MonoBehaviour
{
    private string warpTag;
    private GameObject otherWarp;


    // Start is called before the first frame update
    void Start()
    {
        warpTag = gameObject.tag;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void findOtherWarp()
    {
        foreach (GameObject warps in GameObject.FindGameObjectsWithTag(warpTag))
        {
            if (warps != this.gameObject)
            {
                otherWarp = warps;
                break;
            }
        }
    }

    public void Teleport(Transform player)
    {
        player.position = otherWarp.transform.position;
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return null;
    }
}
