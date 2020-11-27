using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

namespace TestProject.Logic.Card
{
    public class CardParam : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField]
        private Text _valueText;
#pragma warning restore 0649

        public float AnimationDuration { get; set; } = 1;

        private Tween _tweener;

        public void SetValue(int value, bool instant = false)
        {
            StopTween();

            if (int.Parse(_valueText.text) == value)
                return;

            if (instant)
            {
                SetText(value);
            }
            else
            {
                _tweener = DOTween.To(() => int.Parse(_valueText.text), SetText, value, AnimationDuration);
            }
        }

        private void SetText(int value)
        {
            _valueText.text = value.ToString();
        }

        private void StopTween()
        {
            if (_tweener == null)
                return;

            DOTween.Kill(_tweener);
            _tweener = null;
        }
    }
}