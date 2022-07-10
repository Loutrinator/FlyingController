using UnityEngine;

public class RoundShadow : PositionTracking
{
    [SerializeField] private Renderer _renderer;
    private void LateUpdate()
    {
        Track();
        if (_renderer != null)
        {
            _renderer.enabled = target.position.y > transform.position.y;
        }

    }
}