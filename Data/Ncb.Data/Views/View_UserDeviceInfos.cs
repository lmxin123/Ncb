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
        [DisplayName("Mac��ַ")]
        public string ID { get; set; }

        [Column(Order = 1)]
        [DisplayName("����")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short CategoryID { get; set; }
        [DisplayName("����")]
        [StringLength(10)]
        public string CategoryName { get; set; }
        [DisplayName("����")]
        [StringLength(10)]
        public string Name { get; set; }
        [DisplayName("�绰")]
        [StringLength(20, ErrorMessage = "{0}������{1}���ַ�����")]
        [RegularExpression(@"((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)", ErrorMessage = "�绰�����ʽ����ȷ")]
        public string PhoneNumber { get; set; }
        [DisplayName("���")]
        [Column(Order = 2)]
        public decimal Amount { get; set; }
        [DisplayName("����ֵʱ��")]
        public DateTime? LastRechargeDate { get; set; }
        [DisplayName("��������")]
        public DateTime? ExpiryDate { get; set; }
        [DisplayName("����¼ʱ��")]
        [Column(Order = 3)]
        public DateTime LastLoginDate { get; set; }
        [DisplayName("ע��ʱ��")]
        [Column(Order = 4)]
        public DateTime CreateDate { get; set; }
        [DisplayName("�ֻ��ͺ�")]
        [StringLength(10)]
        public string Model { get; set; }
        [DisplayName("ϵͳƽ̨")]
        public int? DeviceType { get; set; }
        [DisplayName("���л����汾")]
        [StringLength(50)]
        public string PlusVersion { get; set; }
        [DisplayName("app�汾")]
        [StringLength(50)]
        public string AppVersion { get; set; }
        [DisplayName("ϵͳ�汾")]
        [StringLength(50)]
        public string OsVersion { get; set; }
        [DisplayName("�ֻ�����")]
        [StringLength(20)]
        public string Vendor { get; set; }
        [DisplayName("��������")]
        public int? NetType { get; set; }
        [DisplayName("�����ƶ��豸��ʶ")]
        [StringLength(50)]
        public string Imei { get; set; }
        [DisplayName("�û������ַ�")]
        [StringLength(500)]
        public string UserAgent { get; set; }
        [DisplayName("״̬")]
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
                        throw new Exception($"�û�״̬��Ч��{RecordState}");
                }
            }
        }
    }

    
}
