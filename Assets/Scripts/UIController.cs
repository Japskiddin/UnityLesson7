using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text scoreLabel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = Time.realtimeSinceStartup.ToString();    
    }

    public void OnOpenSettings() {
        Debug.Log("Open settings"); // метод, вызываемый кнопкой настроек
    }

    public void OnPointerDown() {
        Debug.Log("Pointer down");
    }
}
