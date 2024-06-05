using System.Collections;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class RaycastFromCamera : MonoBehaviour
{
    public float rayDistance = 100.0f;
    public LayerMask layerMask; // 특정 레이어를 레이캐스트로 검사하려면 설정
    public Transform raycastOrigin; // 새로 추가된 필드
    public GameObject objectToMove; // 이동할 물체

    public Collider _collider;

    private bool input_mode = false;

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
            //_collider = objectToMove.GetComponent<Collider>();
            if (_collider == null)
            {
                //Debug.LogError("Collider not found on the object to move. Please ensure the object has a collider.");
            }
        }
    }

    private Transform cubeClone;

    private void Update()
    {
        // Y누르고 언셀레드 하면 벽에 붙음
        if (OVRInput.Get(OVRInput.Button.Four))
        {
            input_mode = true;

            Transform cubeTransform = raycastOrigin.parent.Find("Visuals/ControllerRay/Cube");
            if (cubeClone == null)
            {
                
                cubeClone = Instantiate(cubeTransform, cubeTransform);

                cubeClone.gameObject.SetActive(true);
                cubeClone.localScale = new Vector3(1, 1, 3);
            }
        }
        else
        {
            input_mode = false;
            if (cubeClone != null)
            {
                Destroy(cubeClone.gameObject);
                cubeClone = null;
            }
        }
    }
/*    private void Update()
    {
        Transform cubeTransform = raycastOrigin.parent.Find("Visuals/ControllerRay/Cube");

        // Y누르고 언셀레드 하면 벽에 붙음
        if (OVRInput.Get(OVRInput.Button.Four))
        {
            input_mode = true;

            cubeTransform.gameObject.SetActive(true);
            cubeTransform.localScale = new Vector3(1, 1, 4); // 스케일을 4배로 고정

        }
        else
        {
            input_mode = false;

            // 원본의 스케일을 1, 1, 1로 고정
            cubeTransform.localScale = new Vector3(1, 1, 1);
        }
    }*/


    public void CheckObjectInFront()
    {
        //Debug.Log(input_mode + " : " + raycastOrigin != null);
        if (raycastOrigin != null && input_mode)
        {
            Ray ray = new Ray(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch) + Vector3.forward, OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch)*Vector3.forward);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Object hit: " + hit.collider.name);
                Debug.Log($"Hit point: {hit.point}, distance: {hit.distance}");

                // Start coroutine to move and align the object after 0.2 seconds
                StartCoroutine(MoveAndAlignToHitPointAfterDelay(hit.point, hit.normal, 0.05f));

                // Trigger the event
                OnRaycast.Invoke();
                return; // 첫 번째로 이동할 물체와 동일하지 않은 물체를 타격하면 반환
            }

            Debug.Log("No object hit in front of the raycast origin.");
        }
        else
        {
            Debug.LogError(input_mode + " Raycast origin reference is missing.");
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
