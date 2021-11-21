using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
            Debug.LogError("too many managers in the scene!");
        }
        else {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    public void ChangeScene(string sceneName) {
        DialogueManager.Instance.EndDialogue();
        SceneManager.LoadScene(sceneName);
    }

    public string GetSceneName() {
        return SceneManager.GetActiveScene().name;
    }

    [SerializeField] EnergyBar energyBar;
    public void ChangeEnergyValue(float value, bool fixedValue = false) {
        if (fixedValue) {
            energyBar.SetValue(value);
        }
        else {
            energyBar.ChangeValue(value);
        }
    }
    public void HideEnergyBar(bool value)
    {
        energyBar.gameObject.SetActive(value);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
