using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class BubbleData
{
    [Header("BubblePrefab")]
    [Tooltip("��������A�̃v���n�u��ݒ�")]//�J�[�\�������킹��Ɛ������o�Ă���
    public GameObject prefab;

    [Range(0.0f, 1.0f)]//�X���C�_�[���o�Ă���
    [Tooltip("�A�̏o����")]
    public float weight = 1.0f;
}
public class BubbleSpawner : MonoBehaviour
{
    [Header("Prefab List with Probability")]
    [Tooltip("�v���n�u�Ɗm����o�^���Ă�������")]
    [SerializeField]
    List<BubbleData> list = new List<BubbleData>();
    //BubbleData�^�̃��X�g���쐬
    //���X�g�ɂ����͎̂�ނ������Ă��Ή��ł���悤��

    public float spawnInterval = 1.0f;
    //�X�|�[���Ԋu��ݒ�

    Coroutine spawnCoroutine;//�R���[�`��

    private void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnPrefabLoop());
    }

    IEnumerator SpawnPrefabLoop()//����̓R���[�`�������ɂ������ĕK�{
    {
        while (true)
        {
            SpawnRandomPrefab();
            yield return new WaitForSeconds(spawnInterval);
            spawnInterval = Random.Range(0.5f, 2.0f);
        }
    }

    void SpawnRandomPrefab()//�v���n�u�������_���ɐ������郁�\�b�h
    {//�d�ݕt���m���Ŏ���
        if (list == null || list.Count == 0)
        {
            Debug.LogWarning("�A�̃��X�g����ł�");
            //LogWarning�͊댯�M�����R���\�[���ɏo����
            return;
        }

        float totalWeight = 0.0f;//�d�݂̑��a

        foreach (var bubble in list)
        {//�d�݂̑��a�̌v�Z
            totalWeight += bubble.weight;
        }

        float rand = Random.Range(0.0f, totalWeight);
        //�����A�̗����l
        float currentWeight = 0.0f;
        //���݂̏d�݂�ێ�

        foreach (var bubble in list)
        {
            currentWeight += bubble.weight;//�d�݂��X�V
            if (rand <= currentWeight)
            {//�d�݂̕��ɗ����l���܂܂ꂽ��
                Instantiate(bubble.prefab, this.transform.position, Quaternion.identity);
                //�������d�݂����A�̃v���n�u�𐶐�
                //���W�̓X�|�i�[�̈ʒu
                //Quaternion.identity�ŉ�]�Ȃ�
                break;
            }
        }
    }

    void StopSpawning()
    {
        if (spawnCoroutine != null)
        {//�R���[�`�������������Ă���΁H
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }
}
