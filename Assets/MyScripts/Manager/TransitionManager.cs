using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

public class TransitionManager : Singleton<TransitionManager>
{
    [SerializeField] private GameObject _leftBottomPart;
    [SerializeField] private GameObject _rightTopPart;

    [SerializeField] private RectTransform _leftBottomPartRectTransform;
    [SerializeField] private RectTransform _rightTopPartRectTransform;

    private Vector3 _leftBottomPartPos;
    private Vector3 _rightTopPartPos;

    void Start()
    {
        _leftBottomPartPos = _leftBottomPartRectTransform.anchoredPosition;
        _rightTopPartPos = _rightTopPartRectTransform.anchoredPosition;
    }

    public void TransitionAnimTrigger(Action nextFunc) 
    {
        StartCoroutine(TransitionAnim(nextFunc));
    }

    private IEnumerator TransitionAnim(Action nextFunc)
    {
        yield return new WaitForSeconds(2f);
        
        Sequence towardsMidSequence = DOTween.Sequence();
        towardsMidSequence.Append(_leftBottomPartRectTransform.DOAnchorPos(Vector2.zero, 1f))
                .Join(_rightTopPartRectTransform.DOAnchorPos(Vector2.zero, 1f));

        yield return towardsMidSequence.WaitForCompletion();
        
        nextFunc();
        yield return new WaitForSeconds(0.5f);

        Sequence towardsStartPosSequence = DOTween.Sequence();
        towardsMidSequence.Append(_leftBottomPartRectTransform.DOAnchorPos(_leftBottomPartPos, 1f))
                          .Join(_rightTopPartRectTransform.DOAnchorPos(_rightTopPartPos, 1f));

        yield return towardsStartPosSequence.WaitForCompletion();

    }
}
