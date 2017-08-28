namespace Ncb.Data.Views
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    using Framework.Common;
    using Framework.Common.Extensions;

    public partial class View_UserDeviceInfos
    {
        [Key]
        [Column(Order = 0)]
        [DisplayName("Mac地址")]
        public string ID { get; set; }

        [Column(Order = 1)]
        [DisplayName("分类")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short CategoryID { get; set; }
        [DisplayName("分类")]
        [StringLength(10)]
        public string CategoryName { get; set; }
        [DisplayName("名称")]
        [StringLength(10)]
        public string Name { get; set; }
        [DisplayName("电话")]
        [StringLength(20, ErrorMessage = "{0}必需在{1}个字符以内")]
        [RegularExpression(@"((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)", ErrorMessage = "电话号码格式不正确")]
        public string PhoneNumber { get; set; }
        [DisplayName("余额")]
        [Column(Order = 2)]
        public decimal Amount { get; set; }
        [DisplayName("最后充值时间")]
        public DateTime? LastRechargeDate { get; set; }
        [DisplayName("到期日期")]
        public DateTime? ExpiryDate { get; set; }
        [DisplayName("最后登录时间")]
        [Column(Order = 3)]
        public DateTime LastLoginDate { get; set; }
        [DisplayName("注册时间")]
        [Column(Order = 4)]
        public DateTime CreateDate { get; set; }
        [DisplayName("手机型号")]
        [StringLength(10)]
        public string Model { get; set; }
        [DisplayName("系统平台")]
        public int? DeviceType { get; set; }
        [DisplayName("运行环境版本")]
        [StringLength(50)]
        public string PlusVersion { get; set; }
        [DisplayName("app版本")]
        [StringLength(50)]
        public string AppVersion { get; set; }
        [DisplayName("系统版本")]
        [StringLength(50)]
        public string OsVersion { get; set; }
        [DisplayName("手机厂商")]
        [StringLength(20)]
        public string Vendor { get; set; }
        [DisplayName("网络类型")]
        public int? NetType { get; set; }
        [DisplayName("国际移动设备标识")]
        [StringLength(50)]
        public string Imei { get; set; }
        [DisplayName("用户代理字符")]
        [StringLength(500)]
        public string UserAgent { get; set; }
        [DisplayName("状态")]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public RecordStates RecordState { get; set; }
    }

    public partial class View_UserDeviceInfos
    {
        private const string dateStrFormat = "yyyy-MM-dd hh:mm:ss";
        public string LastLoginDateDisplay
        {
            get
            {
                return LastLoginDate.ToString(dateStrFormat);
            }
        }

        public string CreateDateDisplay
        {
            get
            {
                return CreateDate.ToString(dateStrFormat);
            }
        }

        public string LastRechargeDateDisplay
        {
            get
            {
                return LastRechargeDate?.ToString(dateStrFormat);
            }
        }

        public string ExpiryDateDisplay
        {
            get
            {
                return ExpiryDate?.ToString("yyyy-MM-dd");
            }
        }

        public string RecordStateDisplay
        {
            get
            {
                switch (RecordState)
                {
                    case RecordStates.AuditPass:
                        return UserInfoStateTypes.Normal.GetDisplayName();
                    case RecordStates.Locked:
                        return UserInfoStateTypes.Locked.GetDisplayName();
                    case RecordStates.Deleted:
                        return UserInfoStateTypes.Delete.GetDisplayName();
                    default:
                        throw new Exception($"用户状态无效：{RecordState}");
                }
            }
        }
    }

    
}
