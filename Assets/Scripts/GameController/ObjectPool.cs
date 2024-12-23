using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    private Dictionary<string, List<GameObject>> pools;
    private Dictionary<string, GameObject> objectPrefabs;

    void Awake()
    {
        Instance = this;
        pools = new Dictionary<string, List<GameObject>>();
        objectPrefabs = new Dictionary<string, GameObject>();
    }

    public void InitializePool(string key, GameObject prefab, int initialPoolSize)
    {
        if (!pools.ContainsKey(key))
        {
            pools[key] = new List<GameObject>();
            objectPrefabs[key] = prefab;

            for (int i = 0; i < initialPoolSize; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                pools[key].Add(obj);
            }
        }
    }

    public GameObject GetObject(string key)
    {
        if (pools.ContainsKey(key))
        {
            foreach (GameObject obj in pools[key])
            {
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }

            GameObject newObj = Instantiate(objectPrefabs[key]);
            newObj.SetActive(true);
            pools[key].Add(newObj);
            return newObj;
        }

        return null;
    }

    public void ReturnObject(string key, GameObject obj)
    {
        if (pools.ContainsKey(key))
        {
            obj.SetActive(false);
        }
    }

    public void SetDeactiveAll()
    {
        foreach (List<GameObject> pool in pools.Values)
        {
            foreach (GameObject obj in pool)
            {
                obj.SetActive(false);
            }
        }
        
    }
}