using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tetrisBlocks;
    public float maxMovements;
    public float movementSpeed;
    public float fastMovementSpeed;
    private GameObject currentBlock;
    //private const float boardFloor = -4.5f;
    public float boardCeiling = 7f;
    
    private void Start()
    {
        SpawnBlock();
    }

    private void Update()
    {
        if (currentBlock.GetComponent<PieceMovement>().hitGround)
        {
            SpawnBlock();
        }
    }
    private void SpawnBlock()
    {
        currentBlock = Instantiate(tetrisBlocks[Random.Range(0, tetrisBlocks.Length)], new Vector3(0f, boardCeiling, 0), Quaternion.identity);

    }
}
