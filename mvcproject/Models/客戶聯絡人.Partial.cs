namespace mvcproject.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;

    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        private customerEntities db = new customerEntities();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var contactor = db.客戶聯絡人.Where(c => c.Email == this.Email);
            if (this.Id == 0)
            {
                var result = contactor.Where(c => c.客戶Id == this.客戶Id).Any();
                if (result)
                {
                    yield return new ValidationResult("此郵件已存在資料庫中!");
                }
            }
            else
            {
                var result = contactor.Where(c => c.客戶Id == this.客戶Id && c.Id != this.Id).Any();
                if (result)
                {
                    yield return new ValidationResult("此郵件已存在資料庫中!");
                }
            }
            yield return ValidationResult.Success;
        }
    }
    
    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        [Display(Name = "電子郵件")]
        [EmailAddress(ErrorMessage = "無效電子郵件位址")]
        public string Email { get; set; }
        [RegularExpression(@"^[0][9]\d{2}-\d{6}", ErrorMessage = "手機號碼需為有效數字，如:0987-123456")]
        [StringLength(12, ErrorMessage = "手機號碼不得超過十個數字")]
        public string 手機 { get; set; }

        [RegularExpression(@"^[0][1-9]-[0-9]{8}", ErrorMessage = "電話號碼需為有效數字，如:02-12345678")]
        [StringLength(11, ErrorMessage = "電話號碼不得超過十個數字")]
        public string 電話 { get; set; }
        [Required]
        public bool 是否已刪除 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }


    }
}
