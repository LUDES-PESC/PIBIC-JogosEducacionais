﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraBlock : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public void OnPointerEnter(PointerEventData eventData)
    {
        CameraMovement.canBeMoved++;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        CameraMovement.canBeMoved--;
    }
}
