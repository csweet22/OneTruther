using System;
using PrimeTween;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Character _character;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI statementText;
    [SerializeField] private Image img;
    [SerializeField] private AudioSource hover;
    [SerializeField] private AudioSource click;

    public void Init(Character character)
    {
        _character = character;
        img.color = _character.Color;

        Color textColor = _character.Color.grayscale > 0.5f ? Color.black : Color.white;
        
        nameText.text = $"<color=#{textColor.ToHexString().Substring(0,6)}>{_character.Name}";
        statementText.text = $"<color=#{textColor.ToHexString().Substring(0,6)}>{_character.Statement}";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tween.Scale(transform, endValue: 1.2f, duration: 0.1f, ease: Ease.InOutSine);
        Tween.LocalRotation(transform, endValue: Quaternion.Euler(new Vector3(0f,0f,1f)), duration: 0.1f, ease: Ease.InOutSine);
        // transform.SetAsLastSibling();
        GetComponent<Canvas>().sortingOrder = 10;
        hover.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tween.Scale(transform, endValue: 1f, duration: 0.1f, ease: Ease.InOutSine);
        Tween.LocalRotation(transform, endValue: Quaternion.identity, duration: 0.1f, ease: Ease.InOutSine);
        GetComponent<Canvas>().sortingOrder = 1;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        click.Play();
        
        if (_character.IsTellingTheTruth)
        {
            print("You did it! Loading next level");
            PuzzleGenerator.Instance.NextLevel();
        }
        else
        {
            print("Wrong! Try again!");
            PuzzleGenerator.Instance.ReloadLevel();
        }
    }
}
