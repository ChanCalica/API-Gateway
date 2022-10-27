using System;
using System.Collections.Generic;

namespace API_Gateway.data.Entites
{
    public partial class CrmMemberStoreAssign
    {
        public int MemberInternalKey { get; set; }
        public short StoreInternalKey { get; set; }
        public bool IsHomeStore { get; set; }
        public byte StoreTypeId { get; set; }
        public short MatrixMemberId { get; set; }
        public byte? SourceId { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
