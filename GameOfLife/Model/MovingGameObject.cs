using System;
using System.Linq;

namespace Bierman.Abm.Model
{
    public abstract class MovingGameObject : GameObject
    {
        protected readonly Landscape _field;
        private CellLocation _cellLocation;
        private CellLocation _targetCellLocation;

        protected MovingGameObject(Landscape field, CellLocation location) : base(location.ToPoint())
        {
            _field = field;
            CellLocation = TargetCellLocation = location;
        }

        public const int Layer = 1;

        public CellLocation CellLocation
        {
            get => _cellLocation;
            private set
            {
                if (value.Equals(_cellLocation)) return;
                _cellLocation = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsMoving));
            }
        }

        public CellLocation TargetCellLocation
        {
            get => _targetCellLocation;
            private set
            {
                if (value.Equals(_targetCellLocation)) return;
                _targetCellLocation = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsMoving));
            }
        }

        public bool IsMoving => TargetCellLocation != CellLocation;

        public void SetLocation(CellLocation loc)
        {
            CellLocation = loc;
            Location = loc.ToPoint();
        }
    }
}
