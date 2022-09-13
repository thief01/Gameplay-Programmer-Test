using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pool<T>  where T : MonoBehaviour
{
    public static Pool<T> Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Pool<T>();
            }

            return instance;
        }
    }

    private static Pool<T> instance;

    public int CurrentPoolObjects => pooledObjects.Count;

    public UnityEvent OnObjectDestroyed = new UnityEvent();
    public UnityEvent OnObjectSpawned = new UnityEvent();

    private List<T> pooledObjects = new List<T>();
    private GameObject prefab;
    private Transform poolParrent;

    

    public void InitPool(int items, GameObject gameObject)
    {
        if (poolParrent == null)
        {
            poolParrent = new GameObject().transform;
            poolParrent.name = typeof(Pool<T>).ToString();
        }
        prefab = gameObject;
        for (int i = 0; i < items; i++)
        {
            GameObject g = GameObject.Instantiate(gameObject, poolParrent);
            pooledObjects.Add(g.GetComponent<T>());
            g.SetActive(false);
        }
    }

    public T GetObject()
    {
        if (pooledObjects.Count == 0)
        {
            Debug.LogWarning("No objects in pool");
            return default(T);
        }

        T t = pooledObjects[0];
        t.gameObject.SetActive(true);
        pooledObjects.RemoveAt(0);
        return t;
    }

    public void BackToPool(T t)
    {
        t.gameObject.SetActive(false);
        pooledObjects.Add(t);
    }
}
