using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked; // скрываем указатель мыши в центре экрана
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt)) {
            Cursor.lockState = CursorLockMode.None; // показываем указатель мыши в центре экрана
            Cursor.visible = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt)) {
            Cursor.lockState = CursorLockMode.Locked; // скрываем указатель мыши в центре экрана
            Cursor.visible = false;
        }
        if (Input.GetMouseButtonDown(0) && !Cursor.visible && !EventSystem.current.IsPointerOverGameObject()) { // проверяем, что GUI не используется
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0); // середина экрана - половина его ширины и высоты
            Ray ray = _camera.ScreenPointToRay(point); // создание в этой точке луча
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) { // выпущенный луч заполняет информацией переменную, на которую имеется ссылка
                GameObject hitObject = hit.transform.gameObject; // получаем объект, в который попал луч
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null) { // проверяем наличие у этого объекта компонента ReactiveTarget
                    target.ReactToHit(); // вызов метода для мишени
                    Messenger.Broadcast(GameEvent.ENEMY_HIT); // к реакции на попадание добавлена рассылка сообщения
                } else {
                    StartCoroutine(SphereIndicator(hit.point)); // запуск сопрограммы
                }
            }
        }
    }

    private void OnGUI() {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*"); // команда Label отображает на экране символ
    }

    // сопрограммы пользуются функциями IEnumerator
    private IEnumerator SphereIndicator(Vector3 pos) {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(1); // ключевое слово yield указывает сопрограмме, когда остановиться
        Destroy(sphere); // удаляем и очищаем память
    }
}
