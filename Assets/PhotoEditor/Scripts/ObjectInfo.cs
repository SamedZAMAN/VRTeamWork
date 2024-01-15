using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo
{
    // �rnek bir �zellik (property)
    public string objectName;

    public string objectInfo;

    // EnemyBackground ad�nda yeni bir ��e olu�turuyoruz
    public class EnemyBackground
    {
        public ObjectInfo enemyInfo;

        // Constructor (Yap�c� metod)
        public EnemyBackground(string name, string info)
        {
            enemyInfo = new ObjectInfo();
            enemyInfo.objectName = name;
            enemyInfo.objectInfo = info;
        }
    }

    // �rnek kullan�m
    public class ExampleUsage : MonoBehaviour
    {
        void Start()
        {
            // EnemyBackground ad�nda yeni bir ��e olu�turuyoruz ve bilgilerini dolduruyoruz
            EnemyBackground enemyBackground = new EnemyBackground("Enemy", "Enemy i g�rd�n ve onu kaydettin.");

            // Olu�turdu�umuz ��enin bilgilerine eri�ebiliriz
            Debug.Log("Object Name: " + enemyBackground.enemyInfo.objectName);
            Debug.Log("Object Info: " + enemyBackground.enemyInfo.objectInfo);
        }
    }
}
