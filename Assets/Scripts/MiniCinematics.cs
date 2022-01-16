using System.Collections;
using UnityEngine;

public class MiniCinematics : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (ControlPlayer.Instance.executing != true)
        {
            StartCoroutine(OnStarting(other.gameObject));
        }
    }

    IEnumerator OnStarting(GameObject obj)
    {

            TextingComponent text = obj.GetComponent<TextingComponent>();
            ICinematic cinematic = obj.GetComponent<ICinematic>();
            if (text == null && cinematic == null)
            {
                yield break;
            }

            if (cinematic != null)
            {
                if(obj.GetComponent<Timer>().noStart == true)
                {
                    yield break;
                }
                float time = obj.GetComponent<Timer>().time;
                InGameController.Instance.UIForGame.SetActive(false);
                ControlPlayer.Instance.executing = true;
                cinematic.OnStartCinematic();
                yield return new WaitForSeconds(time);
                if(ControlPlayer.Instance.executing == false)
                {
                yield break;
                }
                ControlPlayer.Instance.executing = false;
                InGameController.Instance.UIForGame.SetActive(true);
            }
            else if (text != null)
            {
                TextingManager.Instance.Talk(text.Lines[0]);
            }
            Destroy(obj.gameObject);
    }

}
