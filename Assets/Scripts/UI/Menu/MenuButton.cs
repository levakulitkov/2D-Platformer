using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private float _fontSizeMultiplier = 0.96f;
    
    private Image _playerHeadImage;
    private TMP_Text _text;
    private float _defaultFontSize;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _playerHeadImage = GetComponentInChildren<Image>();
        _playerHeadImage.enabled = false;
    }

    private void Start()
    {
        _defaultFontSize = _text.fontSize;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        float newValue = _defaultFontSize * _fontSizeMultiplier;

        _text.fontSize = Mathf.Clamp(newValue, _text.fontSizeMin, _text.fontSizeMax);

        _playerHeadImage.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _text.fontSize = _defaultFontSize;

        _playerHeadImage.enabled = false;
    }

    public abstract void OnPointerClick(PointerEventData eventData);
}
