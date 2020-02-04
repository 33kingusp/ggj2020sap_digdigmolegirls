using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCircleBase : MonoBehaviour
{
    public enum AuthPlayer
    {
        All = 0,
        Player1 = 1,
        Player2 = 2,
    } 

    [SerializeField] private SpriteRenderer selectRenderer = default;
    [SerializeField] private float selectTime = 3.0f;
    [SerializeField] private AuthPlayer authPlayer = default;
    private Vector2 defaultSize = default;
    protected bool isTrue = false;

    private void Awake()
    {
        defaultSize = selectRenderer.size;
        Reset();
    }

    private void Update()
    {
        if (isTrue)
            OnTrueStay();
    }

    private void Reset()
    {
        var size = defaultSize;
        size.y = 0;
        selectRenderer.size = size;
        if (isTrue)
        {
            isTrue = false;
            OnFalse();
        }
    }

    private IEnumerator CountTimer()
    {
        float t = 0;
        var size = selectRenderer.size;

        while (t <= 1)
        {
            size.y = defaultSize.y * t;
            selectRenderer.size = size;
            yield return null;
            t += Time.deltaTime / selectTime;
        }
        selectRenderer.size = defaultSize;
        isTrue = true;
        OnTrue();
    }

    /// <summary>
    /// プレイヤー侵入時
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (authPlayer == 0 || collision.name == authPlayer.ToString())
            StartCoroutine(CountTimer());
    }

    /// <summary>
    /// プレイヤー離脱時
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (authPlayer == 0 || collision.name != authPlayer.ToString())
        {
            StopCoroutine(CountTimer());
            Reset();
        }
    }

    protected virtual void OnTrue(){}
    protected virtual void OnTrueStay() { }
    protected virtual void OnFalse() { }
}
