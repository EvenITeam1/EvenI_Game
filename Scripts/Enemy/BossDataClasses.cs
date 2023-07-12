
    [System.Serializable]
    public class BossData
    {
        public static readonly int indexBasis = 5000;
        public BOSS_INDEX Index;
        public string Boss_name;
        public BOSS_CATEGORY Boss_category;
        public float Boss_width;
        public float Boss_height;
        public float Boss_hp;
        public MOVEMENT_INDEX Movement_index;
        public string Boss_filecode;

        public BossData()
        {
            this.Index = BOSS_INDEX.DEFAULT;
            this.Boss_name = "비어있음";
            this.Boss_category = BOSS_CATEGORY.DEFAULT;
            this.Boss_width = 0;
            this.Boss_height = 0;
            this.Boss_hp = 0;
            this.Movement_index = MOVEMENT_INDEX.HOLD;
            this.Boss_filecode = "";
        }

        public BossData(string _parsedLine)
        {
            string[] datas = _parsedLine.Trim().Split('\t');

            this.Index = (BOSS_INDEX)int.Parse(datas[0]);
            this.Boss_name = datas[1].Replace('_', ' ');
            this.Boss_category = (BOSS_CATEGORY)int.Parse(datas[2]);
            this.Boss_width = float.Parse(datas[3]);
            this.Boss_height = float.Parse(datas[4]);
            this.Boss_hp = float.Parse(datas[5]);
            this.Movement_index = (MOVEMENT_INDEX)int.Parse(datas[6]);
            this.Boss_filecode = datas[7].Replace('_', ' ');
        }
    }
