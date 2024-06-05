using System.Collections;

using UnityEngine;
using UnityEngine.Events;

public class RaycastFromCamera : MonoBehaviour
{
    public float rayDistance = 100.0f;
    public LayerMask layerMask; // 특정 레이어를 레이캐스트로 검사하려면 설정
    public Transform raycastOrigin; // 새로 추가된 필드
    public GameObject objectToMove; // 이동할 물체

    private Collider _collider;

    // Event wrapper
    [System.Serializable]
    public class RaycastEvent : UnityEvent { }
    public RaycastEvent OnRaycast;

    void Start()
    {
        if (raycastOrigin == null)
        {
            Debug.LogError("Raycast origin not found. Please set the raycast origin object.");
        }
        if (objectToMove == null)
        {
            Debug.LogError("Object to move not found. Please set the object to move.");
        }
        else
        {
            _collider = objectToMove.GetComponent<Collider>();
            if (_collider == null)
            {
                Debug.LogError("Collider not found on the object to move. Please ensure the object has a collider.");
            }
        }
    }

    public void CheckObjectInFront()
    {
        if (raycastOrigin != null)
        {
            // 레이캐스트 오브젝트의 정면 방향으로 레이캐스트 쏘기
            Ray ray = new Ray(raycastOrigin.position, raycastOrigin.forward);
            RaycastHit[] hits = Physics.RaycastAll(ray, rayDistance, layerMask);

            // 레이의 시작 위치와 방향 출력
            Debug.Log($"Ray origin: {ray.origin}, direction: {ray.direction}");

            foreach (var hit in hits)
            {
                // 이동할 물체와 레이로 닿은 물체가 동일하지 않은 경우에만 처리
                if (hit.collider.gameObject != objectToMove)
                {
                    Debug.Log("Object hit: " + hit.collider.name);
                    Debug.Log($"Hit point: {hit.point}, distance: {hit.distance}");

                    // Start coroutine to move and align the object after 0.2 seconds
                    StartCoroutine(MoveAndAlignToHitPointAfterDelay(hit.point, hit.normal, 0.05f));

                    // Trigger the event
                    OnRaycast.Invoke();
                    return; // 첫 번째로 이동할 물체와 동일하지 않은 물체를 타격하면 반환
                }
            }

            Debug.Log("No object hit in front of the raycast origin.");
        }
        else
        {
            Debug.LogError("Raycast origin reference is missing.");
        }
    }

    private IEnumerator MoveAndAlignToHitPointAfterDelay(Vector3 hitPoint, Vector3 hitNormal, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Move the object to the hit point
        if (_collider != null)
        {
            // Adjusted position so that the object's surface touches the hit point
            Vector3 adjustedPosition = hitPoint + hitNormal * (_collider.bounds.extents.magnitude / 2.5f);
            transform.position = adjustedPosition;

            // Adjust the rotation to align with the surface normal
            Quaternion adjustedRotation = Quaternion.LookRotation(hitNormal);
            adjustedRotation *= Quaternion.Euler(90, 0, 0); // Add 90 degrees to the x-axis rotation
            transform.rotation = adjustedRotation;
        }
    }



    private Quaternion AdjustRotationToNearestOrthogonal(Quaternion targetRotation)
    {
        Vector3 euler = targetRotation.eulerAngles;
        euler.x = Mathf.Round(euler.x / 45) * 45;
        euler.z = Mathf.Round(euler.z / 45) * 45;
        return Quaternion.Euler(euler);
    }
}
