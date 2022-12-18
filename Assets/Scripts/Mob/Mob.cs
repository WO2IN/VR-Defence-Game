using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Mob : MonoBehaviour
{
    // Unity Event�� ��Ȳ, ��� �и� --> �� �� ��Ȳ(�� ����, �� �ı�)
    public UnityEvent OnCreated;
    public UnityEvent OnDestroyed;

    // Destroy Effect
    public float destroyDelay = 1f;
    public bool isDestroyed = false;

    private void Start()
    {
        OnCreated?.Invoke();
        MobManager.Instance.OnSpawned(this);
    }

    public void Destroy()
    {
        if (isDestroyed) return;
        isDestroyed = true;

        Destroy(gameObject, destroyDelay);
        OnDestroyed?.Invoke();

        MobManager.Instance.OnDestroyed(this);
    }
}
