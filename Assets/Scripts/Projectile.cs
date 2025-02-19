using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction; // we want to set this in the editor (want to use up for lasers, up for bullet)
    public float speed;
    public System.Action destroyed;

    private void Update() {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    // detect collision
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Invader"))
        {
            if (this.destroyed != null) {
            this.destroyed.Invoke(); // destroy is invoked - its a way to allow other scripts to register when some event happens
        }
        Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Boundary"))
        {
            if(this.destroyed != null) {
            this.destroyed.Invoke(); // destroy is invoked - its a way to allow other scripts to register when some event happens
            Destroy(this.gameObject);
        }
        }
    }
}
