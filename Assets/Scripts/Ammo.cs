using UnityEngine;

public class Ammo : MonoBehaviour
{
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hopper"))
        {
            Destroy(gameObject);
        }
    }
}
