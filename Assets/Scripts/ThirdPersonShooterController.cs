using Cinemachine;
using StarterAssets;
using UnityEngine;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _aimVirtualCamera;
    [SerializeField] private StarterAssetsInputs _starterAssetsInputs;
    [SerializeField] private ThirdPersonController _thirdPersonController;
    [SerializeField] private float _normalSensitivity;
    [SerializeField] private float _aimSensitivity;
    [SerializeField] private LayerMask _aimColliderLayerMask = new LayerMask();
    [SerializeField] private Animator _animator;
    [SerializeField] private UIControllerView _uiControllerView;
    [SerializeField] private Transform _projectilePrefab;
    [SerializeField] private Transform _projectileSpawnPos;
    [SerializeField] private Transform _vfxHit;
    private Vector3 _screenCenterPoint;
    private Vector3 _mouseWorldPosition;
    private bool _rotated;

    private void Start()
    {
        _screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        _uiControllerView.ShootButton.onClick.AddListener(Shoot);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(_screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, _aimColliderLayerMask))
        {
            _mouseWorldPosition = raycastHit.point;
        }

        if (_starterAssetsInputs.aim)
        {
            _aimVirtualCamera.gameObject.SetActive(true);
            _thirdPersonController.SetSensitivity(_aimSensitivity);
            _thirdPersonController.SetRotateOnMove(false);
            _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            Vector3 worldAimTarget = _mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            _aimVirtualCamera.gameObject.SetActive(false);
            _thirdPersonController.SetSensitivity(_normalSensitivity);
            _thirdPersonController.SetRotateOnMove(true);
            _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }
#if UNITY_EDITOR
        if (_starterAssetsInputs.shoot)
        {
            Shoot();
        }
#endif
    }
    

    private void Shoot()
    {
        Vector3 aimDir = (_mouseWorldPosition - _projectileSpawnPos.position).normalized;
        Instantiate(_projectilePrefab, _projectileSpawnPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
        _starterAssetsInputs.shoot = false;
    }
}