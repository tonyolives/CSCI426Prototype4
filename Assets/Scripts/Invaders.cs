using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Invaders : MonoBehaviour
{
    public Invader[] prefabs;
    public int rows = 5;
    public int columns = 11;
    public float spacing = 2.0f;
    public AnimationCurve speed; // x is percent y is speed
    public Projectile missilePrefab;
    public float missileAttackRate = 1.0f;
    public int amountKilled { get; private set; }
    public int amountAlive => this.totalInvaders - this.amountKilled;
    public int totalInvaders => this.rows * this.columns; // => turns this into calculated property
    public float percentKilled => (float)this.amountKilled / (float)this.totalInvaders;
    private Vector3 _direction = Vector2.right;
    public Ammo ammoPrefab = null;
    [SerializeField] private AudioManager audioManager;

    private void Awake()
    {
        for(int row = 0; row < this.rows; row++) {

            // width and height
            float width = spacing * (this.columns - 1);
            float height = spacing * (this.rows - 1);

            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * spacing), 0.0f);

            for (int col = 0; col < this.columns; col++) {
                Invader invader = Instantiate(this.prefabs[row], this.transform);
                invader.killed += InvaderKilled;
                Vector3 position = rowPosition;
                position.x += col * spacing;
                invader.transform.localPosition = position;
            }
        }
    }

    private void Start() 
    {
        InvokeRepeating(nameof(MissileAttack), this.missileAttackRate, this.missileAttackRate);
    }

    private void Update()
    {
        this.transform.position += _direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime; // mult by delta time to ensure smooth movement at different fps

        // flip when hit edge of screen
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        // check invader positions
        foreach (Transform invader in this.transform) // loop through child transform
        {
            // ensure invader not disabled
            if (!invader.gameObject.activeInHierarchy) {
                continue; // jump to next invader
            }
            // check if invader has touched edge
            if (_direction == Vector3.right && invader.position.x >= (rightEdge.x - 1.0f)) {
                // flip dir and advance to next row
                AdvanceRow();
            } else if (_direction == Vector3.left && invader.position.x <= (leftEdge.x + 1.0f)) {
                AdvanceRow();
            }
            
        }
    }

    private void AdvanceRow()
    {
        // flip dir
        _direction.x *= -1.0f;
        
        // advance row
        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }

    private void MissileAttack()
    {
        // the more there are alive, the smaller chance to spawn missile
        foreach (Transform invader in this.transform) // loop through child transform
        {
            // ensure invader not disabled
            if (!invader.gameObject.activeInHierarchy) {
                continue; // jump to next invader
            }

            if (Random.value < (1.0f / (float)this.amountAlive)) {
                Instantiate(this.missilePrefab, invader.position, Quaternion.identity);
                break;
            }
        }
    }

    private void InvaderKilled()
    {
        audioManager.Dmg();
        Instantiate(ammoPrefab, transform.position, Quaternion.identity);
        
        // keep track of num killed
        this.amountKilled++;

        // auto reload of the scene when all invaders are killed
        if (this.amountKilled >= this.totalInvaders) {
            audioManager.Win();
        }
    }
}