using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance{ get; private set; }

    public List<PooledObject> objectPool = new List<PooledObject>();
    public List<PooledObject> usedPool = new List<PooledObject>();

    public int numberOfObjects = 5;

    public GameObject objectToCreate;


    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
        }
        else if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        for(int i = 0; i < numberOfObjects; i++)
        {
            AddNewObject();
        }
    }


    public void AddNewObject()
    {
        GameObject newObject = Instantiate(objectToCreate, transform.position, Quaternion.identity);
        newObject.GetComponent<PooledObject>().SetObjectPool(this);
        newObject.SetActive(false);
        objectPool.Add(newObject.GetComponent<PooledObject>());
    }

    public PooledObject GetPooledObject()
    {
        if(objectPool.Count > 0)
        {
            usedPool.Add(objectPool[0]);
            objectPool.RemoveAt(0);
            usedPool[usedPool.Count - 1].gameObject.SetActive(true);
            return usedPool[usedPool.Count - 1];
        }

        else
        {
            Debug.Log("No Pooled Object");
            return null;
        }
    }

    public void RestoreObject(PooledObject pooledObject)
    {
        objectPool.Add(pooledObject);
        usedPool.Remove(pooledObject);
        pooledObject.gameObject.SetActive(false);
    }
}