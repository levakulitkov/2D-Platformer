using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartButton : MenuButton
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(Scenes.Level1);
    }
}