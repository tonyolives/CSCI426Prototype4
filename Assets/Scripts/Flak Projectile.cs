using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlakProjectile : MonoBehaviour
{
    public Vector3 direction; // we want to set this in the editor (want to use up for lasers, up for bullet)
    public float speed;
    public System.Action flakDestroyed;
    CircleCollider2D explosion;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        explosion = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.explosion.enabled = false;
    }
    
    private void Update() {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    // detect collision
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Invader"))
        {
            spriteRenderer.enabled = false;
            this.speed = 0;
            this.explosion.enabled = true;
            StartCoroutine(Pause()); 
        }        
        else if (other.gameObject.CompareTag("Boundary"))
        {
            if(this.flakDestroyed != null) {
            this.flakDestroyed.Invoke(); // destroy is invoked - its a way to allow other scripts to register when some event happens
            Destroy(this.gameObject);
        }
        }
        
        
        
        
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(0.1f);
        if (this.flakDestroyed != null) {
            this.flakDestroyed.Invoke(); // destroy is invoked - its a way to allow other scripts to register when some event happens
        }
        Destroy(this.gameObject);
        
    }
}
