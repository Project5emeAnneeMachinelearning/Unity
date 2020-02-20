using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Perceptron : MonoBehaviour
{
    public Transform[] trainingSpheres;

    public Transform[] testSpheres;

    private double[] trainingInputs;

    private double[] trainingExpectedOutputs;

    private IntPtr model;

    [DllImport("machine_learning_lib.dylib")]
    public static extern IntPtr create(int indiceNumber);

    [DllImport("machine_learning_lib.dylib")]
    public static extern void train_classif(IntPtr model, double[] dataset, double[] expected_output, int sizedataset, double pas, int sizeIndice, int epoch);

    [DllImport("machine_learning_lib.dylib")]
    public static extern double predict_classif(IntPtr model, double[] values);

    public void ReInitialize()
    {
        for (var i = 0; i < testSpheres.Length; i++)
        {
            testSpheres[i].position = new Vector3(
                testSpheres[i].position.x,
                0f,
                testSpheres[i].position.z);

        }
    }

    public void CreateModel()
    {
        //model = create(2);
        //Debug.Log("Create fait");
    }

    public void Train()
    {
        //trainingInputs = new double[trainingSpheres.Length * 2];
        //trainingExpectedOutputs = new double[trainingSpheres.Length];

        //for (var i = 0; i < trainingSpheres.Length; i++)
        //{
        //    trainingInputs[2 * i] = trainingSpheres[i].position.x;
        //    trainingInputs[2 * i + 1] = trainingSpheres[i].position.z;
        //    trainingExpectedOutputs[i] = trainingSpheres[i].position.y;
        //}
        ////train_classif(model, trainingInputs, trainingExpectedOutputs, trainingInputs.Length, 0.01, 2, 1000000);
        //Debug.Log("Train fait");

        //// TrainLinearModelRosenblatt(model, trainingInputs, 2, trainingSpheres.Length, trainingExpectedOutputs, 1, 0.01, 1000)
    }

    public void PredictOnTestSpheres()
    {
        //for (var i = 0; i < testSpheres.Length; i++)
        //{
        //    var input = new double[] { testSpheres[i].position.x, testSpheres[i].position.z };
        //    double predictedY = predict_classif(model, input);
        //    //var predictedY = PredictXXXLinearModel(model, input, 2)
        //    //var predictedY = Random.Range(-5, 5);
        //    testSpheres[i].position = new Vector3(
        //        testSpheres[i].position.x,
        //        (float)predictedY,
        //        testSpheres[i].position.z);
        //}
    }


}
