using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager instance;
    [SerializeField] private LevelSO[] levelSO;
    [SerializeField] private GameObject finishLine;

    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        this.GenerateLevel();
        this.finishLine = GameObject.FindWithTag("Finish");
    }

    private void GenerateLevel()
    {
        int currentLevel = GetLevels();
        currentLevel = currentLevel % levelSO.Length;
        LevelSO level = levelSO[currentLevel];
        CreateLevel(level.chunks);
    }

    private void CreateLevel(Chunk[] levelChunks)
    {
        Vector3 chunkPosition = Vector3.zero;
        chunkPosition.y = -5;
        for (int i = 0; i < levelChunks.Length; i++)
        {
            Chunk chunkToCreate = levelChunks[i];
            if (i > 0)
            {
                chunkPosition.z += chunkToCreate.GetLength() / 2;
            }
            Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, this.transform);
            chunkPosition.z += chunkInstance.GetLength() / 2;
        }
    }

    public float GetFinish()
    {
        return finishLine.transform.position.z;
    }

    public int GetLevels()
    {
        return PlayerPrefs.GetInt("Level", 0);
    }
}
