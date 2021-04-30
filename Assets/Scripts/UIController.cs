using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text scoreLabel;
    [SerializeField] private SettingsPopup settingsPopup;
    private int _score;

    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        scoreLabel.text = _score.ToString();
        settingsPopup.Close(); // закрываем всплывающее окно в начале игры

    }

    // Update is called once per frame
    void Update()
    {
    }

    void Awake() {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit); // объявляем, какой метод отвечает на событие ENEMY_HIT
    }

    void OnDestroy() {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit); // при разрушении объекта удаляет подписчика
    }

    public void OnOpenSettings() {
        Debug.Log("Open settings"); // метод, вызываемый кнопкой настроек
        settingsPopup.Open();
    }

    public void OnPointerDown() {
        Debug.Log("Pointer down");
    }

    private void OnEnemyHit() {
        _score += 1; // увеличиваем score в ответ на данное событие
        scoreLabel.text = _score.ToString();
    }
}
