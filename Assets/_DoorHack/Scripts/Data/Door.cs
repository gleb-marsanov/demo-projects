using System;
using StaticData;

namespace Data
{
    public class Door
    {
        private const int MinPinValue = 0;
        private const int MaxPinValue = 10;

        public Door(int pin1, int pin2, int pin3)
        {
            Pin1 = pin1;
            Pin2 = pin2;
            Pin3 = pin3;
            _initialPinValues = new Tuple<int, int, int>(pin1, pin2, pin3);
        }

        public event Action Opened;
        public int Pin1 { get; private set; }
        public int Pin2 { get; private set; }
        public int Pin3 { get; private set; }
        public bool IsOpen => Pin1 == _initialPinValues.Item1 && Pin2 == _initialPinValues.Item2 && Pin3 == _initialPinValues.Item3;
        private readonly Tuple<int, int, int> _initialPinValues;


        public void ApplyTool(Tool tool, int overload = 1)
        {
            Pin1 = Math.Clamp(Pin1 + tool.Pin1 * overload, MinPinValue, MaxPinValue);
            Pin2 = Math.Clamp(Pin2 + tool.Pin2 * overload, MinPinValue, MaxPinValue);
            Pin3 = Math.Clamp(Pin3 + tool.Pin3 * overload, MinPinValue, MaxPinValue);

            if (IsOpen)
                Opened?.Invoke();
        }
    }
}