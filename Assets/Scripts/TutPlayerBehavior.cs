using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutPlayerBehavior : MonoBehaviour
{
    [SerializeField] private UIBehavior uibehavior;
    [SerializeField] private SpawnItems spawnitems;
    [SerializeField] private TMP_Text topDisplayText;
    [SerializeField] private TMP_Text bottomDisplayText;
    [SerializeField] private TMP_Text keyNumText;
    [SerializeField] private GameObject lighter;
    [SerializeField] private AudioClip[] sounds = new AudioClip[4];
    public float speed = 1.5f;
    public int keynum = 0;
    private float XTrans;
    private float YTrans;
    private bool goalReached = false;
    private bool isStarted = false;
    private bool warpCooldown = false;

    private bool interactedKey = false;
    private bool interactedLock = false;
    private bool interactedBulkLock = false;
    private bool interactedButton = false;
    private bool interactedWarp = false;
    private bool isLoading = true;

    private Collider2D collid;
    private Collider2D lightcol;
    private Rigidbody2D rb;
    private AudioSource playeraudio;
    private Animator anim;
    //private Collider2D lightcol;
    // Start is called before the first frame update
    void Start()
    {
        topDisplayText.text = "Welcome to the Tutorial Level!";
        bottomDisplayText.text = "Loading...";
        collid = GetComponent<Collider2D>();
        collid.enabled = false;
        lightcol = lighter.GetComponentInChildren<Collider2D>();
        lightcol.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine(LoadWait());
    }

    // Update is called once per frame
    void Update()
    {
        keyNumText.text = keynum.ToString();
        if (!isStarted && Input.GetKeyDown(KeyCode.Space) && !isLoading)
        {
            topDisplayText.text = "Use 'WASD' or the arrow keys to move.";
            bottomDisplayText.text = "Press 'R' to retry.";
            spawnitems.SetWarps();
            collid.enabled = true;
            lightcol.enabled = true;
            isStarted = true;
            playeraudio = GetComponent<AudioSource>();
            StartCoroutine(TextCoolDown());
        }
    }

    void FixedUpdate()
    {
        if (!goalReached && isStarted)
        {
            Move();
        }
    }

    private void Move()
    {
        XTrans = Input.GetAxis("Horizontal") * speed;
        YTrans = Input.GetAxis("Vertical") * speed;
        anim.SetFloat("xaxis", Input.GetAxis("Horizontal"));
        anim.SetFloat("yaxis", Input.GetAxis("Vertical"));
        //transform.position = new Vector2(XTrans, YTrans);
        rb.velocity += new Vector2(XTrans, YTrans);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Key")
        {
            keynum++;
            playeraudio.clip = sounds[0];
            playeraudio.Play();
            Destroy(col.gameObject);
            if(!interactedKey)
            {
                topDisplayText.text = "You got a key!\n" +
                    "Use keys to open Locks.";
                StartCoroutine(TextCoolDown());
                interactedKey = true;
            }
        }
        if (col.gameObject.tag == "Goal")
        {
            goalReached = true;
            playeraudio.clip = sounds[3];
            playeraudio.Play();
            bottomDisplayText.text = " ";
            uibehavior.activateGoalUI();
        }
        if (col.gameObject.tag == "RedWarp" || col.gameObject.tag == "BlueWarp")
        {
            bottomDisplayText.text = "Press Space to Warp.";
            if (!interactedWarp)
            {
                topDisplayText.text = "This is a Warp.\n" +
                    "Who knows where it'll take you? Try to use it.";
                StartCoroutine(TextCoolDown());
                interactedWarp = true;
            }
        }
        if (col.gameObject.tag == "Button")
        {
            ButtonBehavior button = col.GetComponent<ButtonBehavior>();
            button.Pressed();
            playeraudio.clip = sounds[1];
            playeraudio.Play();
            if (!interactedButton)
            {
                topDisplayText.fontSize = 25;
                topDisplayText.text = "You pressed a button!\n" +
                    "Buttons can activate or deactivate blocks on their corresponding color.";
                StartCoroutine(TextCoolDown());
                interactedButton = true;
            }
        }

    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "RedWarp" || col.gameObject.tag == "BlueWarp")
        {
            WarpBehavior wp = col.GetComponent<WarpBehavior>();
            if (Input.GetKeyDown(KeyCode.Space) && !warpCooldown)
            {
                collid.enabled = false;
                wp.Teleport(gameObject.transform);
                collid.enabled = true;
                warpCooldown = true;
                StartCoroutine(WarpingCooldown());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "RedWarp" || col.gameObject.tag == "BlueWarp")
        {
            bottomDisplayText.text = "Press 'R' to retry.";
        }
        if (col.gameObject.tag == "Button")
        {
            ButtonBehavior button = col.GetComponent<ButtonBehavior>();
            button.Released();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Lock")
        {
            if(keynum >= 1)
            bottomDisplayText.text = "Press Space to Unlock.";
            if (!interactedLock)
            {
                topDisplayText.text = "This is a Lock.\n" +
                    "Find a key to open it!";
                StartCoroutine(TextCoolDown());
                interactedLock = true;
            }
        }
        if (col.gameObject.tag == "BulkLock")
        {
            if (keynum >= 3)
            {
                bottomDisplayText.text = "Press Space to Unlock.";
                if (!interactedBulkLock)
                {
                    topDisplayText.fontSize = 20;
                    topDisplayText.text = "Whoa?! You already found all 3 keys!?\n" +
                        "You're pretty good! Now use the keys to unlock the bulk lock and get to the Goal!";
                    StartCoroutine(TextCoolDown());
                    interactedBulkLock = true;
                }
            }
            else
            {
                bottomDisplayText.text = "Not Enough Keys.";
                if (!interactedBulkLock)
                {
                    topDisplayText.fontSize = 25;
                    topDisplayText.text = "Looks like the goal is locked behind a Bulk Lock...\n" +
                        "Find three keys to unlock the bulk lock to reach the Goal!";
                    StartCoroutine(TextCoolDown());
                    interactedBulkLock = true;
                }
                
            }
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Lock" && keynum >= 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                keynum--;
                playeraudio.clip = sounds[2];
                playeraudio.Play();
                bottomDisplayText.text = "Press 'R' to retry.";
                Destroy(col.gameObject);
            }
        }
        if (col.gameObject.tag == "BulkLock" && keynum >= 3)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                keynum -= 3;
                playeraudio.clip = sounds[2];
                playeraudio.Play();
                bottomDisplayText.text = "Press 'R' to retry.";
                Destroy(col.gameObject);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Lock" || col.gameObject.tag == "BulkLock")
        {
            bottomDisplayText.text = "Press 'R' to retry.";
        }
    }

    IEnumerator WarpingCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        warpCooldown = false;
    }

    IEnumerator TextCoolDown()
    {
        yield return new WaitForSeconds(3.0f);
        topDisplayText.text = "";
        topDisplayText.fontSize = 30;
    }

    IEnumerator LoadWait()
    {
        yield return new WaitForSeconds(3.0f);
        bottomDisplayText.text = "Press Space to Start.";
        isLoading = false;
    }

}
