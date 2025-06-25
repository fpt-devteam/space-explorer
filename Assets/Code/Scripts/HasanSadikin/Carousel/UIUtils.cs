using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace HasanSadikin.Carousel
{
    public static class UIUtils
    {
        public static DG.Tweening.Sequence CreateSequence(this MonoBehaviour mono, object target = null)
        {
            object t = target == null ? mono.gameObject : target;

            DOTween.Kill(t, true);
            DG.Tweening.Sequence s = DOTween.Sequence();
            s.SetTarget(t);
            return s;
        }
    }
}