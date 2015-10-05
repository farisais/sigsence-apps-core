using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NationalInstruments.Analysis.SignalGeneration;
using NationalInstruments.Analysis.Math;
using NationalInstruments.Analysis.Dsp;

namespace DSA_Teaser
{
    class SequenceObjectFunction
    {
        public SequenceObjectFunction()
        {

        }

        private string objectType;//Input or Output
        public string ObjectType
        {
            get
            {
                return objectType;
            }
            set
            {
                objectType = value;
            }
        }

        private List<SequenceObjectFunction> inputNode;
        public List<SequenceObjectFunction> InputNode
        {
            get
            {
                return inputNode;
            }
            set
            {
                inputNode = value;
            }
        }

        private List<SequenceObjectFunction> outputNode;
        public List<SequenceObjectFunction> OutputNode
        {
            get
            {
                return outputNode;
             }
            set
            {
                outputNode = value;
            }
        }

        private int indexObject;
        public int IndexObject
        {
            get
            {
                return indexObject;
            }
            set
            {
                indexObject = value;
            }
        }
    }
}
