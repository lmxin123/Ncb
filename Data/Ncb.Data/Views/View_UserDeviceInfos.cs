namespace Ncb.Data.Views
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_UserDeviceInfos
    {
        [Key]
        [Column(Order = 0)]
        public string ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short CategoryID { get; set; }

        [StringLength(10)]
        public string Name { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(10)]
        public string CategoryName { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal Amount { get; set; }

        public DateTime? LastRechargeDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime LastLoginDate { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime CreateDate { get; set; }

        [StringLength(10)]
        public string Model { get; set; }

        public int? DeviceType { get; set; }

        [StringLength(50)]
        public string PlusVersion { get; set; }

        [StringLength(50)]
        public string AppVersion { get; set; }

        [StringLength(50)]
        public string OsVersion { get; set; }

        [StringLength(20)]
        public string Vendor { get; set; }

        public int? NetType { get; set; }

        [StringLength(50)]
        public string Imei { get; set; }

        [StringLength(500)]
        public string UserAgent { get; set; }

        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RecordState { get; set; }
    }

    public partial class View_UserDeviceInfos
    {
        private const string dateStrFormat = "yyyy-MM-dd hh:mm:ss";
        public string LastLoginDateDesplay
        {
            get
            {
                return LastLoginDate.ToString(dateStrFormat);
            }
        }

        public string CreateDateDesplay
        {
            get
            {
                return CreateDate.ToString(dateStrFormat);
            }
        }

        public string LastRechargeDateDesplay
        {
            get
            {
                return LastRechargeDate?.ToString(dateStrFormat);
            }
        }

        public string ExpiryDateDesplay
        {
            get
            {
                return ExpiryDate?.ToString("yyyy-MM-dd");
            }
        }
    }
}
