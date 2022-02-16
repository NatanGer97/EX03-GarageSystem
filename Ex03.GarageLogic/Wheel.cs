using System;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly float r_MaxAirPressure;
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;

        public Wheel(float i_MaxAirPressure)
        {
            m_ManufacturerName = string.Empty;
            r_MaxAirPressure = i_MaxAirPressure;
            CurrentAirPressure = 0;
        }

        public string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }

            set
            {
                m_ManufacturerName = value;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return r_MaxAirPressure;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                if (value <= 0 && value > MaxAirPressure)
                {
                    throw new ValueOutOfRangeException(0, MaxAirPressure);
                }

                m_CurrentAirPressure = value;
            }
        }

        public void Inflate(float i_AirPressureToInflate)
        {
            CurrentAirPressure += i_AirPressureToInflate;
        }

        // $G$ CSS-027 (-3) Spaces are not kept as required after defying variables and before return statement. 
        public override string ToString()
        {
            string wheelInfoMessage = string.
                Format(
                    "Current Pressure: {0} - ManufacturerName - {1}",
                    CurrentAirPressure,
                    ManufacturerName);
            return wheelInfoMessage;
        }
    }
}
