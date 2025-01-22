using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG
{
    public class Range
    {
        private float _from;
        private float _to;

        public float From
        {
            get => _from;
            set
            {
                if (value > _to)
                {
                    throw new ArgumentException("From cannot be greater than To.");
                }
                _from = value;
            }
        }

        public float To
        {
            get => _to;
            set
            {
                if (value < _from)
                {
                    throw new ArgumentException("To cannot be less than From.");
                }
                _to = value;
            }
        }

        public Range(float from, float to)
        {
            if (from > to)
            {
                throw new ArgumentException("From cannot be greater than To.");
            }

            _from = from;
            _to = to;
        }

        public float GenerateRandom()
        {
            Random random = new Random();
            return _from + (random.NextSingle() * (_to - _from));
        }

        public override string ToString()
        {
            return $"From: {_from}, To: {_to}";
        }
    }
}
