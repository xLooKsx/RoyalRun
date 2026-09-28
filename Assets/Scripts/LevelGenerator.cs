using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkFloorPrefab;
    [SerializeField] int amountOfTiles;
    [SerializeField] Transform chunkParent;
    [SerializeField] int distanceBetweenChunks;
    [SerializeField] float moveSpeed = 5;
    // GameObject[] chunks = new GameObject[12];
    List<GameObject> chunks = new List<GameObject>();
    GameObject lastChunk;
    
    void Start()
    {
        lastChunk = SpawnChunk(transform.position);
        InitiateChunks();
    }

    void Update()
    {
        MoveChunks();
    }

    private void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject selectedChunk = chunks[i];
            selectedChunk.transform.Translate(Vector3.back * (moveSpeed * Time.deltaTime));

            if(selectedChunk.transform.position.z <= Camera.main.transform.position.z - distanceBetweenChunks)
            {
                chunks.Remove(selectedChunk);
                Destroy(selectedChunk);
                lastChunk = SpawnChunk(CalculateNextChunkPosition());
            }
        }
    }

    private void InitiateChunks()
    {
        
        for (int i = 0; i < amountOfTiles; i++)
        {
            Vector3 newPosition = CalculateNextChunkPosition();
            lastChunk = SpawnChunk(newPosition);
        }
    }

    private Vector3 CalculateNextChunkPosition()
    {
        
        return new Vector3(lastChunk.transform.position.x, lastChunk.transform.position.y, lastChunk.transform.position.z + distanceBetweenChunks);
    }

    private GameObject SpawnChunk(Vector3 currentPosition)
    {
        GameObject chunk = Instantiate(chunkFloorPrefab, currentPosition, Quaternion.identity, chunkParent);
        chunks.Add(chunk);
        return chunk;
    }
} 
