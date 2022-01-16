using System.Collections;
using UnityEngine;
using System;

public class SecondCinematic : MonoBehaviour, ICinematic
{
    [SerializeField] Transform[] ToLook;
    [SerializeField] Transform[] ToGo;
    public GameObject door;
    public EntryDoor entryDoor;
    public TextingComponent text;
    public static event Action OnSecondCinematic;
    public void OnStartCinematic()
    {
            StartCoroutine(SecondScene());
    }
    void Start()
    {
        text = GetComponent<TextingComponent>();
        entryDoor = door.GetComponent<EntryDoor>();
    }
    void Update()
    {
        Timer noStart = GetComponent<Timer>();
        if(entryDoor.openned == true)
        {
            noStart.noStart = true;
        } else
        {
            noStart.noStart = false;
        }
    }

    IEnumerator SecondScene()
    {
        ControlPlayer.Instance.MoveTo(ToGo[0]);
        yield return new WaitForSeconds(1);
        ControlPlayer.Instance.LookAt(ToLook[0]);
        TextingManager.Instance.Talk(text.Lines[0]);
        yield return new WaitForSeconds(5f);
        ControlPlayer.Instance.StopCoroutines();
        ControlPlayer.Instance.LookAt(ToLook[1]);
        TextingManager.Instance.Talk(text.Lines[1]);
        yield return new WaitForSeconds(7f);
        ControlPlayer.Instance.StopCoroutines();
        ControlPlayer.Instance.LookAt(ToLook[0]);
        yield return new WaitForSeconds(1f);
        ControlPlayer.Instance.StopCoroutines();
        OnSecondCinematic?.Invoke();
    }
}
