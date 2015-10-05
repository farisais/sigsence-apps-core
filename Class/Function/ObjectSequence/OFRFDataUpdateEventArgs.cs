using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSA_Teaser.Class.Function.ObjectSequence
{
    class OFRFDataUpdateEventArgs: EventArgs
    {
        public OFRFDataUpdateEventArgs(double[] crossPowerSpecMag, double[] crossPowerSpecPh, double[] freqResMag, double[] freqResPh,
            double[] coh, double[] impulseResp, double __df, double[,] _stimulus, double[,] _response)
        {
            crossPowerSpectrumMagnitude = crossPowerSpecMag;
            crossPowerSpectrumPhase = crossPowerSpecPh;
            frequencyResponseMagnitude = freqResMag;
            frequencyResponsePhase = freqResPh;
            coherence = coh;
            impulseResp = impulseResponse;
            _df = __df;
            stimulus = _stimulus;
            response = _response;
            
        }

        private double[] crossPowerSpectrumMagnitude;
        public double[] CrossPowerSpectrumMagnitude
        {
            get
            {
                return crossPowerSpectrumMagnitude;
            }
            set
            {
                crossPowerSpectrumMagnitude = value;
            }
        }

        private double[] crossPowerSpectrumPhase;
        public double[] CrossPowerSpectrumPhase
        {
            get
            {
                return crossPowerSpectrumPhase;
            }
            set
            {
                crossPowerSpectrumPhase = value;
            }
        }

        private double[] frequencyResponseMagnitude;
        public double[] FrequencyResponseMagnitude
        {
            get
            {
                return frequencyResponseMagnitude;
            }
            set
            {
                frequencyResponseMagnitude = value;
            }
        }

        private double[] frequencyResponsePhase;
        public double[] FrequencyResponsePhase
        {
            get
            {
                return frequencyResponsePhase;
            }
            set
            {
                frequencyResponsePhase = value;
            }
        }
        private double[] coherence;
        public double[] Coherence
        {
            get
            {
                return coherence;
            }
            set
            {
                coherence = value;
            }
        }
        private double[] impulseResponse;
        public double[] ImpulseResponse
        {
            get
            {
                return impulseResponse;
            }
            set
            {
                impulseResponse = value;
            }
        }

        private double _df;
        public double df
        {
            get
            {
                return _df;
            }
            set
            {
                _df = value;
            }
        }

        private double[,] stimulus;
        public double[,] Stimulus
        {
            get
            {
                return stimulus;
            }
            set
            {
                stimulus = value;
            }
        }

        private double[,] response;
        public double[,] Response
        {
            get
            {
                return response;
            }
            set
            {
                response = value;
            }
        }
    }
}
