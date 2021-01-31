using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class InteractableUIHandler : MonoBehaviour
{
    private MouseInteraction _interaction;
    private CandleBehaviour _candleData;
    [SerializeField] private Text fearLv, candleLv;
    [SerializeField] private Image interactableUi, notInRangeImage, key, rope, windowKey;

    private void Awake()
    {
        _interaction = FindObjectOfType<MouseInteraction>();
        _candleData = FindObjectOfType<CandleBehaviour>();
        _interaction.Hover += InteractableUI;
        _interaction.HoverOff += ResetUI;
    }

    private void Update()
    {
        key.gameObject.SetActive(_interaction._hasKey);
        rope.gameObject.SetActive(_interaction._hasRope);
        windowKey.gameObject.SetActive(_interaction._hasWindowKey);
        fearLv.text = "Fear: " + _candleData._fearLevel;
        candleLv.text = "Candle: " + _candleData._currentDuration;
        
    }

    private void InteractableUI(AbstractInteractable obj, Vector3 pos, bool inRange)
    {
        interactableUi.gameObject.SetActive(true);
        interactableUi.sprite = obj.interactionUI; //da cambiare nell'immagine
        interactableUi.transform.position = pos;
        notInRangeImage.enabled = !inRange;
    }

    private void ResetUI()
    {
        //if (interactableUiText.text == "") return;
        //Debug.Log("UI Off");
        interactableUi.gameObject.SetActive(false);
        notInRangeImage.enabled = false;
    }
}
