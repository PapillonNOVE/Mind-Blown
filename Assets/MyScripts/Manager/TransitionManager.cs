using UnityEngine;
using DG.Tweening;
using System;

public class TransitionManager : Singleton<TransitionManager>
{
    [Header("Values")]
    [Range(0f, 10f)]
    [SerializeField] private float animationTime;

    [Space(10)]
    [Range(0f, 10f)]
    [SerializeField] private float animationDelay;

    [Space(20)]
    [SerializeField] private Vector2 leftBottomOffset;

    [Space(10)]
    [SerializeField] private Vector2 rightTopOffset;

    [Header("Objects")]
    [SerializeField] private GameObject leftBottomPart;
    [SerializeField] private GameObject rightTopPart;
    [SerializeField] private GameObject cover;

    [Header("RectTransforms")]
    [SerializeField] private RectTransform leftBottomPartRectTransform;
    [SerializeField] private RectTransform rightTopPartRectTransform;
    [SerializeField] private RectTransform coverRectTransform;

    private Vector3 _leftBottomPartPos;
    private Vector3 _rightTopPartPos;

    void Start()
    {
        _leftBottomPartPos = leftBottomPartRectTransform.anchoredPosition;
        _rightTopPartPos = rightTopPartRectTransform.anchoredPosition;
    }

    public void TransitionAnimation(Action targetMethod)
    {
        leftBottomPart.SetActive(true);
        rightTopPart.SetActive(true);

        Sequence seq = DOTween.Sequence();

        seq.Append(leftBottomPartRectTransform.DOAnchorPos(Vector2.zero + leftBottomOffset, animationTime).SetEase(Ease.OutBounce));
        seq.Join(rightTopPartRectTransform.DOAnchorPos(Vector2.zero - rightTopOffset, animationTime).SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                cover.SetActive(true);
                targetMethod();
            }));

        seq.AppendInterval(animationDelay);
        seq.AppendCallback(() => cover.SetActive(false));

        seq.Append(leftBottomPartRectTransform.DOAnchorPos(_leftBottomPartPos, animationTime));
        seq.Join(rightTopPartRectTransform.DOAnchorPos(_rightTopPartPos, animationTime));
        seq.OnComplete(() =>
        {
            leftBottomPart.SetActive(false);
            rightTopPart.SetActive(false);
        });

        seq.SetAutoKill(true);
    }
}
