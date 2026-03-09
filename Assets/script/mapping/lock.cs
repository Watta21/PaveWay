using System;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LockNavigationManager : MonoBehaviour
{
    [Header("Waypoints to Follow (in order)")]
    public Transform[] waypoints;
    public int defaultTargetIndex = 0;

    [Header("Optional Visual Debug")]
    public GameObject lockIndicator;

    [Header("Lock Settings")]
    public bool enableTotalLock = true;
    public bool freezeXRSpaces = true;
    public bool disableAllLocalizers = true;

    private bool isMapLocked = false;
    private Transform currentTarget;
    private float checkInterval = 0.5f;

    // Event for when map is totally locked
    public UnityEvent OnMapTotallyLocked;

    void Start()
    {
        StartCoroutine(CheckLocalizationLoop());
    }

    private IEnumerator CheckLocalizationLoop()
    {
        // Repeatedly check until locked
        while (!isMapLocked)
        {
            TryLockWhenGreenPose();
            yield return new WaitForSeconds(checkInterval);
        }

        // Once locked, stop the coroutine completely
        Debug.Log("🔒 Localization Loop STOPPED - Map is PERMANENTLY LOCKED");
    }

    private void TryLockWhenGreenPose()
    {
        // Find all localizers (Immersal.XR.Localizer)
        var allLocalizers = FindObjectsOfType<Immersal.XR.Localizer>();

        foreach (var loc in allLocalizers)
        {
            if (loc == null || !loc.enabled)
                continue;

            // Reflect to get m_HasLocalizedSuccessfully and pose quality
            var type = loc.GetType();

            var localizedProp = type.GetProperty("m_HasLocalizedSuccessfully",
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.NonPublic);

            var poseQualityProp = type.GetProperty("poseQuality",
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.NonPublic);

            bool localized = false;
            int poseQuality = 0;

            if (localizedProp != null)
                localized = (bool)localizedProp.GetValue(loc);

            if (poseQualityProp != null)
                poseQuality = (int)poseQualityProp.GetValue(loc);

            // ✅ PoseQuality = 2 usually means "Green Pose" (Good Localization)
            if (localized && poseQuality == 2)
            {
                TotalLockMap(allLocalizers);
                return;
            }
        }
    }

    private void TotalLockMap(Immersal.XR.Localizer[] allLocalizers)
    {
        if (isMapLocked) return;

        Debug.Log("🚨 INITIATING TOTAL MAP LOCK...");

        // 1. DISABLE ALL LOCALIZERS PERMANENTLY
        if (disableAllLocalizers)
        {
            foreach (var loc in allLocalizers)
            {
                if (loc != null && loc.enabled)
                {
                    loc.enabled = false;
                    Debug.Log($"🧊 PERMANENTLY DISABLED Localizer: {loc.name}");
                }
            }
        }

        // 2. FREEZE ALL XR SPACES
        if (freezeXRSpaces)
        {
            var xrSpaces = FindObjectsOfType<Transform>().Where(t => t.name.Contains("XRSpace")).ToArray();
            foreach (var space in xrSpaces)
            {
                // Multiple ways to freeze the transform
                space.gameObject.isStatic = true;

                // Disable any rigidbody movement
                var rb = space.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                }

                // Disable any colliders that might cause movement
                var colliders = space.GetComponents<Collider>();
                foreach (var col in colliders)
                {
                    col.enabled = false;
                }

                Debug.Log($"📦 XRSpace TOTALLY FROZEN: {space.name}");
            }
        }

        // 3. MARK AS PERMANENTLY LOCKED
        isMapLocked = true;

        // 4. SET FIRST TARGET (this will be the FINAL target position)
        SetTarget(defaultTargetIndex);

        // 5. VISUAL INDICATOR
        if (lockIndicator != null)
            lockIndicator.SetActive(true);

        // 6. INVOKE LOCK EVENT
        OnMapTotallyLocked?.Invoke();

        // 7. STOP ALL COROUTINES THAT MIGHT AFFECT POSITION
        StopAllCoroutines();

        Debug.Log("✅✅✅ MAP TOTALLY AND PERMANENTLY LOCKED ✅✅✅");
        Debug.Log("🎯 Final Target Set: " + (currentTarget != null ? currentTarget.name : "None"));
        Debug.Log("🔒 No more localization checks - AR world is FROZEN");
    }

    public void SetTarget(int index)
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogWarning("[LockNavigationManager] ⚠️ No waypoints set.");
            return;
        }

        if (index < 0 || index >= waypoints.Length)
        {
            Debug.LogWarning("[LockNavigationManager] ⚠️ Invalid waypoint index: " + index);
            return;
        }

        currentTarget = waypoints[index];

        // If map is locked, this is the FINAL target position
        if (isMapLocked)
        {
            Debug.Log($"🎯 FINAL TARGET SET (PERMANENT): {currentTarget.name}");
        }
        else
        {
            Debug.Log($"🎯 Current target set: {currentTarget.name}");
        }
    }

    // Manual function (optional) - DISABLED WHEN LOCKED
    public void NextWaypoint()
    {
        if (isMapLocked)
        {
            Debug.LogWarning("🚫 CANNOT CHANGE WAYPOINT - MAP IS PERMANENTLY LOCKED");
            return;
        }

        if (waypoints == null || waypoints.Length == 0) return;
        int nextIndex = (Array.IndexOf(waypoints, currentTarget) + 1) % waypoints.Length;
        SetTarget(nextIndex);
    }

    // Public method to check if map is totally locked
    public bool IsMapTotallyLocked()
    {
        return isMapLocked;
    }

    // Emergency unlock (if needed for debugging)
    public void EmergencyUnlock()
    {
        if (!isMapLocked) return;

        Debug.LogWarning("🚨 EMERGENCY UNLOCK - USE FOR DEBUGGING ONLY");
        isMapLocked = false;

        if (lockIndicator != null)
            lockIndicator.SetActive(false);

        StartCoroutine(CheckLocalizationLoop());
    }
}