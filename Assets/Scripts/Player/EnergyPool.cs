using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private Queue<T> pool = new Queue<T>();
    private T prefab;
    private Transform parent;

    // Constructor for initializing the pool
    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;
        PreloadObjects(initialSize);
    }

    // Preload specified number of objects into the pool
    private void PreloadObjects(int size)
    {
        for (int i = 0; i < size; i++)
        {
            T newObject = CreateObject();
            pool.Enqueue(newObject);
        }
    }

    // Create a new instance of the prefab
    private T CreateObject()
    {
        T newObject = Object.Instantiate(prefab, parent);
        EnsureRequiredComponent<T>(newObject);
        newObject.gameObject.SetActive(false);
        return newObject;
    }

    // Check if the required component exists or add it
    private void EnsureRequiredComponent<TComponent>(T obj) where TComponent : Component
    {
        if (!obj.TryGetComponent(out TComponent component))
        {
            obj.gameObject.AddComponent<TComponent>();
            Debug.LogWarning($"Added missing component {typeof(TComponent).Name} to {obj.name}");
        }
    }

    // Get an object from the pool
    public T Get(Vector3 position, Quaternion rotation)
    {
        T obj = pool.Count > 0 ? pool.Dequeue() : CreateObject();
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.gameObject.SetActive(true);
        return obj;
    }

    // Return an object to the pool
    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}
