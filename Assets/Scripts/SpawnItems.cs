using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnItems : MonoBehaviour
{
    [SerializeField] private GameObject goalTile;
    [SerializeField] private Tilemap tm;
    [SerializeField] private Transform tileGroup;
    [SerializeField] private Transform itemGroup;
    //[SerializeField] private TileBase newTile;


    private Vector3Int StartPos = new Vector3Int(-12, 4, 0);
    private Vector3Int EndPos = new Vector3Int(11, -5, 0);
    private Vector3 SpawnPos;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
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
                    if (tile.name == "goal 1")
                    {
                        //Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                        SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                        Instantiate(goalTile, SpawnPos, Quaternion.identity);
                        tm.SetTile(new Vector3Int(x, y, 0), null);
                    }
                }
            }
        }
    }
}
