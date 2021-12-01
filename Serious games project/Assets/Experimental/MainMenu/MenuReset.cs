using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuReset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {  
        if(GameManager.Instance != null) {
            Destroy(GameManager.Instance.transform.root.gameObject);
        }        
    }
}
