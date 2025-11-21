using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("基本ステータス")]
    public int hp = 100;           // ヒットポイント
    public int attack = 10;        // 攻撃力
    public float miningSpeed = 1.0f; // 採掘速度

    [Header("レベルと経験値")]
    public int level = 1;          // プレイヤーのレベル
    public int currentXP = 0;      // 現在の経験値

    // レベルアップに必要なXPのテーブル
    public int[] levelXPTable = {
        0, // ダミー
        3, 4, 5, 6, 7,
        8, 9, 10, 11, 13,
        15, 17, 19, 21, 24,
        27, 30, 33, 37, 41,
        46, 51, 57, 63, 70,
        77, 85
    };

    [Header("採掘に関するステータス")]
    public int digDamage = 1;  // 採掘時のダメージ
    public float digSpeed = 1.0f;  // 採掘速度

    void Start()
    {
        // 初期化などがあればここで行う
    }

    /// <summary>
    /// 経験値を加算し、レベルアップの処理を行う
    /// </summary>
    public void AddXP(int amount)
    {
        currentXP += amount;

        // レベルアップ処理
        while (level < levelXPTable.Length - 1 && currentXP >= levelXPTable[level])
        {
            currentXP -= levelXPTable[level];
            level++;

            Debug.Log("レベルアップ！ → LV " + level);

            // レベルアップ時のステータス更新（攻撃力、HPなど）
            OnLevelUp();
        }
    }

    /// <summary>
    /// レベルアップ時にステータスを更新する処理
    /// </summary>
    private void OnLevelUp()
    {
        // 攻撃力やHPなどをレベルアップ時に増加させる
        attack += 2;  // 攻撃力がレベルごとに増加
        hp += 10;     // HPがレベルごとに増加
        digDamage += 1;  // 採掘ダメージも増加
        digSpeed += 0.1f;  // 採掘速度も上昇

        // 他にレベルアップ時に付加したい効果があればここに追加
    }

    /// <summary>
    /// プレイヤーのステータスを取得（外部からアクセス可能）
    /// </summary>
    public int GetHP() => hp;
    public int GetAttack() => attack;
    public float GetMiningSpeed() => miningSpeed;
    public int GetLevel() => level;
    public int GetCurrentXP() => currentXP;
    public int GetXPToNextLevel() => level < levelXPTable.Length - 1 ? levelXPTable[level] - currentXP : 0;

    // 装備やその他のアクションと連携する場合、適宜追加のメソッドを実装
}
