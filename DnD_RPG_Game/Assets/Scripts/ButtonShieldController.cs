using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShieldController : MonoBehaviour
{
    public Button buttonA;
    public Button buttonB;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(TestCoroutine());

        Create_Shield();
    }

    void Create_Shield()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-4f, 4f), Random.Range(-4f, 4f), 0); // генерируем случайное положение для кнопки Б
        buttonB.transform.position = buttonA.transform.position + randomPosition; // устанавливаем случайное положение для кнопки Б относительно кнопки А

    }

    public void DestroyButtonB()
    {
        //Destroy(buttonB.gameObject); // удаляем кнопку Б

        Create_Shield();
    }

    IEnumerator TestCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            Vector3 randomPosition = new Vector3(Random.Range(-4f, 4f), Random.Range(-4f, 5f), 0); // генерируем случайное положение для кнопки Б
            buttonB.transform.position = buttonA.transform.position + randomPosition; // устанавливаем случайное положение для кнопки Б относительно кнопки А

        }
    }
}
