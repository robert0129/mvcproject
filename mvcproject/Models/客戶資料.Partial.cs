namespace mvcproject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(客戶資料MetaData))]
    public partial class 客戶資料
    {
    }
    
    public partial class 客戶資料MetaData
    {
        [Required]
        public int Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 客戶名稱 { get; set; }
        [RegularExpression(@"^[0-9]{8}", ErrorMessage = "統一編號需為有效數字")]
        [StringLength(8, ErrorMessage="欄位長度不得大於 8 個字元")]
        [Required]
        public string 統一編號 { get; set; }
        [RegularExpression(@"^[0][1-9][0-9]{8}", ErrorMessage = "電話號碼需為有效數字")]
        [StringLength(10, ErrorMessage="電話號碼不得超過十個數字")]
        [Required]
        public string 電話 { get; set; }
        [RegularExpression(@"^[0][1-9][0-9]{8}", ErrorMessage = "傳真號碼需為有效數字,如:0223455678")]
        [StringLength(10, ErrorMessage = "傳真號碼不得超過十個數字")]
        public string 傳真 { get; set; }
        
        [StringLength(100, ErrorMessage="欄位長度不得大於 100 個字元")]
        public string 地址 { get; set; }
        [Display(Name = "電子郵件")]
        [EmailAddress(ErrorMessage = "無效電子郵件位址")]
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        public string Email { get; set; }
        [Required]
        public bool isDeleted { get; set; }
    
        public virtual ICollection<客戶銀行資訊> 客戶銀行資訊 { get; set; }
        public virtual ICollection<客戶聯絡人> 客戶聯絡人 { get; set; }
    }
}
