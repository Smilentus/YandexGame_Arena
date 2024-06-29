using DG.Tweening;
using Dimasyechka.Code.Windows;
using Dimasyechka.Lubribrary.Extensions;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using UniRx;
using UnityEngine;
using Zenject;

namespace Dimasyechka
{
    public class FortuneWheelViewModel : BaseShopViewModel<FortuneWheel>
    {
        [RxAdaptableProperty]
        public ReactiveProperty<bool> IsSpinButtonEnabled = new ReactiveProperty<bool>();


        [SerializeField]
        private Transform _rotatableWheel;


        private RectTransform _rotatableWheelRect;
        private RectTransform RotatableWheelRect
        {
            get
            {
                if (_rotatableWheelRect == null)
                {
                    _rotatableWheelRect = _rotatableWheel.GetComponent<RectTransform>();
                }

                return _rotatableWheelRect;
            }
        }


        [Header("Slots")]
        [SerializeField]
        private FortuneWheelSlotViewModel _slotViewModelPrefab;

        [SerializeField]
        private Transform _slotsContent;


        [Header("Division Lines")]
        [SerializeField]
        private GameObject _divisionLinePrefab;

        [SerializeField]
        private Transform _divisionLinesContent;


        [Header("Smoothing Curves")]
        [SerializeField]
        private AnimationCurve _smoothingCurve;


        private float PrizeAngle => Model != null ? 360f / Model.Prizes.Count : 360f;

        private bool _isSpinning = false;

        private int _winSlot;
        private float _winAngle;
        private float _rotationTime;

        private float _startRotationAngle = 0;
        private float _runtimeRotationTimer = 0;


        [Inject]
        public void Construct(FortuneWheel fortuneWheel)
        {
            ZenjectModel(fortuneWheel);
        }


        /*
        «адаЄм угол, к которому мы должны приблизитьс€ (подкрутка привет)
        ќтсюда формируем сколько кругов мы крутим максимально быстро, а затем замедл€емс€ до нашего значени€
         рутим 15 полных кругов
        ѕосле 15-го круга начинаем замедл€тьс€, чтобы дойти до нужного угла
        16-й круг начинает замедл€ть скорость вращени€ по формуле
        —оответственно существует количество кругов замедлени€
        ƒопустим за 2 круга нужно замедлитьс€ до нашего угла
        «аданный угол = 85 (от 30 до 90 градусов - это победный слот)
        
         */


        private void FixedUpdate()
        {
            if (_isSpinning)
            {
                if (_runtimeRotationTimer < _rotationTime)
                {
                    float angle = _winAngle * _smoothingCurve.Evaluate(_runtimeRotationTimer / _rotationTime);

                    _rotatableWheel.DOLocalRotate(Vector3.forward * (angle + _startRotationAngle), Time.fixedDeltaTime);
                    
                    _runtimeRotationTimer += Time.fixedDeltaTime;
                }
                else
                {
                    _isSpinning = false;
                    
                    // ¬ыдаЄм приз и т.п.
                }
            }
        }


        public override void OnDrawUI()
        {
            IsSpinButtonEnabled.Value = Model.IsSpinAvailable.Value;

            _rotatableWheel.localEulerAngles = Vector3.zero;

            DrawDivisionLines();
            DrawPrizes();
        }


        private void DrawDivisionLines()
        {
            _divisionLinesContent.DestroyChildren();

            for (int i = 0; i < Model.Prizes.Count; i++)
            {
                GameObject divisionLine = Instantiate(_divisionLinePrefab, _divisionLinesContent);
                RectTransform divisionLineRect = divisionLine.GetComponent<RectTransform>();

                divisionLineRect.rect.Set(
                    0, 0,
                    divisionLineRect.rect.width, RotatableWheelRect.rect.height / 2f
                );

                divisionLine.transform.localPosition = Vector3.zero;

                divisionLineRect.Rotate(Vector3.back, (PrizeAngle / 2f) + PrizeAngle * i, Space.Self);
            }
        }

        private void DrawPrizes()
        {
            _slotsContent.DestroyChildren();

            for (int i = 0; i < Model.Prizes.Count; i++)
            {
                FortuneWheelSlotViewModel slotViewModel = Instantiate(_slotViewModelPrefab, _slotsContent);

                slotViewModel.SetupModel(Model.Prizes[i]);

                slotViewModel.transform.localPosition = Vector3.zero;
                slotViewModel.transform.Rotate(Vector3.back, PrizeAngle * i, Space.Self);
            }
        }


        [RxAdaptableMethod]
        public void Spin()
        {
            IsSpinButtonEnabled.Value = false;

            // TODO: ”нести с вьюхи в модельку
            _isSpinning = true;
            _runtimeRotationTimer = 0;
            _startRotationAngle = _rotatableWheel.localEulerAngles.z;

            _winSlot = Random.Range(0, Model.Prizes.Count);

            float _minAngle = PrizeAngle * _winSlot - PrizeAngle * 0.5f;
            float _maxAngle = PrizeAngle * _winSlot + PrizeAngle * 0.5f;

            _rotationTime = Random.Range(5, 15);

            _winAngle = (360 * _rotationTime + (Random.Range(_minAngle, _maxAngle)));

            _winAngle -= _startRotationAngle;

            Debug.Log($"RotationTime: {_rotationTime} / WinSlot: {_winSlot} / WinValue: {Model.Prizes[_winSlot].Value} / WinAngle: {_winAngle} / Min: {_minAngle} / Max: {_maxAngle}");

            Model.Spin();
        }
    }
}
