using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text Lv2Text;
    public Text Lv3Text;
    public Text Lv4Text;
    public Text ammoCountText;
    public int ammo;
    public int Lv2;
    public int Lv3;
    public int Lv4;
    [SerializeField] private AudioManager audioManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ammo = 0;
        Lv2 = 5;
        Lv3 = 10;
        Lv4 = 20;
        Lv2Text.text = "";
        Lv3Text.text = "";
        Lv4Text.text = "";
        ammoCountText.text = "Total Ammo Gained: " + ammo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void AmmoGain()
    {
        audioManager.Gain();
        Debug.Log("Gained!");
        ammoCountText.text = "Total Ammo Gained: " + ammo;

        if (ammo == Lv2)
        {
            audioManager.Lv2();
            Debug.Log("Level 2 Online!");
            Lv2Text.text = "Spread (LShift)";
        }
        else if (ammo == Lv3)
        {
            audioManager.Lv3();
            Debug.Log("Level 3 Online!");
            Lv3Text.text = "Piercing Shot (LMB)";
        }
        else if (ammo == Lv4)
        {
            audioManager.Lv4();
            Debug.Log("Level 4 Online!");
            Lv4Text.text = "Flak Shot (RMB)";
        }
    }
}
