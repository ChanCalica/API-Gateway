using System;
using System.Collections.Generic;

namespace API_Gateway.data.Entites
{
    public partial class Store
    {
        public short MatrixMemberId { get; set; }
        public short StoreInternalKey { get; set; }
        public string StoreName { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime OpenDate { get; set; }
        public short Postype { get; set; }
        public string? Posversion { get; set; }
        public bool? Working24Hours { get; set; }
        public bool? StoreIsActive { get; set; }
        public bool? UploadActive { get; set; }
        public bool? DownLoadActive { get; set; }
        public string? UploadFilesPath { get; set; }
        public byte? CommunicationPackage { get; set; }
        public string? CommunicationFilesPath { get; set; }
        public short? TaxZoneId { get; set; }
        public string? CommPointPassword { get; set; }
        public string? StoreEmailAddress { get; set; }
        public string? OutLookUserName { get; set; }
        public string? OutLookPassword { get; set; }
        public bool? OnlineTran { get; set; }
        public DateTime UpdatedDate { get; set; }
        public byte PublicationStatus { get; set; }
        public byte[] PublicationRowVersion { get; set; } = null!;
        public int? ChangeBatchId { get; set; }
        public int? TaxZonePublicationStatus { get; set; }
        public byte UploadFilesMode { get; set; }
        public string? CdsfilesPath { get; set; }
        public int? MacroId { get; set; }
        public DateTime? MacroDate { get; set; }
        public short? DownloadReformatorTemplateId { get; set; }
        public int? DownloadLastUniqueBatchNumber { get; set; }
        public string? AdditionalName { get; set; }
        public bool DisplayInGui { get; set; }
        public bool? DownloadActiveLoyalty { get; set; }
    }
}
