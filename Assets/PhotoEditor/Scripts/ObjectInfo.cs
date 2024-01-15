using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo
{
    // Örnek bir özellik (property)
    public string objectName;

    public string objectInfo;

    // EnemyBackground adýnda yeni bir öðe oluþturuyoruz
    public class EnemyBackground
    {
        public ObjectInfo enemyInfo;

        // Constructor (Yapýcý metod)
        public EnemyBackground(string name, string info)
        {
            enemyInfo = new ObjectInfo();
            enemyInfo.objectName = name;
            enemyInfo.objectInfo = info;
        }
    }

    // Örnek kullaným
    public class ExampleUsage : MonoBehaviour
    {
        void Start()
        {
            // EnemyBackground adýnda yeni bir öðe oluþturuyoruz ve bilgilerini dolduruyoruz
            EnemyBackground enemyBackground = new EnemyBackground("Enemy", "Enemy i gördün ve onu kaydettin.");

            // Oluþturduðumuz öðenin bilgilerine eriþebiliriz
            Debug.Log("Object Name: " + enemyBackground.enemyInfo.objectName);
            Debug.Log("Object Info: " + enemyBackground.enemyInfo.objectInfo);
        }
    }
}
