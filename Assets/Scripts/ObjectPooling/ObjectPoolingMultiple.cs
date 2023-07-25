using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Auth: Bob
/// Multiple Object Pooling script
/// </summary>

public class ObjectPoolingMultiple : MonoBehaviour
{

    #region Fields

    // Pooling List
    [SerializeField] private List<GameObject> _pooledObjects;

    // Required Components
    public List<ObjectPoolItem> _itemsToPool;
    [SerializeField] private Transform _pooledObjectsContainerTransform;

    #endregion Pooling Method

    #region Methods

    private void Awake()
    {
        SetObjectsToPull();
    }

    private void SetObjectsToPull()
    {
        _pooledObjects = new List<GameObject>();

        foreach (ObjectPoolItem item in _itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = Instantiate(item.objectToPool, _pooledObjectsContainerTransform);
                obj.SetActive(false);
                _pooledObjects.Add(obj);
            }
        }
    }

    private GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy && _pooledObjects[i].tag == tag)
                return _pooledObjects[i];
        }

        foreach (ObjectPoolItem item in _itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = Instantiate(item.objectToPool, _pooledObjectsContainerTransform);
                    obj.SetActive(false);
                    _pooledObjects.Add(obj);
                    return obj;
                }
            }
        }

        return null;
    }

    public void SpawnObject(string objectTag)
    {
        GameObject objectToSpawn = GetPooledObject(objectTag);

        if (objectToSpawn != null)
            objectToSpawn.SetActive(true);
    }

    public GameObject SpawnAndGetObject(string objectTag)
    {
        GameObject objectToSpawn = GetPooledObject(objectTag);

        if (objectToSpawn == null)
            return null;

        objectToSpawn.SetActive(true);
        return objectToSpawn;
    }

    #region Getters/Setters

    public void SetObjectToPool(Transform pooledObjectsContainerTransform) { _pooledObjectsContainerTransform = pooledObjectsContainerTransform; }
    public Transform GetObjectToPool() { return _pooledObjectsContainerTransform; }

    #endregion Getters/Setters

    #endregion Methods

}


[System.Serializable]
public class ObjectPoolItem
{
    public int amountToPool;
    public GameObject objectToPool;
    public string objectTag;
    public bool shouldExpand;
}