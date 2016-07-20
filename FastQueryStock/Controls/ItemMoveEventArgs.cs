using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.Controls
{
    public class ItemMoveEventArgs<T>
    {
        public T OriginalItem { get; set; }

        public T TargetItem { get; set; }

        public int SourceIndex { get; set; }

        public int TargetIndex { get; set; }

        /// <summary>
        /// 取消移動動作
        /// </summary>
        public bool Cancel { get; set; }

        public ItemMoveEventArgs(T original, T target, int sourceIndex, int targetIndex)
        {
            OriginalItem = original;
            TargetItem = target;
            SourceIndex = sourceIndex;
            TargetIndex = targetIndex;
        }

    }
}
