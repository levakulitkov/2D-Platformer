using UnityEngine;
using UnityEngine.EventSystems;

public class ExitButton : MenuButton
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        Application.Quit();
    }
}