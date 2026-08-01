using UnityEngine;

public class Projectile : MonoBehaviour
{
    private const float MIN_SPEED = 0.0001f;

    [SerializeField] private AmmoTypeSO _ammoTypeSO;

    private float expiryTime;
    private bool expired;

    private Vector3 _previousPosition;
    private WeaponTypeSO _firedFrom;
    private int _damageTotal;
    private float _speed;

   
    private void Update()
    {
        if (Time.time >= expiryTime)
        {
            Destroy(gameObject);
            return;
        }

        _previousPosition = transform.position;

        transform.Translate(Vector3.right * Time.deltaTime * _speed);
    }


    public void Fire(WeaponTypeSO firedFrom)
    {
        _firedFrom = firedFrom;

        _speed = Mathf.Max(_ammoTypeSO.Speed, MIN_SPEED);

        _damageTotal = Mathf.RoundToInt(Mathf.Max(firedFrom.Damage * _ammoTypeSO.DamageModifier, 0));

        _previousPosition = transform.position;

        expiryTime = Time.time + ((firedFrom.Range * _ammoTypeSO.RangeModifier) / _speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (expired) { return; }

        bool hasHitSomething = false;
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            hasHitSomething = true;

            damageable.Damage(
                new IDamageable.DamageContext(
                    _damageTotal, 
                    _previousPosition,
                    _firedFrom.AppliesKnockback ? _firedFrom.KnockbackAmount : 0f
                )
            );
        }
        else if (collision.gameObject.TryGetComponent(out ImmoveableObject _))
        {
            hasHitSomething = true;
        }

        if (hasHitSomething)
        {
            // Spawn VFX
            Destroy(gameObject);
        }
    }
}
