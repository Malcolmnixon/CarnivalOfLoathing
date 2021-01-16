using UnityEngine;
using UnityEngine.Events;

public class TimeTrigger : MonoBehaviour
{
    [Tooltip("Timer interval in seconds")]
    public float interval;

    [Tooltip("Timer active flag")]
    public bool active;

    [Tooltip("Repeat timer flag")]
    public bool repeat;

    [Tooltip("Timer events")]
    public UnityEvent onTimer;

    private float _integrator;

    /// <summary>
    /// Update the timer
    /// </summary>
    private void Update()
    {
        // Skip if not active
        if (!active)
            return;

        // Integrate time and return if not at interval
        _integrator += Time.deltaTime;
        if (_integrator < interval)
            return;

        // Either reset or deactivate timer
        if (repeat)
            _integrator -= interval;
        else
            active = false;

        // Invoke the events
        onTimer?.Invoke();
    }
}
