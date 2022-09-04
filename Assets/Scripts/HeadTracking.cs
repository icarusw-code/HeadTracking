using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HeadTracking : MonoBehaviour
{
    public UDPReceive udpReceive;
    public GameObject cameraObject;

    List<float> xList = new List<float>();
    List<float> yList = new List<float>();

    void Start()
    {
        
    }

    void Update()
    {
        string data = udpReceive.data;

        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);

        string[] points = data.Split(',');

        float x = (float.Parse(points[0]) - 640) / 100;
        float y = (float.Parse(points[1]) - 480) / 100;

        xList.Add(x);
        yList.Add(y);

        // 50 => 숫자가 커지면 반응이 느려짐
        if(xList.Count > 50) { xList.RemoveAt(0); }
        if(yList.Count > 50) { yList.RemoveAt(0); }

        float xAverage = Queryable.Average(xList.AsQueryable());
        float yAverage = Queryable.Average(yList.AsQueryable());

        Vector3 cameraPos = cameraObject.transform.localPosition;
        Vector3 cameraRot = cameraObject.transform.localEulerAngles;


        // 이동
        cameraObject.transform.localPosition = new Vector3(-25f - xAverage, 2.0f - yAverage, cameraPos.z);

        // 회전
        cameraObject.transform.localEulerAngles = new Vector3(18f - yAverage * 10, xAverage * 10, cameraRot.z);

    }
}
