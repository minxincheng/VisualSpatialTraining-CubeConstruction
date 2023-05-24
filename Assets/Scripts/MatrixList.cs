using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixList : MonoBehaviour
{

    public GameObject center, matrixcube, Matrix;
    private Vector3 centerPos;

    public void Initialize()
    {
        centerPos = center.transform.position;

        for (int i = 1; i < 10; i++)
        {
            for (int j = 1; j < 10; j++)
            {
                for (int k = 1; k < 10; k++)
                {
                    float x = (i - 5) * 0.04f;
                    float y = (j - 5) * 0.04f;
                    float z = (k - 5) * 0.04f;

                    GameObject temp = Instantiate(matrixcube, new Vector3(centerPos.x - x, centerPos.y - y, centerPos.z - z), Quaternion.Euler(0, 0, 0));
                    temp.name = i.ToString() + "-" + j.ToString() + "-" + k.ToString();
                    temp.transform.SetParent(Matrix.gameObject.transform);
                    temp.gameObject.GetComponent<MeshRenderer>().enabled = false;

                }
            }
        }

        Matrix.gameObject.transform.rotation = Quaternion.Euler(0, 45, 0);

    }
    
}
