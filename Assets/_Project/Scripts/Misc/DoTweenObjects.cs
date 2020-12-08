using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UniRx;
using UnityEngine;

public class DoTweenObjects : MonoBehaviour
{
    [SerializeField] private bool _canMove;
    [SerializeField] private bool _canRotate;
    
    [SerializeField] private Vector3 _moveTo;
    [SerializeField] private float _moveDuration;
     [SerializeField] private Vector3 _rotateTo;
    [SerializeField] private float _rotateDuration;
    
    void Start()
    {
        transform.DOLocalMove(_moveTo, _moveDuration).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
        transform
            .DOLocalRotate(_rotateTo, _rotateDuration, 
                RotateMode.FastBeyond360)
            .SetEase(Ease.Linear).SetLoops(-1);
    }

  
}
