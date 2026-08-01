using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Flash : MonoBehaviour
{
    [SerializeField] private Material damageFlashMaterial;
    [SerializeField] private Material deathFlashMaterial;

    [SerializeField] private float flashTime = .2f;

    private SpriteRenderer renderer;
    private Material defaultMaterial;

    private float startTime = 0;
    private Action flashCallback;


    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();

        defaultMaterial = renderer.material;
    }

    private void Update()
    {
        if (Time.time - startTime > flashTime)
        {
            renderer.material = defaultMaterial;
            flashCallback?.Invoke();
        }
    }

    public void DamageFlash()
    {
        startTime = Time.time;
        renderer.material = damageFlashMaterial;
    }

    public void DeathFlash(Action callback)
    {
        flashCallback = callback;

        startTime = Time.time;
        renderer.material = deathFlashMaterial;
    }
}
