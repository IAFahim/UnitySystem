using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Move_Out
{
    public class MoveDir : MonoBehaviour
    {
        public Button mainButton;
        public Image mainImage;
        public Image[] LeftRightBackgrounds;
        private Vector2[] LeftRightPositions;

        public Button[] buttons;
        private Vector2[] buttonPositions;
        [SerializeField] private bool extended=false;
        [Range(0, 1)] public float explodeTime;
        [Range(0, 1)] public float sliderTime;
        [Range(0, 1)] public float delayTime;

        public void Start()
        {
            StoreInitialPositions();
            Hide();
            mainButton.onClick.AddListener(Toggle);
        }

        void StoreInitialPositions()
        {
            buttonPositions ??= new Vector2[buttons.Length];
            for (int i = 0; i < buttons.Length; i++)
            {
                buttonPositions[i] = buttons[i].transform.localPosition;
            }

            LeftRightPositions ??= new Vector2[LeftRightBackgrounds.Length];
            for (int i = 0; i < LeftRightBackgrounds.Length; i++)
            {
                LeftRightPositions[i] = LeftRightBackgrounds[i].transform.localPosition;
            }
        }

        void Hide()
        {
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
            mainImage.DOFade(1, delayTime);

            for (int i = 0, half = buttons.Length / 2; i < half; i++)
            {
                var localPosition = mainButton.transform.localPosition;
                int left = i;
                buttons[left].transform.DOLocalMove(localPosition, explodeTime / (half - i));
                buttons[left].image.DOFade(0, delayTime);
                int right = i + half;
                buttons[right].transform.DOLocalMove(localPosition, explodeTime / (half - i));
                buttons[right].image.DOFade(0, delayTime);
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
            mainImage.DOFade(0, delayTime);
            for (int i = 0, half = buttons.Length / 2; i < half; i++)
            {
                int left = i;
                buttons[left].transform.DOLocalMove(buttonPositions[left], explodeTime / (half - i));
                buttons[left].image.DOFade(1, delayTime);
                int right = i + half;
                buttons[right].transform.DOLocalMove(buttonPositions[right], explodeTime / (half - i));
                buttons[right].image.DOFade(1, delayTime);
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