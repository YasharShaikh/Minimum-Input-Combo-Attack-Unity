using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private Queue<T> pool = new Queue<T>();
    private T prefab;
    private Transform parent;

    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;
        PreloadObjects(initialSize);
    }

    private void PreloadObjects(int size)
    {
        for (int i = 0; i < size; i++)
        {
            T newObject = CreateObject();
            pool.Enqueue(newObject);
        }
    }

    private T CreateObject()
    {
        T newObject = Object.Instantiate(prefab, parent);

        if (!newObject.TryGetComponent(out Projectile projectile)) 
        {
            newObject.gameObject.AddComponent<Projectile>();
        }

        newObject.gameObject.SetActive(false);
        return newObject;
    }

    public T Get(Vector3 position, Quaternion rotation)
    {
        T obj = pool.Count > 0 ? pool.Dequeue() : CreateObject();
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}
