using System;
using Febucci.UI;
using Febucci.UI.Core;
using TMPro;
using UnityEngine;

public class PlayTextAnimatorOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponentInChildren<TextAnimator_TMP>().ResetState();
        var writer = GetComponentInChildren<TypewriterCore>();
        if (writer)
        {
            writer.StartShowingText(true);
            writer.ShowText(GetComponentInChildren<TextMeshProUGUI>().text);
        }
        var audioSource = GetComponentInChildren<AudioSource>();
        if(audioSource)
            audioSource.Play();
    }
}
