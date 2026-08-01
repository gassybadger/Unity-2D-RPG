using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Class", menuName = "Enemies/Class", order = 100)]
public class EnemyClassSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int MaxHealth { get; private set; }

    // May not need to "live" here....
    [field: SerializeField] public ParticleSystem DeathVFX { get; private set; }
}
