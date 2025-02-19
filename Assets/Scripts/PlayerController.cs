using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Projectile laserPrefab;
    public APProjectile apLaserPrefab;
    public BurstProjectile burstLaserPrefab;
    public FlakProjectile flakLaserPrefab;
    public float speed = 5.0f;
    private bool _laserActive;
    private bool _pierceActive;
    private bool _burstActive;
    private bool _flakActive;

    public SpriteRenderer spriteRenderer;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameManager gameManager;

    void start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

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
       // Burst
       if (Input.GetKeyDown(KeyCode.LeftShift)) {
        Burst();
       }
        // Pierce
       if (Input.GetKeyDown(KeyCode.Mouse0)) {
        Pierce();
       }
       // Flak
       if (Input.GetKeyDown(KeyCode.Mouse1)) {
        Flak();
       }
    }

    private void Shoot()
    {
        if (!_laserActive)
        {
            audioManager.Shoot();
            Projectile projectile = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
            projectile.destroyed += LaserDestroyed; // we want this function to be called when destroyed incoked in projectile.cs
            _laserActive = true;
        }
    }

    private void Pierce()
    {
        if( gameManager.ammo >= gameManager.Lv3)
        {
            if (!_pierceActive)
            {
            audioManager.Shoot();
            APProjectile projectile = Instantiate(this.apLaserPrefab, this.transform.position, Quaternion.identity);
            projectile.apDestroyed += PierceDestroyed; // we want this function to be called when destroyed incoked in projectile.cs
            _pierceActive = true;
            }
        }
        
    }

    private void Burst()
    {
        if( gameManager.ammo >= gameManager.Lv2)
        {
            if (!_burstActive)
            {   
                audioManager.Shoot();
                BurstProjectile projectile = Instantiate(this.burstLaserPrefab, this.transform.position, Quaternion.identity);
                projectile.burstDestroyed += BurstDestroyed; // we want this function to be called when destroyed incoked in projectile.cs
                _burstActive = true;
            }
        }

    }

    private void Flak()
    {
        if( gameManager.ammo >= gameManager.Lv4)
        {
            if (!_flakActive)
            {   
                audioManager.Shoot();
                FlakProjectile projectile = Instantiate(this.flakLaserPrefab, this.transform.position, Quaternion.identity);
                projectile.flakDestroyed += FlakDestroyed; // we want this function to be called when destroyed incoked in projectile.cs
                _flakActive = true;
            }
        }
            
    }

    private void LaserDestroyed()
    {
        _laserActive = false;
    }

        private void PierceDestroyed()
    {
        _pierceActive = false;
    }

    private void BurstDestroyed()
    {
        _burstActive = false;
    }

    private void FlakDestroyed()
    {
        _flakActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Invader") ||
        other.gameObject.layer == LayerMask.NameToLayer("Missile")) {
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.enabled = false;
        audioManager.Die();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
