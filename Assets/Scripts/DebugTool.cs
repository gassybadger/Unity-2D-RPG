using System;
using UnityEngine;

public class DebugTool : MonoBehaviour
{
    [SerializeField] private EnemyAI _enemyPrefab;

    private void Update()
    {
        HandleSpawnSlime();
    }

    private void HandleSpawnSlime()
    {
        if (PlayerController.Instance.Input.Debug.Spawn.WasPressedThisFrame())
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(PlayerController.Instance.Input.Player.MouseLook.ReadValue<Vector2>());

            Instantiate(_enemyPrefab, new Vector3(position.x, position.y, 0), Quaternion.identity, null);
        }
    }
}
