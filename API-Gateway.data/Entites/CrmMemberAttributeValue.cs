using System;
using System.Collections.Generic;

namespace API_Gateway.data.Entites
{
    public partial class CrmMemberAttributeValue
    {
        public int ClubInternalKey { get; set; }
        public int MatrixMemberId { get; set; }
        public int MemberInternalKey { get; set; }
        public int AttributeId { get; set; }
        public string? LongStringValue { get; set; }
        public string? StringValue { get; set; }
        public int? LongValue { get; set; }
        public decimal? MoneyValue { get; set; }
        public double? DoubleValue { get; set; }
        public DateTime? DateValue { get; set; }
        public byte? BooleanValue { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public byte? SourceId { get; set; }
    }
}
