using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private UIBehavior uibehavior;
    [SerializeField] private SpawnItems spawnitems;
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private TMP_Text keyNumText;
    [SerializeField] private GameObject lighter;
    public float speed = 2.5f;
    public int keynum = 0;
    private float XTrans;
    private float YTrans;
    private bool goalReached = false;
    private bool isStarted = false;
    private bool warpCooldown = false;

    private Collider2D collid;
    private Collider2D lightcol;
    //private Collider2D lightcol;
    // Start is called before the first frame update
    void Start()
    {
        displayText.text = "Press Space to Start.";
        collid = GetComponent<Collider2D>();
        collid.enabled = false;
        lightcol = lighter.GetComponentInChildren<Collider2D>();
        lightcol.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        keyNumText.text = keynum.ToString();
        if(!isStarted && Input.GetKeyDown(KeyCode.Space))
        {
            displayText.text = "Press 'R' to retry.";
            spawnitems.SetWarps();
            collid.enabled = true;
            lightcol.enabled = true;
            isStarted = true;
        }
        if(!goalReached && isStarted)
        {
            Move();
        }
    }

    private void Move()
    {
        XTrans = Input.GetAxis("Horizontal") * speed;
        YTrans = Input.GetAxis("Vertical") * speed;
        //transform.position = new Vector2(XTrans, YTrans);
        transform.Translate(new Vector2(XTrans, YTrans) * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Key")
        {
            keynum++;
            Destroy(col.gameObject);
        }
        if(col.gameObject.tag == "Goal")
        {
            goalReached = true;
            displayText.text = " ";
            uibehavior.activateGoalUI();
        }
        if (col.gameObject.tag == "RedWarp" || col.gameObject.tag == "BlueWarp")
        {
            displayText.text = "Press Space to Warp.";
        }
        if(col.gameObject.tag == "Button")
        {
            ButtonBehavior button = col.GetComponent<ButtonBehavior>();
            button.Pressed();
        }

    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "RedWarp" || col.gameObject.tag == "BlueWarp")
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
            displayText.text = "Press 'R' to retry.";
        }
        if (col.gameObject.tag == "Button")
        {
            ButtonBehavior button = col.GetComponent<ButtonBehavior>();
            button.Released();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Lock" && keynum >= 1)
        {
            displayText.text = "Press Space to Unlock.";
        }
        if (col.gameObject.tag == "BulkLock")
        {
            if(keynum >= 3)
                displayText.text = "Press Space to Unlock.";
            else
                displayText.text = "Not Enough Keys.";
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Lock" && keynum >= 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                keynum--;
                displayText.text = "Press 'R' to retry.";
                Destroy(col.gameObject);
            }
        }
        if (col.gameObject.tag == "BulkLock" && keynum >= 3)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                keynum -= 3;
                displayText.text = "Press 'R' to retry.";
                Destroy(col.gameObject);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Lock" || col.gameObject.tag == "BulkLock")
        {
            displayText.text = "Press 'R' to retry.";
        }
    }

    IEnumerator WarpingCooldown()
    {
        yield return new WaitForSeconds(3.0f);
        warpCooldown = false;
    }
}
