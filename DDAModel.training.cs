﻿// This file was auto-generated by ML.NET Model Builder.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.LightGbm;
using Microsoft.ML.Trainers;
using Microsoft.ML;

namespace DDA
{
    public partial class DDAModel
    {
        /// <summary>
        /// Retrains model using the pipeline generated as part of the training process. For more information on how to load data, see aka.ms/loaddata.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <param name="trainData"></param>
        /// <returns></returns>
        public static ITransformer RetrainPipeline(MLContext mlContext, IDataView trainData)
        {
            var pipeline = BuildPipeline(mlContext);
            var model = pipeline.Fit(trainData);

            return model;
        }

        /// <summary>
        /// build the pipeline that is used from model builder. Use this function to retrain model.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.ReplaceMissingValues(@"passive_score", @"passive_score")      
                                    .Append(mlContext.Transforms.Concatenate(@"Features", new []{@"passive_score"}))      
                                    .Append(mlContext.Regression.Trainers.LightGbm(new LightGbmRegressionTrainer.Options(){NumberOfLeaves=5381,NumberOfIterations=1046,MinimumExampleCountPerLeaf=23,LearningRate=0.161964127115298,LabelColumnName=@"difficulty",FeatureColumnName=@"Features",ExampleWeightColumnName=null,Booster=new GradientBooster.Options(){SubsampleFraction=0.376859104690912,FeatureFraction=0.994619287954747,L1Regularization=2.80832127127674E-10,L2Regularization=0.891401139499412},MaximumBinCountPerFeature=221}));

            return pipeline;
        }
    }
}
