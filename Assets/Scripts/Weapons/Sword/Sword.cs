using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Sword 
    : MonoBehaviour
    , IWeapon
{
    [Header("References")]
    [field: SerializeField] public WeaponTypeSO WeaponTypeSO { get; private set; }

    [SerializeField] private PlayerHand _playerHand;
    [SerializeField] private Animator _animator;
    [SerializeField] private DamageSource _damageSource;

    [Header("Settings")]
    [SerializeField] private float _activeWeaponAngleOffset = -15f;
    


    private float lastAttack;


    private void Start()
    {
        lastAttack = Time.time;
        _damageSource.SetWeaponType(WeaponTypeSO);
    }

    private void Update()
    {
        RotateTowardsMouse();
        if (PlayerController.Instance.Input.Player.Attack.IsPressed()
            && !EventSystem.current.IsPointerOverGameObject())
        {
            Attack();
        }
    }


    public void Attack()
    {
        float attackDelta = Time.time - lastAttack;
        if (attackDelta >= WeaponTypeSO.AttackDelay)
        {
            lastAttack = Time.time;

            _animator.SetTrigger("Attack");
        }
    }


    private void RotateTowardsMouse()
    {
        Vector3 mousePos = PlayerController.Instance.Input.Player.MouseLook.ReadValue<Vector2>();
        Vector3 playerPos = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);
        Vector2 dir = mousePos - playerPos;

        // Raw angle from -180 to 180
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        float yRotation = 0f;
        float zRotation = angle;

        // If mouse is on the left
        if (Mathf.Abs(angle) > 90)
        {
            yRotation = 180f;
            // Mirror the Z rotation for the 180-degree Y flip
            zRotation = (180f - Mathf.Abs(angle)) * Mathf.Sign(angle);
        }

        _playerHand.transform.rotation = Quaternion.Euler(0, yRotation, zRotation + _activeWeaponAngleOffset);
        //damageSource.transform.rotation = Quaternion.Euler(0, yRotation, zRotation + activeWeaponAngleOffset);
    }
}
