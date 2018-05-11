using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class tool : MonoBehaviour {
    //public static tool Instance = null;

    //// Use this for initialization
    //void Awake () {
    //    Instance = this;
    //}
    public List<int[]> keyihudeshuzhu = new List<int[]>();
    public List<int[]> keyichideshuzhu = new List<int[]>();
    public List<int[]> keyipengdeshuzhu = new List<int[]>();
    public List<int[]> keyigangdeshuzhu = new List<int[]>();
    class shunode
    {
        public shunode before;
        public int chengshu;
        public int datasize;
        public System.Object data;
        public int[] shengyushuzhu;
        public List<shunode> nextcheng;
        public  bool isEqual(shunode b)
        {
            if (b != null)
            {
                if (datasize != b.datasize)
                {
                    return false;
                }
                if (datasize == 4)
                {
                    if (((sizhangmajiang)data).majiang1 == ((sizhangmajiang)b.data).majiang1)
                        return true;
                    else
                        return false;
                }
                else if(datasize == 3)
                {
                    if (((sanzhangmajiang)data).majiang1 == ((sanzhangmajiang)b.data).majiang1 && ((sanzhangmajiang)data).majiang2 == ((sanzhangmajiang)b.data).majiang2)
                        return true;
                    else
                        return false;
                }
                
            }
            return false;
        }
    };
    class sanzhangmajiang
    {
        public int majiang1;
        public int majiang2;
        public int majiang3;
        public bool piPei(int shumu, int[] shuzhu)//shuzhu为排序好的从小到大，shumu为数组包含的元素数目
        {
            bool find1 = false;
            bool find2 = false;
            bool find3 = false;
            for (int i = 0; i < shumu; i++)
            {
                if (shuzhu[i] == majiang1)
                {
                    find1 = true;
                }
                else if (shuzhu[i] == majiang2)
                {
                    find2 = true;
                }
                else if (shuzhu[i] == majiang3)
                {
                    find3 = true;
                }
            }
            if (find1 && find2 && find3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    class sizhangmajiang
    {
        public int majiang1;
        public int majiang2;
        public int majiang3;
        public int majiang4;
        public bool piPei(int shumu, int[] shuzhu)//shuzhu为排序好的从小到大，shumu为数组包含的元素数目
        {
            bool find1 = false;
            bool find2 = false;
            bool find3 = false;
            bool find4 = false;
            for (int i = 0; i < shumu; i++)
            {
                if (shuzhu[i] == majiang1)
                {
                    find1 = true;
                }
                else if (shuzhu[i] == majiang2)
                {
                    find2 = true;
                }
                else if (shuzhu[i] == majiang3)
                {
                    find3 = true;
                }
                else if (shuzhu[i] == majiang4)
                {
                    find4 = true;
                }
            }
            if (find1 && find2 && find3 && find4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    class liangzhangmajiang
    {
        public int majiang1;
        public int majiang2;
        public bool piPei(int shumu, int[] shuzhu)//shuzhu为排序好的从小到大，shumu为数组包含的元素数目
        {
            bool find1 = false;
            bool find2 = false;
            for (int i = 0; i < shumu; i++)
            {
                if (shuzhu[i] == majiang1)
                {
                    find1 = true;
                }
                else if (shuzhu[i] == majiang2)
                {
                    find2 = true;
                }

            }
            if (find1 && find2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    //算法 先找到每种吃、碰、杠的情况，找最终剩余的单牌，剩余两张时如果相同则能胡
    //因为吃的优先级最低，且必须要上家出牌才能吃到，所以要专门判断
    //参数chumajiangshu为新打出的麻将

    #region 从数组时排除三张麻将后剩余的数组
    int[] shengXiaDeMaJiang(sanzhangmajiang sanzhangmj, int[] shuzhu)
    {
        int shumu = shuzhu.Length;
        int[] shuzhu2 = new int[shumu];
        Array.Copy(shuzhu, shuzhu2, shumu);

        bool jian1 = false;
        bool jian2 = false;
        bool jian3 = false;
        for (int i = 0; i < shumu; i++)
        {
            if (!jian1)
            {
                if (shuzhu2[i] == sanzhangmj.majiang1)
                {
                    shuzhu2[i] = 0;
                    jian1 = true;
                }
            }
            else if (!jian2)
            {
                if (shuzhu2[i] == sanzhangmj.majiang2)
                {
                    shuzhu2[i] = 0;
                    jian2 = true;
                }
            }
            else if (!jian3)
            {
                if (shuzhu2[i] == sanzhangmj.majiang3)
                {
                    shuzhu2[i] = 0;
                    jian3 = true;
                }
            }
            if (jian1 && jian2 && jian3)
                break;
        }
        int[] temp = new int[shumu - 3];
        int index = 0;
        for (int i = 0; i < shumu; i++)
        {
            if (shuzhu2[i] != 0)
            {
                temp[index] = shuzhu2[i];
                index++;
            }
        }
        return temp;
    }
    #endregion
    #region 从数组时排除四张麻将后剩余的数组
    int[] shengXiaDeMaJiang(sizhangmajiang sizhangmj, int[] shuzhu)
    {
        int shumu = shuzhu.Length;
        int[] shuzhu2 = new int[shumu];
        Array.Copy(shuzhu, shuzhu2, shumu);
        bool jian1 = false;
        bool jian2 = false;
        bool jian3 = false;
        bool jian4 = false;
        for (int i = 0; i < shumu; i++)
        {
            if (!jian1)
            {
                if (shuzhu2[i] == sizhangmj.majiang1)
                {
                    shuzhu2[i] = 0;
                    jian1 = true;
                }
            }
            else if (!jian2)
            {
                if (shuzhu2[i] == sizhangmj.majiang2)
                {
                    shuzhu2[i] = 0;
                    jian2 = true;
                }
            }
            else if (!jian3)
            {
                if (shuzhu2[i] == sizhangmj.majiang3)
                {
                    shuzhu2[i] = 0;
                    jian3 = true;
                }
            }
            else if (!jian4)
            {
                if (shuzhu2[i] == sizhangmj.majiang4)
                {
                    shuzhu2[i] = 0;
                    jian4 = true;
                }
            }
            if (jian1 && jian2 && jian3 & jian4)
                break;
        }
        int[] temp = new int[shumu];
        int index = 0;
        for (int i = 0; i < shumu - 4; i++)
        {
            if (shuzhu2[i] != 0)
            {
                temp[index] = shuzhu2[i];
                index++;
            }
        }
        return temp;
    }
    #endregion
    #region 从数组时排除两张麻将后剩余的数组
    int[] shengXiaDeMaJiang(liangzhangmajiang liangzhangmj, int[] shuzhu)
    {
        int shumu = shuzhu.Length;
        int[] shuzhu2 = new int[shumu];
        Array.Copy(shuzhu, shuzhu2, shumu);
        bool jian1 = false;
        bool jian2 = false;
        for (int i = 0; i < shumu; i++)
        {
            if (!jian1)
            {
                if (shuzhu2[i] == liangzhangmj.majiang1)
                {
                    shuzhu2[i] = 0;
                    jian1 = true;
                }
            }
            else if (!jian2)
            {
                if (shuzhu2[i] == liangzhangmj.majiang2)
                {
                    shuzhu2[i] = 0;
                    jian2 = true;
                }
            }

            if (jian1 && jian2)
                break;
        }
        int[] temp = new int[shumu -2];
        int index = 0;
        for (int i = 0; i < shumu; i++)
        {
            if (shuzhu2[i] != 0)
            {
                temp[index] = shuzhu2[i];
                index++;
            }
        }
        return temp;
    }
    #endregion
    #region 判断数组里是否包含三张麻将
    bool findMaJiangsInShuZhu(sanzhangmajiang sanzhangmj, int[] shuzhu)
    {

        int shumu = shuzhu.Length;
        bool jian1 = false;
        bool jian2 = false;
        bool jian3 = false;
        for (int i = 0; i < shumu; i++)
        {
            if (!jian1)
            {
                if (shuzhu[i] == sanzhangmj.majiang1)
                {
                    jian1 = true;
                }
            }
            else if (!jian2)
            {
                if (shuzhu[i] == sanzhangmj.majiang2)
                {

                    jian2 = true;
                }
            }
            else if (!jian3)
            {
                if (shuzhu[i] == sanzhangmj.majiang3)
                {

                    jian3 = true;
                }
            }
            if (jian1 && jian2 && jian3)
            {
                return true;
            }
        }
        return false;
    }
    #endregion
    #region 判断数组里是否包含四张麻将
    bool findMaJiangsInShuZhu(sizhangmajiang sizhangmj, int[] shuzhu)
    {
        int shumu = shuzhu.Length;
        bool jian1 = false;
        bool jian2 = false;
        bool jian3 = false;
        bool jian4 = false;
        for (int i = 0; i < shumu; i++)
        {
            if (!jian1)
            {
                if (shuzhu[i] == sizhangmj.majiang1)
                {

                    jian1 = true;
                }
            }
            else if (!jian2)
            {
                if (shuzhu[i] == sizhangmj.majiang2)
                {

                    jian2 = true;
                }
            }
            else if (!jian3)
            {
                if (shuzhu[i] == sizhangmj.majiang3)
                {

                    jian3 = true;
                }
            }
            else if (!jian4)
            {
                if (shuzhu[i] == sizhangmj.majiang4)
                {

                    jian4 = true;
                }
            }
            if (jian1 && jian2 && jian3 & jian4)
            {
                return true;
            }
        }
        return false;
    }
    #endregion
    #region 判断数组里是否包含两张麻将
    bool findMaJiangsInShuZhu(liangzhangmajiang liangzhangmj, int[] shuzhu)
    {
        int shumu = shuzhu.Length;
        bool jian1 = false;
        bool jian2 = false;
        for (int i = 0; i < shumu; i++)
        {
            if (!jian1)
            {
                if (shuzhu[i] == liangzhangmj.majiang1)
                {

                    jian1 = true;
                }
            }
            else if (!jian2)
            {
                if (shuzhu[i] == liangzhangmj.majiang2)
                {

                    jian2 = true;
                }
            }

            if (jian1 && jian2)
            {
                return true;
            }
        }
        return false;
    }
    #endregion
    #region 添加分析出的匹配麻将到相关的集合供后续分析用
    void addMaJiangsToList(sanzhangmajiang sanzhangmj, List<sanzhangmajiang> sanzhangmajianglist)
    {
        sanzhangmajianglist.Add(sanzhangmj);
    }
    void addMaJiangsToList(sizhangmajiang sizhangmj, List<sizhangmajiang> sizhangmajianglist)
    {
        sizhangmajianglist.Add(sizhangmj);
    }
    void addMaJiangsToList(liangzhangmajiang liangzhangmj, List<liangzhangmajiang> liangzhangmajianglist)
    {
        liangzhangmajianglist.Add(liangzhangmj);
    }
    #endregion
    List<sanzhangmajiang> sanzhangmajianglist = new List<sanzhangmajiang>();
    List<sizhangmajiang> sizhangmajianglist = new List<sizhangmajiang>();
    List<liangzhangmajiang> liangzhangmajianglist = new List<liangzhangmajiang>();
    List<sizhangmajiang> angangmajianglist = new List<sizhangmajiang>();//暗杠集合

    int[] analysisInit(bool wofanghuihe, bool shangjiachumajiang, int chumajiangshu, int[] shuzhu, ref int chulijieguo, ref bool istingpai, ref List<int> yaochudemajiangshuzhu)//shuzhu中的数可以重复
    {//shuzhu参数为已经从小到大排序的麻将数组,其中肯定包含了新出的麻将即chumajiangshu
     //为了AI判断，可以把chumajiangshu设为0，并添加到shuzhu开头，再分析即为旧情况,此时不包含新出的麻将,相当于我方从麻将堆里摸出一个值为0的麻将

        //判断的场景分三种
        //1.上家出了一张麻将
        //2.另外两家出了一张麻将
        //3.我方从麻将堆里摸出一张新麻将
        //参数wofanghuihe为true都为自己摸出一张新麻将，这时可以判断能否暗杠或胡
        int shumu = shuzhu.Length;
        if (shumu % 3 != 2)
            return null;
        //先找出所有匹配特例

        #region 上家出麻将判断能不能吃
        if (!wofanghuihe)
        {
            if (shangjiachumajiang)
            {
                //判断能不能吃，要排除麻将不同将花色相同的重复情况
                //总共有三种情况，共五个数相关
                bool findshu1 = false;
                bool findshu2 = false;
                bool findshu3 = true;
                bool findshu4 = false;
                bool findshu5 = false;
                for (int i = 0; i < shumu; i++)
                {
                    if (shuzhu[i] < chumajiangshu - 2)
                        continue;
                    if (shuzhu[i] > chumajiangshu + 2)
                        break;
                    if (shuzhu[i] == chumajiangshu - 2)
                    {
                        findshu1 = true;
                    }
                    else if (shuzhu[i] == chumajiangshu - 1)
                    {
                        findshu2 = true;
                    }
                    else if (shuzhu[i] == chumajiangshu + 1)
                    {
                        findshu4 = true;
                    }
                    else if (shuzhu[i] == chumajiangshu + 2)
                    {
                        findshu5 = true;
                    }

                }
                if (findshu1 && findshu2)
                {
                    sanzhangmajiang temp = new sanzhangmajiang();
                    temp.majiang1 = chumajiangshu - 2;
                    temp.majiang2 = chumajiangshu - 1;
                    temp.majiang3 = chumajiangshu;
                    addMaJiangsToList(temp, sanzhangmajianglist);
                }
                if (findshu2 && findshu4)
                {
                    sanzhangmajiang temp = new sanzhangmajiang();
                    temp.majiang1 = chumajiangshu - 1;
                    temp.majiang2 = chumajiangshu;
                    temp.majiang3 = chumajiangshu + 1;
                    addMaJiangsToList(temp, sanzhangmajianglist);
                }
                if (findshu4 && findshu5)
                {
                    sanzhangmajiang temp = new sanzhangmajiang();
                    temp.majiang1 = chumajiangshu;
                    temp.majiang2 = chumajiangshu + 1;
                    temp.majiang3 = chumajiangshu + 2;
                    addMaJiangsToList(temp, sanzhangmajianglist);
                }

            }
        }
        #endregion
        #region 别人出麻将我方判断能不能碰或杠
        if (!wofanghuihe)
        {
            int inum = 0;
            for (int i = 0; i < shumu; i++)
            {
                if (shuzhu[i] == chumajiangshu)
                {
                    inum++;
                }
            }
            if (inum >= 3)
            {
                sanzhangmajiang temp = new sanzhangmajiang();
                temp.majiang1 = chumajiangshu;
                temp.majiang2 = chumajiangshu;
                temp.majiang3 = chumajiangshu;
                addMaJiangsToList(temp, sanzhangmajianglist);
            }
            if (inum == 4)
            {
                sizhangmajiang temp = new sizhangmajiang();
                temp.majiang1 = chumajiangshu;
                temp.majiang2 = chumajiangshu;
                temp.majiang3 = chumajiangshu;
                temp.majiang4 = chumajiangshu;
                addMaJiangsToList(temp, sizhangmajianglist);
            }
        }
        #endregion
        #region 接下来分析不受新麻将的影响而原本存在的麻将，如原本存在的暗杠,三张同花或是三张连花
        if (!wofanghuihe)
        {

            for (int i = 0; i < shumu; i++)
            {
                if (shuzhu[i] == chumajiangshu)
                {
                    continue;
                }
                if (i + 2 < shumu)
                {
                    if (shuzhu[i] == shuzhu[i + 1] && shuzhu[i] == shuzhu[i + 2])//判断三个同色
                    {
                        sanzhangmajiang sanzhangmj = new sanzhangmajiang();
                        sanzhangmj.majiang1 = shuzhu[i];
                        sanzhangmj.majiang2 = shuzhu[i];
                        sanzhangmj.majiang3 = shuzhu[i];
                        addMaJiangsToList(sanzhangmj, sanzhangmajianglist);
                        i += 2;
                        continue;
                    }
                }
                if (i + 3 < shumu)
                {
                    if (shuzhu[i] == shuzhu[i + 1] && shuzhu[i] == shuzhu[i + 2] && shuzhu[i] == shuzhu[i + 3])//判断四个同色
                    {
                        sizhangmajiang angangmj = new sizhangmajiang();
                        angangmj.majiang1 = shuzhu[i];
                        angangmj.majiang2 = shuzhu[i];
                        angangmj.majiang3 = shuzhu[i];
                        angangmj.majiang4 = shuzhu[i];
                        angangmajianglist.Add(angangmj);
                        i += 1;
                        continue;
                    }
                }
                if (shuzhu[i] < chumajiangshu - 2)//判断三连顺小的一边
                {
                    bool b1 = false;
                    bool b2 = false;
                    for (int k = i; k < shumu; k++)
                    {
                        if (shuzhu[k] == chumajiangshu)
                        {
                            break;
                        }
                        if (shuzhu[k] == shuzhu[i] + 1)
                        {
                            b1 = true;
                        }
                        else if (shuzhu[k] == shuzhu[i] + 2)
                        {
                            b2 = true;
                            if (b1)
                            {
                                sanzhangmajiang temp = new sanzhangmajiang();
                                temp.majiang1 = shuzhu[i];
                                temp.majiang2 = shuzhu[i] + 1;
                                temp.majiang3 = shuzhu[i] + 2;
                                addMaJiangsToList(temp, sanzhangmajianglist);
                            }
                            break;
                        }
                    }
                }
                else if (shuzhu[i] > chumajiangshu)//判断三连顺大的一边，跳过了已经判断的部分
                {
                    bool b1 = false;
                    bool b2 = false;
                    for (int k = i; k < shumu; k++)
                    {
                        if (shuzhu[k] == shuzhu[i] + 1)
                        {
                            b1 = true;
                        }
                        else if (shuzhu[k] == shuzhu[i] + 2)
                        {
                            b2 = true;
                            if (b1)
                            {
                                sanzhangmajiang temp = new sanzhangmajiang();
                                temp.majiang1 = shuzhu[i];
                                temp.majiang2 = shuzhu[i] + 1;
                                temp.majiang3 = shuzhu[i] + 2;
                                addMaJiangsToList(temp, sanzhangmajianglist);
                            }
                            break;
                        }
                    }

                }



            }
        }
        #endregion
        #region 我方从麻将堆里摸了一个麻将，此时判断是否能暗杠
        if (wofanghuihe)
        {
            for (int i = 0; i < shumu; i++)
            {
                if ((i + 3) <= shumu - 1)//找暗杠
                {
                    if (shuzhu[i] == shuzhu[i + 1] && shuzhu[i] == shuzhu[i + 2] && shuzhu[i] == shuzhu[i + 3])
                    {
                        {
                            sizhangmajiang temp = new sizhangmajiang();
                            temp.majiang1 = shuzhu[i];
                            temp.majiang2 = shuzhu[i];
                            temp.majiang3 = shuzhu[i];
                            temp.majiang4 = shuzhu[i];
                            angangmajianglist.Add(temp);//此时能暗杠
                            yaochudemajiangshuzhu.Add(shuzhu[i]);
                            yaochudemajiangshuzhu.Add(shuzhu[i]);
                            yaochudemajiangshuzhu.Add(shuzhu[i]);
                            yaochudemajiangshuzhu.Add(shuzhu[i]);
                        }
                        {//这段稍显无用
                            sanzhangmajiang temp = new sanzhangmajiang();
                            temp.majiang1 = shuzhu[i];
                            temp.majiang2 = shuzhu[i];
                            temp.majiang3 = shuzhu[i];
                            sanzhangmajianglist.Add(temp);
                        }
                        i += 2;
                        chulijieguo = 3;// 暗杠
                        continue;

                    }
                }
                if ((i + 2) <= shumu - 1)//找三个花色相同
                {
                    if (shuzhu[i] == shuzhu[i + 1] && shuzhu[i] == shuzhu[i + 2])
                    {
                        sanzhangmajiang temp = new sanzhangmajiang();
                        temp.majiang1 = shuzhu[i];
                        temp.majiang2 = shuzhu[i];
                        temp.majiang3 = shuzhu[i];
                        sanzhangmajianglist.Add(temp);
                        i += 1;
                        continue;
                    }
                }
                //找三连顺
                bool b1 = false;
                bool b2 = false;
                for (int k = i; k < shumu; k++)
                {
                    if (shuzhu[k] == shuzhu[i] + 1)
                    {
                        b1 = true;
                    }
                    else if (shuzhu[k] == shuzhu[i] + 2)
                    {
                        b2 = true;
                        if (b1)
                        {
                            sanzhangmajiang temp = new sanzhangmajiang();
                            temp.majiang1 = shuzhu[i];
                            temp.majiang2 = shuzhu[i] + 1;
                            temp.majiang3 = shuzhu[i] + 2;
                            addMaJiangsToList(temp, sanzhangmajianglist);
                        }
                        break;
                    }
                }
            }


        }
        #endregion

        return null;
    }
    int chengshumax = 0;

    void shengChengShuZhi(shunode root)
    {


        for (int i = 0; i < sanzhangmajianglist.Count; i++)
        {
            if (findMaJiangsInShuZhu(sanzhangmajianglist[i], root.shengyushuzhu))
            {

                int[] currentshuzhu = shengXiaDeMaJiang(sanzhangmajianglist[i], root.shengyushuzhu);
                shunode temp = new shunode();
                temp.chengshu = root.chengshu + 1;
                temp.datasize = 3;
                temp.data = sanzhangmajianglist[i];
                temp.shengyushuzhu = currentshuzhu;
                temp.nextcheng = new List<shunode>();
                temp.before = root;
                root.nextcheng.Add(temp);
                if (temp.chengshu > chengshumax)
                {
                    chengshumax = temp.chengshu;

                
                }
            }
        }
        for (int i = 0; i < sizhangmajianglist.Count; i++)
        {
            if (findMaJiangsInShuZhu(sizhangmajianglist[i], root.shengyushuzhu))
            {
                int[] currentshuzhu = shengXiaDeMaJiang(sizhangmajianglist[i], root.shengyushuzhu);
                shunode temp = new shunode();
                temp.chengshu = root.chengshu + 1;
                if (temp.chengshu > chengshumax)
                    chengshumax = root.chengshu;
                temp.datasize = 4;
                temp.data = sanzhangmajianglist[i];
                temp.shengyushuzhu = currentshuzhu;
                temp.nextcheng = new List<shunode>();
                temp.before = root;
                root.nextcheng.Add(temp);
            }
        }
        for (int i = 0; i < angangmajianglist.Count; i++)
        {
            if (findMaJiangsInShuZhu(angangmajianglist[i], root.shengyushuzhu))
            {
                int[] currentshuzhu = shengXiaDeMaJiang(angangmajianglist[i], root.shengyushuzhu);
                shunode temp = new shunode();
                temp.chengshu = root.chengshu + 1;
                if (temp.chengshu > chengshumax)
                    chengshumax = root.chengshu;
                temp.datasize = 4;
                temp.data = sanzhangmajianglist[i];
                temp.shengyushuzhu = currentshuzhu;
                temp.nextcheng = new List<shunode>();
                temp.before = root;
                root.nextcheng.Add(temp);
            }
        }
    }
    void findAndBuildAllNodes(shunode root)
    {

        if (root.chengshu > xiandingchenggaomax)
        {
            return;
        }
        shengChengShuZhi(root);

        //以下为还没到指定层数情况
        if (root.nextcheng.Count > 0)
        {
            for (int i = 0; i < root.nextcheng.Count; i++)
            {
                findAndBuildAllNodes(root.nextcheng[i]);
            }
        }

    }
    bool findShuInSanZhangMaJiang(int shu, sanzhangmajiang sanzhangmj)
    {
        if (sanzhangmj.majiang1 == shu || sanzhangmj.majiang2 == shu || sanzhangmj.majiang3 == shu)
        {
            return true;
        }
        else
            return false;
    }
    bool findShuInSiZhangMaJiang(int shu, sizhangmajiang sizhangmj)
    {
        if (sizhangmj.majiang1 == shu || sizhangmj.majiang2 == shu || sizhangmj.majiang3 == shu || sizhangmj.majiang4 == shu)
        {
            return true;
        }
        else
            return false;
    }
    bool findone = false;
    shunode findonenode = null;
    int lessshengyushu = 14;
    bool findOneShuNodeOnChengShuWithLessShengYuMaJiangShu(shunode root, int chengshu)
    {
        if (root.chengshu > chengshu)
        {
            return false;
        }
        if (root.chengshu == chengshu)
        {
            if (root.shengyushuzhu.Length < lessshengyushu && root.shengyushuzhu.Length > 0)
            {
                findone = true;
                findonenode = root;
                lessshengyushu = root.shengyushuzhu.Length;
                return true;
            }
            return false;
        }
        //以下为还可以拓展情况
        if (root.nextcheng.Count > 0)
        {
            for (int i = 0; i < root.nextcheng.Count; i++)
            {
                findOneShuNodeOnChengShuWithLessShengYuMaJiangShu(root.nextcheng[i], chengshu);
            }
        }
        return false;
    }
    bool findOneShuNodeOnChengShu(shunode root, int chengshu, int shu)
    {
        if (findone)
            return false;

        if (root.chengshu > chengshu)
        {
            return false;
        }

        if (root.chengshu == chengshu)
        {
            if (root.datasize == 3)
            {
                if (findShuInSanZhangMaJiang(shu, (sanzhangmajiang)root.data))
                {
                   
                    findone = true;
                    findonenode = root;

                    return true;
                }
            }
            else if (root.datasize == 4)
            {
                if (findShuInSiZhangMaJiang(shu, (sizhangmajiang)root.data))
                {
                 
                    findone = true;
                    findonenode = root;
                    return true;
                }
            }
            return false;
        }
        //以下为还没到指定层数情况
        if (root.nextcheng.Count > 0)
        {
            for (int i = 0; i < root.nextcheng.Count; i++)
            {
                findOneShuNodeOnChengShu(root.nextcheng[i], chengshu, shu);
            }
        }
        return false;
    }
    int xiandingchenggaomax = 4;
    void shengChengShu(shunode root)
    {

        findAndBuildAllNodes(root);

    }
    void deleteShu(shunode root)
    {
        if (root != null)
        {
            if (root.nextcheng.Count > 0)
            {
                int count = root.nextcheng.Count;
                for (int i = count - 1; i > 0; i--)
                {
                    deleteShu(root.nextcheng[i]);
                }
            }
            root = null;
        }
    }
    void analysisOld(int chumajiangshu, int[] shuzhu, ref int chulijieguo, ref bool istingpai, ref List<int> yaochudemajiangshuzhu)
    {
        //为了AI判断，可以把chumajiangshu设为0，并添加到shuzhu开头，再分析即为旧情况,此时不包含新出的麻将,相当于我方从麻将堆里摸出一个值为0的麻将
        int[] oldshuzhu = new int[shuzhu.Length];
        oldshuzhu[0] = 0;
        bool tiaoguo = false;
        int index = 1;
        for (int i = 0; i < shuzhu.Length; i++)
        {
            if (shuzhu[i] == chumajiangshu)
            {
                if (!tiaoguo)
                {
                    tiaoguo = true;
                    continue;
                }
            }
            oldshuzhu[index] = shuzhu[i];
            index++;
        }
        analysisInit(true, false, 0, oldshuzhu,ref chulijieguo,ref istingpai,ref yaochudemajiangshuzhu);
    }
    void freshLists()
    {
        sanzhangmajianglist.Clear();
        sizhangmajianglist.Clear();
        liangzhangmajianglist.Clear();
        angangmajianglist.Clear();

    }
    void shengChengShuZhiWithTwoMaJiangs(shunode root)
    {
        for (int i = 0; i < liangzhangmajianglist.Count; i++)
        {
            if (findMaJiangsInShuZhu(liangzhangmajianglist[i], root.shengyushuzhu))
            {

                int[] currentshuzhu = shengXiaDeMaJiang(liangzhangmajianglist[i], root.shengyushuzhu);
                shunode temp = new shunode();
                temp.chengshu = root.chengshu + 1;
                temp.datasize = 2;
                temp.data = liangzhangmajianglist[i];
                temp.shengyushuzhu = currentshuzhu;
                temp.nextcheng = new List<shunode>();
                temp.before = root;
                root.nextcheng.Add(temp);
                if (temp.chengshu > chengshumax)
                {

                    chengshumax = temp.chengshu;
                    findone = true;
                    findonenode = temp;
                    
                }
            }
        }
    }
    void findAndBuildAllNodesOnChengShuWithTwoMajiangs(shunode root)
    {

        if (root.chengshu > 2)
        {
            return;
        }
        shengChengShuZhiWithTwoMaJiangs(root);

        //以下为还可以拓展情况
        if (root.nextcheng.Count > 0)
        {
            for (int i = 0; i < root.nextcheng.Count; i++)
            {
                findAndBuildAllNodesOnChengShuWithTwoMajiangs(root.nextcheng[i]);
            }
        }

    }
    void analysisWhenZhangShuLess5(ref int yaochudemajiangshu, int[] shuzhu)
    {
        liangzhangmajianglist.Clear();
        findone = false;
        chengshumax = 0;
     
        int shumu = shuzhu.Length;
        for (int i = 0; i < shumu - 1; i++)
        {
            if (shuzhu[i] == shuzhu[i + 1] || shuzhu[i] == shuzhu[i + 1] - 1 || shuzhu[i] == shuzhu[i + 1] - 2)
            {
                liangzhangmajiang liangzhangmj = new liangzhangmajiang();
                liangzhangmj.majiang1 = shuzhu[i];
                liangzhangmj.majiang2 = shuzhu[i + 1];
                addMaJiangsToList(liangzhangmj, liangzhangmajianglist);
                i += 1;
                continue;
            }
        }
        shunode root;
        root = new shunode();
        root.shengyushuzhu = shuzhu;
        root.chengshu = 0;
        root.nextcheng = new List<shunode>();
        findAndBuildAllNodesOnChengShuWithTwoMajiangs(root);

        if (findone)
        {
            //DebugLog(findonenode.shengyushuzhu);
            if(findonenode.shengyushuzhu.Length == 0)
            {
                yaochudemajiangshu = shuzhu[shuzhu.Length - 1];//先从风拆对
            }
            else if (findonenode.shengyushuzhu.Length == 1)
            {
                yaochudemajiangshu = findonenode.shengyushuzhu[0];
            }
            else
            {
                for(int i = findonenode.shengyushuzhu.Length - 1;i>=0;i--)
                {
                    if(findonenode.shengyushuzhu[i]>400)
                    {
                        yaochudemajiangshu = findonenode.shengyushuzhu[i];
                        return;
                    }
                    else if(findonenode.shengyushuzhu[i] < 400)
                    {
                        break;
                    }
                }
                int num1 = 0;
                int num2 = 0;
                int num3 = 0;
                for(int i = 0;i< findonenode.shengyushuzhu.Length;i++)
                {
                    if(findonenode.shengyushuzhu[i]<200)
                    {
                        num1++;
                    }
                    else if(findonenode.shengyushuzhu[i] < 300)
                    {
                        num2++;
                    }
                    else if(findonenode.shengyushuzhu[i] < 400)
                    {
                        num3++;
                    }
                }

                int imax = num1;
                if(imax<num2)
                {
                    imax = num2;
                }
                if(imax<num3)
                {
                    imax = num3;
                }
                if (imax > 0)
                {
                    if (imax == num1)
                    {

                        System.Random rd = new System.Random();
                        int shijishumu = num2 + num3;
                        float t = rd.Next(0, shijishumu);
                        yaochudemajiangshu = findonenode.shengyushuzhu[num1 + (int)t - 1];
                    }
                    else if (imax == num2)
                    {
                        System.Random rd = new System.Random();
                        int shijishumu = num1 + num3;
                        float t = rd.Next(0, shijishumu);
                        if ((int)t > num1)
                        {
                            yaochudemajiangshu = findonenode.shengyushuzhu[num2 + (int)t - 1];
                        }
                        else
                        {
                            yaochudemajiangshu = findonenode.shengyushuzhu[(int)t];
                        }
                    }
                    else if (imax == num3)
                    {
                        System.Random rd = new System.Random();
                        int shijishumu = num1 + num2;
                        float t = rd.Next(0, shijishumu);
                        yaochudemajiangshu = findonenode.shengyushuzhu[(int)t ];
                    }
                }
                else//quan feng
                {
                    yaochudemajiangshu = shuzhu[shuzhu.Length - 1];
                }
            }
        }
        else
        {

            yaochudemajiangshu = shuzhu[shuzhu.Length - 1];//没对先从风出
        }
    }

    //别人回合我方要不要集成方法
    public void yaoBuYao(int currentchudeshu, int[] shuzhu,ref int chulijieguo, ref bool istingpai, ref List<int> yaochudemajiangshuzhu)
    {
     
        sizhangmajianglist.Clear();
        sanzhangmajianglist.Clear();
        angangmajianglist.Clear();
        liangzhangmajianglist.Clear();
        yaochudemajiangshuzhu.Clear();
        chulijieguo = -1;
        istingpai = false;
        #region 这是要不要牌的分析
        shunode root;
        //特例分析，七大对
        if (shuzhu.Length == 14)
        {
            if (shuzhu[0] == shuzhu[1] && shuzhu[2] == shuzhu[3] && shuzhu[4] == shuzhu[5] && shuzhu[6] == shuzhu[7] && shuzhu[8] == shuzhu[9] && shuzhu[10] == shuzhu[11] && shuzhu[12] == shuzhu[13])
            {
                chulijieguo = 4;//此为胡
               
                return;
            }
        }
        {
            //freshLists();

            chengshumax = 0;
            analysisOld(currentchudeshu, shuzhu,ref chulijieguo,ref istingpai,ref yaochudemajiangshuzhu);
        
            root = new shunode();
            root.shengyushuzhu = shuzhu;
            root.chengshu = 0;
            root.nextcheng = new List<shunode>();
            shengChengShu(root);
            deleteShu(root);
        }
        int oldchengshumax = chengshumax;
        {
            freshLists();
            chengshumax = 0;

            analysisInit(false, true, currentchudeshu, shuzhu,ref chulijieguo,ref istingpai,ref yaochudemajiangshuzhu);
            root = new shunode();
            root.shengyushuzhu = shuzhu;
            root.chengshu = 0;
            root.nextcheng = new List<shunode>();
            shengChengShu(root);

        }
        int xingchengshumax = chengshumax;
        if (xingchengshumax >= oldchengshumax && chengshumax > 0)
        {
            findone = false;
            lessshengyushu = 14;
            findOneShuNodeOnChengShuWithLessShengYuMaJiangShu(root, chengshumax);
            if (findone)//单牌可以在组成对子胡时要
            {
               
              
                int shengyushu = findonenode.shengyushuzhu.Length;
             
                if (shengyushu == 2)
                {
                    if (findonenode.shengyushuzhu[0] == findonenode.shengyushuzhu[1])
                    {                       
                        chulijieguo = 4;//此为胡
                        
                        return;
                    }
                }

            }

            findone = false;
            findOneShuNodeOnChengShu(root, chengshumax, currentchudeshu);
            if (findone)//可以吃 碰 杠
            {
           
              
                int shengyushu = findonenode.shengyushuzhu.Length;
                if (shengyushu == 2)
                {
                    if (findonenode.shengyushuzhu[0] == findonenode.shengyushuzhu[1])
                    {
                        chulijieguo = 4;//可以胡
                       
                        return;
                    }
                    else
                    {
                        istingpai = true;
                    }
                }
                if (findonenode.datasize == 4)
                {
                    chulijieguo = 3;
                    yaochudemajiangshuzhu.Add(((sizhangmajiang)findonenode.data).majiang1);
                    yaochudemajiangshuzhu.Add(((sizhangmajiang)findonenode.data).majiang2);
                    yaochudemajiangshuzhu.Add(((sizhangmajiang)findonenode.data).majiang3);
                    yaochudemajiangshuzhu.Add(((sizhangmajiang)findonenode.data).majiang4);
                   
                    return;
                }
                else if (findonenode.datasize == 3)
                {
                    if (((sanzhangmajiang)findonenode.data).majiang1 == ((sanzhangmajiang)findonenode.data).majiang2)
                    {
                        chulijieguo = 2;

                    }
                    else
                    {
                        chulijieguo = 1;

                    }
                    yaochudemajiangshuzhu.Add(((sanzhangmajiang)findonenode.data).majiang1);
                    yaochudemajiangshuzhu.Add(((sanzhangmajiang)findonenode.data).majiang2);
                    yaochudemajiangshuzhu.Add(((sanzhangmajiang)findonenode.data).majiang3);
                    
                    return;
                }

            }

        }
        chulijieguo = 0;
        
        return;
        
        #endregion
    }


    //我方回合集成方法
    public void woFangChuMaJiangXuanZhe(int[] shuzhu, ref int chulijieguo, ref bool istingpai,ref int yaochudemajiangshu,ref List<int> yaochudemajiangshuzhu) //chulijieguo大于10则为要出的麻将数值
    {
       
        sizhangmajianglist.Clear();
        sanzhangmajianglist.Clear();
        angangmajianglist.Clear();
        liangzhangmajianglist.Clear();
        yaochudemajiangshu = 0;
        istingpai = false;
        chulijieguo = -1;
        yaochudemajiangshuzhu.Clear();
        #region 这里是我方牌权时怎么出牌的分析
        analysisInit(false, true, 0, shuzhu,ref chulijieguo,ref istingpai,ref yaochudemajiangshuzhu);

        //特例分析，七大对
        if (shuzhu.Length == 14)
        {
            if (shuzhu[0] == shuzhu[1] && shuzhu[2] == shuzhu[3] && shuzhu[4] == shuzhu[5] && shuzhu[6] == shuzhu[7] && shuzhu[8] == shuzhu[9] && shuzhu[10] == shuzhu[11] && shuzhu[12] == shuzhu[13])
            {
  
                chulijieguo =  4;

            }
        }
        {
            freshLists();
            chengshumax = 0;

            analysisInit(true, false, 0, shuzhu,ref chulijieguo,ref istingpai,ref yaochudemajiangshuzhu);//能杠则杠,有杠胡不了，除了七大对
            if (chulijieguo == 3)//暗杠
            {
                
                chulijieguo =  3;
                return;
            }
            else
            {
                freshLists();
                chengshumax = 0;

                analysisInit(true, false, 0, shuzhu,ref chulijieguo,ref istingpai,ref yaochudemajiangshuzhu);
                int num = sanzhangmajianglist.Count + sizhangmajianglist.Count + angangmajianglist.Count;
                if(num == 0)
                {
                    analysisWhenZhangShuLess5(ref yaochudemajiangshu, shuzhu);
                    chulijieguo = 0;
                  
                    return;
                }
                shunode root = new shunode();
                root.shengyushuzhu = shuzhu;
                root.chengshu = 0;
                root.nextcheng = new List<shunode>();
                shengChengShu(root);

                findone = false;
                lessshengyushu = 14;
                findOneShuNodeOnChengShuWithLessShengYuMaJiangShu(root, chengshumax);
                if (findone)
                {
            
                    int shengyushu = findonenode.shengyushuzhu.Length;
                  

                    if (shengyushu == 2)
                    {
                        if (findonenode.shengyushuzhu[0] == findonenode.shengyushuzhu[1])
                        {
                       
                            deleteShu(root);
                            chulijieguo =  4;
                        }
                        else
                        {
                           
                            istingpai = true;
          
                            System.Random rd = new System.Random();
                            float t = rd.Next(0, 2);
                            if (t > 1)
                            {
                                yaochudemajiangshu = findonenode.shengyushuzhu[1];
                            }
                            else
                            {
                                yaochudemajiangshu = findonenode.shengyushuzhu[0];
                            }
                            deleteShu(root);
                            chulijieguo = 0;
                        }
                    }
                    else if (shengyushu <= 5)//进行另一种分析,找出两张麻将，可以间隔一个数或是相同
                    {
                        analysisWhenZhangShuLess5(ref yaochudemajiangshu,findonenode.shengyushuzhu);
                        deleteShu(root);
                        chulijieguo =  0;
                    }
                    else
                    {
                        analysisWhenZhangShuLess5(ref yaochudemajiangshu, findonenode.shengyushuzhu);//本来想偷懒的，但还是细致分析吧
                        deleteShu(root);
                        chulijieguo =  0;
                    }
                }
            }

        }
        if(chulijieguo == 0 && yaochudemajiangshu == 0)//漏网
        {
            yaochudemajiangshu = shuzhu[shuzhu.Length - 1];
        }
        #endregion
       
    }



    public int ChiXuanZheShu( int xingshu,int[] shuzhu)
    {
        int shumu = shuzhu.Length;
        int num = 0;
        bool b1 = false;
        bool b2 = false;
        bool b3 = false;
        bool b4 = false;
        for (int i = 0; i < shumu; i++)
        {
            if (shuzhu[i] == xingshu - 1)
            {
                b1 = true;
            }
            else if (shuzhu[i] == xingshu + 1)
            {
                b2 = true;
            }
            else if(shuzhu[i] == xingshu - 2)
            {
                b3 = true;
            }
            else if(shuzhu[i] == xingshu + 2)
            {
                b4 = true;
            }
        }
        if (b1 && b2)
        {
            num = 1;
            int []temp = new int[3] { xingshu - 1, xingshu, xingshu + 1 };
            keyichideshuzhu.Add(temp);
        }
        else
            return 0;

        if (b3)
        {
            int[] temp = new int[3] { xingshu - 2, xingshu-1, xingshu };
            keyichideshuzhu.Add(temp);
            num++;
        }
        if (b4)
        {
            int[] temp = new int[3] { xingshu, xingshu+1, xingshu + 2 };
            keyichideshuzhu.Add(temp);
            num++;
        }
        return num;
    }

    public int PengOrGangShu(int xingshu,int[] shuzhu ) //0 没有  3碰 4杠
    {
        int num = 0;
        if (xingshu != 0)//别人回合
        {

            for (int i = 0; i < shuzhu.Length; i++)
            {
                if (shuzhu[i] == xingshu)
                {
                    num++;
                }
            }
            if (num == 3)
            {
                int[] temp = new int[4] { xingshu, xingshu, xingshu, xingshu };
                keyigangdeshuzhu.Add(temp);
            }
            if (num >= 2)
            {
                int[] temp = new int[3] { xingshu, xingshu, xingshu };
                keyipengdeshuzhu.Add(temp);
            }
        }
        else
        {
            
        }
        return num;
    }
    int compareShuNodeList(List<shunode> a, List<shunode> b) //返回0相等  返回其它则不等
    {
        Debug.Log("a.count" + a.Count);
        Debug.Log("b.count" + b.Count);
        for (int i = 0;i<a.Count;i++)
        {
            bool find = false;
            for (int j = 0;j<b.Count;j++)
            {
                if(b[j].isEqual( a[i]))
                {
                    find = true;
                    Debug.Log("find it");
                    printshunode(a[i]);
                    break;
                }
            }
            if(!find)
            {
                Debug.Log("not find it");
                printshunode(a[i]);
                return 1;
            }
        }
        return 0;
    }
    List<List<shunode>> hulist = new List<List<shunode>>();
    void getAllHu(int shumu,shunode root)
    {
        if (root != null)
        {
            if (root.nextcheng.Count > 0)
            {
               
                int count = root.nextcheng.Count;
                for (int i = count - 1; i >= 0; i--)
                {
                    
                    getAllHu(shumu,root.nextcheng[i]);
                }
            }
            else //在树梢
            {
                Debug.Log("hi" + root.shengyushuzhu.Length);
                if (root.shengyushuzhu.Length == 2)
                {
                    if(root.shengyushuzhu[0] == root.shengyushuzhu[1])//胡了
                    {
                        Debug.Log("hu le");
                        shunode first = root;
                        List<shunode> shunodelist = new List<shunode>();
                        
                        shunodelist.Add(first);
                        Debug.Log("shunodelist.Count" + shunodelist.Count);
                        printshunode(first);

                        while (first.before!=null && first.before.chengshu!=0)
                        {
                            first = first.before;
                            Debug.Log("before");
                            printshunode(first);
                            shunodelist.Add(first);
                        }
                        Debug.Log("shunodelist.Count"+shunodelist.Count);


                        bool chongfu = false;
                        foreach(var list in hulist)
                        {
                            if(compareShuNodeList(list,shunodelist) == 0)
                            {
                                chongfu = true;
                                Debug.Log("chong fu le11111111111111");
                                break;
                            }
                        }
                        if(!chongfu)
                        {
                            hulist.Add(shunodelist);
                            Debug.Log("add one");
                        }
                    }
                   
                }
               

            }
        }
 
    }
    void analysis(int[] shuzhu)
    {
     
        sizhangmajianglist.Clear();
        sanzhangmajianglist.Clear();
        angangmajianglist.Clear();
        int shumu = shuzhu.Length;
        for (int i = 0; i < shumu; i++)
        {
            if (i + 2 < shumu)
            {
                if (shuzhu[i] == shuzhu[i + 1] && shuzhu[i] == shuzhu[i + 2])//判断三个同色
                {
                    sanzhangmajiang sanzhangmj = new sanzhangmajiang();
                    sanzhangmj.majiang1 = shuzhu[i];
                    sanzhangmj.majiang2 = shuzhu[i];
                    sanzhangmj.majiang3 = shuzhu[i];
                    addMaJiangsToList(sanzhangmj, sanzhangmajianglist);
                    i += 2;
              
                    continue;
                }
            }
            if (i + 3 < shumu)
            {
                if (shuzhu[i] == shuzhu[i + 1] && shuzhu[i] == shuzhu[i + 2] && shuzhu[i] == shuzhu[i + 3])//判断四个同色
                {
                    sizhangmajiang angangmj = new sizhangmajiang();
                    angangmj.majiang1 = shuzhu[i];
                    angangmj.majiang2 = shuzhu[i];
                    angangmj.majiang3 = shuzhu[i];
                    angangmj.majiang4 = shuzhu[i];
                    angangmajianglist.Add(angangmj);
                    i += 3;
                    continue;
                }
            }

            {
                bool b1 = false;
                bool b2 = false;
                for (int k = i; k < shumu; k++)
                {
                    if (shuzhu[k] == shuzhu[i] + 1)
                    {
                        b1 = true;
                    }
                    else if (shuzhu[k] == shuzhu[i] + 2)
                    {
                        b2 = true;
                        if (b1)
                        {
                            sanzhangmajiang temp = new sanzhangmajiang();
                            temp.majiang1 = shuzhu[i];
                            temp.majiang2 = shuzhu[i] + 1;
                            temp.majiang3 = shuzhu[i] + 2;
                            addMaJiangsToList(temp, sanzhangmajianglist);
                        }
                        break;
                    }
                }

            }
        }
    }
    public bool canHu(int xingshu,int[] shuzhu)
    {
        int []newshuzhu = new int[shuzhu.Length + 1];
        int index = 0;
        for(int i = 0;i<newshuzhu.Length - 1;i++)
        {
            
                newshuzhu[i] = shuzhu[index];
                index++;
          
        }
        newshuzhu[newshuzhu.Length - 1] = xingshu;
        if (newshuzhu.Length == 14)
        {
            if (newshuzhu[0] == newshuzhu[1] && newshuzhu[2] == newshuzhu[3] && newshuzhu[4] == newshuzhu[5] && newshuzhu[6] == newshuzhu[7] && newshuzhu[8] == newshuzhu[9] && newshuzhu[10] == newshuzhu[11] && newshuzhu[12] == newshuzhu[13])
            {
                //此为胡(七大对)
                int[] temp = new int[14] { newshuzhu[0], newshuzhu[1], newshuzhu[2], newshuzhu[3], newshuzhu[4], newshuzhu[5], newshuzhu[6], newshuzhu[7], newshuzhu[8], newshuzhu[9], newshuzhu[10], newshuzhu[11], newshuzhu[12], newshuzhu[13] };
                keyihudeshuzhu.Add(temp);
                return true;
            }   
        }
        analysis(newshuzhu);
        var root = new shunode();
        root.before = null;
        root.shengyushuzhu = newshuzhu;
        root.chengshu = 0;
        root.nextcheng = new List<shunode>();
        shengChengShu(root);
        getAllHu(newshuzhu.Length, root);
        Debug.Log("hulist.Count" + hulist.Count);


        if (hulist.Count > 0)
        {
            Debug.Log("here");
            for (int i = 0; i < hulist.Count; i++)
            {
                Debug.Log("here2");
                List<shunode> temp = hulist[i];
                for (int j = 0; j < temp.Count; j++)
                {
                    printshunode(temp[j]);
                }
            }
        }
        for (int k = 0; k < hulist.Count; k++)
        {
            int[] temp = new int[shuzhu.Length + 1];
            index = 0;
            Debug.Log("shuchu");
            for (int i = hulist[k].Count - 1; i >= 0; i--)
            {
                printshunode(hulist[k][i]);
                if (hulist[k][i].datasize == 3)
                {
                    temp[index] = ((sanzhangmajiang)hulist[k][i].data).majiang1;
                    index++;
                    temp[index] = ((sanzhangmajiang)hulist[k][i].data).majiang2;
                    index++;
                    temp[index] = ((sanzhangmajiang)hulist[k][i].data).majiang3;
                    index++;
                }
                //else if (hulist[k][i].datasize == 4)//这里运行不到
                //{
                //    temp[index] = ((sizhangmajiang)hulist[k][i].data).majiang1;
                //    index++;
                //    temp[index] = ((sizhangmajiang)hulist[k][i].data).majiang2;
                //    index++;
                //    temp[index] = ((sizhangmajiang)hulist[k][i].data).majiang3;
                //    index++;
                //    temp[index] = ((sizhangmajiang)hulist[k][i].data).majiang4;
                //    index++;
                //}
            }
            // Debug.Log(hulist[k][0].shengyushuzhu[0]);
            temp[index] = hulist[k][0].shengyushuzhu[0];
            index++;
            temp[index] = hulist[k][0].shengyushuzhu[1];
            keyihudeshuzhu.Add(temp);
        }
        //deleteShu(root);
        return false;
    }

    void printshunode(shunode node)
    {
        if(node.datasize == 3)
        {
            Debug.Log("chengshu" + node.chengshu);
                Debug.Log(((sanzhangmajiang)node.data).majiang1 + " ");
            Debug.Log(((sanzhangmajiang)node.data).majiang2 + " ");
            Debug.Log(((sanzhangmajiang)node.data).majiang3 + " ");
        }else if(node.datasize==4)
        {
            Debug.Log("chengshu" + node.chengshu);
            Debug.Log(((sizhangmajiang)node.data).majiang1 + " ");
            Debug.Log(((sizhangmajiang)node.data).majiang2 + " ");
            Debug.Log(((sizhangmajiang)node.data).majiang3 + " ");
            Debug.Log(((sizhangmajiang)node.data).majiang4 + " ");
        }
        else
        {
            Debug.Log("chengshu" + node.chengshu);
            Debug.Log(node.datasize);
        }
    }
    public void test()
    {
        hulist.Clear();
        int[] shuzhu = new int[7] { 100,100,120,120,130,131,132};
        canHu(100, shuzhu);
        if(hulist.Count>0)
        {
            Debug.Log("here");
            for(int i = 0;i<hulist.Count;i++)
            {
                Debug.Log("here2");
                List<shunode> temp = hulist[i];
                for(int j = 0;j<temp.Count;j++)
                {
                    printshunode(temp[j]);
                }
            }
        }
        int index = 0;
        for (int k = 0; k < hulist.Count; k++)
        {
            int[] temp = new int[shuzhu.Length+1];
            index = 0;
            Debug.Log("shuchu");
            for (int i = hulist[k].Count - 1; i >= 0; i--)
            {
                printshunode(hulist[k][i]);
                if (hulist[k][i].datasize == 3)
                {
                    temp[index] = ((sanzhangmajiang)hulist[k][i].data).majiang1;
                    index++;
                    temp[index] = ((sanzhangmajiang)hulist[k][i].data).majiang2;
                    index++;
                    temp[index] = ((sanzhangmajiang)hulist[k][i].data).majiang3;
                    index++;
                }
                else if (hulist[k][i].datasize == 4)
                {
                    temp[index] = ((sizhangmajiang)hulist[k][i].data).majiang1;
                    index++;
                    temp[index] = ((sizhangmajiang)hulist[k][i].data).majiang2;
                    index++;
                    temp[index] = ((sizhangmajiang)hulist[k][i].data).majiang3;
                    index++;
                    temp[index] = ((sizhangmajiang)hulist[k][i].data).majiang4;
                    index++;
                }
            }
           // Debug.Log(hulist[k][0].shengyushuzhu[0]);
            temp[index] = hulist[k][0].shengyushuzhu[0];
            index++;
            temp[index] = hulist[k][0].shengyushuzhu[1];
            keyihudeshuzhu.Add(temp);
        }
        //deleteShu(root);
    }
}
