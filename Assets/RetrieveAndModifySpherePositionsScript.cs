﻿using System;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class RetrieveAndModifySpherePositionsScript : MonoBehaviour
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
        model = create(2);
        Debug.Log("Create fait");
    }

    public void Train()
    {
        trainingInputs = new double[trainingSpheres.Length * 2];
        trainingExpectedOutputs = new double[trainingSpheres.Length];

        for (var i = 0; i < trainingSpheres.Length; i++)
        {
            trainingInputs[2 * i] = trainingSpheres[i].position.x;
            trainingInputs[2 * i + 1] = trainingSpheres[i].position.z;
            trainingExpectedOutputs[i] = trainingSpheres[i].position.y;
        }
        train_classif(model, trainingInputs, trainingExpectedOutputs, trainingInputs.Length, 0.01,  2, 1000000);
        Debug.Log("Train fait");

        // TrainLinearModelRosenblatt(model, trainingInputs, 2, trainingSpheres.Length, trainingExpectedOutputs, 1, 0.01, 1000)
    }

    public void PredictOnTestSpheres()
    {
        for (var i = 0; i < testSpheres.Length; i++)
        {
            var input = new double[] {testSpheres[i].position.x, testSpheres[i].position.z};
            double predictedY = predict_classif(model, input);
            //var predictedY = PredictXXXLinearModel(model, input, 2)
            //var predictedY = Random.Range(-5, 5);
            testSpheres[i].position = new Vector3(
                testSpheres[i].position.x,
                (float) predictedY,
                testSpheres[i].position.z);
        }
    }

    //public void Soft(int taille)
    //{
    //    ReInitialize();
    //    int quotaSuperieurZero = trainingSpheres.Length / 2;
    //    for (int i = 0; i < trainingSpheres.Length; i++)
    //    {

    //        if (quotaSuperieurZero > 0)
    //        {
    //            var predictedZ = Random.Range(zmin, -1);
    //            var predictedX = Random.Range(xmin, xmax);
    //            trainingSpheres[i].position = new Vector3(
    //               predictedX,
    //               1,
    //               predictedZ);
    //        }
    //        else
    //        {
    //            var predictedZ = Random.Range(1, zmax);
    //            var predictedX = Random.Range(xmin, xmax);
    //            trainingSpheres[i].position = new Vector3(
    //               predictedX,
    //               -1,
    //               predictedZ);
    //        }
    //        quotaSuperieurZero--;
    //        if(i > taille)
    //            trainingSpheres[i] = null;
    //    }
    //}



    //public void Xor()
    //{
    //    ReInitialize();
    //    int predictForY = Random.Range(1, 3);
    //    switch (predictForY)
    //    {
    //        case 1 :
    //            Instantiate(sphereRouges, new Vector3(xmax, -1, zmin), Quaternion.identity);
    //            Instantiate(sphereRouges, new Vector3(xmin, -1, zmax), Quaternion.identity);
    //            Instantiate(sphereRouges, new Vector3(xmax, 1, zmin), Quaternion.identity);
    //            Instantiate(sphereRouges, new Vector3(xmin, 1, zmin), Quaternion.identity);
    //            /*trainingSpheres[0].position = new Vector3(xmax, -1, zmin);
    //            trainingSpheres[1].position = new Vector3(xmin, -1, zmax);
    //            trainingSpheres[2].position = new Vector3(xmax, 1, zmax);
    //            trainingSpheres[3].position = new Vector3(xmin, 1, zmin);*/
    //            break;
    //        case 2:
    //            Instantiate(sphereRouges, new Vector3(xmax, 1, zmin), Quaternion.identity);
    //            Instantiate(sphereRouges, new Vector3(xmin, 1, zmax), Quaternion.identity);
    //            Instantiate(sphereRouges, new Vector3(xmax, -1, zmin), Quaternion.identity);
    //            Instantiate(sphereRouges, new Vector3(xmin, -1, zmin), Quaternion.identity);
    //            /*
    //            trainingSpheres[0].position = new Vector3(xmax, 1, zmin);
    //            trainingSpheres[1].position = new Vector3(xmin, 1, zmax);
    //            trainingSpheres[2].position = new Vector3(xmax, -1, zmax);
    //            trainingSpheres[3].position = new Vector3(xmin, -1, zmin);*/
    //            break;
    //    }
    //}

    //public void Real()
    //{
    //    ReInitialize();
    //    float predictedZ;
    //    float predictedX;
    //    int quotaSuperieurZero = trainingSpheres.Length / 2;
    //    for (var i = 0; i < trainingSpheres.Length; i++)
    //    {

    //        if (quotaSuperieurZero > 0)
    //        {
    //           predictedZ = Random.Range(zmin, 1);
    //           predictedX = Random.Range(xmin, 0);
    //            trainingSpheres[i].position = new Vector3(
    //           predictedX,
    //           1,
    //           predictedZ);
    //        }
    //        else
    //        {
    //            predictedZ = Random.Range(2, zmax);
    //            predictedX = Random.Range(1, xmax);
    //            trainingSpheres[i].position = new Vector3(
    //           predictedX,
    //           1,
    //           predictedZ);
    //        }
    //        quotaSuperieurZero--;

    //    }

    //}

    //public void Cross()
    //{
    //    ReInitialize();
    //    float predictedZ;
    //    float predictedX;
    //    var positionSquare = Random.Range(1, 5);

    //    int quotaSuperieurZero = trainingSpheres.Length / 2;
    //    for (var i = 0; i < trainingSpheres.Length; i++)
    //    {

    //        if (quotaSuperieurZero > 0)
    //        {
    //            predictedZ = Random.Range(zmin, 1);
    //            predictedX = Random.Range(xmin, 0);
    //            trainingSpheres[i].position = new Vector3(
    //           predictedX,
    //           1,
    //           predictedZ);
    //        }
    //        else
    //        {
    //            predictedZ = Random.Range(2, zmax);
    //            predictedX = Random.Range(1, xmax);
    //            trainingSpheres[i].position = new Vector3(
    //           predictedX,
    //           1,
    //           predictedZ);
    //        }
    //        quotaSuperieurZero--;

    //    }
    //}

    public void ReleaseModel()
    {
        // FreeLinearModel(model);
    }



    //public void chooseFunctionWithMultipleSpheres(Dropdown target)
    //{
    //    switch (target.value)
    //    {
    //        case 3: Cross();
    //            break;
    //        default: ReInitialize();
    //            break;
    //    }
    //}


}