using UnityEngine;
using UnityEngine.UI;

namespace HitmarkerMod
{
    //TODO: Learn how to use animators.
    public class Hitmarker : MonoBehaviour
    {
        public enum AnimationMode
        {
            Static,
            Dynamic
        }

        //Visual properties
        float dynamicMaxHeight = 40;
        float dynamicMinHeight = 10;
        float dynamicCenterOffset = 20;
        float dynamicStrokeWidth = 8;
        float sizeFromDamageMultiplier = 6f;

        float dynamicCurrentHeight;
        float dynamicCurrentWidth;
        float dynamicCurrentOffset;
        float sizeFromDamage = 0f;

        static Hitmarker instance;

        float elapsedTime = 0f;
        Image staticImage;
        Image[] dynamicStrokeImages;
        RectTransform[] dynamicStrokeTransforms;

        AnimationMode currentAnimationMode;

        public static Hitmarker Instance { get => instance; }
        public AnimationMode CurrentAnimationMode { get => currentAnimationMode; }

        public void Awake()
        {
            if (instance != null)
            {
                DestroyImmediate(gameObject);
            }
            instance = this;
        }
        public void SetAnimationMode(AnimationMode animationMode)
        {
            currentAnimationMode = animationMode;

            if (currentAnimationMode == AnimationMode.Static)
            {
                staticImage = GetComponent<Image>();

                Color color = staticImage.color;
                color.a = 0;
                staticImage.color = color;
            }
            if (currentAnimationMode == AnimationMode.Dynamic)
            {
                dynamicStrokeImages = new Image[4];
                dynamicStrokeTransforms = new RectTransform[4];
                for (int i = 0; i < 4; i++)
                {
                    Transform strokeTransform = transform.GetChild(i).GetChild(0);
                    dynamicStrokeImages[i] = strokeTransform.GetComponent<Image>();
                    dynamicStrokeTransforms[i] = strokeTransform.GetComponent<RectTransform>();

                    Color color = dynamicStrokeImages[i].color;
                    color.a = 0;
                    dynamicStrokeImages[i].color = color;
                }

                ModifyStrokeTransforms(dynamicStrokeWidth, dynamicMinHeight, dynamicCenterOffset);
                sizeFromDamage = dynamicCurrentHeight;
            }
        }

        void ModifyStrokeTransforms(float width, float height, float offset)
        {
            height = Mathf.Clamp(height, dynamicMinHeight, dynamicMaxHeight);
            dynamicCurrentHeight = height;
            dynamicCurrentWidth = width;
            dynamicCurrentOffset = offset;

            foreach (var strokeRectTransform in dynamicStrokeTransforms)
            {
                strokeRectTransform.sizeDelta = new Vector2(width, height);

                Vector3 position = strokeRectTransform.anchoredPosition;
                position.y = offset;
                strokeRectTransform.anchoredPosition = position;
            }
        }

        void ApplyColor(float alpha)
        {
            if (currentAnimationMode == AnimationMode.Static)
            {
                Color color = staticImage.color;
                color.a = alpha;
                staticImage.color = color;
            }
            if (currentAnimationMode == AnimationMode.Dynamic)
            {
                foreach (var image in dynamicStrokeImages)
                {
                    Color color = image.color;
                    color.a = alpha;
                    image.color = color;
                }
            }
        }

        void ApplyColor(float r, float g, float b, float a)
        {
            Color color = new Color(r, g, b, a);
            if (currentAnimationMode == AnimationMode.Static)
            {
                staticImage.color = color;
            }
            if (currentAnimationMode == AnimationMode.Dynamic)
            {
                foreach (var image in dynamicStrokeImages)
                {
                    image.color = color;
                }
            }
        }

        public void OnEnemyDamage(float damageMultiplier)
        {
            ApplyColor(1f, 1f, 1f, 1f);
            sizeFromDamage += damageMultiplier * sizeFromDamageMultiplier;
#if DEBUG
            Debug.Log($"Damage Multiplier: {damageMultiplier}");
            Debug.Log($"Size from damage: {dynamicMinHeight - (1 * sizeFromDamageMultiplier) + sizeFromDamage}");
#endif
            ModifyStrokeTransforms(dynamicCurrentWidth, dynamicMinHeight - (1 * sizeFromDamageMultiplier) + sizeFromDamage, dynamicCurrentOffset);

            elapsedTime = 0.3f;
        }

        public void Update()
        {
            if (elapsedTime > 0)
            {
                elapsedTime -= Time.deltaTime;
                ApplyColor(elapsedTime / 0.3f);
                return;
            }
            if (elapsedTime <= 0)
            {
                ApplyColor(0f);
                elapsedTime = 0f;
                sizeFromDamage = 0f;
            }
        }
    }
}