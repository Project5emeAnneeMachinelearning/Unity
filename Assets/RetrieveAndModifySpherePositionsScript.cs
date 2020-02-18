using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RetrieveAndModifySpherePositionsScript : MonoBehaviour
{
    public Transform[] trainingSpheres;

    public Transform[] testSpheres;

    public Dropdown myDropdown;

    private double[] trainingInputs;

    private double[] trainingExpectedOutputs;

    private IntPtr model;

    private float xmax = 0, xmin = 0, zmax = 0, zmin =0;

    public void ReInitialize()
    {
        for (var i = 0; i < testSpheres.Length; i++)
        {
            testSpheres[i].position = new Vector3(
                testSpheres[i].position.x,
                0f,
                testSpheres[i].position.z);

            if (xmax < testSpheres[i].position.x)
                xmax = testSpheres[i].position.x;
            if (xmin > testSpheres[i].position.x)
                xmin = testSpheres[i].position.x;
            if (zmax < testSpheres[i].position.z)
                zmax = testSpheres[i].position.z;
            if (zmin > testSpheres[i].position.z)
                zmin = testSpheres[i].position.z;
        }
    }
    
    public void CreateModel()
    {
        //model = CreateLinearModel(2, 1);
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
        
        // TrainLinearModelRosenblatt(model, trainingInputs, 2, trainingSpheres.Length, trainingExpectedOutputs, 1, 0.01, 1000)
    }

    public void PredictOnTestSpheres()
    {
        for (var i = 0; i < testSpheres.Length; i++)
        {
            var input = new double[] {testSpheres[i].position.x, testSpheres[i].position.z};
            //var predictedY = PredictXXXLinearModel(model, input, 2)
            var predictedY = Random.Range(-5, 5);
            testSpheres[i].position = new Vector3(
                testSpheres[i].position.x,
                predictedY,
                testSpheres[i].position.z);
        }

    }

 
    public void Soft()
    {
        ReInitialize();
        int quotaSuperieurZero = trainingSpheres.Length / 2;
        for (var i = 0; i < trainingSpheres.Length; i++)
        {
            if (quotaSuperieurZero > 0)
            {
                var predictedZ = Random.Range(zmin, zmax);
                var predictedX = Random.Range(xmin, xmax);
                trainingSpheres[i].position = new Vector3(
                   predictedX,
                   1,
                   predictedZ);
            }
            else
            {
                var predictedZ = Random.Range(zmin, zmax);
                var predictedX = Random.Range(xmin, xmax);
                trainingSpheres[i].position = new Vector3(
                   predictedX,
                   -1,
                   predictedZ);
            }
            quotaSuperieurZero--;
        }
    }

    public void Xor()
    {
        ReInitialize();
        int predictForY = Random.Range(1, 3);
        switch (predictForY)
        {
            case 1 :
                trainingSpheres[0].position = new Vector3(xmax, -1, zmin);
                trainingSpheres[1].position = new Vector3(xmin, -1, zmax);
                trainingSpheres[2].position = new Vector3(xmax, 1, zmax);
                trainingSpheres[3].position = new Vector3(xmin, 1, zmin);
                break;
            case 2:
                trainingSpheres[0].position = new Vector3(xmax, 1, zmin);
                trainingSpheres[1].position = new Vector3(xmin, 1, zmax);
                trainingSpheres[2].position = new Vector3(xmax, -1, zmax);
                trainingSpheres[3].position = new Vector3(xmin, -1, zmin);
                break;
        }
    }

    public void Real()
    {
        ReInitialize();
        float predictedZ;
        float predictedX;
        int quotaSuperieurZero = trainingSpheres.Length / 2;
        for (var i = 0; i < trainingSpheres.Length; i++)
        {

            if (quotaSuperieurZero > 0)
            {
               predictedZ = Random.Range(zmin, 1);
               predictedX = Random.Range(xmin, 0);
                trainingSpheres[i].position = new Vector3(
               predictedX,
               1,
               predictedZ);
            }
            else
            {
                predictedZ = Random.Range(2, zmax);
                predictedX = Random.Range(1, xmax);
                trainingSpheres[i].position = new Vector3(
               predictedX,
               1,
               predictedZ);
            }
            quotaSuperieurZero--;
            
        }

    }

    public void ReleaseModel()
    {
        // FreeLinearModel(model);
    }

    public void chooseFunction(Dropdown target)
    {
        Debug.Log(target.value);
        switch(target.value)
        {
            case 0: Soft();
                break;
            case 1: Xor();
                break;
            case 2: Real();
                break;
            default: ReInitialize();
                break;
        }
    }
}