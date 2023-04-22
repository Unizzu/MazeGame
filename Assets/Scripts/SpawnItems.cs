using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnItems : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject loadStart;
    [SerializeField] private GameObject loadEnd;
    [SerializeField] private GameObject goalTile;
    [SerializeField] private GameObject pathTile;
    [SerializeField] private GameObject blockTile;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject lockTile;
    [SerializeField] private GameObject bulkLockTile;
    [SerializeField] private GameObject redWarp;
    [SerializeField] private GameObject blueWarp;
    [SerializeField] private GameObject redButton;
    [SerializeField] private GameObject blueButton;
    [SerializeField] private GameObject redBlock;
    [SerializeField] private GameObject blueBlock;
    [SerializeField] private Tilemap tm;
    [SerializeField] private Transform tileGroup;
    [SerializeField] private Transform itemGroup;
    //[SerializeField] private TileBase newTile;


    private Vector3Int StartPos;
    private Vector3Int EndPos;
    private Vector3 SpawnPos;

    // Start is called before the first frame update
    void Start()
    {
        StartPos = tm.WorldToCell(loadStart.transform.position);
        EndPos = tm.WorldToCell(loadEnd.transform.position);
        Spawn();
        Destroy(loadStart);
        Destroy(loadEnd);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn()
    {
        for (int x = StartPos.x; x <= EndPos.x; x++)
        {
            for (int y = StartPos.y; y >= EndPos.y; y--)
            {
                TileBase tile = tm.GetTile(new Vector3Int(x, y, 0));
                
                if (tile != null)
                {
                    string tileName = tile.name;
                    /*if (tile.name == "goal 1")
                    {
                        //Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                        SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                        Instantiate(goalTile, SpawnPos, Quaternion.identity);
                        tm.SetTile(new Vector3Int(x, y, 0), null);
                    }*/
                    switch (tileName)
                    {
                        case "player":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            player.position = SpawnPos;
                            break;
                        case "goal 1":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(goalTile, SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "tile 1":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(blockTile, SpawnPos, Quaternion.identity, tileGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "path":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "key":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(key, SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "locktile":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(lockTile, SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "3locktile":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(bulkLockTile, SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "warpred":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(redWarp, SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "warpblue":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(blueWarp, SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "buttonred":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(redButton, SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "buttonblue":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(blueButton, SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "buttonblockred":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(redBlock, SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "buttonblockblue":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(blueBlock, SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    public void SetWarps()
    {
        foreach (GameObject warps in GameObject.FindGameObjectsWithTag("RedWarp"))
        {
            WarpBehavior wp = warps.GetComponent<WarpBehavior>();
            wp.findOtherWarp();
        }
        foreach (GameObject warps in GameObject.FindGameObjectsWithTag("BlueWarp"))
        {
            WarpBehavior wp = warps.GetComponent<WarpBehavior>();
            wp.findOtherWarp();
        }
    }
}
