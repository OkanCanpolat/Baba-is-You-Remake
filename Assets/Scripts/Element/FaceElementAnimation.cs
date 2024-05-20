using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[Serializable]
public class FaceSpriteWrapper
{
    public List<Sprite> Sprites;
}
public class FaceElementAnimation : MonoBehaviour
{
    [SerializeField] private List<FaceSpriteWrapper> faceRightSprites;
    [SerializeField] private List<FaceSpriteWrapper> faceLeftSprites;
    [SerializeField] private List<FaceSpriteWrapper> faceUpSprites;
    [SerializeField] private List<FaceSpriteWrapper> faceDownSprites;
    [SerializeField] private List<Sprite> currentSprites;
    private SpriteRenderer spriteRenderer;
    private Element element;
    private static float animationInterval = 0.15f;
    private WaitForSeconds delay;
    private SignalBus signalBus;
    private int index;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
        signalBus.Subscribe<MovemenetSignal>(OnMove);
    }

    private void Awake()
    {
        element = GetComponent<Element>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        delay = new WaitForSeconds(animationInterval);
    }
    private void Start()
    {
        SetInitialSprites();
        StartCoroutine(StartAnimation());
    }
    private void OnMove(MovemenetSignal signal)
    {
        if (signal.Element != element) return;

        switch (signal.Direction)
        {
            case Vector3Int v when v.Equals(Vector3Int.right):
                index++;
                index %= faceRightSprites.Count;
                currentSprites = faceRightSprites[index].Sprites;
                break;
            case Vector3Int v when v.Equals(Vector3Int.left):
                index++;
                index %= faceLeftSprites.Count;
                currentSprites = faceLeftSprites[index].Sprites;
                break;
            case Vector3Int v when v.Equals(Vector3Int.up):
                index++;
                index %= faceUpSprites.Count;
                currentSprites = faceUpSprites[index].Sprites;
                break;
            case Vector3Int v when v.Equals(Vector3Int.down):
                index++;
                index %= faceDownSprites.Count;
                currentSprites = faceDownSprites[index].Sprites;
                break;
        }
    }

    private IEnumerator StartAnimation()
    {
        int index = 0;

        while (true)
        {
            spriteRenderer.sprite = currentSprites[index];
            index++;
            index %= currentSprites.Count;
            yield return delay;
        }
    }
    private void SetInitialSprites()
    {
        switch (element.FacingDirection)
        {
            case FacingDirection.Up:
                currentSprites = faceUpSprites[index].Sprites;
                break;
            case FacingDirection.Down:
                currentSprites = faceDownSprites[index].Sprites;
                break;
            case FacingDirection.Right:
                currentSprites = faceRightSprites[index].Sprites;
                break;
            case FacingDirection.Left:
                currentSprites = faceLeftSprites[index].Sprites;
                break;
        }
    }

    private void OnDestroy()
    {
        signalBus.Unsubscribe<MovemenetSignal>(OnMove);
    }
}
