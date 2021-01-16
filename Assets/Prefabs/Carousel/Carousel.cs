using UnityEngine;

public class Carousel : MonoBehaviour
{
    [Tooltip("Carousel core transform")]
    public Transform core;

    [Tooltip("Carousel total run duration")]
    public float duration;

    [Tooltip("Carousel ramp up/down duration")]
    public float rampDuration;

    [Tooltip("Rotational speed (degrees per second)")]
    public float rotateSpeed;

    private bool _running;

    private float _runTime;

    public void StartCarouselRun()
    {
        _runTime = 0;
        _running = true;
    }

    private void Update()
    {
        // Skip if not running
        if (!_running)
            return;

        _runTime += Time.deltaTime;

        // Handle rotation
        if (_runTime < rampDuration)
        {
            // Handle ramp-up
            var speed = Mathf.Lerp(0, rotateSpeed, _runTime / rampDuration);
            core.Rotate(Vector3.up, speed * Time.deltaTime);
        }
        else if (_runTime < duration - rampDuration)
        {
            // Handle running
            core.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }
        else if (_runTime < duration)
        {
            // Handle ramp-down
            var speed = Mathf.Lerp(rotateSpeed, 0, (_runTime - (duration - rampDuration)) / rampDuration);
            core.Rotate(Vector3.up, speed * Time.deltaTime);
        }
        else
        {
            _running = false;
        }
    }
}
