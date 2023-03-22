using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = Unity.Mathematics.Random;

namespace UI.Move_Out
{
    public class MoveDir : MonoBehaviour
    {
        public Button mainButton;
        public Image mainImage;
        public Image[] LeftRightBackgrounds;
        [SerializeField] private Vector2[] LeftRightPositions;

        public Button[] buttons;
        [SerializeField] private Vector2[] buttonPositions;
        private Random random;
        [SerializeField] private bool extended;
        [Range(0, 1)] public float startingTime;
        [Range(0, 1)] public float endingTime;
        [Range(0, 1)] public float sliderTime;
        [Range(0, 1)] public float delayTime;

        public void Start()
        {
            StoreInitialPositions();
            Hide();
            random = new Random((uint)System.DateTime.Now.Millisecond);
            mainButton.onClick.AddListener(Toggle);
        }

        void StoreInitialPositions()
        {
            if (buttonPositions != null) buttonPositions = new Vector2[buttons.Length];
            for (int i = 0; i < buttons.Length; i++)
            {
                buttonPositions[i] = buttons[i].transform.localPosition;
            }

            if (LeftRightPositions != null) LeftRightPositions = new Vector2[LeftRightBackgrounds.Length];
            for (int i = 0; i < LeftRightBackgrounds.Length; i++)
            {
                LeftRightPositions[i] = LeftRightBackgrounds[i].transform.localPosition;
            }
        }

        void Hide()
        {
            mainImage.color = new Color(mainImage.color.r, mainImage.color.g, mainImage.color.b, 0);
            foreach (var background in LeftRightBackgrounds)
            {
                background.color = new Color(background.color.r, background.color.g, background.color.b, 0);
                background.transform.localPosition = mainButton.transform.localPosition;
            }

            foreach (var button in buttons)
            {
                button.image.color = new Color(button.image.color.r, button.image.color.g, button.image.color.b, 0);
                button.transform.localPosition = mainButton.transform.localPosition;
            }
        }

        void Toggle()
        {
            if (!extended)
                MoveOut();
            else
                MoveIn();
        }

        private void MoveIn()
        {
            mainImage.DOFade(0, delayTime);
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].transform.DOLocalMove(mainButton.transform.localPosition, random.NextFloat(startingTime, endingTime));
                buttons[i].image.DOFade(0, delayTime);
            }

            foreach (var image in LeftRightBackgrounds)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
                image.transform.DOLocalMove(mainButton.transform.localPosition, sliderTime);
            }
            StartCoroutine(ToggleExtended());
        }

        private void MoveOut()
        {
            mainImage.DOFade(1, delayTime);
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].transform.DOLocalMove(buttonPositions[i], random.NextFloat(startingTime, endingTime));
                buttons[i].image.DOFade(1, delayTime);
            }

            for (int i = 0; i < LeftRightBackgrounds.Length; i++)
            {
                var image = LeftRightBackgrounds[i];
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
                LeftRightBackgrounds[i].transform.DOLocalMove(LeftRightPositions[i], sliderTime);
            }

            StartCoroutine(ToggleExtended());
        }

        private IEnumerator ToggleExtended()
        {
            yield return new WaitForSecondsRealtime(sliderTime);
            extended = !extended;
        }
    }
}