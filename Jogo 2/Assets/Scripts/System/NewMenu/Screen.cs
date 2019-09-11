using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Screen : MonoBehaviour
{
    private const float TRANSITION_LENGTH = 0.25f;

    public abstract IEnumerator Open(float duration = TRANSITION_LENGTH);
    public abstract IEnumerator Close(float duration = TRANSITION_LENGTH);
    public abstract void OnOpen();
    public abstract void OnClose();}
