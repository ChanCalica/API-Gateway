using System;
using System.Collections.Generic;

namespace API_Gateway.data.Entites
{
    public partial class CrmAccumulatedAccount
    {
        public int AccountInternalKey { get; set; }
        public int MatrixMemberId { get; set; }
        public int AccountId { get; set; }
        public string Name { get; set; } = null!;
        public short Type { get; set; }
        public decimal? AccumulationRatio { get; set; }
        public decimal? RedemptionRatio { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte? PublicationStatus { get; set; }
        public byte[]? PublicationRowVersion { get; set; }
        public int? PosMessageId { get; set; }
        public decimal? InitialAccountValue { get; set; }
        public byte? ResetAccountValue { get; set; }
        public string? ResetAccountValueOn { get; set; }
        public byte? PrintAccountBalance { get; set; }
        public byte? Status { get; set; }
        public short? GroupId { get; set; }
        public byte? ViewType { get; set; }
        public decimal? TresholdValue { get; set; }
        public int? MovementToSegment { get; set; }
        public byte? ActionScope { get; set; }
        public int? ActionPeriod { get; set; }
        public byte? MovementScope { get; set; }
        public int? MovementToAccount { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? PrintingStartDate { get; set; }
        public DateTime? PrintingEndDate { get; set; }
        public byte BalanceType { get; set; }
        public bool? IsManualUpdate { get; set; }
        public bool IsArchiveSource { get; set; }
        public bool IsArchiveDestination { get; set; }
        public bool? BalanceExpires { get; set; }
        public short? BalanceExpirationPeriod { get; set; }
        public bool IsManualUpdateMs { get; set; }
        public byte DigitsNumAfterDecimalPoint { get; set; }
        public byte RoundingRule { get; set; }
        public byte? IsAllowedNegativeBalanceFollowingReduce { get; set; }
        public int ClubInternalKey { get; set; }
        public byte BalanceExpirationScope { get; set; }
        public short? ExpirationRepetitionPeriod { get; set; }
        public byte? ExpirationReferenceMonth { get; set; }
        public string? ExpirationReferenceYear { get; set; }
        public short? ArchiveRepetitionPeriod { get; set; }
        public byte? ArchiveReferenceMonth { get; set; }
        public string? ArchiveReferenceYear { get; set; }
        public short? ResetRepetitionPeriod { get; set; }
        public byte? ResetReferenceMonth { get; set; }
        public string? ResetReferenceYear { get; set; }
        public byte ConversionScope { get; set; }
        public short? ConversionPeriod { get; set; }
        public short? ConversionRepetitionPeriod { get; set; }
        public byte? ConversionReferenceMonth { get; set; }
        public string? ConversionReferenceYear { get; set; }
        public int? ConvertedResultMovementToAccount { get; set; }
        public decimal? ConvertedResultLimitFrom { get; set; }
        public decimal? ConvertedResultLimitTo { get; set; }
        public decimal? ConvertedResultIntervals { get; set; }
        public byte? ConversionLeftOverActionScope { get; set; }
        public int? ConversionLeftOverMovementToAccount { get; set; }
        public bool SyncMsg2 { get; set; }
        public bool InheritFromMa { get; set; }
        public byte? InheritScope { get; set; }
        public int? InheritFromAccount { get; set; }
        public byte PointsSubjectToExpirationScope { get; set; }
        public short? PointsSubjectToExpirationPeriod { get; set; }
        public short? PointsSubjectToExpirationRepetitionPeriod { get; set; }
        public int? PointsSubjectToExpirationSourceAccount { get; set; }
        public byte? PointsSubjectToExpirationRepetitionType { get; set; }
        public byte ExtendExpirationDateScope { get; set; }
        public short? ExtendExpirationDateBy { get; set; }
        public short? ExtendExpirationDatePeriod { get; set; }
        public short? ExtendExpirationDateRepetitionPeriod { get; set; }
        public byte? ExtendExpirationDateRepetitionType { get; set; }
        public int? ExtendExpirationDateSegmentInternalKey { get; set; }
        public bool OverrideRedemptionPrivileges { get; set; }
        public bool ExpirationFromPromotion { get; set; }
        public int? ConversionSegmentInternalKey { get; set; }
        public byte? LeveledConversionScope { get; set; }
        public decimal? PosupdateMaximumBalance { get; set; }
        public decimal? PosupdateMaximumAccumulationPerTransaction { get; set; }
        public int? ResetSegmentInternalKey { get; set; }
        public bool CompetitorMemberAccount { get; set; }
        public bool DisplayPointsInEj { get; set; }
        public bool IsInheritSourceGui { get; set; }
        public bool IsInheritSourceWs { get; set; }
        public bool IsInheritSourcePos { get; set; }
        public bool IsInheritSourceImport { get; set; }
        public bool EnablePointsTransferBetweenHh { get; set; }
        public int? TransferPointsTargetHh { get; set; }
        public int? TransferPointsFromMa { get; set; }
        public bool EnableStartDate { get; set; }
        public bool? TransactionLevelActivity { get; set; }
    }
}
