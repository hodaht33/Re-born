using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 카메라
/// </summary>

public class ViewScaleWithWheel : MonoBehaviour
{
    [SerializeField]
    private float mSpeed = 5.0f;

    private float mScroll;
    private Camera mCamera;

    private void Awake()
    {
        mCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        mScroll = Input.GetAxis("Mouse ScrollWheel") * mSpeed;
        
        // 카메라 z축 이동 방식
        //if (scroll != 0.0f)
        //{
        //    transform.Translate(Vector3.forward * scroll * speed);
        //}

        // fov조절 방식
        mCamera.fieldOfView -= mScroll;
        if (mCamera.fieldOfView <= 30.0f && 
            mScroll > 0.0f)
        {
            mCamera.fieldOfView = 30.0f;
        }
        else if (mCamera.fieldOfView >= 60.0f && 
            mScroll < 0.0f)
        {
            mCamera.fieldOfView = 60.0f;
        }

    }
}
