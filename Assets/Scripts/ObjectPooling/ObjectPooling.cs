using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Auth: Bob
/// Object Pooling script
/// </summary>

public class ObjectPooling : MonoBehaviour
{

    #region Fields

    [Space]
    [Header("Object Pooling:")]
    [SerializeField] private List<GameObject> _pooledObjects;
    [SerializeField] private GameObject _objectToPool;
    [SerializeField] private int _amountToPool;
    [SerializeField] private bool _shouldExpand = true;
    [SerializeField] private Transform _pooledObjectsParentTransform;

    #endregion Pooling Method

    #region Methods

    private void Awake()
    {
        SetObjectsToPull();
    }

    private void SetObjectsToPull()
    {
        _pooledObjects = new List<GameObject>();
        GameObject gameObjectTmp;
        for (int i = 0; i < _amountToPool; i++)
        {
            gameObjectTmp = Instantiate(_objectToPool, _pooledObjectsParentTransform);
            gameObjectTmp.SetActive(false);
            _pooledObjects.Add(gameObjectTmp);
        }
    }

    private GameObject GetPooledObject()
    {
        for (int i = 0; i < _amountToPool; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy)
            {
                return _pooledObjects[i];
            }
        }
        if (_shouldExpand)
        {
            GameObject obj = Instantiate(_objectToPool, _pooledObjectsParentTransform);
            obj.SetActive(false);
            _pooledObjects.Add(obj);
            return obj;
        }
        else
        {
            return null;
        }
    }

    public void ActivateObject()
    {
        GameObject gameObject = GetPooledObject();

        if (gameObject != null)
            gameObject.SetActive(true);
    }

    public GameObject ActivateAndGetObject()
    {
        GameObject gameObject = GetPooledObject();

        if (gameObject == null)
            return null;

        gameObject.SetActive(true);
        return gameObject;
    }

    public void ActivateObjectOnPosition(Vector3 spawnPosition)
    {
        GameObject gameObject = GetPooledObject();
       
        if (gameObject == null)
            return;

        gameObject.transform.position = spawnPosition;
        gameObject.SetActive(true);
    }

    #region Getters/Setters

    public void SetObjectToPool(GameObject objectToPool) { _objectToPool = objectToPool; }
    public GameObject GetObjectToPool() { return _objectToPool; }

    public void SetAmountToPool(int amountToPool) { _amountToPool = amountToPool; }
    public int GetAmountToPool() { return _amountToPool; }

    #endregion Getters/Setters

    #endregion Methods

}
