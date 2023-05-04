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
    [SerializeField] private GameObject blinder;
    public float speed = 2.5f;
    public int keynum = 0;
    private float XTrans;
    private float YTrans;
    private bool goalReached = false;
    private bool isStarted = false;
    private bool warpCooldown = false;
    private bool isLoading = true;
    private bool cursed = false;
    private bool[] keyInventory = new bool[4];

    private Collider2D collid;
    private CircleCollider2D lightcol;
    private Rigidbody2D rb;
    private Transform blindTransform;
    //private Collider2D lightcol;
    // Start is called before the first frame update
    void Start()
    {
        displayText.text = "Loading...";
        collid = GetComponent<Collider2D>();
        collid.enabled = false;
        lightcol = lighter.GetComponentInChildren<CircleCollider2D>();
        lightcol.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        blindTransform = blinder.GetComponent<Transform>();
        StartCoroutine(LoadWait());
    }

    // Update is called once per frame
    void Update()
    {
        keyNumText.text = keynum.ToString();
        if(!isStarted && Input.GetKeyDown(KeyCode.Space) && !isLoading)
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
        if(cursed)
        {
            blindTransform.localScale = new Vector3(1f, 1f, 1f);
            lightcol.radius = 1.3f;
            speed = 1f;
        }
        else
        {
            blindTransform.localScale = new Vector3(1.4f, 1.4f, 1f);
            lightcol.radius = 1.75f;
            speed = 2.5f;
        }
    }

    private void Move()
    {
        XTrans = Input.GetAxis("Horizontal") * speed;
        YTrans = Input.GetAxis("Vertical") * speed;
        //transform.position += new Vector3(XTrans, YTrans, 0);
        //transform.Translate(new Vector2(XTrans, YTrans) * Time.deltaTime);
        rb.velocity += new Vector2(XTrans, YTrans);
        
        /*if(Input.GetKeyDown(KeyCode.N))
        {
            blindTransform.localScale = new Vector3(2f, 2f, 2f);
            lightcol.radius = 2.6f;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            blindTransform.localScale = new Vector3(1f, 1f, 1f);
            lightcol.radius = 1.3f;
        }*/
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Key")
        {
            keynum++;
            Destroy(col.gameObject);
        }
        if(col.gameObject.tag == "RedKey")
        {
            keyInventory[3] = true;
            cursed = true;
            Destroy(col.gameObject);
        }
        if(col.gameObject.tag == "GreenKey")
        {
            keyInventory[1] = true;
            if (cursed)
                cursed = false;
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
        if (col.gameObject.tag == "RedLock")
        {
            if(keyInventory[3])
            {
                displayText.text = "Press Space to Unlock.";
            }
        }
        if (col.gameObject.tag == "GreenLock")
        {
            if (keyInventory[1])
            {
                displayText.text = "Press Space to Unlock.";
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
        if (col.gameObject.tag == "RedLock" && keyInventory[3])
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                keyInventory[3] = false;
                displayText.text = "Press 'R' to retry.";
                RemoveColoredLocks(col.gameObject.tag);
            }
        }
        if (col.gameObject.tag == "GreenLock" && keyInventory[1])
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                keyInventory[1] = false;
                displayText.text = "Press 'R' to retry.";
                RemoveColoredLocks(col.gameObject.tag);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Lock" || col.gameObject.tag == "BulkLock" || col.gameObject.tag == "RedLock" || col.gameObject.tag == "GreenLock")
        {
            displayText.text = "Press 'R' to retry.";
        }
    }

    private void RemoveColoredLocks(string tag)
    {
        foreach (GameObject locks in GameObject.FindGameObjectsWithTag(tag))
        {
            Destroy(locks);
        }
    }

    IEnumerator WarpingCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        warpCooldown = false;
    }

    IEnumerator LoadWait()
    {
        yield return new WaitForSeconds(3.0f);
        isLoading = false;
        displayText.text = "Press Space to Start.";
    }
}
