using Unity.VisualScripting;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public Sprite[] animationSprites;
    public float animationTime = 1.0f; // how often does it cycle to next sprite
    public System.Action killed;
    private SpriteRenderer _spriteRenderer;
    private int _animationFrame;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>(); // will look under same game obj script is attached to, look for speecified component
    }

    private void Start() {
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime); // call AnimateSprite() every animationTime seconds
    }

    private void AnimateSprite()
    {
        _animationFrame++;

        // if exceeded frames provied, loop back to beginning
        if (_animationFrame >= this.animationSprites.Length) {
            _animationFrame = 0;
        }

        _spriteRenderer.sprite = this.animationSprites[_animationFrame];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser")) {
            this.killed.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
