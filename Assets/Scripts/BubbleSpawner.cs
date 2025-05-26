using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class BubbleData
{
    [Header("BubblePrefab")]
    [Tooltip("生成する泡のプレハブを設定")]//カーソルを合わせると説明が出てくる
    public GameObject prefab;

    [Range(0.0f, 1.0f)]//スライダーが出てくる
    [Tooltip("泡の出現率")]
    public float weight = 1.0f;
}
public class BubbleSpawner : MonoBehaviour
{
    [Header("Prefab List with Probability")]
    [Tooltip("プレハブと確率を登録してください")]
    [SerializeField]
    List<BubbleData> list = new List<BubbleData>();
    //BubbleData型のリストを作成
    //リストにしたのは種類が増えても対応できるように

    public float spawnInterval = 1.0f;
    //スポーン間隔を設定

    Coroutine spawnCoroutine;//コルーチン

    private void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnPrefabLoop());
    }

    IEnumerator SpawnPrefabLoop()//これはコルーチンを作るにあたって必須
    {
        while (true)
        {
            SpawnRandomPrefab();
            yield return new WaitForSeconds(spawnInterval);
            spawnInterval = Random.Range(0.5f, 2.0f);
        }
    }

    void SpawnRandomPrefab()//プレハブをランダムに生成するメソッド
    {//重み付き確率で実装
        if (list == null || list.Count == 0)
        {
            Debug.LogWarning("泡のリストが空です");
            //LogWarningは危険信号をコンソールに出せる
            return;
        }

        float totalWeight = 0.0f;//重みの総和

        foreach (var bubble in list)
        {//重みの総和の計算
            totalWeight += bubble.weight;
        }

        float rand = Random.Range(0.0f, totalWeight);
        //生成泡の乱数値
        float currentWeight = 0.0f;
        //現在の重みを保持

        foreach (var bubble in list)
        {
            currentWeight += bubble.weight;//重みを更新
            if (rand <= currentWeight)
            {//重みの幅に乱数値が含まれた時
                Instantiate(bubble.prefab, this.transform.position, Quaternion.identity);
                //加えた重みを持つ泡のプレハブを生成
                //座標はスポナーの位置
                //Quaternion.identityで回転なし
                break;
            }
        }
    }

    void StopSpawning()
    {
        if (spawnCoroutine != null)
        {//コルーチンが何か動いていれば？
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }
}
