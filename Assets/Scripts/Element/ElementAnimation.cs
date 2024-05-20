using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementAnimation : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;
    private SpriteRenderer spriteRenderer;
    private static float animationInterval = 0.15f;
    private WaitForSeconds delay;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        delay = new WaitForSeconds(animationInterval);
    }

    private void Start()
    {
        StartCoroutine(StartAnimation());
    }
    private void OnEnable()
    {
        StartCoroutine(StartAnimation());
    }

    private IEnumerator StartAnimation()
    {
        int index = 0;

        while (true)
        {
            spriteRenderer.sprite = sprites[index];
            index++;
            index %= sprites.Count;
            yield return delay;
        }
    }
}
