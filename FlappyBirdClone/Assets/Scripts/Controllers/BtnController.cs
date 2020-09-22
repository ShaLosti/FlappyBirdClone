using UnityEngine;
using UnityEngine.EventSystems;

public class BtnController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(SoundManager.PlaySound(Enums.AudioSounds.ButtonEnter, () => { }));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}