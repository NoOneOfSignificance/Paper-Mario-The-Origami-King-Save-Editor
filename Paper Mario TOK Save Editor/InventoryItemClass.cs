using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paper_Mario_TOK_Save_Editor
{
    class InventoryItem
    {
        public string itemId { get; set; }
        public int usedEndurance { get; set; }
        public int usedBreakRate { get; set; }
        public int type { get; set; }
        public int stack { get; set; }
        public string slotInInventory { get; set; }
        public string itemName { get; set; }
    }
}
