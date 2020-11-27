using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestProject.Logic.Card
{
    public class CardView : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField]
        private Text _nameText;

        [SerializeField]
        private Text _descriptionText;

        [SerializeField]
        private RawImage _iconImage;

        [SerializeField]
        private CardParam _manaParam;

        [SerializeField]
        private CardParam _attackParam;

        [SerializeField]
        private CardParam _healthParam;

        [SerializeField]
        private float _counterAnimationDuration = 1;

        [SerializeField]
        private Canvas _canvas;

        [SerializeField]
        private Animator _animator;
#pragma warning restore 0649

        public event EventHandler DeathAnimationEnded;

        private void Awake()
        {
            _healthParam.AnimationDuration = _attackParam.AnimationDuration = _manaParam.AnimationDuration = _counterAnimationDuration;
        }

        public int SortingOrder
        {
            get => _canvas.sortingOrder;
            set => _canvas.sortingOrder = value;
        }

        public void SetNameText(string name)
        {
            _nameText.text = name;
        }

        public void SetDescriptionText(string description)
        {
            _descriptionText.text = description;
        }

        public void SetIcon(Texture texture)
        {
            _iconImage.texture = texture;
        }

        public void SetManaValue(int value, bool instant = false)
        {
            _manaParam.SetValue(value, instant);
        }

        public void SetHealthValue(int value, bool instant = false)
        {
            _healthParam.SetValue(value, instant);
        }

        public void SetAttackValue(int value, bool instant = false)
        {
            _attackParam.SetValue(value, instant);
        }

        public void StartDeathAnimation()
        {
            var pos = transform.position;
            transform.SetParent(null, true);
            transform.position = pos;

            _canvas.sortingOrder = 100;

            _animator.Play("Death");
        }

        public void DeathAnimationEnd()
        {
            DeathAnimationEnded?.Invoke(this, EventArgs.Empty);
        }
    }
}