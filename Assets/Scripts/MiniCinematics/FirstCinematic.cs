using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FirstCinematic : MonoBehaviour, ICinematic
{
    [SerializeField] Transform[] ToLook;
    [SerializeField] Transform[] ToGo;
    [SerializeField] TextingComponent text;
    public static event Action OnFirstCinematic;

    private void Start()
    {
        text = GetComponent<TextingComponent>();
    }
    public void OnStartCinematic()
    {
        StartCoroutine(FirstScene());
    }
    private void Update()
    {
        if(ControlPlayer.Instance.executing == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                StopCinematic();
            }
        }
    }
    void StopCinematic()
    {
        ControlPlayer.Instance.StopCoroutines();
        ControlPlayer.Instance.player.position = ToGo[0].position;
        ControlPlayer.Instance.executing = false;
        InGameController.Instance.UIForGame.SetActive(true);
        OnFirstCinematic?.Invoke();
        Destroy(gameObject);
    }
    IEnumerator FirstScene()
    {
        ControlPlayer.Instance.MoveTo(ToGo[0]);
        yield return new WaitForSeconds(2.5f);
        TextingManager.Instance.Talk(text.Lines[0]);
        ControlPlayer.Instance.LookAt(ToLook[0]);
        yield return new WaitForSeconds(11f);
        ControlPlayer.Instance.LookAt(ToLook[1]);
        TextingManager.Instance.Talk(text.Lines[1]);
        yield return new WaitForSeconds(10.5f);
        OnFirstCinematic?.Invoke();
    }
}
