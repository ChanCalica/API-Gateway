using System;
using System.Collections.Generic;

namespace API_Gateway.data.Entites
{
    public partial class StoreCode
    {
        public short MatrixMemberId { get; set; }
        public short StoreInternalKey { get; set; }
        public byte TypeId { get; set; }
        public string StoreId { get; set; } = null!;
    }
}
