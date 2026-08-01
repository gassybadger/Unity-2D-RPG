using TMPro;
using UnityEngine;

public class RuntimeUI : MonoBehaviour
{
    private const string ENEMY_COUNT_TEXT_FORMAT = "Enemies Left: {0}";

    [SerializeField] private TextMeshProUGUI _enemyCountText;

    private int _enemyCount;


    private void Awake()
    {
        EventBus<EnemySpawned>.OnEvent += HandleEnemySpawnedEvent;
        EventBus<EnemyDied>.OnEvent += HandleEnemyDiedEvent;
    }

    private void HandleEnemyDiedEvent(EnemyDied @event)
    {
        _enemyCount--;
        if (_enemyCount < 0) _enemyCount = 0;

        _enemyCountText.SetText(string.Format(ENEMY_COUNT_TEXT_FORMAT, _enemyCount));
    }

    private void HandleEnemySpawnedEvent(EnemySpawned @event)
    {
        _enemyCountText.SetText(string.Format(ENEMY_COUNT_TEXT_FORMAT, ++_enemyCount));
    }
}