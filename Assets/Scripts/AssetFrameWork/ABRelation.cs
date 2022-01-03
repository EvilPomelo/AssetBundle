using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABFW
{
    public class ABRelation
    {
        /// <summary>
        /// 当前AssetBundle包名
        /// </summary>
        private string abName;

        /// <summary>
        /// 本包所有依赖关系集合包
        /// </summary>
        private List<string> listAllDependenceAB;

        /// <summary>
        /// 本包所有引用关系集合包
        /// </summary>
        private List<string> listAllReferenceAB;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ABRelation(string abName)
        {
            if (string.IsNullOrEmpty(abName))
            {
                this.abName = abName;
            }

            listAllDependenceAB = new List<string>();
            listAllReferenceAB = new List<string>();
        }

        /// <summary>
        /// 增加依赖关系
        /// </summary>
        /// <param name="abName"></param>
        public void AddDenpendece(string abName)
        {
            if (!listAllDependenceAB.Contains(abName))
            {
                listAllDependenceAB.Add(abName);
            }
        }


        /// <summary>
        /// 移除依赖关系
        /// </summary>
        /// <param name="abName"></param>
        /// <returns>true:没有依赖项 false:仍有依赖项</returns>
        public bool RemoveDenpendece(string abName)
        {
            if (!listAllDependenceAB.Contains(abName))
            {
            }
            if (listAllDependenceAB.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 获取所有的依赖关系
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllDenpendeces()
        {
            return listAllDependenceAB;
        }

        /// <summary>
        /// 增加引用关系
        /// </summary>
        /// <param name="abName"></param>
        public void AddReferences(string abName)
        {
            if (!listAllReferenceAB.Contains(abName))
            {
                listAllReferenceAB.Add(abName);
            }
        }

        /// <summary>
        /// 移除引用关系
        /// </summary>
        /// <param name="abName"></param>
        /// <returns>true:没有依赖项 false:仍有依赖项</returns>
        public bool RemoveReferences()
        {
            if (!listAllReferenceAB.Contains(abName))
            {
            }
            if (listAllReferenceAB.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// 获取所有的引用关系
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllReferences()
        {
            return listAllReferenceAB;
        }
    }
}