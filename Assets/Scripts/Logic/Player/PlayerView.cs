using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TestProject.Logic.Card;
using DG.Tweening;

namespace TestProject.Logic.Player
{
    public class PlayerView : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField]
        private GameObject _cardPrefab;

        [SerializeField]
        private Transform _cardsParent;

        [SerializeField]
        private Vector2 _arcSize;

        [SerializeField]
        private float _repositionDuration;
#pragma warning restore 0649

        public event Action RepositionCompleted;

        public CardView AddCard()
        {
            //TODO: use pool
            var cardObj = Instantiate(_cardPrefab, _cardsParent) as GameObject;
            var cardView = cardObj.GetComponent<CardView>();
            return cardView;
        }

        public void RemoveCard(CardView cardView)
        {
            //TODO: use pool
            Destroy(cardView.gameObject);
        }

        public void Repositions(bool instant)
        {
            var count = _cardsParent.childCount;

            var step = 180f / (count + 1);
            var currentAngle = 180f;
            for (var index = 0; index < count; index++)
            {
                currentAngle -= step;
                var x = _arcSize.x * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
                var y = _arcSize.y * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

                var child = _cardsParent.GetChild(index);

                var targetPosition = new Vector2(x, y);

                var targetRotation = child.eulerAngles;
                targetRotation.z = currentAngle - 90;

                child.eulerAngles = targetRotation;

                if (instant)
                {
                    child.localPosition = targetPosition;
                    child.eulerAngles = targetRotation;
                }
                else
                {
                    child.DOLocalMove(targetPosition, _repositionDuration);
                    child.DORotate(targetRotation, _repositionDuration);
                }

                var cardView = child.GetComponent<CardView>();
                cardView.SortingOrder = index;
            }

            if (!instant)
            {
                StartCoroutine(WaitingRepositionRoutine());
            }
        }

        private IEnumerator WaitingRepositionRoutine()
        {
            yield return new WaitForSeconds(_repositionDuration);

            RepositionCompleted?.Invoke();
        }
    }
}