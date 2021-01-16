using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class SnappedEvent : UnityEvent<OVRGrabbable>
{
}

[Serializable]
public class UnsnappedEvent : UnityEvent<OVRGrabbable>
{
}

public class SnapZone : MonoBehaviour
{
    [Tooltip("Events to fire when object snapped")]
    public SnappedEvent onSnapped;

    [Tooltip("Events to fire when object unsnapped")]
    public UnsnappedEvent onUnsnapped;

    [Tooltip("Template game object to show snap zone")]
    public GameObject template;

    [Tooltip("Filter for snap objects")]
    public string filter;

    private OVRGrabbable _snapped;

    private readonly HashSet<OVRGrabbable> _potential = new HashSet<OVRGrabbable>();

    private void Start()
    {
        if (template == null) template = new GameObject();
        if (filter == null) filter = string.Empty;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains(filter))
        {
            var otherGrab = other.GetComponent<OVRGrabbable>();
            if (otherGrab != null)
            {
                _potential.Add(otherGrab);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains(filter))
        {
            var otherGrab = other.GetComponent<OVRGrabbable>();
            if (otherGrab != null)
            {
                _potential.Remove(otherGrab);
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // Handle unsnapping by grabbing
        if (_snapped != null && _snapped.isGrabbed)
        {
            var item = _snapped;
            _snapped = null;

            onUnsnapped?.Invoke(item);
        }

        // Handle snapping of first un-grabbed potential
        if (_snapped == null)
        {
            _snapped = _potential.FirstOrDefault(p => !p.isGrabbed);
            if (_snapped)
            {
                _snapped.GetComponent<Rigidbody>().isKinematic = true;
                _snapped.transform.SetParent(transform);
                _snapped.transform.localPosition = Vector3.zero;
                _snapped.transform.localRotation = Quaternion.identity;
                onSnapped?.Invoke(_snapped);
            }
        }

        // Handle activating template when ready to receive potential
        template.SetActive(_snapped == null && _potential.Any());
    }
}
