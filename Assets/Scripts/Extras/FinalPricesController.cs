using System.Collections;
using UnityEngine;

public class FinalPricesController : MonoBehaviour
{
    [SerializeField] GameObject[] eyes;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(default, Vector2.zero, CursorMode.Auto);
        StartCoroutine(EyesActive());
    }
    IEnumerator EyesActive()
    {
        for (int i = 0; i < GameManager.Instance.finals.Length; i++)
        {
            if (GameManager.Instance.finals[i] == false)
            {
                continue;
            }
            eyes[i].SetActive(true);
            yield return new WaitForSecondsRealtime(1.25f);
        }
    }
}
