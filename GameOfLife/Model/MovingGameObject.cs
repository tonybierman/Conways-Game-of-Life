/*
 * Author: Tony Bierman
 * Website: http://www.tonybierman.com
 * License: MIT License
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy,
 * modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software
 * is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
 * LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
 * IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
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
