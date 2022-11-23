using UnityEngine;
using UnityEngine.UI;

namespace HitmarkerMod
{
    public class Hitmarker : MonoBehaviour
    {
        static Hitmarker instance;

        float elapsedTime = 0f;
        Image image;

        public static Hitmarker Instance { get => instance; }

        public void Awake()
        {
            if (instance != null)
            {
                DestroyImmediate(gameObject);
            }
            instance = this;

            image = GetComponent<Image>();

            Color color = image.color;
            color.a = 0;
            image.color = color;
        }

        public void GenericAnimation()
        {
            Color color = image.color;
            color.a = 1f;
            image.color = color;
            elapsedTime = 0.3f;
        }

        public void Update()
        {
            if (elapsedTime > 0)
            {
                elapsedTime -= Time.deltaTime;
                Color color = image.color;
                color.a = elapsedTime / 0.3f;
                image.color = color;
                return;
            }
            if (elapsedTime <= 0)
            {
                Color color = image.color;
                color.a = 0;
                image.color = color;
                elapsedTime = 0;
            }
        }
    }
}