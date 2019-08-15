using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using K = Keras.Backend;
using Keras;
using Keras.Applications;
using Keras.PreProcessing.Image;
using Numpy;
using Keras.Applications.ResNet;

using Keras.Models;
using Keras.Layers;
using Keras.Optimizers;
using Keras.Regularizers;

using Keras.Callbacks;


using Loss.softmax_cross_entropy_with_logits;

namespace gordon_sketch_00
{
    //Base class
    public class GenModel
    {
        protected float _regConst;
        protected Keras.Shape _inputDim;
        protected Keras.Shape _outputDim;
        protected float _learningRate;
        protected Model _model;

        public GenModel(
            float regConst,
            float learningRate,
            (int, int) inputShape,
            int outputShape)
        {
            _regConst = regConst;
            _learningRate = learningRate;
            _inputDim = inputShape; // ???
            _outputDim = outputShape;
        }

        public NDarray Predict(NDarray x)
        {
            return _model.Predict(x);
        }

        public History Fit(states, targets, epochs, verbose, validation_split, batch_size)
        {
            return _model.Fit(states, targets, epochs = epochs, verbose = verbose, validation_split = validation_split, batch_size = batch_size)
        }

        public void Write(NDarray game, string version)
        {
            _model.Save($"{Config.runFolder}models/version{version}.h5");
        }

        public BaseModel Read(game, runNumber, version)
        {
            //path = run_archive_folder + game + '/run' + str(run_number).zfill(4) + "/models/version" + "{0:0>4}".format(version) + '.h5', custom_objects={'softmax_cross_entropy_with_logits': softmax_cross_entropy_with_logits}
            return _model.LoadModel();
        }

        public void PrintWeightAverages()
        {
            foreach (var (i, l) in _model.Layers)
        }

    }

    public class ResidualCNN : GenModel
    {
        //    private Keras.Shape _inputDim;
        //    private float _learningRate;
        //    private Model _model;

        private int _numLayers;
        private List<Config.LayerDetails> _hiddenLayers;

        ResidualCNN(
            float regConst, 
            float learningRate, 
            (int, int) inputShape, 
            int outputShape, 
            List<Config.LayerDetails> hiddenLayers) : base(regConst,learningRate,inputShape,outputShape)
        {
            _hiddenLayers = hiddenLayers;
            _numLayers = hiddenLayers.Count();


        }

        private BaseLayer _residualLayer()
        {

        }

        private Conv2D _convLayer(Input x, int filters, (int, int) kernalSize)
        {
            var x = new Conv2D(filters: filters);

            var y = new BatchNormalization(axis: 1)(x);
        }


        public void ResidualLayer()
        {
            var x =
            x = Conv2D()
        }


        private Model _buildModel()
        {
            var mainInput = new Input(shape: _inputDim, name: "main_input");

            var x = _convLayer(mainInput,
                               Config.HiddenCNNLayers[0].filters,
                               Config.HiddenCNNLayers[0].kernalSize);

            if (Config.HiddenCNNLayers.Count > 1)
            {
                foreach (var h in Config.HiddenCNNLayers.Skip(1))
                {
                    x = _residualLayer(x, h.filters, h.kernalSize);
                }
            }

            var vh = _valueHead(x);
            var ph = _policyHead(x);

            var model = new Model(
                    inputs: new BaseLayer[] { mainInput },
                    outputs: new BaseLayer[] { vh, ph }
                );

            model.Compile(
                    loss: "value_head: mean_squared_error, policy_head: softmax_cross_entropy_with_logits",
                    optimizer: new SGD(lr: _learningRate, momentum: Config.MOMENTUM),
                    loss_weights: /*value head, policy head*/ new float[] { 0.5f, 0.5f }
                );

            return model;
        }
    }
}
