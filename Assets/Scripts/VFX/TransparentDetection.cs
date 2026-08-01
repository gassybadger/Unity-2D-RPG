using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentDetection : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Component targetComponent;


    private FadeTarget _targetInternal;


    [SerializeField, Range(0, 1)] private float fadeAmount;
    [SerializeField] private float fadeDuration;

    private enum FadeDirection
    {
        Out,
        In
    }

    private bool isFadeActive;
    private FadeDirection direction;
    private float timeEntered;
    private float timeExited;

    private float originalAlpha;
    private float startAlpha;



    private void Awake()
    {
        if (targetComponent == null)
        {
            Debug.LogError($"Target must not be null on object {gameObject}");
            this.enabled = false;
            return;
        }

        _targetInternal = new FadeTarget(targetComponent);
        if (!_targetInternal.IsValid)
        {
            Debug.LogError($"Unsupported target - {targetComponent}. Target must be one of Tilemap or SpriteRenderer");
            this.enabled = false;
            return;
        }

        if (!TryGetComponent(out Collider2D _))
        {
            Debug.LogWarning($"Transparent Detection requires a collider - detection will not function for {gameObject}");
            this.enabled = false;
            return;
        }

        originalAlpha = _targetInternal.Color.a;
        enabled = false;
    }

    private void Update()
    {
        if (!isFadeActive) return;

        if (direction == FadeDirection.Out)
        {
            float fadeTime = (Time.time - timeEntered);
            if (fadeTime < fadeDuration)
            {
                // Do the fade.
                float newAlpha = Mathf.Lerp(startAlpha, fadeAmount, fadeTime / fadeDuration);
                _targetInternal.Color = new Color(
                    _targetInternal.Color.r,
                    _targetInternal.Color.g,
                    _targetInternal.Color.b,
                    newAlpha);

                return;
            }
        }
        else if (direction == FadeDirection.In)
        {
            float fadeTime = (Time.time - timeExited);
            if (fadeTime < fadeDuration)
            {
                float newAlpha = Mathf.Lerp(startAlpha, originalAlpha, fadeTime / fadeDuration);
                _targetInternal.Color = new Color(
                    _targetInternal.Color.r,
                    _targetInternal.Color.g,
                    _targetInternal.Color.b,
                    newAlpha);

                return;
            }
        }

        isFadeActive = false;
        enabled = false;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out PlayerController _))
        {
            // Not the player, so dont do anything.
            return;
        }

        startAlpha = _targetInternal.Color.a;
        direction = FadeDirection.Out;
        timeEntered = Time.time;
        isFadeActive = true;
        enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out PlayerController _))
        {
            // Not the player, so dont do anything.
            return;
        }

        startAlpha = _targetInternal.Color.a;
        direction = FadeDirection.In;
        timeExited = Time.time;
        isFadeActive = true;
        enabled = true;
    }


    private class FadeTarget
    {
        private object _target;

        public FadeTarget(Component target)
        {
            if (target.TryGetComponent(out SpriteRenderer renderer))
            {
                _target = renderer;
                IsValid = true;
                return;
            }
            else if (target.TryGetComponent(out Tilemap tilemap))
            {
                _target = tilemap;
                IsValid = true;
                return;
            }

            IsValid = false;
        }

        public bool IsValid { get; }

        public Color Color
        {
            get
            {
                if (_target is Tilemap tilemap)
                {
                    return tilemap.color;
                }
                else if (_target is SpriteRenderer renderer)
                {
                    return renderer.color;
                }

                return Color.white;
            }
            set
            {
                if(_target is SpriteRenderer renderer)
                {
                    renderer.color = value;
                }
                else if(_target is Tilemap tilemap)
                {
                    tilemap.color = value;
                }
            }
        }
    }
}
