using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class EnergySource
    {
        private readonly float r_MaxEnergyCapacity;
        private float m_CurrentEnergyAmountPercentage;
        private float m_CurrentEnergyAmount;

        protected EnergySource(float i_CurrentEnergyAmountPercentage, float i_MaxEnergyCapacity)
        {
            m_CurrentEnergyAmountPercentage = i_CurrentEnergyAmountPercentage;
            r_MaxEnergyCapacity = i_MaxEnergyCapacity;
        }

        public float CurrentEnergyAmountPercentage
        {
            get
            {
                return m_CurrentEnergyAmountPercentage;
            }

            set
            {
                m_CurrentEnergyAmountPercentage = (value / MaxEnergyCapacity) * 100f;
            }
        }

        public float CurrentEnergyAmount
        {
            get
            {
                return m_CurrentEnergyAmount;
            }

            set
            {
                if(m_CurrentEnergyAmount + value > MaxEnergyCapacity)
                {
                    throw new ValueOutOfRangeException(0, MaxEnergyCapacity);
                }
                else
                {
                    m_CurrentEnergyAmount = value;
                    CurrentEnergyAmountPercentage = m_CurrentEnergyAmount;
                }
            }
        }

        public float MaxEnergyCapacity
        {
            get
            {
                return r_MaxEnergyCapacity;
            }
        }

        public abstract override string ToString();
    }
}
