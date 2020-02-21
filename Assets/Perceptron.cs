using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        model = create(4);
        Debug.Log("model fait");



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

        //Debug.Log("Create fait");
    }


    public void Train()
    {
        var reader = new StreamReader(File.OpenRead("/Users/bartholome/Downloads/iris.csv"));
        int taille_fichier = 0;
        int compt = 0;
        List<double> listInput = new List<double>();
        List<double> listExpectedOutput = new List<double>();

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var cell = line.Split(',');
            for (int i = 0; i < cell.Length; i++)
            {
                if (i == 4)
                {
                    listExpectedOutput.Add(Convert.ToDouble(cell[i]));
                }
                else
                {
                    listInput.Add(Convert.ToDouble(cell[i]));
                }
                taille_fichier++;
            }
            compt++;
        }
        trainingExpectedOutputs = new double[compt];
        trainingInputs = new double[compt * 4];
        for (int i = 0; i < listExpectedOutput.ToArray().Length; i++)
        {
            trainingExpectedOutputs[i] = listExpectedOutput[i];
        }
        for (int i = 0; i < listInput.ToArray().Length; i++)
        {
            trainingInputs[i] = listInput[i];
        }

        train_classif(model, trainingInputs, trainingExpectedOutputs, trainingInputs.Length, 0.01, 4, 1000000);
        Debug.Log("entrainement terminé");
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

        var reader = new StreamReader(File.OpenRead("/Users/bartholome/Downloads/testPrediction.csv"));
        int taille_fichier = 0;
        int compt = 0;
        double purcentage = 0;
        List<double> listInput = new List<double>();
        List<double> listExpectedOutput = new List<double>();


        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var cell = line.Split(',');
            for (int i = 0; i < cell.Length; i++)
            {
                if (i == 4)
                {
                    listExpectedOutput.Add(Convert.ToDouble(cell[i]));
                }
                else
                {
                    listInput.Add(Convert.ToDouble(cell[i]));
                }
                taille_fichier++;
            }
            compt++;
        }
        
        for (int i = 0; i < (listInput.ToArray().Length / 4); i++)
        {
            var result = predict_classif(model, new double[4] {listInput[i * 4], listInput[i * 4 + 1], listInput[i * 4 + 2], listInput[i * 4 + 3] });
            //print(i +"resultat attendu" + listExpectedOutput[i] + "notre resultat" + result);
            if (listExpectedOutput[i].Equals(result))
            {
                purcentage++;
            }

        }

        purcentage = (purcentage / (listInput.ToArray().Length / 4)) * 100;

        print("pourcentage de reussite " + purcentage);
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
