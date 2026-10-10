using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public ObjectPool poolReference;

    public void SetObjectPool(ObjectPool pool)
    {
        poolReference = pool;
    }

    public void DestroyWithTime(float time)
    {
        Invoke("ResetObject", time);
    }

    public void ResetObject()
    {
        poolReference.RestoreObject(this);
    }
}