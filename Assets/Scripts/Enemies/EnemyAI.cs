using System.Collections;
using UnityEngine;

public class EnemyAI : AbstractDamageable
{
    [field: SerializeField] public EnemyClassSO ClassSO { get; private set; }

    private enum State
    {
        Roaming
    }

    private State state;
    private EnemyPathfinding enemyPathfinding;


    protected override void Awake()
    {
        base.Awake();

        MaxHealth = ClassSO.MaxHealth;
        CurrentHealth = MaxHealth;

        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }

    private void Start()
    {
        EventBus<EnemySpawned>.Raise(new EnemySpawned());
        StartCoroutine(RoamingRoutine());
    }


    protected override void OnDeath()
    {
        base.OnDeath();
        EventBus<EnemyDied>.Raise(new EnemyDied());
    }

    protected override ParticleSystem GetDeathVFX() => ClassSO.DeathVFX;

    
    private IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            
            enemyPathfinding.MoveTo(roamPosition);

            yield return new WaitForSeconds(1);
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }
}
