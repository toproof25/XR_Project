using System.Collections;

using UnityEngine;
using UnityEngine.Events;

public class RaycastFromCamera : MonoBehaviour
{
    public float rayDistance = 100.0f;
    public LayerMask layerMask; // Ư�� ���̾ ����ĳ��Ʈ�� �˻��Ϸ��� ����
    public Transform raycastOrigin; // ���� �߰��� �ʵ�
    public GameObject objectToMove; // �̵��� ��ü

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
            // ����ĳ��Ʈ ������Ʈ�� ���� �������� ����ĳ��Ʈ ���
            Ray ray = new Ray(raycastOrigin.position, raycastOrigin.forward);
            RaycastHit[] hits = Physics.RaycastAll(ray, rayDistance, layerMask);

            // ������ ���� ��ġ�� ���� ���
            Debug.Log($"Ray origin: {ray.origin}, direction: {ray.direction}");

            foreach (var hit in hits)
            {
                // �̵��� ��ü�� ���̷� ���� ��ü�� �������� ���� ��쿡�� ó��
                if (hit.collider.gameObject != objectToMove)
                {
                    Debug.Log("Object hit: " + hit.collider.name);
                    Debug.Log($"Hit point: {hit.point}, distance: {hit.distance}");

                    // Start coroutine to move and align the object after 0.2 seconds
                    StartCoroutine(MoveAndAlignToHitPointAfterDelay(hit.point, hit.normal, 0.2f));

                    // Trigger the event
                    OnRaycast.Invoke();
                    return; // ù ��°�� �̵��� ��ü�� �������� ���� ��ü�� Ÿ���ϸ� ��ȯ
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

        // Move the object to the hit point along the hit normal
        if (_collider != null)
        {
            Vector3 adjustedPosition = hitPoint + hitNormal * (_collider.bounds.extents.magnitude / 2);
            objectToMove.transform.position = adjustedPosition;

            // Adjust the rotation based on the hit normal
            Quaternion targetRotation = Quaternion.LookRotation(hitNormal);
            objectToMove.transform.rotation = AdjustRotationToNearestOrthogonal(targetRotation);
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
