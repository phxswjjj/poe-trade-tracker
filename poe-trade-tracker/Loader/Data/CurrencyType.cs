using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE.Loader.Data
{

    public enum CurrencyType
    {
        [EnumAttribute("unknown")]
        None = 0,
        /// <summary>
        /// 卡蘭德的魔鏡
        /// </summary>
        [EnumAttribute("卡蘭德的魔鏡")]
        MirrorOfKalandra = 1,
        /// <summary>
        /// 後悔石
        /// </summary>
        [EnumAttribute("後悔石")]
        RegretOrb = 2,
        /// <summary>
        /// 混沌石
        /// </summary>
        [EnumAttribute("混沌石")]
        ChaosOrb = 3,
        /// <summary>
        /// 機會石
        /// </summary>
        [EnumAttribute("機會石")]
        ChanceOrb = 4,
        /// <summary>
        /// 點金石
        /// </summary>
        [EnumAttribute("點金石")]
        AlchemyOrb = 5,
        /// <summary>
        /// 增幅石
        /// </summary>
        [EnumAttribute("增幅石")]
        AugmentationOrb = 6,
        /// <summary>
        /// 改造石
        /// </summary>
        [EnumAttribute("改造石")]
        AlterationOrb = 7,
        /// <summary>
        /// 重鑄石
        /// </summary>
        [EnumAttribute("重鑄石")]
        ScouringOrb = 8,
        /// <summary>
        /// 崇高石
        /// </summary>
        [EnumAttribute("崇高石")]
        ExaltedOrb = 9,
        /// <summary>
        /// 富豪石
        /// </summary>
        [EnumAttribute("富豪石")]
        RegalOrb = 10,
        /// <summary>
        /// 寶石匠的稜鏡
        /// </summary>
        [EnumAttribute("寶石匠的稜鏡")]
        PrismOfGemcutter = 11,
        /// <summary>
        /// 製圖釘
        /// </summary>
        [EnumAttribute("製圖釘")]
        ChiselOfCartographer = 12,
        /// <summary>
        /// 鏈結石
        /// </summary>
        [EnumAttribute("鏈結石")]
        FusingOrb = 13,
        /// <summary>
        /// 工匠石
        /// </summary>
        [EnumAttribute("工匠石")]
        JewelerOrb = 14,
        /// <summary>
        /// 神聖石
        /// </summary>
        [EnumAttribute("神聖石")]
        DivineOrb = 15,
        /// <summary>
        /// 祝福石
        /// </summary>
        [EnumAttribute("祝福石")]
        BlessedOrb = 16,
        /// <summary>
        /// 幻色石
        /// </summary>
        [EnumAttribute("幻色石")]
        ChromaticOrb = 17,
        /// <summary>
        /// 普蘭德斯金幣
        /// </summary>
        [EnumAttribute("普蘭德斯金幣")]
        PerandusCoin = 18,
        /// <summary>
        /// 瓦爾寶珠
        /// </summary>
        [EnumAttribute("瓦爾寶珠")]
        VaalOrb = 19
    }
}
