using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : MonoBehaviour
{
    [SerializeField] private Slider speedSlider;
    [SerializeField] private InputField nameField;

    // Start is called before the first frame update
    void Start()
    {
        speedSlider.value = PlayerPrefs.GetFloat("speed", 0f);
        nameField.text = PlayerPrefs.GetString("name", "Name");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open() {
        gameObject.SetActive(true); // активируем объект, чтобы открыть окно
    }

    public void Close() {
        gameObject.SetActive(false); // деактивируем объект, чтобы закрыть окно
    }

    public void OnSubmitName(string name) { // этот метод срабатывает в момент начала ввода данных в текстовое поле
        Debug.Log(name);
        PlayerPrefs.SetString("name", name);
    }

    public void OnSpeedValue(float speed) { // этот метод срабатывает при изменении положения ползунка
        Debug.Log("Speed: " + speed);
        Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speed);
        PlayerPrefs.SetFloat("speed", speed);
    }
}
