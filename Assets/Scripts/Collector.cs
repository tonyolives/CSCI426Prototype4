using UnityEngine;

public class Collector : MonoBehaviour
{
    
    public GameManager gameManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            gameManager.ammo = gameManager.ammo + 1;
            gameManager.AmmoGain();
        }
    }

}
