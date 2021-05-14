using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.ViewModels.Index
{
    public class ChartsPieModel
    {
        public ChartsPieModel(string name, decimal y = 0, bool sliced = false, bool selected = false)
        {
            this.name = name;
            this.y = y;
            this.sliced = sliced;
            this.selected = selected;
        }
        /// <summary>
        /// 分类的名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public decimal y { get; set; }

        /// <summary>
        /// 是否分类
        /// </summary>
        public bool sliced { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool selected { get; set; }
    }
    public class ChartsColumnModel
    {
        public ChartsColumnModel()
        {
        }
        public ChartsColumnModel(string name) : base()
        {
            this.name = name;
            this.data = new List<decimal>();
        }
        /// <summary>
        /// 分类的名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public List<decimal> data { get; set; }
    }
}
