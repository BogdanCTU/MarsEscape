using UnityEngine;
using System.Collections.Generic;
using Unity.AI.Navigation;
using System.Linq;

/// <summary>
/// Auth: Bob
/// Description: Dinamically generates map blocks as the player moves on the map and
/// deleted the old ones in order to optimise the gameplay experience
/// </summary>

public class MapGeneratorDynamic : MonoBehaviour
{

    #region Fields

    [Header("Prefabs:")]
    [SerializeField] private GameObject firstBlock;
    [SerializeField] private List<GameObject> _blockPrefabs;
    [SerializeField] private Transform _blockContainer;
    [SerializeField] private Transform _player;
    [SerializeField] private NavMeshSurface _navMeshSurface;

    [Header("Map Stats:")]
    [SerializeField] private int _initialMapSize = 3;
    [SerializeField] private int _blockSpacing = 7;
    [SerializeField] private int _currentBlocks = 1;
    [SerializeField] private int _maxGeneratedBlocks = 50;

    [SerializeField] private List<GameObject> _generatedBlocks = new List<GameObject>();
    [SerializeField] private List<Vector2Int> _generatedBlocksPositions = new List<Vector2Int>();
    [SerializeField] private Vector2Int _currentPlayerBlock;
    

    #endregion Fields

    #region Mono

    void Start()
    {
        _currentPlayerBlock = WorldToBlockCoordinates(_player.position);

        _generatedBlocks.Add(firstBlock);
        _generatedBlocksPositions.Add(Vector2Int.zero);

        GenerateNewBlocksAroundPlayer();
    }

    // Called when the player enters a collider on a block
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BlockDetectionCollider"))
        {
            Vector2Int newPlayerBlock = WorldToBlockCoordinates(other.transform.position);

            if (newPlayerBlock != _currentPlayerBlock)
            {
                _currentPlayerBlock = newPlayerBlock;
                GenerateNewBlocksAroundPlayer();
            }
        }
    }

    #endregion Mono

    #region Methods

    void GenerateNewBlocksAroundPlayer()
    {
        for (int x = -7; x <= 7; x++)
        {
            for (int y = -7; y <= 7; y++)
            {
                if ((x % _blockSpacing == 0) && (y % _blockSpacing == 0))
                {
                    Vector2Int blockPos = _currentPlayerBlock + new Vector2Int(x, y);

                    // Check if the distance between the current block and the new block is greater than or equal to blockSpacing
                    if (!_generatedBlocksPositions.Contains(blockPos) && Vector2Int.Distance(blockPos, _currentPlayerBlock) >= _blockSpacing)
                    {
                        // Check how many blocks was generated
                        CheckBlocksCount();

                        GenerateBlock(blockPos);

                        // Refresh Navmesh including new generated blocks
                        _navMeshSurface.BuildNavMesh();
                    }
                }
            }
        }
    }

    void GenerateBlock(Vector2Int position)
    {
        GameObject block = Instantiate(_blockPrefabs[Random.Range(0, _blockPrefabs.Count)], BlockToWorldCoordinates(position), Quaternion.identity, _blockContainer);
        _generatedBlocks.Add(block);
        _generatedBlocksPositions.Add(position);
    }

    Vector2Int WorldToBlockCoordinates(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x);
        int y = Mathf.FloorToInt(worldPosition.z);
        return new Vector2Int(x, y);
    }

    Vector3 BlockToWorldCoordinates(Vector2Int blockPosition)
    {
        float x = blockPosition.x;
        float y = 0f;   // Blocks are at the same height
        float z = blockPosition.y;
        return new Vector3(x, y, z);
    }

    private void CheckBlocksCount()
    {
        if (_currentBlocks < _maxGeneratedBlocks)
            _currentBlocks++;
        else
        {
            // Destroy the first pair
            GameObject firstBlock = _generatedBlocks.First();
            _generatedBlocks.Remove(firstBlock);
            Destroy(firstBlock);
            _generatedBlocksPositions.Remove(_generatedBlocksPositions.First());
        }
    }

    #endregion Methods

}
