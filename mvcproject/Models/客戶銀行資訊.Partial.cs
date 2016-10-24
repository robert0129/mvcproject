namespace mvcproject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(客戶銀行資訊MetaData))]
    public partial class 客戶銀行資訊
    {
    }
    
    public partial class 客戶銀行資訊MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage= "銀行名稱欄位長度不得大於 50 個字元")]
        [Required]
        public string 銀行名稱 { get; set; }
        [Required]
        //[StringLength(3, ErrorMessage = "銀行代碼欄位長度不得大於 3 個字元", MinimumLength = 1)]
        //[RegularExpression(@"^[0-9]{3}", ErrorMessage = "銀行代碼需為有效數字，如:013")]
        public int 銀行代碼 { get; set; }
        //[RegularExpression(@"^[0-9]{4}", ErrorMessage = "銀行分代碼需為有效數字，如:0004")]
        //[StringLength(4, ErrorMessage = "分行代碼欄位長度不得大於 4 個字元")]
        public Nullable<int> 分行代碼 { get; set; }
        
        [StringLength(50, ErrorMessage= "帳戶名稱欄位長度不得大於 50 個字元")]
        [Required]
        public string 帳戶名稱 { get; set; }
        
        [StringLength(17, ErrorMessage= "帳戶號碼長度不得大於 17 個字元")]
        [Required]
        [RegularExpression(@"^[0-9]{3}-[0-9]{4}-[0-9]{3}-[0-9]{4}", ErrorMessage = "帳戶號碼需為有效數字，如:013-1234-567-8900")]
        public string 帳戶號碼 { get; set; }
        [Required]
        public bool isDeleted { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
