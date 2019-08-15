using System.Collections.Generic;

namespace gordon_sketch_00
{
    class Config
    {
        // SELF PLAY
        public static readonly int EPISODES = 30;
        public static readonly int MCTS_SIMS = 50;
        public static readonly int MEMORY_SIZE = 30000;
        public static readonly int TURNS_UNTIL_TAU = 10;
        public static readonly int CPUCT = 1;
        public static readonly float EPSILON = 0.2f;
        public static readonly float ALPHA = 0.8f;

        // RETRAINING
        public static readonly int BATCH_SIZE = 256;
        public static readonly int EPOCHS = 1;
        public static readonly float REG_CONST = 0.0001f;
        public static readonly float LEARNING_RATE = 0.1f;
        public static readonly float MOMENTUM = 0.9f;
        public static readonly int TRAINING_LOOPS = 10;


        public struct LayerDetails
        {
            public int filters;
            public (int,int) kernalSize;
        }

        public static readonly List<LayerDetails> HiddenCNNLayers = new List<LayerDetails>
        {
            new LayerDetails { filters=75, kernalSize=(4,4) },
            new LayerDetails { filters=75, kernalSize=(4,4) },
            new LayerDetails { filters=75, kernalSize=(4,4) },
            new LayerDetails { filters=75, kernalSize=(4,4) },
            new LayerDetails { filters=75, kernalSize=(4,4) },
            new LayerDetails { filters=75, kernalSize=(4,4) },
        };

        // EVALUATION

        public static readonly int EVAL_EPISODES = 20;
        public static readonly float SCORING_THRESHOLD = 1.3f;


        // SETTINGS
        public static string runFolder = "./run/";
        public static string runFolderArchive = "./run_archive/";
    }
}
