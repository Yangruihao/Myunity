using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LookCamera : MonoBehaviour
{
    //[HideInInspector]
    public ZoomCamera zoomCamera;

    public const string AxisScroll = "Mouse ScrollWheel";

    private float rotationTargetX;
    private float rotationX;
    private float rotationXspeed;
    private float rotationTargetY;
    private float rotationY;
    private float rotationYspeed;
    public float maxRotationX = 360;
    public float minRotationX = -360;
    private float rotationSpeed = 5;

    private bool m_IsMoving;


    /// <summary>
    /// 限制相机缩放
    /// </summary>
    public bool IsCanZoom=true;
    /// <summary>
    /// 限制相机旋转
    /// </summary>
    public bool IsCanRorate { get; set; } = true;

    public static LookCamera Instance { get; private set; }
    private void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        zoomCamera = GetComponentInChildren<ZoomCamera>();
        IsCanZoom = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_IsMoving)
        {
            Rotation();          
        }
        if(IsCanZoom)
            Zoom();
    }

    void LateUpdate()
    {
        if (!m_IsMoving)
            Caculate();
    }
    /// <summary>
    /// 缩放
    /// </summary>
    private void Zoom()
    {
        float deltaZoom= Input.GetAxis(AxisScroll);
        if (deltaZoom != 0)
        {
            zoomCamera.zoom += deltaZoom;
        }      
    }
    /// <summary>
    /// 旋转
    /// </summary>
    private void Rotation()
    {
        if (!IsCanRorate) return;
        if (Input.GetMouseButton(1))
        {
            rotationTargetY += Input.GetAxis("Mouse X") * rotationSpeed;
            rotationTargetX -= Input.GetAxis("Mouse Y") * rotationSpeed;
            rotationTargetX = Mathf.Clamp(rotationTargetX, minRotationX, maxRotationX);
        }
    }
    private void Caculate()
    {
        rotationY = Mathf.Lerp(rotationY, rotationTargetY, Time.deltaTime * 4);
        rotationX = Mathf.Lerp(rotationX, rotationTargetX, Time.deltaTime * 4);
        Vector3 rt = new Vector3(rotationX, rotationY, 0f);
        transform.rotation = Quaternion.Euler(rt);
    }

    private Sequence m_Squence;
    public Sequence Sequence { get; internal set; }
    /// <summary>
    /// 相机移动，移动到目标位置 旋转到目标角度
    /// </summary>
    /// <param name="target"></param>
    /// <param name="duration"></param>
    /// <param name="onComplete"></param>
    public void MoveTo(Transform target, float duration = 0.4f, System.Action onComplete = null)
    {
        float angle = Quaternion.Angle(target.rotation, transform.rotation);
        duration = (angle / 180f)*0.8f;
        Debug.LogWarning("timer:" + duration);
        if (duration < 0.4f)
        {
            duration = 0.4f;
        }
        SetDistance(0);
        m_IsMoving = true;
        Sequence sequence = DOTween.Sequence();
        m_Squence = sequence;
        sequence.Append(transform.DOLookAt(target.position, duration).SetEase(Ease.Linear));
        sequence.Insert(0, transform.DOMove(target.position, duration).SetEase(Ease.Linear));
        sequence.Insert(0, transform.DORotate(target.eulerAngles, duration).SetEase(Ease.Linear));
        onComplete?.Invoke();

        sequence.OnComplete(() =>
        {
            OnMoveingEnd();
        });
    }
    private void OnMoveingEnd()
    {
        rotationTargetY = transform.eulerAngles.y;
        rotationTargetX = transform.eulerAngles.x;
        rotationY = transform.eulerAngles.y;
        rotationX = transform.eulerAngles.x;
        m_IsMoving = false;
       
    }

    public void SetDistance(float distance)
    {
        zoomCamera.SetZoom(distance);
    }
}
