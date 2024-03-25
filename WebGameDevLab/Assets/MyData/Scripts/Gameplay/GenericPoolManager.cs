using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPoolManager<T> : PersistentSingolton<GenericPoolManager<T>> where T : Component
{
    [SerializeField] private T pooledObject;
    protected Queue<T> pool = new();

    public T Get()
    {
        if (pool.Count == 0) // If it's empty
        {
            // Generate a new one
            Add(1);
        }
        return pool.Dequeue();
    }

    private void Add(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var generic = Instantiate(pooledObject);
            generic.gameObject.SetActive(false);
            pool.Enqueue(generic);
        }
    }

    public void ReturnToPool(T generic)
    {
        generic.gameObject.SetActive(false);
        pool.Enqueue(generic);
    }
}
