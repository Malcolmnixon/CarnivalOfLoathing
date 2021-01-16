using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class CollideEvent : UnityEvent<Collider>
{
}

public class CollideTrigger : MonoBehaviour
{
    [Tooltip("Collide name filter")]
    public string filter;

    [Tooltip("Collide enter events")]
    public CollideEvent onEnter;

    [Tooltip("Collide exit events")]
    public CollideEvent onExit;

    void Start()
    {
        if (filter == null) filter = string.Empty;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains(filter))
        {
            onEnter?.Invoke(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains(filter))
        {
            onExit?.Invoke(other);
        }
    }
}
