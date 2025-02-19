using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Projectile laserPrefab;
    public float speed = 5.0f;
    private bool _laserActive;

    void Update()
    {
       // change pos of player based on speed
       if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) { // GetKEy instead of KetKeyDown bc GetKey is True every frame you're preessing down, keydown is only first frame
        this.transform.position += Vector3.left * this.speed * Time.deltaTime;
       } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
        this.transform.position += Vector3.right * this.speed * Time.deltaTime;
       }

       // laser
       if (Input.GetKeyDown(KeyCode.Space)) {
        Shoot();
       }
    }

    private void Shoot()
    {
        if (!_laserActive)
        {
            Projectile projectile = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
            projectile.destroyed += LaserDestroyed; // we want this function to be called when destroyed incoked in projectile.cs
            _laserActive = true;
        }
    }

    private void LaserDestroyed()
    {
        _laserActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Invader") ||
        other.gameObject.layer == LayerMask.NameToLayer("Missile")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
