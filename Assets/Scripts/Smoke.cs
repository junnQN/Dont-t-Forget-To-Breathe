using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem smokeParticle;

    [SerializeField]
    private float rateOverTime = 50;

    [SerializeField]
    private bool isSwinging = false;

    [SerializeField]
    private float swingTime = 3f;

    [SerializeField]
    private float swingAngle = 40f;

    [SerializeField]
    private float targetAngleShape = 70f;

    [HideInInspector]
    public bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Init()
    {
        var emission = smokeParticle.emission;
        emission.rateOverTime = rateOverTime;
        ShowSmoke();

        if (isSwinging)
        {
            SetSwinging();
        }
        else
        {
            SetShapeAngle();
        }
    }

    Tween swingTween;
    private void SetSwinging()
    {
        swingTween?.Kill();

        swingTween = DOVirtual.Float(-1, 1, swingTime, (v) =>
        {
            var shape = smokeParticle.shape;
            var rotation = shape.rotation;
            rotation.y = swingAngle * v;
            shape.rotation = rotation;
        }).SetLoops(-1, LoopType.Yoyo)
        .SetEase(Ease.InOutSine);
    }

    Tween shapeTween;
    private void SetShapeAngle()
    {
        shapeTween?.Kill();
        shapeTween = DOVirtual.Float(30, targetAngleShape, 2f, (v) =>
        {
            var shape = smokeParticle.shape;
            shape.angle = v;
        });
    }

    public void ReduceSmoke()
    {
        StartCoroutine(ReduceSmokeOverTime());
    }

    private IEnumerator ReduceSmokeOverTime(float duration = 1f)
    {
        float elapsedTime = 0f;
        float startRate = smokeParticle.emission.rateOverTime.constant;
        float endRate = 0f;

        var emission = smokeParticle.emission;
        while (elapsedTime < duration)
        {
            float rate = Mathf.Lerp(startRate, endRate, elapsedTime / duration);
            emission.rateOverTime = rate;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        emission.rateOverTime = endRate;

        HideSmoke();
    }

    public void ShowSmoke()
    {
        isPlaying = true;
        smokeParticle.Play();
    }

    public void HideSmoke()
    {
        isPlaying = false;
        smokeParticle.Stop();
    }

    void Update()
    {
        var boxCollider = GetComponent<BoxCollider2D>();
        // Lấy ra thông tin về kích thước và vị trí của BoxCollider2D
        Vector2 size = boxCollider.size;
        Vector2 center = (Vector2)transform.position + boxCollider.offset;

        // Kiểm tra overlap với các Collider 2D trong BoxCollider2D
        Collider2D[] colliders = Physics2D.OverlapBoxAll(center, size, 0f);

        // Kiểm tra từng Collider 2D có overlap với Collider 2D của đối tượng hay không
        foreach (Collider2D collider in colliders)
        {
            if (isPlaying && collider != null && collider.CompareTag("WaterGlass") && collider != boxCollider)
            {
                Debug.Log("<color=red>Smoke is in water</color>");
                GameManager.instance.HideSmoke();
            }
        }
    }
}
