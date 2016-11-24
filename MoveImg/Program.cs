using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoveImg
{
    class Program
    {
        static void Main(string[] args)
        {
            //test//


            //test//
            //ok,开始搬库吧
            /*先搬过大图,小图*/
            //PostUrlList1(1, 0, 0);
            //PostUrlList2(2, 0, 0);

            PostUrlList3(3, 0, 0);
            //PostUrlList6(6, 0, 0);
            //PostUrlList7(7, 0, 0);

            PostUrlList4(4, 0, 0);
            //PostUrlList5(5, 0, 0);

        }
        /// <summary>
        ///  大图
        /// </summary>
        /// <param name="Type">图片类型，1=大图，2=小图，3=海报，4=剧照，5=写真，6=专辑，7=综艺，8=品牌</param>
        /// <param name="DownLoad">1=需下载，0=不需下载</param>
        /// <param name="Status">发包状态，0=原图、缩略图都发包成功，1=原图成功，2=缩略图成功，3=都没发包成功。</param>
        private static void PostUrlList1(int Type, int DownLoad, int Status)
        {
            List<Img> bigList = TaskList.GetBigImg();
            foreach (var item in bigList)
            {
                ToUrlList(Type, DownLoad, Status, item);
            }
        }
        /// <summary>
        /// 小图
        /// </summary>
        /// <param name="Type">图片类型，1=大图，2=小图，3=海报，4=剧照，5=写真，6=专辑，7=综艺，8=品牌</param>
        /// <param name="DownLoad">1=需下载，0=不需下载</param>
        /// <param name="Status">发包状态，0=原图、缩略图都发包成功，1=原图成功，2=缩略图成功，3=都没发包成功。</param>
        private static void PostUrlList2(int Type, int DownLoad, int Status)
        {
            List<Img> smallList = TaskList.GetSmallImg();
            foreach (var item in smallList)
            {
                ToUrlList(Type, DownLoad, Status, item);
            }
        }
        private static void PostUrlList3(int Type, int DownLoad, int Status)
        {
            List<Img> haibaoList = TaskList.GetHaiBaoImg();
            foreach (var item in haibaoList)
            {
                ToUrlList(Type, DownLoad, Status, item);
            }
        }

        /// <summary>
        /// 剧照
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="DownLoad"></param>
        /// <param name="Status"></param>
        private static void PostUrlList4(int Type, int DownLoad, int Status)
        {
            List<Img> juzhaoList = TaskList.GetJuZhao();
            foreach (var item in juzhaoList)
            {
                //item.
                string[] name = item.ImgName.Split('|');

                foreach (var it in name)
                {
                    Img img = new Img() { ID = item.ID, ImgName = it };
                    ToUrlList(Type, DownLoad, Status, img);
                }

            }


        }

        private static void PostUrlList5(int Type, int DownLoad, int Status)
        {
            List<Img> XieZhenList = TaskList.GetXieZhen();
            foreach (var item in XieZhenList)
            {
                //item.
                string[] name = item.ImgName.Split('|');

                foreach (var it in name)
                {
                    Img img = new Img() { ID = item.ID, ImgName = it };
                    ToUrlList(Type, DownLoad, Status, img);
                }

            }


        }

        private static void PostUrlList6(int Type, int DownLoad, int Status)
        {
            List<Img> ZhuanJiList = TaskList.GetZhuanJi();
            foreach (var item in ZhuanJiList)
            {
                ToUrlList(Type, DownLoad, Status, item);
            }
        }

        private static void PostUrlList7(int Type, int DownLoad, int Status)
        {
            List<Img> ZongYiList = TaskList.GetZongYi();
            foreach (var item in ZongYiList)
            {
                ToUrlList(Type, DownLoad, Status, item);
            }
        }

        private static void ToUrlList(int Type, int DownLoad, int Status, Img item)
        {
            UrlList urlList = new UrlList();
            urlList.Type = Type;
            urlList.TaskID = item.ID;
            urlList.URL = item.URL;
            urlList.ImgName = item.ImgName;
            urlList.DownLoad = DownLoad;
            urlList.Status = Status;
            TaskList.AddUrlList(urlList);
        }

    }
}
